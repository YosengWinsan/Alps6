import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ProductService } from '../product.service';
import { QueryService } from '../../infrastructure/infrastructure.module';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-product-sku-edit',
  templateUrl: './product-sku-edit.component.html',
  styleUrls: ['./product-sku-edit.component.css']
})
export class ProductSkuEditComponent implements OnInit {
  productSkuForm: FormGroup;
  product: string;
  productID: string;
  constructor(private productService: ProductService, private queryService: QueryService, private formBuilder: FormBuilder, private activatedRoute: ActivatedRoute, private router: Router) {
    this.productSkuForm = formBuilder.group({
      id: [], name: [], productID: [], description: [], code: [], vendable: [false,],
      commodityName: [], listPrice: [0,], quantityRate: [0,], preSellQuantity: [0,], preSellAuxiliaryQuantity: [0,]
    });
    this.productSkuForm.get("vendable").valueChanges.subscribe(value => {
      if (value && !this.productSkuForm.get("commodityName").value) {
        this.productSkuForm.get("commodityName").setValue(this.productSkuForm.get("name").value);
      }
    });

  }

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(params => {
      var id = params["id"] ? params["id"] : null;
      if (id) {
        this.productService.getProductSku(id).subscribe(data => {
          this.productSkuForm.patchValue(data);
          this.loadProduct(data.productID);
        });
      }
      else {
        this.productID = params["productID"] ? params["productID"] : null;
        if (this.productID) {
          this.productSkuForm.patchValue({ productID: this.productID });
          this.loadProduct(this.productID);
        }
      }

    }
    );

  }
  private loadProduct(id) {
    if (id) {
      this.productID = id;
      this.queryService.getProductOption(id).subscribe(data => {
        this.product = data.displayValue;
        if (!this.productSkuForm.controls.name.value)
          this.productSkuForm.controls.name.setValue(this.product);
      });
    }
  }
  save() {
    if (this.productSkuForm.valid) {
      this.productService.saveProductSku(this.productSkuForm.value).subscribe(d => {
        this.router.navigate(["../productdetail"], { queryParams: { id: this.productID }, relativeTo: this.activatedRoute });
      });
    }
  }
}
