import { Component, OnInit } from '@angular/core';
import { LoanService } from '../loan.service';
import { MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-lender-list',
  templateUrl: './lender-list.component.html',
  styleUrls: ['./lender-list.component.css']
})
export class LenderListComponent implements OnInit {

  constructor(private loanService: LoanService) { }
  lendersDatasource: MatTableDataSource<any>;
  displayedColumns = ["name", "idNumber", "mobilePhoneNumber", "action"];
  ngOnInit() {
    this.loanService.getLenders().subscribe(data => {
      this.lendersDatasource = new MatTableDataSource(data);
    });
  }
  applyFilter(filter) {
    this.lendersDatasource.filter = filter;
  }
}
