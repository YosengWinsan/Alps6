import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTable, MatDialog } from '@angular/material';
import { FormGroup, FormBuilder } from '@angular/forms';
import { StockService } from '../stock.service';
import { ActivatedRoute, Router } from '@angular/router';
import { QueryService } from '../../infrastructure/infrastructure.module';
import { AlpsConst } from '../../infrastructure/alps-const';
import { StockOutItemEditComponent } from './stock-out-item-edit/stock-out-item-edit.component';

@Component({
  selector: 'app-stock-out',
  templateUrl: './stock-out.component.html',
  styleUrls: ['./stock-out.component.css']
})
export class StockOutComponent implements OnInit {
  
  @ViewChild('table') itemTable: MatTable<any>;
  stockOutForm: FormGroup;
  customerOptions;
  departmentOptions;
  displayedColumns = ["productSku",  "auxiliaryQuantity","quantity", "price", "position", "serialNumber", "action"];

  constructor(private stockService: StockService, private formBuilder: FormBuilder, private activatedRoute: ActivatedRoute, private queryService: QueryService
    , private matDialog: MatDialog, private router: Router) {
    this.stockOutForm = formBuilder.group({ id: [], customerID: [], departmentID: [], items: [] })
  }

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe((params) => {
      var id = params["id"];
      if (!id) {
        id = "";
      }
      this.queryService.getCustomerOptions().subscribe((res) => {
        
        this.customerOptions = res;
      });
      this.queryService.getDepartmentOptions().subscribe((res) => {        
        this.departmentOptions = res;
      });
      if (id != "")
        this.stockService.getStockOut(id).subscribe((res) => {
            this.stockOutForm.patchValue(res);          
        });

    });
  }
  save() {
    this.stockService.updateStockOut(this.stockOutForm.value).subscribe((res) => {
      history.back();
        //this.router.navigate(["./stockoutlist"], { relativeTo: this.activatedRoute.parent });
    });
  }
  editItem(row) {
    this.matDialog.open(StockOutItemEditComponent, { data: row, minWidth: "90vw" }).afterClosed().subscribe(res => {
      if (res && res.result) {
        this.combinItem(row, res);
      }    });
  }
  addItem() {
    this.matDialog.open(StockOutItemEditComponent, { data: { id: AlpsConst.GUID_EMPTY }, minWidth: "90vw" }).afterClosed().subscribe(res => {
      if (res && res.result) {
        var items = this.stockOutForm.controls["items"].value;
        if (!items) {
          this.stockOutForm.patchValue({ items: [] });
          items = this.stockOutForm.controls["items"].value;
        }
        var row = items[items.push({}) - 1];
        this.combinItem(row, res);
        this.itemTable.renderRows();
      }
    });
  }
  deleteItem(row) {
    if (confirm("确定要删除？")) {
      var items: any[] = this.stockOutForm.controls["items"].value;
      items.splice(items.indexOf(row), 1);
      this.itemTable.renderRows();
    }
  }
  combinItem(row, res) {
    var item = res.value;
    row.id = item.id;
    row.productSkuID = item.productSkuID;
    row.productSku = item.productSku;
    row.quantity = item.quantity;
    row.auxiliaryQuantity = item.auxiliaryQuantity;
    row.price = item.price;
    row.serialNumber = item.serialNumber;
    row.positionID = item.positionID;
    row.position = item.position;
  }
}
