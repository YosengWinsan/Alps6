import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-distribution',
  templateUrl: './distribution.component.html',
  styleUrls: ['./distribution.component.css']
})
export class DistributionComponent implements OnInit {

  constructor() { }
  totalItemsColumns = { commodityName: "品名", auxiliaryQuantity: "辅助数量", quantity: "数量", price: "单价", amount: "金额", remark: "备注" };
totalItems=[];
  displayedColumns;
  ngOnInit() {
    this.displayedColumns = Object.keys(this.totalItemsColumns);

  }

}
