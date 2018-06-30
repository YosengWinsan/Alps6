import { Component, OnInit, ViewChild } from '@angular/core';
import { ProductService } from '../product.service';
import { QueryService } from '../../infrastructure/infrastructure.module';
import { MatTableDataSource, MatSort } from '@angular/material';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  constructor(private productService: ProductService, private queryService: QueryService) {
  }
  @ViewChild(MatSort) matSort: MatSort;
  catagoryID;
  catagoryOptions;
  productDataSource: MatTableDataSource<any>;
  displayedColumns = ["name", "fullName", "catagory", "action"];
  ngOnInit() {
    this.queryService.getCatagoryOptions().subscribe(res => {
      this.catagoryOptions = res;
    });
  }
  onCatagoryChanged(value) {
    if (value && value !== "") {
      this.productService.getProductByCatagoryID(value).subscribe(res => {
        this.productDataSource = new MatTableDataSource(res);
        this.productDataSource.sort = this.matSort;
      });
    }
  }
}
