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
  displayedColumns = ["date", "type", "name", "amount", "interestRate", "interest", "isInvalid", "action"];
  ngOnInit() {
    this.loanService.getWaterBills().subscribe(rst => {
      this.waterbills = rst;
      this.waterbills.forEach(p => {
        if (!p.isInvalid) {
          if (p.type == 1)
            this.totalDeposit = this.totalDeposit + p.amount;
          if (p.type == 2)
            this.totalWithdraw = this.totalWithdraw + p.amount;
          if (p.type == 3)
            this.totalSettleInterest = this.totalSettleInterest + p.amount;
        }
      });
    });

  }

}
