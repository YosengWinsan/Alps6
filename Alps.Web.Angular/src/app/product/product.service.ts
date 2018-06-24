import { Injectable } from '@angular/core';
import { RepositoryService } from '../infrastructure/infrastructure.module';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProductService extends RepositoryService {

  constructor(http: HttpClient) {
    super(http);
    this.setBaseUrl("api/products");
  }
  getProductByCatagoryID(id) {
    return this.query("getProductByCatagoryID/"+id);
  }
}
