import { Component, OnInit, ViewChild } from '@angular/core';
import { LoanService } from '../loan.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-settlable-interest-summary',
  templateUrl: './settlable-interest-summary.component.html',
  styleUrls: ['./settlable-interest-summary.component.scss']
})
export class SettlableInterestSummaryComponent implements OnInit {

  constructor(private loanService: LoanService) { }
  @ViewChild(MatSort) matSort: MatSort;
  summmayDataSource: MatTableDataSource<any>;
  summary: any[];
  totalInterest: number = 0;
  totalAmount: number = 0;
  totalCount: number = 0;
  displayedColumns = ["lender", "count", "totalAmount", "totalInterest", "action"];
  ngOnInit(): void {
   

  }
  search(str) {
    this.loanService.getinterestsummary(str).subscribe(rst => {
      this.summmayDataSource = new MatTableDataSource(rst);
      this.summmayDataSource.sort = this.matSort;
      this.summary = rst;
      this.totalInterest = 0;
      this.totalCount = 0;
      this.totalAmount = 0;
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
