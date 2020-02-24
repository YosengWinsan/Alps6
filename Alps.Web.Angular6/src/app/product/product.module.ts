import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductRoutingModule } from './product-routing.module';
import { ProductListComponent } from './product-list/product-list.component';
import { InfrastructureModule } from '../infrastructure/infrastructure.module';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatCardModule } from '@angular/material/card';




import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ProductEditComponent } from './product-edit/product-edit.component';
import { ProductSkuEditComponent } from './product-sku-edit/product-sku-edit.component';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { CatagoryListComponent } from './catagory-list/catagory-list.component';
import { CatagoryEditComponent } from './catagory-edit/catagory-edit.component';
import { ProductComponent } from './product/product.component';

@NgModule({
  imports: [
    CommonModule,
    ProductRoutingModule, InfrastructureModule, MatTableModule, MatSortModule, MatIconModule, ReactiveFormsModule, MatFormFieldModule, FormsModule, MatInputModule, MatIconModule, MatButtonModule
    , MatDividerModule, MatCheckboxModule, MatCardModule
  ],
  declarations: [ProductListComponent, ProductEditComponent, ProductSkuEditComponent, ProductDetailComponent, CatagoryListComponent, CatagoryEditComponent, ProductComponent]
})
export class ProductModule { }
