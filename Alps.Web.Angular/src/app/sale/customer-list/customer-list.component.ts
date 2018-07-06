import { Component, OnInit } from '@angular/core';
import { SaleService } from '../sale.service';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css']
})
export class CustomerListComponent implements OnInit {

  constructor(private saleService: SaleService) { }
  customers = [];
  displayedColumns=["name","contact","address","action"];
  ngOnInit() {
    this.saleService.getCustomers().subscribe((res) => {
        this.customers = res;
    });
  }
}