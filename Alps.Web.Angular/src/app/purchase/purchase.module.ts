import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PurchaseRoutingModule } from './purchase-routing.module';
import { PurchaseContainerComponent } from './purchase-container/purchase-container.component';
import { SupplierListComponent } from './supplier-list/supplier-list.component';
import { InfrastructureModule } from '../infrastructure/infrastructure.module';
import { SupplierEditComponent } from './supplier-edit/supplier-edit.component';
import { PurchaseOrderListComponent } from './purchase-order-list/purchase-order-list.component';
import { PurchaseOrderEditComponent } from './purchase-order-edit/purchase-order-edit.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    PurchaseRoutingModule,InfrastructureModule,ReactiveFormsModule
  ],
  declarations: [ PurchaseContainerComponent,  SupplierListComponent, SupplierEditComponent, PurchaseOrderListComponent, PurchaseOrderEditComponent]
})
export class PurchaseModule { }
