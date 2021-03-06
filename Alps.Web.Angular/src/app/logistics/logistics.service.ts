import { Injectable, Injector } from '@angular/core';
import { RepositoryService } from '../infrastructure/infrastructure.module';

@Injectable({
  providedIn: 'root'
})
export class LogisticsService extends RepositoryService {

  constructor(injector: Injector) {
    super(injector);
    this.setBaseUrl("api/logistics");
  }
  getCars(){
    return this.query("getcars");
  }
  getDispatchRecord(id)
  {
    return this.query("getDispatchRecord/"+id);
  }
  
}
