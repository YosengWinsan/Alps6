import { Component, OnInit, ViewChild } from '@angular/core';
import { SaleService } from '../sale.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css']
})
export class CustomerListComponent implements OnInit {
  @ViewChild(MatSort) sort;
  constructor(private saleService: SaleService) { }
  customerDataSource: MatTableDataSource<any>;
  displayedColumns = ["name", "contact", "address", "action"];
  ngOnInit() {
    this.saleService.getCustomers().subscribe((res) => {
      this.customerDataSource = new MatTableDataSource(res);
      this.customerDataSource.sort = this.sort;
    });
  }
  applyFilter(filter) {
    this.customerDataSource.filter = filter;
  }
}