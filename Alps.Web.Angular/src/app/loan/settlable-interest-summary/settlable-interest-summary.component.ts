import { Component, OnInit } from '@angular/core';
import { LoanService } from '../loan.service';

@Component({
  selector: 'app-settlable-interest-summary',
  templateUrl: './settlable-interest-summary.component.html',
  styleUrls: ['./settlable-interest-summary.component.scss']
})
export class SettlableInterestSummaryComponent implements OnInit {

  constructor(private loanService: LoanService) { }

  summary: any[];
  totalInterest: number = 0;
  totalAmount: number = 0;
  totalCount: number = 0;
  displayedColumns = ["lender", "count","totalAmount" ,"totalInterest", "action"];
  ngOnInit(): void {
    this.loanService.getinterestsummary().subscribe(rst => {
      this.summary = rst;
      this.summary.forEach(p => {
        this.totalInterest = this.totalInterest + p.totalInterest;
        this.totalCount = this.totalCount + p.count;
        this.totalAmount = this.totalAmount + p.totalAmount;
      });
      this.totalInterest = Math.round(this.totalInterest);
      this.totalCount = Math.round(this.totalCount);
      this.totalAmount = Math.round(this.totalAmount);
    });
  }
}
