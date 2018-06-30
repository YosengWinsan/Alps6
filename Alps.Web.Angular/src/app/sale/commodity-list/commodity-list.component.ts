import { Component, OnInit } from '@angular/core';
import { SaleService } from '../sale.service';
import { AlpsLoadingBarService } from '../../infrastructure/service/alps-loading-bar.service';

@Component({
  selector: 'app-commodity-list',
  templateUrl: './commodity-list.component.html',
  styleUrls: ['./commodity-list.component.css']
})
export class CommodityListComponent implements OnInit {

  constructor(private saleService: SaleService,private loadingBarService :AlpsLoadingBarService) { }
  commodities = [];
  displayedColumns=["name","description","quantity","auxiliaryQuantity","listPrice","action"];
  ngOnInit() {
    this.saleService.getCommodities().subscribe((res) => {
        this.commodities = res;
    });
  }
}
