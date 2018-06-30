import { Component, OnInit } from '@angular/core';
import { ProductService } from '../product.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent implements OnInit {
  displayedColumns=["name","description","auxiliaryQuantity","quantity","action"];
  product={};
  constructor(private productService: ProductService, private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(params => {
      const id = params["id"] ? params["id"] : null;
      if (id)
        this.productService.getProductDetail(id).subscribe(data => {
          this.product = data;
        });
    });

  }

}
