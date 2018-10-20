import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PurchaseContainerComponent } from './purchase-container/purchase-container.component';
import { SupplierListComponent } from './supplier-list/supplier-list.component';
import { SupplierEditComponent } from './supplier-edit/supplier-edit.component';
import { PurchaseOrderListComponent } from './purchase-order-list/purchase-order-list.component';
import { PurchaseOrderEditComponent } from './purchase-order-edit/purchase-order-edit.component';

const routes: Routes = [{
  path: "", component: PurchaseContainerComponent, children: [
    { path: "", redirectTo: "purchaseorderlist", pathMatch: "full" },
     { path: "supplierlist", component: SupplierListComponent },
     { path: "supplieredit", component: SupplierEditComponent},
     { path: "purchaseorderlist", component: PurchaseOrderListComponent },
     { path: "purchaseorderedit", component: PurchaseOrderEditComponent },
    // { path: "lenderedit", component: LenderEditComponent },
    // { path: "lenderlist", component: LenderListComponent },
    // { path: "withdraw", component: WithdrawComponent },
    // { path: "deposit", component: DepositComponent },
    // { path: "waterbills", component: WaterBillsComponent },
    // { path: "settleinterest", component: SettleInterestComponent },
    // { path: "printvoucher", component: PrintVoucherComponent }
  ]
}];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PurchaseRoutingModule { }
