import { Component } from '@angular/core';
import {QueryService  } from "../infrastructure/infrastructure.module";
@Component({
  selector: 'dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  cards = [
    { title: 'Card 1', cols: 2, rows: 1 },
    { title: 'Card 2', cols: 1, rows: 1 },
    { title: 'Card 3', cols: 1, rows: 2 },
    { title: 'Card 4', cols: 1, rows: 1 }
  ];
  
name="b3d1411b-e0d4-cada-4c9c-08d5cbbc6faf";
options;
test()
{
  this.name="test";
}
  constructor(private dasboardService: QueryService) {
    this.dasboardService.query("ProductSkuOptions").subscribe((res)=>
    {
      this.options=res;
    });
  }
  initDatabase() {
    console.info(this.dasboardService);
    this.dasboardService.initDatabase().subscribe(data => {
      if (data)
        alert("初始化成功");
    });
  }
}
