import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoanService } from '../loan.service';

@Component({
  selector: 'app-loan-setting',
  templateUrl: './loan-setting.component.html',
  styleUrls: ['./loan-setting.component.scss']
})
export class LoanSettingComponent implements OnInit {
  newStartExecutionDate;
  newRate;
  setting: any = {};
  testInterestForm: FormGroup;
  interesestResult = 0;
 // minDepositDay;
  //minDepositAmount;
  constructor(private formBuilder: FormBuilder, private loanService: LoanService) {
    this.testInterestForm = formBuilder.group({ startDate: [new Date()], endDate: [new Date()], rate: [30], amount: [1], notEnoughSubDay: [0] });

    //this.settingForm = this.formBuilder.group({ rate: [, Validators.required], startExecutionDate: [, Validators.required] });
    // this.rateForm = this.formBuilder.group({ rate: [, Validators.required], startExecutionDate: [, Validators.required] });
  }

  ngOnInit(): void {
    this.loadSetting();
  }
  loadSetting() {
    this.loanService.getloansetting().subscribe((rst) => {
      this.setting = rst;
      //this.rateList = rst.interestRates;
    });
  }
  saveloansetting() {
   // this.setting.minDepositDay=this.minDepositDay;
    //this.setting.minDepositAmount=this.minDepositAmount;
    this.loanService.saveloansetting(this.setting).subscribe((rst) => {
      this.setting = rst;
    });
  }
  publishNewRate() {
    this.setting.interestRates.push({ startExecutionDate: this.newStartExecutionDate, rate: this.newRate });
  }
  testcalculateInterest() {
    this.loanService.testCalculateInterest(this.testInterestForm.value).subscribe(rst => {
      this.interesestResult = rst;
    });
  }
}
