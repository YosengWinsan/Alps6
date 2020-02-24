import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SaleService } from '../sale.service';
import { QueryService} from '../../infrastructure/infrastructure.module';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-commodity-edit',
  templateUrl: './commodity-edit.component.html',
  styleUrls: ['./commodity-edit.component.css']
})
export class CommodityEditComponent implements OnInit {
  commodityForm: FormGroup;
  productSkuOptions;
  constructor(private activatedRoute: ActivatedRoute, private router: Router, private saleService: SaleService, private queryService: QueryService, private formBuilder: FormBuilder) {
    this.commodityForm = formBuilder.group({ id: [], name: [], description: [], quantity: [],auxiliaryQuantity:[], listPrice: [],productSkuID:[] });
  }

  ngOnInit() {
    this.queryService.getProductSkuOptions().subscribe(res=>{
      this.productSkuOptions=res;
    });
    
    this.activatedRoute.queryParams.subscribe(param => {
      var id = param["id"] ? param["id"] : "";
      if (id != "") {
        this.saleService.getCommodity(id).subscribe((res) => {
            this.commodityForm.patchValue(res);
        });
      }
    });
  }
save(){
  if(this.commodityForm.valid)
  {
    this.saleService.saveCommodity(this.commodityForm.value).subscribe(
      (res) => {
          this.router.navigate(["./commoditylist"], { relativeTo: this.activatedRoute.parent});
      }
    );
  }
}
}
