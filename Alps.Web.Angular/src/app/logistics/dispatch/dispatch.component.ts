import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-dispatch',
  templateUrl: './dispatch.component.html',
  styleUrls: ['./dispatch.component.css']
})
export class DispatchComponent implements OnInit {

  constructor() { }
  carList;
  private currentCar=new Subject<string>();
  carDetail={};
  ngOnInit() {
    this.currentCar.subscribe(p => {
      this.carDetail = {name:p};
    });
    this.carList = [{ name: "winsan",id:"1" }, { name: "amei",id:"2" }];
  }
  chooseCar(carID) {
    this.currentCar.next(carID);

  }

}
