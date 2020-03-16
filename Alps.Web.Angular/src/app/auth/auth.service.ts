import { Injectable, Injector } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { map } from "rxjs/operators";
import { RepositoryService } from '../infrastructure/infrastructure.module';
//import {Buffer} from 'buffer';
import { Base64 } from '../extends/Base64';
@Injectable({
  providedIn: 'root'
})
export class AuthService extends RepositoryService {

  private readonly TOKEN_SIGN: string = "ALPS_TOKEN";
  constructor(injector: Injector) {
    super(injector);
    this.setBaseUrl("api/auth");
    this.parseToken();
  }
  // getRoles(): string {
  //   let token = JSON.parse(localStorage.getItem(this.TOKEN_SIGN));
  //   return token;
  // }
  private _token: string;
  get tokenString() { return this._token; }
  private _idName: string;
  get IdName() { return this._idName; }
  private _userName: string;
  get username() { return this._userName; }
  private _roles: string[];
  get roles() { return this._roles; }
  private loginStatusSubject = new BehaviorSubject<boolean>(false);
  loginStatus = this.loginStatusSubject.asObservable();
  login(user: string, password: string) {

    return this.action("login", { userName: user, password: password }).pipe(map(data => {
      if (data.result) {
        sessionStorage.setItem(this.TOKEN_SIGN, data.token);
        this.parseToken();
        this.loginStatusSubject.next(true);
      }
      else
        this.loginStatusSubject.next(false);
      return data.result;
    }));
    //return this.loginStatus;
  }
  logout() {
    sessionStorage.removeItem(this.TOKEN_SIGN);
    this.parseToken();
    this.loginStatusSubject.next(false);
  }
  private parseToken() {
    let tokenString = sessionStorage.getItem(this.TOKEN_SIGN);
    let token = null;
    if (tokenString) {
      let decodeStr =(new Base64()).decode(tokenString.split('.')[1]);
      token = JSON.parse(decodeStr);
    }


    if (token && token.exp * 1000 > Date.now()) {
      this._userName = token.name;
      this._idName = token.idName;//['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
      if (typeof (token.role) == "string")
        this._roles = [token.role];
      else
        this._roles = token.role;//['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
      this._token = tokenString;
      this.loginStatusSubject.next(true);
    }
    else {
      this._userName = "";
      this._roles = [];
      this._token = "";
    }

  }
  register(id: string, password: string, realname: string, identityNumber: string, mobilePhoneNumber: string) {
    return this.action("register", { userName: id, realName: realname, password: password, identityNumber: identityNumber, mobilePhoneNumber: mobilePhoneNumber });
  }

}
