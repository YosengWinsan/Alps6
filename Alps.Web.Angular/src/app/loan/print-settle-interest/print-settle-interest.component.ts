import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LoanService } from '../loan.service';

@Component({
  selector: 'app-print-settle-interest',
  templateUrl: './print-settle-interest.component.html',
  styleUrls: ['./print-settle-interest.component.scss']
})
export class PrintSettleInterestComponent implements OnInit {

  constructor(private activatedRoute: ActivatedRoute, private loanService: LoanService) { }
  printInfo:any={};
  ngOnInit(): void {

    this.activatedRoute.queryParams.subscribe(param => {
      let id = param["id"] ? param["id"] : "";
      if (id != "") {
        this.loanService.getSettleInteresetPrintInfo(id).subscribe((res) => {
          this.printInfo = res;
        });
      }
    });

  }
}
