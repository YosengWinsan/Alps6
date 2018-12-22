import { Component, OnInit } from '@angular/core';
import { QueryService } from "../infrastructure/infrastructure.module";
@Component({
  selector: 'dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  cards = [
    // { title: '产品动态', cols: 1, rows: 1, content: "<div>类别数：{{catagoryCount}}<div>" },
    // { title: '仓库动态', cols: 1, rows: 1 },
    // { title: '订单动态', cols: 1, rows: 1 },
    // { title: '资金动态', cols: 1, rows: 1 }
  ];

  constructor(private queryService: QueryService) {
    this.cw = document.body.clientWidth;
    this.ch = document.body.clientHeight;
  }
  cw;
  ch;
  info: any = {};
  ngOnInit() {
    this.queryService.getDashboardInfo().subscribe(data => {
      this.info = data;
    });
  }
  initDatabase() {
    if (confirm("确定要初始化？会爆哦！")) {
      this.clearCache();
      this.queryService.initDatabase().subscribe(data => {
        if (data)
          alert("初始化成功");
      });
    }
  }
  getPath()
  {
    this.queryService.getPath().subscribe();
  }


  clearCache() {
    this.queryService.clearCache();
  }
  t(card) {
    card.collapsed = !card.collapsed;
    console.info(card);
  }
  removeCard(card) {

    //this.cards.splice(this.cards.indexOf(card),1);
  }
}
