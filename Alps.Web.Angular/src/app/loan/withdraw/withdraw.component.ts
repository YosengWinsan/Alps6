import { Component, OnInit } from '@angular/core';
import { LoanService } from '../loan.service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-withdraw',
  templateUrl: './withdraw.component.html',
  styleUrls: ['./withdraw.component.css']
})
export class WithdrawComponent implements OnInit {

  constructor(private loanService: LoanService, private activatedRoute: ActivatedRoute, private formBuilder: FormBuilder, private router: Router) {
    this.withdrawForm = formBuilder.group({ loanVoucherID: [], amount: [],operateTime:[] });
    this.loanVoucher={};
  }
  withdrawForm: FormGroup;
  loanVoucher;
  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(param => {
      var id = param["id"] ? param["id"] : "";
      if (id != "") {
        this.loanService.getLoanVoucherInfo(id).subscribe((res) => {
          this.loanVoucher = res;
          this.withdrawForm.controls.loanVoucherID.setValue(id);
        });
      }
    });
  }
  confirm() {
    if (this.withdrawForm.valid) {
      this.loanService.withdraw(this.withdrawForm.value).subscribe(res => {
        
        this.router.navigate(['./printvoucher'], { relativeTo: this.activatedRoute.parent,queryParams:{id:res,type:2} });
          //this.router.navigate(['./waterbills'],{relativeTo:this.activatedRoute.parent});
      });
    }
  }
}