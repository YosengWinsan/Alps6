import { Component, OnInit } from '@angular/core';
import { LoanService } from '../loan.service';

@Component({
  selector: 'app-water-bills',
  templateUrl: './water-bills.component.html',
  styleUrls: ['./water-bills.component.css']
})
export class WaterBillsComponent implements OnInit {

  constructor(private loanService:LoanService) { }
  waterbills;
  displayedColumns=["date","type","name","amount","interestRate","interest","action"];
  ngOnInit() {
    this.waterbills= this.loanService.getWaterBills();
  }

}
