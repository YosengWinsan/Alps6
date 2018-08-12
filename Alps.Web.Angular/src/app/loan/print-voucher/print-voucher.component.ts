import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '../../../../node_modules/@angular/router';
import { LoanService } from '../loan.service';

@Component({
  selector: 'app-print-voucher',
  templateUrl: './print-voucher.component.html',
  styleUrls: ['./print-voucher.component.css']
})
export class PrintVoucherComponent implements OnInit {

  constructor(private activatedRoute:ActivatedRoute,private loanService:LoanService) { 
    this.printInfo={};
  }
printInfo;
type=1;
  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(param => {
      let id = param["id"] ? param["id"] : "";
      this.type = param["type"] ? param["type"] : "";
      if (id != "" && (this.type>0)) {
        this.loanService.getPrintInfo(this.type,id).subscribe((res) => {
          this.printInfo = res;
        });
      }
    });
  }

}
