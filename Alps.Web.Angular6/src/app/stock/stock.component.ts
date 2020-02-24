import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-stock',
  templateUrl: './stock.component.html',
  styleUrls: ['./stock.component.css']
})
export class StockComponent implements OnInit {

  constructor() { }
  navLinks = {
    "入库": "stockin", "出库": "stockout", "库存明细": 'stockinfo', "入库明细": 'stockinlist', "出库明细": 'stockoutlist', "仓位明细": 'positionlist'
  };
  ngOnInit() {
  }

}
