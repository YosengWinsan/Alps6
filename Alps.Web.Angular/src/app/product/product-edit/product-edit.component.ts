import { Component, OnInit } from '@angular/core';
import { ProductService } from '../product.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { QueryService, } from '../../infrastructure/infrastructure.module';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.css']
})
export class ProductEditComponent implements OnInit {
  catagoryOptions;
  productForm: FormGroup;
  constructor(private productService: ProductService, private queryService: QueryService, private formBuilder: FormBuilder, private activatedRoute: ActivatedRoute,private router:Router) {
    this.productForm = formBuilder.group({ id: [], name: [], fullName: [], catagoryID: [] });
  }

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(params => {
      var id = params["id"];
      if (!id) {
        id = "";
      }
      this.productService.get(id).subscribe(res => {
        this.productForm.patchValue(res);
        this.queryService.getCatagoryOptions().subscribe(options => {
          this.catagoryOptions = options;
        });

      });
    });

  }
  back(){
    history.back();
  }
  save(){
    if(this.productForm.valid)
    this.productService.createAndUpdate(this.productForm.value).subscribe(
      (res) => {
          this.router.navigate(["./"], { relativeTo: this.activatedRoute.parent});
    });
  }

}
