import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SaleRoutingModule } from './sale-routing.module';
import { CommodityListComponent } from './commodity-list/commodity-list.component';
import { SaleOrderListComponent } from './sale-order-list/sale-order-list.component';
import { SaleOrderEditComponent } from './sale-order-edit/sale-order-edit.component';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule} from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule} from '@angular/material/button';
import { MatGridListModule} from '@angular/material/grid-list';
import { MatDividerModule} from '@angular/material/divider';
import { MatSortModule } from '@angular/material/sort';
import { MatExpansionModule } from '@angular/material/expansion';


import { CommodityEditComponent } from './commodity-edit/commodity-edit.component';
import { ReactiveFormsModule } from '@angular/forms';
import { InfrastructureModule } from '../infrastructure/infrastructure.module';
import { SaleOrderItemEditComponent } from './sale-order-edit/sale-order-item-edit/sale-order-item-edit.component';
import { SaleOrderDetailComponent } from './sale-order-detail/sale-order-detail.component';
import { CustomerListComponent } from './customer-list/customer-list.component';
import { CustomerEditComponent } from './customer-edit/customer-edit.component';
import { SaleComponent } from './sale/sale.component';
import { CommoditySelectorComponent } from './commodity-selector/commodity-selector.component';

@NgModule({
  imports: [
    CommonModule,
    SaleRoutingModule,MatTableModule,MatIconModule,ReactiveFormsModule,MatFormFieldModule,MatInputModule,InfrastructureModule,MatButtonModule,MatGridListModule,MatDividerModule
    ,MatSortModule,MatExpansionModule
  ],
  declarations: [CommodityListComponent, SaleOrderListComponent, SaleOrderEditComponent, CommodityEditComponent, SaleOrderItemEditComponent, SaleOrderDetailComponent, CustomerListComponent, CustomerEditComponent, SaleComponent, CommoditySelectorComponent],
  entryComponents:[SaleOrderItemEditComponent]
})
export class SaleModule { }
