import { Component, OnInit, ViewChild } from '@angular/core';
import { SaleService } from '../sale.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';
import { QueryService } from '../../infrastructure/infrastructure.module';
import { MatDialog } from '@angular/material';
import { SaleOrderItemEditComponent } from './sale-order-item-edit/sale-order-item-edit.component';
import { AlpsConst } from '../../infrastructure/alps-const';

@Component({
  selector: 'app-sale-order-edit',
  templateUrl: './sale-order-edit.component.html',
  styleUrls: ['./sale-order-edit.component.css']
})
export class SaleOrderEditComponent implements OnInit {
  saleOrderForm: FormGroup;
  customerOptions;
  saleOrderItemColumns = { commodity: "品名", auxiliaryQuantity: "辅助数量"
  , quantity: "数量", price: "金额" };
  displayedColumns;
  @ViewChild("itemTable") itemTable;
  constructor(private saleService: SaleService, private activatedRoute: ActivatedRoute, private router: Router, private formBuilder: FormBuilder
    , private queryService: QueryService,private matDialog:MatDialog) {
    this.saleOrderForm = this.formBuilder.group({ id: [], customerID: [], items: [] });
    this.displayedColumns = Object.keys(this.saleOrderItemColumns);
    this.displayedColumns.push("action");
  }

  ngOnInit() {
    this.queryService.getCustomerOptions().subscribe(data => {
      this.customerOptions = data;
    });
    this.activatedRoute.queryParams.subscribe(param => {
      var id = param["id"] ? param["id"] : "";
      if (id != "") {
        this.saleService.getSaleOrder(id).subscribe(data => { this.saleOrderForm.patchValue(data);
        });
      }
    });
  }

  editItem(item){
  this.matDialog.open(SaleOrderItemEditComponent, { data: item, minWidth: "90vw" }).afterClosed().subscribe(res => {
    if (res && res.result) {
      this.combinItem(item, res);
    }
  });
}
addItem() {
  this.matDialog.open(SaleOrderItemEditComponent, { data: { id: AlpsConst.GUID_EMPTY }, minWidth: "90vw" }).afterClosed().subscribe(res => {
    if (res && res.result) {
      var items = this.saleOrderForm.controls["items"].value;
      if (!items) {
        this.saleOrderForm.patchValue({ items: [] });
        items = this.saleOrderForm.controls["items"].value;
      }
      var row = items[items.push({}) - 1];
      this.combinItem(row, res);
      this.itemTable.renderRows();
    }
  });
}
deleteItem(item) {
  if (confirm("确定要删除？")) {
    var items: any[] = this.saleOrderForm.controls["items"].value;
    items.splice(items.indexOf(item), 1);
    this.itemTable.renderRows();
  }
}
combinItem(row, res) {
  var item = res.value;
  row.id = item.id;
  row.commodityID = item.commodityID;
  row.commodity = item.commodity;
  row.quantity = item.quantity;
  row.auxiliaryQuantity = item.auxiliaryQuantity;
  row.price = item.price;
  //row.serialNumber = item.serialNumber;
 // row.positionID = item.positionID;
 // row.position = item.position;
}
save()
{
  if(this.saleOrderForm.valid)
{
  this.saleService.saveSaleOrder(this.saleOrderForm.value).subscribe((res) => {
    this.router.navigate(["./saleorderlist"], { relativeTo: this.activatedRoute.parent });
});
}
}
}
