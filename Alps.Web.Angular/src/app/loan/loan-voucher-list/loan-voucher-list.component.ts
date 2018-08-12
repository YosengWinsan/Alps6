import { Component, OnInit } from '@angular/core';
import { LoanService } from '../loan.service';
import { debounceTime } from '../../../../node_modules/rxjs/operators';
import { ActivatedRoute, Router } from '../../../../node_modules/@angular/router';

@Component({
  selector: 'app-loan-voucher-list',
  templateUrl: './loan-voucher-list.component.html',
  styleUrls: ['./loan-voucher-list.component.css']
})
export class LoanVoucherListComponent implements OnInit {

  constructor(private loanService: LoanService,private activatedRoute:ActivatedRoute,private router:Router) { }
  loanvouchers;
  displayedColumns=["date","lender","amount","interestRate","interestSettlable","action"];
  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(param => {
      var filter = param["filter"] ? param["filter"] : "";
      if (filter != "") {
        this.loanvouchers =this.loanService.getLoanVouchersByHashCode(filter);
      }
    });
  }
  search(hashcode) {
    this.router.navigate(['./'],{queryParams:{filter:hashcode},relativeTo:this.activatedRoute});
    //this.loanvouchers = this.loanService.getLoanVouchersByHashCode(hashcode);
  }
}
