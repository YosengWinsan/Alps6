import { Component, OnInit, ViewChild } from '@angular/core';
import { LoanService } from '../loan.service';
import { MatTableDataSource, MatPaginator } from '@angular/material';

@Component({
  selector: 'app-lender-list',
  templateUrl: './lender-list.component.html',
  styleUrls: ['./lender-list.component.css']
})
export class LenderListComponent implements OnInit {

  constructor(private loanService: LoanService) { }
  lendersDatasource: MatTableDataSource<any>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  displayedColumns = ["name", "idNumber", "mobilePhoneNumber", "action"];
  ngOnInit() {
    this.loanService.getLenders().subscribe(data => {
      this.lendersDatasource = new MatTableDataSource(data);
      this.lendersDatasource.paginator=this.paginator;
    });
  }
  applyFilter(filter) {
    this.lendersDatasource.filter = filter;
  }
}
