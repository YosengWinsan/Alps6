import { Injectable, Injector } from '@angular/core';
import { RepositoryService } from '../infrastructure/infrastructure.module';

@Injectable({
  providedIn: 'root'
})
export class ProductService extends RepositoryService {

  constructor(injector: Injector) {
    super(injector);
    this.setBaseUrl("api/products");
  }
private product(){
this.setBaseUrl("api/products");
return this;
}
  private productSku()
  {
    this.setBaseUrl("api/productskus");
    return this;
  }
  getProductByCatagoryID(id) {
    return this.product().query("getProductByCatagoryID/" + id);
  }
  getProductDetail(id) {
    return this.product().query("detail/" + id);
  }
  getProductSku(id) {
    
    return this.productSku().get(id);
  }
  saveProductSku(sku)
  {
    return this.productSku().createAndUpdate(sku);
  }
  saveProduct(product)
  {
    return this.product().createAndUpdate(product);
  }
}
