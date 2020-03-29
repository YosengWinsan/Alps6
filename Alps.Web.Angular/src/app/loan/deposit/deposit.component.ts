import { Component, OnInit } from '@angular/core';
import { LoanService } from '../loan.service';
import { QueryService, AlpsConst } from '../../infrastructure/infrastructure.module';
import { FormBuilder, Validators, FormGroup } from '../../../../node_modules/@angular/forms';
import { ActivatedRoute, Router } from '../../../../node_modules/@angular/router';
import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { PinYinHelper } from '../../extends/PinYinHelper';

@Component({
  selector: 'app-deposit',
  templateUrl: './deposit.component.html',
  styleUrls: ['./deposit.component.css']
})
export class DepositComponent implements OnInit {

  constructor(private loanService: LoanService, private queryService: QueryService, private formBuilder: FormBuilder, private router: Router, private activatedRoute: ActivatedRoute) {
    this.depositForm = formBuilder.group({ lenderID: [, Validators.required], date: [new Date()], amount: [, [Validators.required, Validators.pattern(AlpsConst.NON_ZERO_NUMBER_PATTERN)]], voucherNumber: [] });
    //this.depositForm.controls.date.disable();
  }
  lenderOptions: Observable<any>;
  depositForm: FormGroup;
  lenderDs;
  ngOnInit() {
    this.lenderDs=this.queryService.getLenderOptions();
    this.queryService.getLenderOptions().subscribe(data => {
      for(let item of data)
      {
        item.pinyin=PinYinHelper.ConvertPinyin( item.displayValue).toLowerCase();
      }
      this.lenderOptions = this.depositForm.controls.lenderID.valueChanges.pipe(
        startWith(''), map((v:any)=> typeof v==="string"?v:v.displayValue) ,map(v => this.filterLenderOption(v, data))
      );
    });
    //this.depositForm.patchValue({lenderID:'1fbb3218-775f-45b2-3b59-08d6100ad6bf'});
  }
  lenderInputOut(e){
    console.info(this.depositForm.controls.lenderID.value);
    console.info(e);
  }
  filterLenderOption(name: string, options: any[]) {
    let filterValue = name.toLowerCase();
    return options.filter(option => option.displayValue.toLowerCase().includes(filterValue)|| option.pinyin.includes(filterValue));
  }
  displayLenderOption(option) {
    return option ? option.displayValue : undefined;
  }
  confirm() {
    if (this.depositForm.valid) {
      this.loanService.deposit(this.depositForm.value).subscribe((data) => {
        this.router.navigate(['./printvoucher'], { relativeTo: this.activatedRoute.parent, queryParams: { id: data, type: 1 } });
      });
    }
  }
}
