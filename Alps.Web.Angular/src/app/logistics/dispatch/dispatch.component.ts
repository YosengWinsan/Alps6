import { Component, OnInit, ViewChild } from '@angular/core';
import { Subject } from 'rxjs';
import { LogisticsService } from '../logistics.service';
import { MatTable } from '@angular/material';

@Component({
  selector: 'app-dispatch',
  templateUrl: './dispatch.component.html',
  styleUrls: ['./dispatch.component.css']
})
export class DispatchComponent implements OnInit {

  constructor(private logisticsService: LogisticsService) { }
  carList;
  @ViewChild("weightListTable")
  private weightListTable:MatTable<any>;
  private currentCar = new Subject<string>();
  carDetail : any={};
  weightListColumns=["grossWeight","tareWeight","netWeight"];
  ngOnInit() {    
    this.currentCar.subscribe(p => {
      this.logisticsService.getDispatchRecord(p).subscribe(k => { this.carDetail = k; });
    });
    
    this.logisticsService.getCars().subscribe(p => {
      this.carList = p;
      if (this.carList && this.carList.length > 0)
        this.currentCar.next(this.carList[0].id);
    });
  }
  chooseCar(carID) {
    this.currentCar.next(carID);
  }
  addWl(){
    this.carDetail.weightLists.push({grossWeight:null,tareWeight:null});
    
    this.weightListTable.renderRows();
  }
  getWeight()
  {

    return Math.round(Math.random()*3000)/100;
  }

}
