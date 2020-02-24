import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PurchaseService } from '../purchase.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { QueryService, AlpsConst } from 'src/app/infrastructure/infrastructure.module';

@Component({
  selector: 'app-supplier-class-edit',
  templateUrl: './supplier-class-edit.component.html',
  styleUrls: ['./supplier-class-edit.component.css']
})
export class SupplierClassEditComponent implements OnInit {

  constructor(private activatedRoute: ActivatedRoute, private purchaseService: PurchaseService,private queryService:QueryService, private formBuilder: FormBuilder) {
    this.supplierForm = formBuilder.group({ id: [], name: []});
  }
  supplierForm: FormGroup;
  ngOnInit() {
    this.activatedRoute.queryParams.subscribe((params) => {
      var id = params["id"] ? params["id"] : null;
      if (id) {
        this.purchaseService.getSupplierClass(id).subscribe((data: any) => {
          this.supplierForm.patchValue(data);
        });
      }
    });
  }
  save() {
    if (this.supplierForm.valid)
      this.purchaseService.saveSupplierClass(this.supplierForm.value).subscribe((d) => {
        this.queryService.clearCache("SupplierClassOptions");
        history.back();
      });
  }

}
