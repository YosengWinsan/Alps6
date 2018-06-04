import { Injectable } from '@angular/core';
import { RepositoryService } from "../infrastructure/repository.service";
import { HttpClient } from "@angular/common/http";
import { supportsPassiveEventListeners } from '@angular/cdk/platform'; 

@Injectable({
  providedIn: 'root'
})
export class CatagoryService extends RepositoryService {

  constructor( http: HttpClient) {
    super(http);    
    this.setBaseUrl( "api/catagories");
  }
  getListByParentID(id: string) {
    
    return this.query("GetListByParentID/" + id);
  }
}
