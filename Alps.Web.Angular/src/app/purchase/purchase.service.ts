import { Injectable, Injector } from '@angular/core';
import { RepositoryService } from '../infrastructure/infrastructure.module';

@Injectable({
  providedIn: 'root'
})
export class PurchaseService extends RepositoryService {

  constructor(injector: Injector) {
    super(injector);
  }
  public getSuppliers() {
    this.setBaseUrl("api/suppliers")
    return this.getall();
  }
  public getSupplier(id: string) {
    this.setBaseUrl("api/suppliers");
    return this.get(id);
  }
  public saveSupplier(supplier) {
    this.setBaseUrl("api/suppliers");
    return this.createAndUpdate(supplier);
  }
}
