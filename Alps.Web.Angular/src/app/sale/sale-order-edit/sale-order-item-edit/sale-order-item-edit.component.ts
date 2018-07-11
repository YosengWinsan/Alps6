import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { AlpsSelectorComponent } from '../../../infrastructure/alps-selector/alps-selector.component';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { QueryService } from '../../../infrastructure/infrastructure.module';
import { AlpsConst } from '../../../infrastructure/alps-const';

@Component({
  selector: 'app-sale-order-item-edit',
  templateUrl: './sale-order-item-edit.component.html',
  styleUrls: ['./sale-order-item-edit.component.css']
})
export class SaleOrderItemEditComponent implements OnInit {
  @ViewChild('commoditySelector') commoditySelector: AlpsSelectorComponent;
  itemForm: FormGroup;
  commodityOptions;
  constructor(@Inject(MAT_DIALOG_DATA) private formValue, private formBuilder: FormBuilder, private queryService: QueryService, private matDialogRef: MatDialogRef<SaleOrderItemEditComponent>) {
    this.itemForm = formBuilder.group({
      id: [], commodityID: [, [Validators.required]], commodity: [], quantity: [0, [Validators.required, Validators.pattern(AlpsConst.NON_ZERO_NUMBER_PATTERN)]],
      auxiliaryQuantity: [0, [Validators.pattern(AlpsConst.NUMBER_PATTERN)]], price: [0, [Validators.required, Validators.pattern(AlpsConst.NON_ZERO_NUMBER_PATTERN)]]
      , remark: [],amount:[]
    });

  }

  ngOnInit() {
    if (this.formValue)
      this.itemForm.patchValue(this.formValue);
    this.queryService.getCommodityOptions().subscribe(res => {
      this.commodityOptions = res;     
    });
    this.itemForm.get("quantity").valueChanges.subscribe(quantity => {   
      this.itemForm.get("amount").setValue(this.itemForm.get("price").value*quantity);
      if (this.commoditySelector.getOptionValue("quantityRate") > 0)
        this.itemForm.get("auxiliaryQuantity").setValue(quantity / this.commoditySelector.getOptionValue("quantityRate"));
    });
    this.itemForm.get("price").valueChanges.subscribe(price=>
    {
      this.itemForm.get("amount").setValue(this.itemForm.get("quantity").value*price);
    });
  }

  addAq() {
    //this.itemForm.get("auxiliaryQuantity").setValue(this.itemForm.get("auxiliaryQuantity").value+1);
    this.itemForm.get("quantity").setValue(this.itemForm.get("quantity").value + this.commoditySelector.getOptionValue("quantityRate"));
  }
  removeAq() {
    //this.itemForm.get("auxiliaryQuantity").setValue(this.itemForm.get("auxiliaryQuantity").value-1);

    this.itemForm.get("quantity").setValue(this.itemForm.get("quantity").value - this.commoditySelector.getOptionValue("quantityRate"));
  }
  cancel() {
    this.matDialogRef.close({ result: false });
  }
  confirm() {
    if (this.itemForm.valid) {
      this.itemForm.patchValue({ commodity: this.commoditySelector.getDisplayValue() });
      this.matDialogRef.close({ result: true, value: this.itemForm.value });
    }
  }
}
