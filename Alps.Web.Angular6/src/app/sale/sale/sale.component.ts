import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-sale',
  templateUrl: './sale.component.html',
  styleUrls: ['./sale.component.css']
})
export class SaleComponent implements OnInit {

  constructor() { }
  links = {
    "商品列表": "commoditylist", "订单列表": "saleorderlist", "客户列表": "customerlist", "新客户": "customeredit",// "新商品": "commodityedit",
    "新订单": "saleorderedit"
  };
  ngOnInit() {
  }

}
