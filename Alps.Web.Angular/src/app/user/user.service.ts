import { Injectable, Injector } from '@angular/core';
import { RepositoryService } from '../infrastructure/infrastructure.module';

@Injectable({
  providedIn: 'root'
})
export class UserService extends RepositoryService {

  constructor(injector: Injector) {
    super(injector);
    this.setBaseUrl("api/Users");
  }
  //private setRoles(){this.setBaseUrl()}
  getUsers(){
    return this.getall();
  }
  getUser(id){
    return this.get(id);
  }
  getRoles(){

    return this.query("getroles");
  }
}
