import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PurchaseService } from '../purchase.service';
import { QueryService } from 'src/app/infrastructure/infrastructure.module';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-supplier-edit',
  templateUrl: './supplier-edit.component.html',
  styleUrls: ['./supplier-edit.component.css']
})
export class SupplierEditComponent implements OnInit {

  constructor(private activatedRoute: ActivatedRoute, private purchaseService: PurchaseService, private queryService: QueryService, private formBuilder: FormBuilder) {
    this.supplierForm = formBuilder.group({ id: [], name: [], contact: [], address: this.formBuilder.group({ countyID: [], street: [] }) });
  }
  countyOptions;
  supplierForm: FormGroup;
  ngOnInit() {
    this.activatedRoute.queryParams.subscribe((params) => {
      var id = params["id"] ? params["id"] : null;
      if (id) {
        this.purchaseService.getSupplier(id).subscribe((data: any) => {
          this.supplierForm.patchValue(data);
        });
      }
    });
    this.queryService.getCountyOptions().subscribe(data => {
      this.countyOptions = data;
    });
  }
  save() {
    if (this.supplierForm.valid)
      this.purchaseService.saveSupplier(this.supplierForm.value).subscribe((d) => {
        this.queryService.clearCache("SupplierOptions");
        history.back();
      });
  }
}
