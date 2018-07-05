import { Injectable, Injector } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, filter, map, tap } from 'rxjs/operators';
import { throwError, Observable } from "rxjs";
import { AlpsConst } from '../alps-const';
import { AlpsActionResponse, AlpsActionResultCode } from './alpsActionResponse';
import { AlpsLoadingBarService } from '../service/alps-loading-bar.service';

@Injectable()
export class RepositoryService {
  _baseUrl: string = "api";
  protected httpClient:HttpClient;
protected loadingBarService:AlpsLoadingBarService;
  constructor(protected injector:Injector) {
    this.httpClient=this.injector.get(HttpClient);
    this.loadingBarService=this.injector.get(AlpsLoadingBarService);
   }
   private startLoad(){
    setTimeout(() => {
      this.loadingBarService.open();
    }, 0);
   }
   private finishLoad(){
     setTimeout(() => {
      this.loadingBarService.close();      
     }, 0);
   }
   private queryError(){
    setTimeout(() => {
     this.loadingBarService.error();      
    }, 0);
  }
  setBaseUrl(url: string) {
    this._baseUrl = url;
  }
  processPipe(ob:Observable<any>):Observable<any>
  {
    return ob.pipe(tap(()=>{this.finishLoad();}), catchError((err: any) => this.handleError(err)), filter(this.filterError), map(this.upPackResponse));
  }
  getall() {
   this.startLoad();
    return this.processPipe(this.httpClient.get(this._baseUrl));
  }
  get(id: string) {
    this.startLoad();
    return this.processPipe(this.httpClient.get(this._baseUrl + "/" + id));
  }
  query(action: string) {
    this.startLoad();
    var self = this;
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.processPipe(this.httpClient.get(this._baseUrl + "/" + action, { headers: headers }));
  }
  action(action: string, param?: any) {
    this.startLoad();
    let body = JSON.stringify(param);
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.processPipe(this.httpClient.post(this._baseUrl + "/" + action, body, { headers: headers }));
  }

  createAndUpdate(entity) {
    if (!entity.hasOwnProperty("id"))
      throw Error("不存在标识");
    if (entity.id && entity.id !== "" && entity.id !== AlpsConst.GUID_EMPTY) {
      return this.update(entity, entity.id);
    }
    else
      return this.create(entity);
  }
  create(entity) {
    this.startLoad();
    if (!entity.id || entity.id === "")
      entity.id = AlpsConst.GUID_EMPTY;
    let body = JSON.stringify(entity);
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.processPipe(this.httpClient.post(this._baseUrl, body, { headers: headers }));
  }
  update(entity, id: string) {
    this.startLoad();
    let body = JSON.stringify(entity);
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.processPipe(this.httpClient.put(this._baseUrl + "/" + id, body, { headers: headers }));
  }
  delete(id: string) {
    this.startLoad();
    return this.processPipe(this.httpClient.delete(this._baseUrl + "/" + id));
  }
  protected handleError(error) {
this.queryError();
    return throwError( '与服务器交互失败！');
  }
  protected filterError(res) {
    if (res && (res.resultCode) && res.resultCode !== AlpsActionResultCode.Ok) {
      alert(res.messages);
      return false;

    }
    else
      return true;
  }
  protected upPackResponse(res) {
    if (!!res  && res.resultCode && res.resultCode == AlpsActionResultCode.Ok && res.data)
      return res.data;
    else
      return res;
  }
}