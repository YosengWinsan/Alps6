import { Component, OnInit } from '@angular/core';
import { LoanService } from '../loan.service';
import { QueryService, AlpsConst } from '../../infrastructure/infrastructure.module';
import { FormBuilder, Validators, FormGroup } from '../../../../node_modules/@angular/forms';
import { ActivatedRoute, Router } from '../../../../node_modules/@angular/router';

@Component({
  selector: 'app-deposit',
  templateUrl: './deposit.component.html',
  styleUrls: ['./deposit.component.css']
})
export class DepositComponent implements OnInit {

  constructor(private loanService: LoanService, private queryService: QueryService, private formBuilder: FormBuilder, private router: Router, private activatedRoute: ActivatedRoute) {
    this.depositForm = formBuilder.group({ lenderID: [, Validators.required], date: [], amount: [, [Validators.required, Validators.pattern(AlpsConst.NON_ZERO_NUMBER_PATTERN)]], voucherNumber: [] });
    this.depositForm.controls.date.disable();
  }
  lenderOptions;
  depositForm: FormGroup;
  ngOnInit() {
    this.lenderOptions = this.queryService.getLenderOptions();
    this.depositForm.controls.date.setValue(new Date());
  }
  confirm() {
    if (this.depositForm.valid) {
      this.loanService.deposit(this.depositForm.value).subscribe((data) => {
        this.router.navigate(['./printvoucher'], { relativeTo: this.activatedRoute.parent,queryParams:{id:data,type:1} });
        //this.router.navigate(['./waterbills'], { relativeTo: this.activatedRoute.parent });
      });
    }
  }
}
