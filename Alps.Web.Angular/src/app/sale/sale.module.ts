import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SaleRoutingModule } from './sale-routing.module';
import { CommodityListComponent } from './commodity-list/commodity-list.component';
import { SaleOrderListComponent } from './sale-order-list/sale-order-list.component';
import { SaleOrderEditComponent } from './sale-order-edit/sale-order-edit.component';
import { MatTableModule, MatIconModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatGridListModule, MatDividerModule } from '@angular/material';
import { CommodityEditComponent } from './commodity-edit/commodity-edit.component';
import { ReactiveFormsModule } from '@angular/forms';
import { InfrastructureModule } from '../infrastructure/infrastructure.module';
import { SaleOrderItemEditComponent } from './sale-order-edit/sale-order-item-edit/sale-order-item-edit.component';
import { SaleOrderDetailComponent } from './sale-order-detail/sale-order-detail.component';
import { CustomerListComponent } from './customer-list/customer-list.component';
import { CustomerEditComponent } from './customer-edit/customer-edit.component';

@NgModule({
  imports: [
    CommonModule,
    SaleRoutingModule,MatTableModule,MatIconModule,ReactiveFormsModule,MatFormFieldModule,MatInputModule,InfrastructureModule,MatButtonModule,MatGridListModule,MatDividerModule
  ],
  declarations: [CommodityListComponent, SaleOrderListComponent, SaleOrderEditComponent, CommodityEditComponent, SaleOrderItemEditComponent, SaleOrderDetailComponent, CustomerListComponent, CustomerEditComponent],
  entryComponents:[SaleOrderItemEditComponent]
})
export class SaleModule { }
