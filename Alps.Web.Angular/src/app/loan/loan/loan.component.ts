import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-loan',
  templateUrl: './loan.component.html',
  styleUrls: ['./loan.component.css']
})
export class LoanComponent implements OnInit {

  constructor() { }
  links = {"今日流水":"waterbills" ,"存款":"deposit", "取款结息": "loanvoucherlist", "存款人信息": "lenderlist"};
  //,"取款":"withdraw","结息":"settleinterest"
  ngOnInit() {
    
  }

}
