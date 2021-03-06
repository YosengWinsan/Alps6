import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '../../../../node_modules/@angular/router';
import { LoanService } from '../loan.service';

@Component({
  selector: 'app-print-voucher',
  templateUrl: './print-voucher.component.html',
  styleUrls: ['./print-voucher.component.css']
})
export class PrintVoucherComponent implements OnInit {

  constructor(private activatedRoute: ActivatedRoute, private loanService: LoanService) {
    this.printInfo = {};
  }
  printInfo;
  type = "";
  typeDecription = "";
  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(param => {
      let id = param["id"] ? param["id"] : "";
      this.type = param["type"] ? param["type"] : "";
      if (id != "" && (this.type != "")) {
        this.loanService.getPrintInfo(this.type, id).subscribe((res) => {
          this.printInfo = res;
          switch (this.type) {
            case "Deposit":
              this.typeDecription = "借本金款";
              break;
            case "Withdraw":
              this.typeDecription = "取款结息";
              break;
            case "SettleInterest":
              this.typeDecription = "结息";
              break;
          }
        });
      }

    });
  }

}
