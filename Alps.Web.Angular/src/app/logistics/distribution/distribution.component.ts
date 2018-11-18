import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-distribution',
  templateUrl: './distribution.component.html',
  styleUrls: ['./distribution.component.css']
})
export class DistributionComponent implements OnInit {

  constructor() { }
  totalItemsColumns = { commodityName: "品名", auxiliaryQuantity: "辅助数量", quantity: "数量", price: "单价", amount: "金额", remark: "备注" 
  
  ,stockOutAQ:"出仓辅数",stockOutQ:"出仓数量",distributedAQ:"已配辅数",distributedQ:"已配数量",distributableAQ:"可配辅数",distributableQ:"出仓数量"
  ,"distributionAQ":"配送辅数","distributionQ":"配送数量"};
totalItems=[];
  displayedColumns;
  ngOnInit() {
    this.displayedColumns = Object.keys(this.totalItemsColumns);

  }

}
