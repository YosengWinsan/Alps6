import { Component, OnInit, ViewChild } from '@angular/core';
import { StockService } from '../stock.service';
import { MatSort, MatTableDataSource } from '@angular/material';
import { QueryService } from '../../infrastructure/infrastructure.module';

@Component({
  selector: 'app-stock-info',
  templateUrl: './stock-info.component.html',
  styleUrls: ['./stock-info.component.css']
})
export class StockInfoComponent implements OnInit {

  
  constructor(private stockService: StockService,private queryService:QueryService ) { }

  @ViewChild(MatSort) matSort:MatSort;
  stockOutList: MatTableDataSource<StockListDto>;
  displayedColumns=['name','auxiliaryQuantity','quantity','warehouse','owner','serialNumber'];
  catagoryOptions;
  selectedCatagory;
  ngOnInit() {
    this.queryService.getCatagoryOptions().subscribe(res => {
      this.catagoryOptions = res;
    });
  }
  onCatagoryChange(value)
  {
    if (value && value !== "")
    this.stockService.getStocksByCatagory(value).subscribe((res: StockListDto[]) => {
      this.stockOutList = new MatTableDataSource<StockListDto>(res);
      this.stockOutList.sort=this.matSort;
    });
  }
}

interface StockListDto {
  id: string;
  name: string;
  quantity:number;
  auxiliaryQuantity:number;
  warehouse:string;
  owner:string;
  serialNumber:string;
}
