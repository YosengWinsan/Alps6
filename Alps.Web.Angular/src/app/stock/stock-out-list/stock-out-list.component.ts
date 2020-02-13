import { Component, OnInit, ViewChild } from '@angular/core';
import { StockService } from '../stock.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-stock-out-list',
  templateUrl: './stock-out-list.component.html',
  styleUrls: ['./stock-out-list.component.css']
})
export class StockOutListComponent implements OnInit {

 
  constructor(private stockService: StockService ) { }

  @ViewChild(MatSort) matSort:MatSort;
  stockOutList: MatTableDataSource<StockOutVoucherListDto>;
  displayedColumns=['createTime','destination','department','totalQuantity','totalAmount','status','action'];
  ngOnInit() {
    this.stockService.getStockOutVouchers().subscribe((res: StockOutVoucherListDto[]) => {
      this.stockOutList = new MatTableDataSource<StockOutVoucherListDto>(res);
      this.stockOutList.sort=this.matSort;
    });
  }

}

interface StockOutVoucherListDto {
  id: string;
  createTime: Date;
  confirmTime: Date;
  destination: string;
  department: string;
  totalQuantity: number;
  totalAmount: number;
  totalAuxiliaryQuantity: number;
  status: string;
}
