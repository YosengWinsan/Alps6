import { Injectable, Injector } from '@angular/core';
import { RepositoryService } from '../repository/repository.service';
import { tap } from 'rxjs/operators';
import { of } from 'rxjs';

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
  getPath(){
    return this.query("GetAnswer");
  }
  private queryAndCache(url) {
    const cache = JSON.parse(sessionStorage.getItem(url));
    return cache ? of(cache) : this.query(url).pipe(tap((res) => {
      sessionStorage.setItem(url, JSON.stringify(res));
    }));
  }
  clearCache(token?: string) {
    if (token)
      sessionStorage.removeItem(token);
    else
      sessionStorage.clear();
  }

  getDashboardInfo() {
    return this.queryAndCache("DashboardInfo");
  }
  getCatagoryOptions() {
    return this.queryAndCache("CatagoryOptions");
  }
  getTradeAccountOptions() {
    return this.queryAndCache("TradeAccountOptions");
  }
  getDepartmentOptions() {
    return this.queryAndCache("DepartmentOptions");
  }
  getCustomerOptions() {
    return this.queryAndCache("CustomerOptions");
  }
  getSupplierOptions() {
    return this.queryAndCache("SupplierOptions");
  }
  getProductSkuOptions() {
    return this.queryAndCache("ProductSkuOptions");
  }
  getPositionOptions() {
    return this.queryAndCache("PositionOptions")
  }
  getCommodityOptions() {
    return this.queryAndCache("CommodityOptions");
  }
  getCatagoryOption(id) {
    return this.query("CatagoryOption/" + id);
  }
  getProductOption(id) {
    return this.query("ProductOption/" + id);
  }
  getCountyOptions() {
    return this.queryAndCache("CountyOptions");
  }
  getLenderOptions(){
    return this.queryAndCache("LenderOptions");
  }
  getRoleOptions(){
    return this.queryAndCache("RoleOptions");
  }
  getDispatchRecordOptions(){
    return this.query("DispatchRecordOptions");
  }
}

