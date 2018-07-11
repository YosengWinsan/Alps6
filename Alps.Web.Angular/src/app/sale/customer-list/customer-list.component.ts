import { Component, OnInit, ViewChild } from '@angular/core';
import { SaleService } from '../sale.service';
import { MatTableDataSource, MatSort } from '@angular/material';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css']
})
export class CustomerListComponent implements OnInit {
@ViewChild(MatSort) sort;
  constructor(private saleService: SaleService) { }
  customerDataSource :MatTableDataSource<any>;
  displayedColumns=["name","contact","address","action"];
  ngOnInit() {
    this.saleService.getCustomers().subscribe((res) => {
        this.customerDataSource =new MatTableDataSource( res);
        this.customerDataSource.sort=this.sort;
    });
  }
  applyFilter(filter)
  {
    this.customerDataSource.filter=filter;
  }
}