import { Component, OnInit, ViewChild } from '@angular/core';
import { StockService } from '../stock.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-stock-in-list',
  templateUrl: './stock-in-list.component.html',
  styleUrls: ['./stock-in-list.component.css']
})
export class StockInListComponent implements OnInit {

  constructor(private stockService: StockService) { }

  @ViewChild(MatSort) matSort: MatSort;
  stockInList: MatTableDataSource<StockInVoucherListDto>;
  displayedColumns = ['createTime', 'supplier', 'department', 'totalQuantity', 'totalAmount', 'status', 'action'];
  ngOnInit() {
    this.loadData();
  }
  loadData() {
    this.stockService.getStockInVouchers().subscribe((res: any) => {
      this.stockInList = new MatTableDataSource<StockInVoucherListDto>(res);
      this.stockInList.sort = this.matSort;
    });

  }


}

interface StockInVoucherListDto {
  id: string;
  createTime: Date;
  confirmTime: Date;
  source: string;
  department: string;
  totalQuantity: number;
  totalAmount: number;
  totalAuxiliaryQuantity: number;
  status: string;
}