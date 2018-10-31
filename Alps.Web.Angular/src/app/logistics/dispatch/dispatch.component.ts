import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dispatch',
  templateUrl: './dispatch.component.html',
  styleUrls: ['./dispatch.component.css']
})
export class DispatchComponent implements OnInit {

  constructor() { }
carList;
  ngOnInit() {
this.carList=[{name:"winsan"},{name:"amei"}];
  }

}
