import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { StockService } from '../stock.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { QueryService } from 'src/app/infrastructure/infrastructure.module';

@Component({
  selector: 'app-position-edit',
  templateUrl: './position-edit.component.html',
  styleUrls: ['./position-edit.component.css']
})
export class PositionEditComponent implements OnInit {

  constructor(private activatedRoute: ActivatedRoute, private router: Router, private stockService: StockService, private formBuilder: FormBuilder
    , private queryService: QueryService) {
    this.positionForm = formBuilder.group({ name: [, Validators.required], id: [], number: [],warehouse:[] });

  }
  positionForm: FormGroup;
  ngOnInit() {
    this.activatedRoute.queryParams.subscribe((params) => {
      var id = params["id"] ? params["id"] : null;
      if (id) {
        this.stockService.getPosition(id).subscribe((data: any) => {
          this.positionForm.patchValue(data);
        });
      }
    });
  }
  save() {
    var self = this;
    if (this.positionForm.valid) {
      this.stockService.createAndUpdate(this.positionForm.value).subscribe(res => {
        history.back();
      });
    }
  }
}
