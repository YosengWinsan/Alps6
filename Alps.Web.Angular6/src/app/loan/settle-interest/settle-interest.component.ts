import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '../../../../node_modules/@angular/router';
import { LoanService } from '../loan.service';

@Component({
  selector: 'app-settle-interest',
  templateUrl: './settle-interest.component.html',
  styleUrls: ['./settle-interest.component.css']
})
export class SettleInterestComponent implements OnInit {

  constructor(private activatedRoute:ActivatedRoute,private loanService:LoanService,private router:Router) {
    this.loanVoucher={};
   }
loanVoucher;
loanVoucherID;
//settleInterestForm:FormGroup;
  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(param => {
      var id = param["id"] ? param["id"] : "";
      if (id != "") {
        this.loanService.getLoanVoucherInfo(id).subscribe((res) => {
          this.loanVoucher = res;
          this.loanVoucherID=id;
          //this.settleInterestForm.controls.loanVoucherID.setValue(id);
        });
      }
    });
  }
  confirm() {
    this.loanService.settleInterest(this.loanVoucherID).subscribe((res)=>{
      this.router.navigate(['./printvoucher'], { relativeTo: this.activatedRoute.parent,queryParams:{id:res,type:3} });
      //this.router.navigate(['../waterbills'],{relativeTo:this.activatedRoute});
    });
    
  }

}
