import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SaleService } from '../sale.service';
import { QueryService } from '../../infrastructure/infrastructure.module';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-customer-edit',
  templateUrl: './customer-edit.component.html',
  styleUrls: ['./customer-edit.component.css']
})
export class CustomerEditComponent implements OnInit {

  constructor(private activatedRoute: ActivatedRoute, private saleService: SaleService, private queryService: QueryService, private formBuilder: FormBuilder) {
    this.customerForm = formBuilder.group({ id: [], name: [], contact: [], address: this.formBuilder.group({ countyID: [], street: [] }) });
  }
  countyOptions;
  customerForm: FormGroup;
  ngOnInit() {
    this.activatedRoute.queryParams.subscribe((params) => {
      var id = params["id"] ? params["id"] : null;
      if (id) {
        this.saleService.getCustomer(id).subscribe((data: any) => {
          this.customerForm.patchValue(data);
        });
      }
    });
    this.queryService.getCountyOptions().subscribe(data => {
      this.countyOptions = data;
    });
  }
  save() {
    if (this.customerForm.valid)
      this.saleService.saveCustomer(this.customerForm.value).subscribe((d) => {
        this.queryService.clearCache("CustomerOptions");
        history.back();
      });
  }
}
