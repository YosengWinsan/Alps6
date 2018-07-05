import { Component } from '@angular/core';
import {QueryService  } from "../infrastructure/infrastructure.module";
@Component({
  selector: 'dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  cards = [
    { title: '产品动态', cols: 1, rows: 1 },
 { title: '仓库动态', cols: 1, rows: 1 },
     { title: '订单动态', cols: 1, rows: 1 },
     { title: '资金动态', cols: 1, rows: 1 }
  ];

  constructor(private dasboardService: QueryService) {
    this.cw=document.body.clientWidth;
    this.ch=document.body.clientHeight;
  }
  cw;
  ch;
  initDatabase() {
this.dasboardService.clearCache();
    this.dasboardService.initDatabase().subscribe(data => {
      if (data)
        alert("初始化成功");
    });
  }
}
