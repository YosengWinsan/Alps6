import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { PurchaseService } from '../purchase.service';

@Component({
  selector: 'app-supplier-class-list',
  templateUrl: './supplier-class-list.component.html',
  styleUrls: ['./supplier-class-list.component.css']
})
export class SupplierClassListComponent implements OnInit {

  @ViewChild(MatSort) sort;
  constructor(private purchaseService: PurchaseService) { }
  supplierClassDataSource: MatTableDataSource<any>;
  displayedColumns = ["name", "action"];
  ngOnInit() {
    this.purchaseService.getSupplierClasses().subscribe((res) => {
      this.supplierClassDataSource = new MatTableDataSource(res);
      this.supplierClassDataSource.sort = this.sort;
    });
  }
}
