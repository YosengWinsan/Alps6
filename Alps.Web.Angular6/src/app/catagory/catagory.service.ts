import { Injectable, Injector } from '@angular/core';
import {RepositoryService } from "../infrastructure/infrastructure.module";
import { supportsPassiveEventListeners } from '@angular/cdk/platform'; 

@Injectable({
  providedIn: 'root'
})
export class CatagoryService extends RepositoryService {

  constructor( injector:Injector) {
    super(injector);    
    this.setBaseUrl( "api/catagories");
  }
  getListByParentID(id: string) {
    
    return this.query("GetListByParentID/" + id);
  }
}
