import { Component, OnInit, ViewChild } from '@angular/core';
import { Subject } from 'rxjs';
import { LogisticsService } from '../logistics.service';
import { MatTable } from '@angular/material/table';

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
  selectedCar:any;
  vouchersColumns=["source","destination","aQuantity","quantity","amount","operator","action"];
  totalAmount;
  totalAQuantity;
  totalQuantity;
  ngOnInit() {    
    this.currentCar.subscribe(p => {
      this.selectedCar=p;
      this.logisticsService.getDispatchRecord(p).subscribe(k => { 
        this.carDetail = k;
      this.totalAmount=this.carDetail.vouchers.map(p=>p.amount).reduce((acc,value)=>acc+value,0);
      });
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
  setWeight(type)
  {
    //todo:需要换成从服务器生成数据
    if(type==0)
    {
      this.carDetail.tareWeight=Math.round(Math.random()*3000)/100;
      this.carDetail.tareWeightTime=new Date();
      this.carDetail.tareWeightOperator="测试"
    }
    else if(type==1)
    {
      this.carDetail.grossWeight=Math.round(Math.random()*3000)/100;
      this.carDetail.grossWeightTime=new Date();
      this.carDetail.grossWeightOperator="测试"
    }
    //else

    return Math.round(Math.random()*3000)/100;
  }

}
