import { Component, OnInit } from '@angular/core';
import { LoanService } from '../loan.service';
import { FormBuilder } from '../../../../node_modules/@angular/forms';
import { ActivatedRoute } from '../../../../node_modules/@angular/router';

@Component({
  selector: 'app-lender-edit',
  templateUrl: './lender-edit.component.html',
  styleUrls: ['./lender-edit.component.css']
})
export class LenderEditComponent implements OnInit {

  constructor(private loanService: LoanService, private fomrBuilder: FormBuilder,private activatedRoute:ActivatedRoute) { }
  lenderForm = this.fomrBuilder.group({ name: [], idNumber: [], mobilePhoneNumber: [] });
  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(param => {
      var id = param["id"] ? param["id"] : "";
      if (id != "") {
        this.loanService.getLender(id).subscribe((res) => {
            this.lenderForm.patchValue(res);
        });
      }
    });

  }
  save(){

  }

}
