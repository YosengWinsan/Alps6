import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'alps-info-chip',
  templateUrl: './alps-info-chip.component.html',
  styleUrls: ['./alps-info-chip.component.css']
})
export class AlpsInfoChipComponent implements OnInit {

  @Input("label")
  label;
  @Input("value")
  value;
  constructor() { 
  }

  ngOnInit() {
  }

}
