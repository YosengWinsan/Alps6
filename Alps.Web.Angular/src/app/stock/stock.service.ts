import { Injectable, Injector } from '@angular/core';
import { RepositoryService } from '../infrastructure/infrastructure.module';

@Injectable({
  providedIn: 'root'
})
export class StockService extends RepositoryService {

  constructor(injector: Injector) {
    super(injector);
  }
  private setStockIn() { this.setBaseUrl("api/StockInVouchers"); }
  private setStockOut() { this.setBaseUrl("api/stockoutvouchers"); }
  private setStock() { this.setBaseUrl("api/stocks"); }
  private setPosition() { this.setBaseUrl("api/positions"); }
  getStockInVouchers() {
    this.setStockIn();
    return this.getall();
  }
  getStockOutVouchers() {
    this.setStockOut();
    return this.getall();
  }
  getStocks() {
    this.setStock();
    return this.getall();
  }
  getStocksByCatagory(catagoryID){
    this.setStock();
    return this.query("GetStocksByCatagory/"+catagoryID);
  }
  getStockIn(id: string) {
    this.setStockIn();
    return this.get(id);
  }
  getStockInDetail(id:string)
  {
    this.setStockIn();
    return this.query("detail/"+id);
  }
  updateStockIn(entity)
  {
    this.setStockIn();
    return this.createAndUpdate(entity);
  }
  deleteStockIn(id:string)
  {
    this.setStockIn();
    return this.delete(id);
  }
  submitStockIn(id:string)
  {
    this.setStockIn();
    return this.action("Submit/"+id);
  }
  getStockOutDetail(id:string)
  {
    this.setStockOut();
    return this.query("detail/"+id);
  }
  
  getStockOut(id: string) {
    this.setStockOut();
    return this.get(id);
  }
  updateStockOut(entity)
  {
    this.setStockOut();
    return this.createAndUpdate(entity);
  }
  deleteStockOut(id:string)
  {
    this.setStockOut();
    return this.delete(id);
  }
  submitStockOut(id:string)
  {
    this.setStockOut();
    return this.action("Submit/"+id);
  }
  getPositions()
  {
    this.setPosition();
    return this.getall();
  }
  getPosition(id)
  {
    this.setPosition();
    return this.get(id);
  }
  savePosition(entity)
  {
    this.setPosition();
    return this.createAndUpdate(entity);
  }
}
