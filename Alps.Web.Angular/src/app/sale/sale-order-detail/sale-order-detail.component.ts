import { Component, OnInit } from '@angular/core';
import { SaleService } from '../sale.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-sale-order-detail',
  templateUrl: './sale-order-detail.component.html',
  styleUrls: ['./sale-order-detail.component.css']
})
export class SaleOrderDetailComponent implements OnInit {

  saleOrder:any = {};
  editable:boolean=false;
  constructor(private saleService: SaleService, private activatedRoute: ActivatedRoute, private router: Router) { }
  saleOrderItemColumns = { commodity: "品名",auxiliaryQuantity: "辅助数量", quantity: "数量",  price: "单价",amount:"金额" ,remark:"备注"};
  
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
    this.saleService.getSaleOrderDetail(id).subscribe((res) => {
        this.saleOrder = res;
        this.editable=(res.statusValue>0);

    });
  }
  edit(id) {
    this.router.navigate(['../saleorderedit'],{relativeTo:this.activatedRoute,queryParams:{id:id}});
  }
  delete(id) {
    if (confirm("确定要删除？"))
      this.saleService.deleteSaleOrder(id).subscribe((res) => {
          this.router.navigate(["../saleorderlist"],{relativeTo:this.activatedRoute});
        
      });
  }
  submit(id) {
    if (confirm("确定要提交？")) {
      this.saleService.submitSaleOrder(id).subscribe((res) => {
          this.loadData(id);        
      });
    }
  }
  back() {
    history.back();
  }
}
