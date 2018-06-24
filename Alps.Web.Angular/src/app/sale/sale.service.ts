import { Injectable } from '@angular/core';
import { RepositoryService } from '../infrastructure/infrastructure.module';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SaleService extends RepositoryService {

  constructor(http: HttpClient) {
    super(http);
  }
  private setCommodity() {
    this.setBaseUrl("api/Commodities");
  }
  private setSaleOrder() {
    this.setBaseUrl("api/SaleOrders");
  }
  getCommodities() {
    this.setCommodity();
    return this.getall();
  }
  getCommodity(id: string) {
    this.setCommodity();
    return this.get(id);
  }
  saveCommodity(entity) {
    this.setCommodity();
    return this.createAndUpdate(entity);
  }
  getSaleOrders() {
    this.setSaleOrder();
    return this.getall();
  }
  getSaleOrder(id:string)
  {
    this.setSaleOrder();
    return this.get(id);
  }
  getSaleOrderDetail(id:string)
  {
    this.setSaleOrder();
    return this.query("detail/"+ id);
  }
  saveSaleOrder(saleOrder)
  {
    this.setSaleOrder();
    return this.createAndUpdate(saleOrder);
  }
  deleteSaleOrder(id)
  {
    this.setSaleOrder();
    return this.delete(id);
  }
  submitSaleOrder(id)
  {
    this.setSaleOrder();
    return this.action("Submit/"+id);
  }
}
