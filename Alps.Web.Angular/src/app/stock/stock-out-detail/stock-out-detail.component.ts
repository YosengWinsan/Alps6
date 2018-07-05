import { Component, OnInit } from '@angular/core';
import { StockService } from '../stock.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-stock-out-detail',
  templateUrl: './stock-out-detail.component.html',
  styleUrls: ['./stock-out-detail.component.css']
})
export class StockOutDetailComponent implements OnInit {

  stockOutVoucher: any = {};
  editable: boolean = false;
  constructor(private stockService: StockService, private activatedRoute: ActivatedRoute, private router: Router) { }
  displayedColumns = ["productSku", "auxiliaryQuantity", "quantity", "price", "position", "serialNumber"];
  ngOnInit() {
    this.activatedRoute.queryParams.subscribe((params) => {
      var id = params["id"];
      this.loadData(id);

    });
  }
  loadData(id) {
    if (!id) {
      id = "";
    }
    this.stockService.getStockOutDetail(id).subscribe((res) => {
      this.stockOutVoucher = res;
      this.editable = (res.statusValue > 0);

    });
  }
  edit(id) {
    this.router.navigate(['../stockout'], { relativeTo: this.activatedRoute, queryParams: { id: id } });
  }
  delete(id) {
    if (confirm("确定要删除？"))
      this.stockService.deleteStockOut(id).subscribe((res) => {
        this.router.navigate(["../stockoutlist"], { relativeTo: this.activatedRoute });

      });
  }
  submit(id) {
    if (confirm("确定要提交？")) {
      this.stockService.submitStockOut(id).subscribe((res) => {
        this.loadData(id);
      });
    }
  }
  back() {
    history.back();
  }
}
