import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-logistics',
  templateUrl: './logistics.component.html',
  styleUrls: ['./logistics.component.css']
})
export class LogisticsComponent implements OnInit {

  constructor() { }
links={"调度中心":"dispatch","物流信息":"logisticsinfo","配送明细":"distribution","车辆管理":"vehicle"}
  ngOnInit() {
  }

}
