import { Injectable } from '@angular/core';
import { RepositoryService } from '../repository/repository.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class QueryService extends RepositoryService {

  constructor(http: HttpClient) {
    super(http);
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
}

