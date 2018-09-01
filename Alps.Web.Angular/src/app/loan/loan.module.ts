import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoanRoutingModule } from './loan-routing.module';
import { LoanComponent } from './loan/loan.component';
import { LenderListComponent } from './lender-list/lender-list.component';
import { LenderEditComponent } from './lender-edit/lender-edit.component';
import { LoanVoucherListComponent } from './loan-voucher-list/loan-voucher-list.component';
import { InfrastructureModule } from '../infrastructure/infrastructure.module';
import { ReactiveFormsModule } from '../../../node_modules/@angular/forms';
import { DepositComponent } from './deposit/deposit.component';
import { WithdrawComponent } from './withdraw/withdraw.component';
import { WaterBillsComponent } from './water-bills/water-bills.component';
import { MatDatepickerModule, MatNativeDateModule,MatPaginatorModule, MatAutocompleteModule } from '../../../node_modules/@angular/material';
import { SettleInterestComponent } from './settle-interest/settle-interest.component';
import { PrintVoucherComponent } from './print-voucher/print-voucher.component';

@NgModule({
  imports: [
    CommonModule,InfrastructureModule,
    LoanRoutingModule,ReactiveFormsModule,MatDatepickerModule,MatNativeDateModule,MatPaginatorModule,MatAutocompleteModule
  ],
  declarations: [LoanComponent,  LenderListComponent, LenderEditComponent, LoanVoucherListComponent, DepositComponent, WithdrawComponent, WaterBillsComponent, SettleInterestComponent, PrintVoucherComponent]
})
export class LoanModule { }
