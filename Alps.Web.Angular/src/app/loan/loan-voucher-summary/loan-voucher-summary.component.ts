import { Component, OnInit } from '@angular/core';
import { LoanService } from '../loan.service';

@Component({
  selector: 'app-loan-voucher-summary',
  templateUrl: './loan-voucher-summary.component.html',
  styleUrls: ['./loan-voucher-summary.component.scss']
})
export class LoanVoucherSummaryComponent implements OnInit {

  constructor(private loanService: LoanService) { }

  summary: any[];
  totalAmount: number = 0;
  totalCount: number = 0;
  displayedColumns = ["lender", "totalAmount", "count", "action"];
  ngOnInit(): void {
    this.loanService.getVoucherSummary().subscribe(rst => {
      this.summary = rst;
      this.summary.forEach(p => {
        this.totalAmount = this.totalAmount + p.totalAmount;
        this.totalCount = this.totalCount + p.count;
      });
      this.totalAmount = Math.round(this.totalAmount * 10000) / 10000;
      this.totalCount = Math.round(this.totalCount);
    });

  }

}
