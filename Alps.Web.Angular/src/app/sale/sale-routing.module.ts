import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CommodityListComponent } from './commodity-list/commodity-list.component';
import { CommodityEditComponent } from './commodity-edit/commodity-edit.component';
import { SaleOrderListComponent } from './sale-order-list/sale-order-list.component';
import { SaleOrderEditComponent } from './sale-order-edit/sale-order-edit.component';
import { SaleOrderDetailComponent } from './sale-order-detail/sale-order-detail.component';
import { CustomerListComponent } from './customer-list/customer-list.component';
import { CustomerEditComponent } from './customer-edit/customer-edit.component';

const routes: Routes = [
  {path:"",redirectTo:"commoditylist",pathMatch:"full"},
  {path:"commoditylist",component:CommodityListComponent},
  {path:"commodityedit",component:CommodityEditComponent},
  {path:"saleorderlist",component:SaleOrderListComponent},
  {path:"saleorderedit",component:SaleOrderEditComponent},
  {path:"saleorderdetail",component:SaleOrderDetailComponent},
  {path:"customerlist",component:CustomerListComponent},
  {path:"customeredit",component:CustomerEditComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SaleRoutingModule { }
