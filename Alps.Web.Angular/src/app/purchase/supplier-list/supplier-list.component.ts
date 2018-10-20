import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, MatTableDataSource } from '@angular/material';
import { PurchaseService } from '../purchase.service';

@Component({
  selector: 'app-supplier-list',
  templateUrl: './supplier-list.component.html',
  styleUrls: ['./supplier-list.component.css']
})
export class SupplierListComponent implements OnInit {

  @ViewChild(MatSort) sort;
  constructor(private purchaseService: PurchaseService) { }
  supplierDataSource :MatTableDataSource<any>;
  displayedColumns=["name","contact","address","action"];
  ngOnInit() {
    this.purchaseService.getSuppliers().subscribe((res) => {
        this.supplierDataSource =new MatTableDataSource( res);
        this.supplierDataSource.sort=this.sort;
    });
  }
  applyFilter(filter)
  {
    this.supplierDataSource.filter=filter;
  }
}
