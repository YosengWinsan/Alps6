import { Injectable, Injector } from '@angular/core';
import { RepositoryService } from '../repository/repository.service';

@Injectable({
  providedIn: 'root'
})
export class QueryService extends RepositoryService {

  constructor(injector: Injector) {
    super(injector);
    this.setBaseUrl("api/query");
  }
  initDatabase() {
    return this.query("InitDatabase");
  }
  getCatagoryOptions()
  {
    return this.query("CatagoryOptions");
  }
  getTradeAccountOptions(){
    return this.query("TradeAccountOptions");
  }
  getProductSkuOptions(){
    return this.query("ProductSkuOptions");
  }
  getPositionOptions(){
    return this.query("PositionOptions")
  }
  getCommodityOptions(){
    return this.query("CommodityOptions");
  }
  getCatagoryOption(id)
  {
return this.query("CatagoryOption/"+id);
  }
  getProductOption(id)
  {
return this.query("ProductOption/"+id);
  }
}

