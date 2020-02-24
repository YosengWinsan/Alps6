import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { AlpsSelectorComponent } from '../../../infrastructure/alps-selector/alps-selector.component';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { QueryService } from '../../../infrastructure/infrastructure.module';
import { AlpsConst } from '../../../infrastructure/alps-const';

@Component({
  selector: 'app-stock-out-item-edit',
  templateUrl: './stock-out-item-edit.component.html',
  styleUrls: ['./stock-out-item-edit.component.css']
})
export class StockOutItemEditComponent implements OnInit {
  @ViewChild('skuSelector') skuSelector: AlpsSelectorComponent;
  @ViewChild('positionSelector') positionSelector: AlpsSelectorComponent;
  itemForm: FormGroup;
  productSkuOptions;
  positionOptions;
  constructor(@Inject(MAT_DIALOG_DATA) private formValue, private formBuilder: FormBuilder, private queryService: QueryService, private matDialogRef: MatDialogRef<StockOutItemEditComponent>) {
    this.itemForm = formBuilder.group({
      id: [], productSkuID: [, [Validators.required]], productSku: [], quantity: [0, [Validators.required, Validators.pattern(AlpsConst.NON_ZERO_NUMBER_PATTERN)]],
      auxiliaryQuantity: [0, [Validators.pattern(AlpsConst.NUMBER_PATTERN)]], positionID: [, [Validators.required]], position: [], serialNumber: [], price: [0, [Validators.required, Validators.pattern(AlpsConst.NON_ZERO_NUMBER_PATTERN)]]
    });


  }

  ngOnInit() {
    if (this.formValue)
      this.itemForm.patchValue(this.formValue);
    this.queryService.getProductSkuOptions().subscribe(res => {
      this.productSkuOptions = res;
    });
    this.queryService.getPositionOptions().subscribe(res => {
      this.positionOptions = res;
    });

  }
  cancel() {
    this.matDialogRef.close({ result: false });
  }
  confirm() {
    if (this.itemForm.valid) {
      this.itemForm.patchValue({ productSku: this.skuSelector.getDisplayValue() });
      this.itemForm.patchValue({ position: this.positionSelector.getDisplayValue() });
      this.matDialogRef.close({ result: true, value: this.itemForm.value });
    }
  }
}
