import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '../../../../node_modules/@angular/router';
import { LoanService } from '../loan.service';

@Component({
  selector: 'app-settle-interest',
  templateUrl: './settle-interest.component.html',
  styleUrls: ['./settle-interest.component.css']
})
export class SettleInterestComponent implements OnInit {

  constructor(private activatedRoute: ActivatedRoute, private loanService: LoanService, private router: Router) {
    //this.loanVoucher = {};
  }
  vouchers = [];
  records = [];
  displayedColumns = ["date", "lender", "amount", "interest", "interestSettlable", "action"];
  recorddisplayedColumns = ["date", "type", "name", "amount", "interestRate", "interest", "isInvalid", "action"];
  lender = "";
  totalSettleInterest = 0;
  ngOnInit() {

    this.loadDetail();
  }
  loadDetail() {
    this.activatedRoute.queryParams.subscribe(param => {
      var id = param["id"] ? param["id"] : "";
      if (id != "") {
        this.lender = id;
        this.loanService.getinteresetdetal(id).subscribe(rst => {
          this.vouchers = rst.vouchers;
          this.records = rst.records;
          this.totalSettleInterest = 0;
          this.totalSettleInterest=this.records.reduce((t,c)=>t+(c.isInvalid ? 0 : c.interest),0);
          // this.records.forEach(c=>{
          //   console.log(c.interest);
          //   console.log(c.isInvalid);
          //   this.totalSettleInterest=this.totalSettleInterest + (c.isInvalid ? 0 : c.interest);
          //   console.log(this.totalSettleInterest);
          // });

        });
      }
    });
  }
  settle(id) {
    this.loanService.settleInterest(id).subscribe(rst => {
      this.loadDetail();
    });
  }
  invalidrecord(id) {
    if (confirm("确定要作废此单据？")) {
      this.loanService.invalidrecord(id).subscribe((rst) => {
        this.loadDetail();
      });
    }
  }
  print(lender) {

  }
  // confirm() {
  //   this.loanService.settleInterest(this.loanVoucherID).subscribe((res) => {
  //     this.router.navigate(['./printvoucher'], { relativeTo: this.activatedRoute.parent, queryParams: { id: res, type: 3 } });
  //     //this.router.navigate(['../waterbills'],{relativeTo:this.activatedRoute});
  //   });

  // }

}
