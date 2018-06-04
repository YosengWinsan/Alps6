import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort } from '@angular/material';
import { TtDataSource } from './tt-datasource';

@Component({
  selector: 'catagory/tt',
  templateUrl: './tt.component.html',
  styleUrls: ['./tt.component.css']
})
export class TtComponent implements OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSource: TtDataSource;

  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['id', 'name'];

  ngOnInit() {
    this.dataSource = new TtDataSource(this.paginator, this.sort);
  }
}
