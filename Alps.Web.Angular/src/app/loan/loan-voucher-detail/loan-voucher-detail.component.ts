import { Component, OnInit } from '@angular/core';
import { LoanService } from '../loan.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-loan-voucher-detail',
  templateUrl: './loan-voucher-detail.component.html',
  styleUrls: ['./loan-voucher-detail.component.scss']
})
export class LoanVoucherDetailComponent implements OnInit {

  constructor(private loanService: LoanService, private activatedRoute: ActivatedRoute) { }
  detail: any = {};
  displayedColumns = ["date", "type", "name", "amount", "interestRate", "interest", "isInvalid", "action"];
  currentID;
  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(param => {
      let id = param["id"] ? param["id"] : "";
      if (id != "") {
        this.currentID = id;
        this.loadDetail(id);
      }
    });
  }
  loadDetail(id) {
    this.loanService.getloanvoucherdetail(id).subscribe((rst) => {
      if (rst) {
        this.detail = rst;
      }
    });
  }
  invalidrecord(id) {
    if (confirm("确定要作废此单据？")) {
      this.loanService.invalidrecord(id).subscribe((rst) => {
        if (rst)
        this.loadDetail(this.currentID);
      });
    }
  }
  printvoucher() { alert("功能未完成"); }
  printrecord() { alert("功能未完成"); }

}
