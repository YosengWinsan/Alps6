import { Component, OnInit, Input, ViewChild, HostListener, ElementRef } from '@angular/core';
import { MatTable } from '@angular/material/table';
import { MatExpansionPanel } from '@angular/material/expansion';
import { ShopcartService } from '../service/shopcart.service';
import { IShopcartItem } from '../infrastructure.module';

@Component({
  selector: 'alps-shopcart',
  templateUrl: './alps-shopcart.component.html',
  styleUrls: ['./alps-shopcart.component.css']
})
export class AlpsShopcartComponent implements OnInit {
  shopcartColumns = {
    commodityName: "品名", auxiliaryQuantity: "辅助数量", quantity: "数量", price: "定价", amount: "金额"
  };
  displayedColumns = Object.keys(this.shopcartColumns).concat("action");

  _shopcart: IShopcartItem[];
  _totalAuxiliaryQuantity = 0;
  _totalQuantity = 0;
  _totalAmount = 0;
  _cartItemCount = 0;
  @ViewChild("shopcartPanel")
  shopcartPanel: MatExpansionPanel;
  @ViewChild("shopcartTable")
  shopcartTable: MatTable<any>;
  @Input()
  set saleOrderID(id) {
    this._saleOrderID = id;
  }
  _saleOrderID;
  constructor(private shopcartService: ShopcartService, private el: ElementRef) {
    this.shopcartService.shopcart.subscribe((data: IShopcartItem[]) => {
      this._shopcart = data;
      this._totalAmount = 0;
      this._totalAuxiliaryQuantity = 0;
      this._totalQuantity = 0;
      this._shopcart.forEach((item: IShopcartItem) => {
        this._totalAmount += item.amount;
        this._totalAuxiliaryQuantity += item.auxiliaryQuantity;
        this._totalQuantity += item.quantity;
      });
      if (this._cartItemCount != this._shopcart.length) {
        this._cartItemCount = this._shopcart.length;
        if (this.shopcartTable)
          this.shopcartTable.renderRows();
      }

    });
  }

  ngOnInit() {
    //this._shopcart.push({commodityName:"FFF"});
  }
  @HostListener('mouseleave', ['$event'])
  onFocusOut(e) {
    setTimeout(() => {
      this.shopcartPanel.close();
    }, 100);

  }

  @HostListener('mouseenter', ['$event'])
  onMouseEnter(e) {
    setTimeout(() => {
      this.shopcartPanel.open();
    }, 100);
  }
  fixed = false;
  @HostListener("window:scroll", [])
  onWindowScroll() {
    if (window.scrollY > this.el.nativeElement.offsetTop) {
      if (!this.fixed) { this.fixed = true; }
    }
    else
      if (this.fixed) { this.fixed = false; }
  }
  remove(item) {
    this.shopcartService.remove(item);
  }
  clear() {
    this.shopcartService.clear();
  }
}

