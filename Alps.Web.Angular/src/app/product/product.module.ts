import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductRoutingModule } from './product-routing.module';
import { ProductListComponent } from './product-list/product-list.component';
import { InfrastructureModule } from '../infrastructure/infrastructure.module';
import { MatTableModule, MatSortModule, MatIconModule, MatFormFieldModule, MatInputModule, MatButtonModule } from '@angular/material';
import { ReactiveFormsModule,FormsModule } from '@angular/forms';
import { ProductEditComponent } from './product-edit/product-edit.component';

@NgModule({
  imports: [
    CommonModule,
    ProductRoutingModule,InfrastructureModule,MatTableModule,MatSortModule,MatIconModule,ReactiveFormsModule,MatFormFieldModule,FormsModule,MatInputModule,MatIconModule,MatButtonModule
  ],
  declarations: [ProductListComponent, ProductEditComponent]
})
export class ProductModule { }
