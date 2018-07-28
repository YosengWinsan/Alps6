import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductRoutingModule } from './product-routing.module';
import { ProductListComponent } from './product-list/product-list.component';
import { InfrastructureModule } from '../infrastructure/infrastructure.module';
import { MatTableModule, MatSortModule, MatIconModule, MatFormFieldModule, MatInputModule, MatButtonModule,MatDividerModule, MatCheckboxModule, MatCardModule } from '@angular/material';
import { ReactiveFormsModule,FormsModule } from '@angular/forms';
import { ProductEditComponent } from './product-edit/product-edit.component';
import { ProductSkuEditComponent } from './product-sku-edit/product-sku-edit.component';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { CatagoryListComponent } from './catagory-list/catagory-list.component';
import { CatagoryEditComponent } from './catagory-edit/catagory-edit.component';
import { ProductComponent } from './product/product.component';

@NgModule({
  imports: [
    CommonModule,
    ProductRoutingModule,InfrastructureModule,MatTableModule,MatSortModule,MatIconModule,ReactiveFormsModule,MatFormFieldModule,FormsModule,MatInputModule,MatIconModule,MatButtonModule
    ,MatDividerModule,MatCheckboxModule,MatCardModule
  ],
  declarations: [ProductListComponent, ProductEditComponent, ProductSkuEditComponent, ProductDetailComponent, CatagoryListComponent, CatagoryEditComponent, ProductComponent]
})
export class ProductModule { }
