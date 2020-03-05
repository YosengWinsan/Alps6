import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoanService } from '../loan.service';

@Component({
  selector: 'app-loan-setting',
  templateUrl: './loan-setting.component.html',
  styleUrls: ['./loan-setting.component.scss']
})
export class LoanSettingComponent implements OnInit {
  settingForm: FormGroup;
  rateForm: FormGroup;
  rateList: {}
  setting:{};
  constructor(private formBuilder: FormBuilder, private loanService: LoanService) {
    //this.settingForm = this.formBuilder.group({ rate: [, Validators.required], startExecutionDate: [, Validators.required] });
    this.rateForm = this.formBuilder.group({ rate: [, Validators.required], startExecutionDate: [, Validators.required] });
  }

  ngOnInit(): void {
    this.loanService.getloansetting().subscribe((rst) => {
      this.setting = rst;
      this.rateList=rst.interestRates;
    });
  }
  publishNewInterestRate() {
    this.loanService.publishnewrate(this.rateForm.controls.startExecutionDate.value, this.rateForm.controls.rate.value).subscribe((rst) => {
      this.loanService.getloaninterestrates().subscribe(rst => {
        this.rateList = rst;
      });
    });

  }

}
