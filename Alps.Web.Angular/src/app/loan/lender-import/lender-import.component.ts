import { Component, OnInit } from '@angular/core';
import { LoanService } from '../loan.service';

@Component({
  selector: 'app-lender-import',
  templateUrl: './lender-import.component.html',
  styleUrls: ['./lender-import.component.scss']
})
export class LenderImportComponent implements OnInit {

  constructor(private loanService: LoanService) { }
  liststr = "";
  voucherstr = "";
  depositstr="";
  withdrawstr="";
  deposits=[];
  withdraws=[];
  lenders: any[] = [];
  vouchers = [];
  ngOnInit(): void {
  }
  parseLender() {
    this.liststr.split("^").forEach(p => {
      let l = p.replace(/\n/g,"").split(",");
      if (l.length == 3)
        this.lenders.push({ name: l[0], mobilePhoneNumber: l[1], idNumber: l[2] });
    });

  }
  importLender() {
    this.loanService.importLender(this.lenders).subscribe((rst) => {
      if (rst)
        alert("成功导入" + rst + "条记录");
    });
  }
  parsevoucher() {
    this.voucherstr.replace(/\n/g,"").split("^").forEach(p => {
      let l = p.split(",");
      if (l.length == 5) {
        if (l[3] == "")
          l[3] = l[2];
        this.vouchers.push({ lender: l[0], amount: Number(l[1]), date: new Date(l[2]), reWriteTime: new Date(l[3]), memo: l[4] });
      }
    });
  }
  importvoucher() {
    this.loanService.importvoucher(this.vouchers).subscribe(rst => {
      alert("成功导入" + rst + "条记录");
    });
  }
  parsewithdraw(){
    this.withdrawstr.replace(/\n/g,"").split("^").forEach(p => {
      let l = p.split(",");
      if (l.length == 5) {
        if (l[4] == "")
          l[4] = l[2];
        this.withdraws.push({ lender: l[0], amount: Number(l[2]), date: new Date(l[1]), depositDate: new Date(l[3]), depositAmount: Number(l[4]) });
      }
    });
  }
  importwithdraw(){
    this.loanService.importwithdraw(this.withdraws).subscribe(rst=>{
      alert("成功导入" + rst + "条记录");
    });
  }
}
