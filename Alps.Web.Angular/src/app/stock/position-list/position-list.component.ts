import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { StockService } from '../stock.service';

@Component({
  selector: 'app-position-list',
  templateUrl: './position-list.component.html',
  styleUrls: ['./position-list.component.css']
})
export class PositionListComponent implements OnInit {


  constructor(private stockService: StockService) { }
  @ViewChild(MatSort) matSort: MatSort;
  positionList: MatTableDataSource<any>;
  displayedColumns = ['number', 'name', 'warehouse', 'action'];

  ngOnInit() {
    this.stockService.getPositions().subscribe((res) => {
      this.positionList = new MatTableDataSource(res);
      this.positionList.sort = this.matSort;
    });
  }

}
