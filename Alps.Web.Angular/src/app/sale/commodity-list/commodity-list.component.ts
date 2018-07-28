import { Component, OnInit } from '@angular/core';
import { SaleService } from '../sale.service';
import { AlpsLoadingBarService } from '../../infrastructure/service/alps-loading-bar.service';
import { QueryService } from '../../infrastructure/infrastructure.module';
import { ShopcartService } from '../../infrastructure/service/shopcart.service';
import { ActivatedRoute } from '../../../../node_modules/@angular/router';

@Component({
  selector: 'app-commodity-list',
  templateUrl: './commodity-list.component.html',
  styleUrls: ['./commodity-list.component.css']
})
export class CommodityListComponent implements OnInit {

  constructor(private saleService: SaleService, private queryService: QueryService, private shopcartService: ShopcartService,private activatedRouter:ActivatedRoute) { }

  //displayedColumns=["name","description","preSellQuantity","preSellAuxiliaryQuantity","listPrice","action"];

  shopcart: { productSkuID, commodityName, auxiliaryQuantity, quantity, quantityRate, price }[] = [];
  commodities ;
  catagories ;
  saleOrderID;
  saleOrderItems;
  ngOnInit() {
    this.catagories=this.queryService.getCatagoryOptions();
    this.activatedRouter.queryParams.subscribe(param=>{
      this.saleOrderID=param["saleOrderID"]?param["saleOrderID"]:"";
      this.saleService.getSaleOrder(this.saleOrderID).subscribe(data=>{

      });
    });
  }
  onCatagoryChanged(catagoryID) {
    this.commodities=this.saleService.getCommoditiesByCatagoryID(catagoryID);
  }
  addToCart(item) {
    this.shopcartService.add(item);
  }
}
