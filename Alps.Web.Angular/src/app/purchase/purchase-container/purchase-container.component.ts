import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-purchase-container',
  templateUrl: './purchase-container.component.html',
  styleUrls: ['./purchase-container.component.css']
})
export class PurchaseContainerComponent implements OnInit {

  constructor() { }
  links = {
    "采购订单": "purchaseorderlist", "采购": "purchaseorderedit", "供应商": "supplierlist", "新客户": "supplieredit"//, "新商品": "commodityedit",
  };
  ngOnInit() {
  }

}
