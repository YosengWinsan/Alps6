import { Injectable, Injector } from '@angular/core';
import { RepositoryService } from '../infrastructure/infrastructure.module';

@Injectable({
  providedIn: 'root'
})
export class SaleService extends RepositoryService {

  constructor(injector: Injector) {
    super(injector);
  }
  private setCommodity() {
    this.setBaseUrl("api/Commodities");
  }
  private setSaleOrder() {
    this.setBaseUrl("api/SaleOrders");
  }
  private setCustomer() {
    this.setBaseUrl("api/Customers");
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
  getSaleOrder(id: string) {
    this.setSaleOrder();
    return this.get(id);
  }
  getSaleOrderDetail(id: string) {
    this.setSaleOrder();
    return this.query("detail/" + id);
  }
  saveSaleOrder(saleOrder) {
    this.setSaleOrder();
    return this.createAndUpdate(saleOrder);
  }
  deleteSaleOrder(id) {
    this.setSaleOrder();
    return this.delete(id);
  }
  submitSaleOrder(id) {
    this.setSaleOrder();
    return this.action("Submit/" + id);
  }
  getCustomers() {
    this.setCustomer();
    return this.getall();
  }
  getCustomer(id) {
    this.setCustomer();
    return this.get(id);
  }
}
