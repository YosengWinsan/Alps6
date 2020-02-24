import { Injectable, Injector } from '@angular/core';
import { RepositoryService } from '../infrastructure/infrastructure.module';

@Injectable({
  providedIn: 'root'
})
export class PurchaseService extends RepositoryService {

  constructor(injector: Injector) {
    super(injector);
  }
  private supplier():PurchaseService{
    this.setBaseUrl("api/suppliers")
    return this;
  }
  public getSuppliers() {
    return this.supplier().getall();
  }
  public getSupplier(id: string) {
    return this.supplier().get(id);
  }
  public saveSupplier(supplier) {
    return this.supplier().createAndUpdate(supplier);
  }
  public getSupplierClasses(){
    return this.supplier().query("getSupplierClasses");
  }
  public getSupplierClass(id){
    return this.supplier().query("getSupplierClass/"+id);
  }
  public saveSupplierClass(supplierClass){
    this.checkAndFillID(supplierClass);
    return this.supplier().action("saveSupplierClass",supplierClass);
  }
}
