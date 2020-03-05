import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoanComponent } from './loan/loan.component';
import { LoanVoucherListComponent } from './loan-voucher-list/loan-voucher-list.component';
import { LenderListComponent } from './lender-list/lender-list.component';
import { LenderEditComponent } from './lender-edit/lender-edit.component';
import { WithdrawComponent } from './withdraw/withdraw.component';
import { DepositComponent } from './deposit/deposit.component';
import { WaterBillsComponent } from './water-bills/water-bills.component';
import { SettleInterestComponent } from './settle-interest/settle-interest.component';
import { PrintVoucherComponent } from './print-voucher/print-voucher.component';
import { LenderImportComponent } from './lender-import/lender-import.component';
import { LoanSettingComponent } from './loan-setting/loan-setting.component';
import { LoanVoucherDetailComponent } from './loan-voucher-detail/loan-voucher-detail.component';
import { LoanVoucherSummaryComponent } from './loan-voucher-summary/loan-voucher-summary.component';

const routes: Routes = [{
  path: "", component: LoanComponent, children: [
    { path: "", redirectTo: "waterbills", pathMatch: "full" },
    { path: "loanvoucherlist", component: LoanVoucherListComponent },
    { path: "lenderedit", component: LenderEditComponent },
    { path: "lenderlist", component: LenderListComponent },
    { path: "withdraw", component: WithdrawComponent },
    { path: "deposit", component: DepositComponent },
    { path: "waterbills", component: WaterBillsComponent },
    { path: "settleinterest", component: SettleInterestComponent },
    { path: "printvoucher", component: PrintVoucherComponent },
    { path: "lenderimport", component: LenderImportComponent },
    { path: "loansetting", component: LoanSettingComponent },
    { path: "loanvoucherdetail", component: LoanVoucherDetailComponent },
    { path: "loanvouchersummary", component: LoanVoucherSummaryComponent }
  ],
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LoanRoutingModule { }
