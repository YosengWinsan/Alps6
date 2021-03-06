import { Component, OnInit } from '@angular/core';
import { LoanService } from '../loan.service';

@Component({
  selector: 'app-water-bills',
  templateUrl: './water-bills.component.html',
  styleUrls: ['./water-bills.component.css']
})
export class WaterBillsComponent implements OnInit {

  constructor(private loanService: LoanService) { }
  waterbills: any[] = [];
  totalDeposit = 0;
  totalWithdraw = 0;
  totalSettleInterest = 0;
  totalWithdrawInterest = 0;
  displayedColumns = ["date", "type", "name", "amount", "interestRate", "interest", "isInvalid", "action"];
  ngOnInit() {
    this.loanService.getWaterBills().subscribe(rst => {
      this.waterbills = rst;
      this.totalDeposit = 0;
      this.totalWithdraw = 0;
      this.totalSettleInterest = 0;
      this.totalWithdrawInterest = 0;
      this.waterbills.forEach(p => {
        if (!p.isInvalid) {
          if (p.type == 1)
            this.totalDeposit = this.totalDeposit + p.amount;
          if (p.type == 2) {
            this.totalWithdraw = this.totalWithdraw + p.amount;
            this.totalWithdrawInterest = this.totalWithdrawInterest + p.interest;
          }
          if (p.type == 3)
            this.totalSettleInterest = this.totalSettleInterest + p.interest;
        }
      });
    });

  }

}
