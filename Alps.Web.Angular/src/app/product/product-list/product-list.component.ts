import { Component, OnInit, ViewChild } from '@angular/core';
import { ProductService } from '../product.service';
import { QueryService } from '../../infrastructure/infrastructure.module';
import { MatTableDataSource  } from '@angular/material/table';
import {  MatSort } from '@angular/material/sort';
//import { AlpsSelectorComponent } from '../../infrastructure/alps-selector/alps-selector.component';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  constructor(private productService: ProductService, private queryService: QueryService) {
  }
  @ViewChild(MatSort) matSort: MatSort;
  //@ViewChild("catagorySelector") catagorySelector:AlpsSelectorComponent;
  catagoryID;
  catagoryOptions;
  productDataSource: MatTableDataSource<any>;
  displayedColumns = ["name", "fullName", "catagory", "action"];
  ngOnInit() {
    this.catagoryOptions=this.queryService.getCatagoryOptions();
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
