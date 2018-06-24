import { Component, OnInit } from '@angular/core';
import { ProductService } from '../product.service';
import { QueryService } from '../../infrastructure/infrastructure.module';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  constructor(private productService: ProductService, private queryService: QueryService) { 
  }
catagoryID;
  catagoryOptions;
  productDataSource;
  displayedColumns=["name","fullName","catagory","action"];
  ngOnInit() {
    this.queryService.getCatagoryOptions().subscribe(res => {
      this.catagoryOptions = res;
    });
  }
  onCatagoryChanged(value )
  {
    if(value && value!=="")
  {
this.productService.getProductByCatagoryID(value).subscribe(res=>{
this.productDataSource=res;
});
  }
  }

}
