import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoanRoutingModule } from './loan-routing.module';
import { LoanComponent } from './loan/loan.component';
import { LenderListComponent } from './lender-list/lender-list.component';
import { LenderEditComponent } from './lender-edit/lender-edit.component';
import { LoanVoucherListComponent } from './loan-voucher-list/loan-voucher-list.component';
import { InfrastructureModule } from '../infrastructure/infrastructure.module';
import { ReactiveFormsModule,FormsModule } from '../../../node_modules/@angular/forms';
import { DepositComponent } from './deposit/deposit.component';
import { WithdrawComponent } from './withdraw/withdraw.component';
import { WaterBillsComponent } from './water-bills/water-bills.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSortModule } from '@angular/material/sort';
//import { MatToolbarModule } from "@angular/material/toolbar";

import { SettleInterestComponent } from './settle-interest/settle-interest.component';
import { PrintVoucherComponent } from './print-voucher/print-voucher.component';
import { LenderImportComponent } from './lender-import/lender-import.component';
import { LoanSettingComponent } from './loan-setting/loan-setting.component';
import { LoanVoucherDetailComponent } from './loan-voucher-detail/loan-voucher-detail.component';
import { LoanVoucherSummaryComponent } from './loan-voucher-summary/loan-voucher-summary.component';
import { SettlableInterestSummaryComponent } from './settlable-interest-summary/settlable-interest-summary.component';
import { PrintSettleInterestComponent } from './print-settle-interest/print-settle-interest.component';
import { ReviewerComponent } from './reviewer/reviewer.component';

@NgModule({
  imports: [
    CommonModule, InfrastructureModule,FormsModule,
    LoanRoutingModule, ReactiveFormsModule, MatDatepickerModule, MatNativeDateModule, MatPaginatorModule, MatAutocompleteModule, MatCheckboxModule,MatSortModule//,MatToolbarModule
  ],
  declarations: [LoanComponent, LenderListComponent, LenderEditComponent, LoanVoucherListComponent, DepositComponent, WithdrawComponent, WaterBillsComponent, SettleInterestComponent, PrintVoucherComponent, LenderImportComponent, LoanSettingComponent, LoanVoucherDetailComponent, LoanVoucherSummaryComponent, SettlableInterestSummaryComponent, PrintSettleInterestComponent, ReviewerComponent]
})
export class LoanModule { }
