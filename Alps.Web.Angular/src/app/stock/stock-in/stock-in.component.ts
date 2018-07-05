import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { StockService } from '../stock.service';
import { ActivatedRoute, Router } from '@angular/router';
import { QueryService } from '../../infrastructure/infrastructure.module';
import { MatDialog, MatTable } from '@angular/material';
import { StockInItemEditComponent } from './stock-in-item-edit/stock-in-item-edit.component';
import { AlpsConst } from '../../infrastructure/alps-const';

@Component({
  selector: 'app-stock-in',
  templateUrl: './stock-in.component.html',
  styleUrls: ['./stock-in.component.css']
})
export class StockInComponent implements OnInit {

  @ViewChild('table') itemTable: MatTable<any>;
  stockInForm: FormGroup;
  supplierOptions;
  departmentOptions;
  displayedColumns = ["productSku", "quantity", "price", "auxiliaryQuantity", "position", "serialNumber", "action"];

  constructor(private stockService: StockService, private formBuilder: FormBuilder, private activatedRoute: ActivatedRoute, private queryService: QueryService
    , private matDialog: MatDialog, private router: Router) {
    this.stockInForm = formBuilder.group({ id: [], supplierID: [], departmentID: [], items: [] })
  }

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe((params) => {
      var id = params["id"];
      if (!id) {
        id = "";
      }
      this.queryService.getSupplierOptions().subscribe((res) => {
        
        this.supplierOptions = res;
      });
      this.queryService.getDepartmentOptions().subscribe((res) => {        
        this.departmentOptions = res;
      });
      if (id != "")
        this.stockService.getStockIn(id).subscribe((res) => {
            this.stockInForm.patchValue(res);
          
        });

    });
  }
  save() {
    this.stockService.updateStockIn(this.stockInForm.value).subscribe((res) => {
        this.router.navigate(["./stockinlist"], { relativeTo: this.activatedRoute.parent });
    });
  }
  editItem(row) {
    this.matDialog.open(StockInItemEditComponent, { data: row, minWidth: "90vw" }).afterClosed().subscribe(res => {
      if (res && res.result) {
        this.combinItem(row, res);
      }    });
  }
  addItem() {
    this.matDialog.open(StockInItemEditComponent, { data: { id: AlpsConst.GUID_EMPTY }, minWidth: "90vw" }).afterClosed().subscribe(res => {
      if (res && res.result) {
        var items = this.stockInForm.controls["items"].value;
        if (!items) {
          this.stockInForm.patchValue({ items: [] });
          items = this.stockInForm.controls["items"].value;
        }
        var row = items[items.push({}) - 1];
        this.combinItem(row, res);
        this.itemTable.renderRows();
      }
    });
  }
  deleteItem(row) {
    if (confirm("确定要删除？")) {
      var items: any[] = this.stockInForm.controls["items"].value;
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
