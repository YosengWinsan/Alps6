import { Component, OnInit } from '@angular/core';
import { LoanService } from '../loan.service';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-lender-edit',
  templateUrl: './lender-edit.component.html',
  styleUrls: ['./lender-edit.component.css']
})
export class LenderEditComponent implements OnInit {

  constructor(private loanService: LoanService, private fomrBuilder: FormBuilder, private activatedRoute: ActivatedRoute, private router: Router) { }
  lenderForm = this.fomrBuilder.group({ id: [], name: [, Validators.required], idNumber: [, Validators.required], mobilePhoneNumber: [] ,memo:[]});
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
  save() {
    if (this.lenderForm.valid)
      this.loanService.saveLender(this.lenderForm.value).subscribe((rst) => {
        if (rst)
          this.router.navigate(['lenderlist'], { relativeTo: this.activatedRoute.parent });
      });
  }

}
