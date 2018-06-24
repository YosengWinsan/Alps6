import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, filter, map } from 'rxjs/operators';
import { throwError } from "rxjs";
import { AlpsConst } from '../alps-const';
import { AlpsActionResponse, AlpsActionResultCode } from './alpsActionResponse';

@Injectable()
export class RepositoryService {
  _baseUrl: string = "api";
  constructor(protected httpClient: HttpClient) { }
  setBaseUrl(url: string) {
    this._baseUrl = url;
  }
  getall() {
    return this.httpClient.get(this._baseUrl).pipe(catchError((err: any) => this.handleError(err)), filter(this.filterError), map(this.upPackResponse));
  }
  get(id: string) {
    return this.httpClient.get(this._baseUrl + "/" + id).pipe(catchError((err: any) => this.handleError(err)), filter(this.filterError), map(this.upPackResponse));
  }
  query(action: string) {
    var self = this;
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.httpClient.get(this._baseUrl + "/" + action, { headers: headers }).pipe(catchError((err) => self.handleError(err)
    ), filter(this.filterError), map(this.upPackResponse));
  }
  action(action: string, param?: any) {
    let body = JSON.stringify(param);
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.httpClient.post(this._baseUrl + "/" + action, body, { headers: headers }).pipe(catchError((err) => this.handleError(err)), filter(this.filterError));
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
    if (!entity.id || entity.id === "")
      entity.id = AlpsConst.GUID_EMPTY;
    let body = JSON.stringify(entity);
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.httpClient.post(this._baseUrl, body, { headers: headers }).pipe(catchError((err) => this.handleError(err)), filter(this.filterError));
  }
  update(entity, id: string) {
    let body = JSON.stringify(entity);
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.httpClient.put(this._baseUrl + "/" + id, body, { headers: headers }).pipe(catchError((err) => this.handleError(err)), filter(this.filterError));
  }
  delete(id: string) {
    return this.httpClient.delete(this._baseUrl + "/" + id).pipe(catchError((err) => this.handleError(err)), filter(this.filterError));
  }
  protected handleError(error) {
    console.info('与服务器交互失败！');

    return throwError(error.message || '与服务器交互失败！');
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