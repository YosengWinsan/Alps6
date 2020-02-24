import { Component, OnInit } from '@angular/core';
import { StockService } from '../stock.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-stock-in-detail',
  templateUrl: './stock-in-detail.component.html',
  styleUrls: ['./stock-in-detail.component.css']
})
export class StockInDetailComponent implements OnInit {

  stockInVoucher: any = {};
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
    this.stockService.getStockInDetail(id).subscribe((res) => {
      this.stockInVoucher = res;
      this.editable = (res.statusValue > 0);

    });
  }
  edit(id) {
    this.router.navigate(['../stockin'], { relativeTo: this.activatedRoute, queryParams: { id: id } });
  }
  delete(id) {
    if (confirm("确定要删除？"))
      this.stockService.deleteStockIn(id).subscribe((res) => {
        this.router.navigate(["../stockinlist"], { relativeTo: this.activatedRoute });

      });
  }
  submit(id) {
    if (confirm("确定要提交？")) {
      this.stockService.submitStockIn(id).subscribe((res) => {
        this.loadData(id);
      });
    }
  }
  back() {
    history.back();
  }
}
