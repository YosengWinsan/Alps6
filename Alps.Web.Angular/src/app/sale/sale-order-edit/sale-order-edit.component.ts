import { Component, OnInit, ViewChild } from '@angular/core';
import { SaleService } from '../sale.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';
import { QueryService, ShopcartService } from '../../infrastructure/infrastructure.module';
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
  saleOrderItemColumns = {
    commodityName: "品名", auxiliaryQuantity: "辅助数量"
    , quantity: "数量", price: "单价", amount: "金额", remark: "备注"//,productSkuID:"商品ID"
  };
  displayedColumns;
  fromshopcart = false;
  @ViewChild("itemTable") itemTable;
  constructor(private saleService: SaleService, private activatedRoute: ActivatedRoute, private router: Router, private formBuilder: FormBuilder
    , private queryService: QueryService, private matDialog: MatDialog, private shopcartService: ShopcartService) {
    this.saleOrderForm = this.formBuilder.group({ id: [], customerID: [], items: [] });
    this.displayedColumns = Object.keys(this.saleOrderItemColumns);
    this.displayedColumns.push("action");
  }
  id = "";
  ngOnInit() {
    this.queryService.getCustomerOptions().subscribe(data => {
      this.customerOptions = data;
    });
    this.activatedRoute.queryParams.subscribe(param => {
      this.id = param["id"] ? param["id"] : "";
      this.fromshopcart = param["fromshopcart"] ? param["fromshopcart"] : false;
      if (this.id != "") {
        this.saleService.getSaleOrder(this.id).subscribe(data => {
          this.saleOrderForm.patchValue(data);
        });
      }
      if (this.fromshopcart) {
        this.shopcartService.shopcart.subscribe(data => this.saleOrderForm.patchValue({ items: data }));
      }
    });
    this.catagories = this.queryService.getCatagoryOptions();
  }
  catagories;
  commodities;
  selectorMode = false;
  onCatagoryChanged(catagoryID) {
    this.commodities = this.saleService.getCommoditiesByCatagoryID(catagoryID);
  }
  addToOrder(commodity) {
    let items = this.saleOrderForm.controls["items"].value;
    if (!items) {
      this.saleOrderForm.patchValue({ items: [] });
      items = this.saleOrderForm.controls["items"].value;
    }
    //var row = items[items.push({}) - 1];
    let row = items.find(p => p.productSkuID == commodity.id);
    if (row) {
      row.auxiliaryQuantity++;
      row.quantity += row.auxiliaryQuantity * commodity.quantityRate;
      row.price = commodity.listPrice;
      row.amount = commodity.listPrice * row.quantity;
    }
    else {
      row = { id: commodity.id, productSkuID: commodity.id, commodityName: commodity.commodityName, auxiliaryQuantity: 1, 
        quantity: commodity.quantityRate,price:commodity.listPrice, amount: commodity.quantityRate * commodity.listPrice };
      items.push(row);

      this.itemTable.renderRows();
    }
    //   row.id = commodity.id;
    // row.productSkuID = commodity.id;
    // row.commodityName = commodity.commodityName;
    // row.auxiliaryQuantity ++;
    // row.quantity += row.auxiliaryQuantity*commodity.quantityRate;
    // row.price = commodity.listPrice;
    // row.amount=commodity.listPrice*row.quantity;
    //row.remark=commodity.remark;
  }

  editItem(item) {
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
    Object.assign(row, item);
    // row.id = item.id;
    // row.commodityID = item.commodityID;
    // row.commodity = item.commodity;
    // row.quantity = item.quantity;
    // row.auxiliaryQuantity = item.auxiliaryQuantity;
    // row.price = item.price;
    // row.amount=item.amount;
    // row.remark=item.remark;

  }
  save() {
    if (this.saleOrderForm.valid) {
      this.saleService.saveSaleOrder(this.saleOrderForm.value).subscribe((res) => {
        if (this.fromshopcart)
          this.shopcartService.clear();
        history.back();
        //this.router.navigate(["./saleorderlist"], { relativeTo: this.activatedRoute.parent });
      });
    }
  }
}
