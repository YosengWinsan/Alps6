import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'alps-commodity-selector',
  templateUrl: './commodity-selector.component.html',
  styleUrls: ['./commodity-selector.component.css']
})
export class CommoditySelectorComponent implements OnInit {
  _commodityColumns = {
    commodityName: "品名",
    sellableAuxiliaryQuantity: "可售辅数", sellableQuantity: "可售数量", quantityRate: "数比", listPrice: "定价"
  };

  _displayedColumns = Object.keys(this._commodityColumns).concat("action");
  _commodities;
  @Input()
  set commodities(value) {
    this._commodities = value;
  }
  @Output()
  onSelected = new EventEmitter<any>();
  constructor() { }

  ngOnInit() {
  }
  addToCart(commodity) {
    this.onSelected.emit(commodity);
  }
}
