import { Component, OnInit } from '@angular/core';
import { LoanService } from '../loan.service';

@Component({
  selector: 'app-lender-import',
  templateUrl: './lender-import.component.html',
  styleUrls: ['./lender-import.component.scss']
})
export class LenderImportComponent implements OnInit {

  constructor(private loanService: LoanService) { }
  list = "";
  ngOnInit(): void {
  }
  import() {
    this.loanService.importLender(this.list).subscribe((rst) => {
      if (rst)
        alert("导入成功");
    });
  }
}
