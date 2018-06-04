import { Component } from '@angular/core';
import { DashboardService } from "./dashboard.service";
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
  constructor(private dasboardService: DashboardService) {
    
  }
  initDatabase() {
    console.info(this.dasboardService);
    this.dasboardService.initDatabase().subscribe(data => {
      if (data)
        alert("初始化成功");
    });
  }
}
