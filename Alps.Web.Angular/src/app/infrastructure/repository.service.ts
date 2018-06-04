import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Observable, throwError } from "rxjs";
import { AppModule } from "../app.module";
import { AppConst } from "../app.const";

@Injectable()
export class RepositoryService {
  _baseUrl: string="api";
  constructor(protected httpClient: HttpClient) {  }
  setBaseUrl(url: string) {
    this._baseUrl = url;
  }
  getall() {
    return this.httpClient.get(this._baseUrl).pipe(catchError((err: any) => this.handleError(err)));
  }
  get(id: string) {
    return this.httpClient.get(this._baseUrl + "/" + id).pipe(catchError((err: any) => this.handleError(err)));
  }
  query(action: string) {
    var self=this;
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.httpClient.get( this._baseUrl + "/" +action, { headers: headers }).pipe(catchError((err) =>  self.handleError(err)
  ));
  }
  action(action: string, param?: any) {
    let body = JSON.stringify(param); 
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.httpClient.post(this._baseUrl + "/" + action, body, { headers: headers }).pipe(catchError((err) => this.handleError(err)));
  }

  createAndUpdate(entity) {
    if (!entity.hasOwnProperty("id"))
      throw Error("不存在标识");
    if (entity.id && entity.id !== "" && entity.id !== AppConst.GUID_EMPTY) {
      return this.update(entity, entity.id);
    }
    else
      return this.create(entity);
  }
  create(entity) {
    if (!entity.id || entity.id === "")
      entity.id = AppConst.GUID_EMPTY;
    let body = JSON.stringify(entity);
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.httpClient.post(this._baseUrl, body, { headers: headers }).pipe(catchError((err) => this.handleError(err)));
  }
  update(entity, id: string) {
    let body = JSON.stringify(entity);
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.httpClient.put(this._baseUrl + "/" + id, body, { headers: headers }).pipe(catchError((err) => this.handleError(err)));
  }
  delete(id: string) {
    return this.httpClient.delete(this._baseUrl + "/" + id).pipe(catchError((err) => this.handleError(err)));
  }
  protected handleError(error) {
    //console.info('与服务器交互失败！');
    
    return throwError(error|| '与服务器交互失败！');
  }
}