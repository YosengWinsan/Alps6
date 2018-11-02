import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { LogisticsService } from '../logistics.service';

@Component({
  selector: 'app-dispatch',
  templateUrl: './dispatch.component.html',
  styleUrls: ['./dispatch.component.css']
})
export class DispatchComponent implements OnInit {

  constructor(private logisticsService:LogisticsService) { }
  carList;
  private currentCar=new Subject<string>();  
  carDetail={};
  ngOnInit() {
    this.carList=this.logisticsService.getCars();
    this.currentCar.subscribe(p => {
      this.carDetail = {name:p};      
    });
    //this.carList = [{ name: "winsan",id:"1" }, { name: "amei",id:"2" }];
  }
  chooseCar(carID) {
    console.info(carID);
    this.currentCar.next(carID);

  }

}
