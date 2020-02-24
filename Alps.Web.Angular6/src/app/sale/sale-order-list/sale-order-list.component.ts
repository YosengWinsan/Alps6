import { Component, OnInit } from '@angular/core';
import { SaleService } from '../sale.service';

@Component({
  selector: 'app-sale-order-list',
  templateUrl: './sale-order-list.component.html',
  styleUrls: ['./sale-order-list.component.css']
})
export class SaleOrderListComponent implements OnInit {

  constructor(private slaeService: SaleService) { }
  saleOrders;
  //displayedColumns=["orderTime","customer","totalQuantity","totalAuxiliaryQuantity","totalAmount","status","action"];
  saleOrderColumns = { customer: "客户", totalQuantity: "数量", totalAuxiliaryQuantity: "辅助数量", totalAmount: "金额", status: "状态" };
  displayedColumns = (Object.keys(this.saleOrderColumns));
  ngOnInit() {
    this.displayedColumns.unshift("orderTime");
    this.displayedColumns.push("action");
    this.slaeService.getSaleOrders().subscribe(data => {
      this.saleOrders = data;
    });
  }

}
