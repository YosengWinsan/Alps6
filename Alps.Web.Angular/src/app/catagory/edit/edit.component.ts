import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { CatagoryService } from "../catagory.service";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { QueryService } from "../../infrastructure/infrastructure.module";
@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  constructor(private activatedRoute: ActivatedRoute, private router: Router, private catagoryService: CatagoryService, private formBuilder: FormBuilder
    , private queryService: QueryService) {
    this.catagoryForm = formBuilder.group({ name: [, Validators.required], id: [], parentID: [] });

  }
  parentCatagory;
  catagoryForm: FormGroup;
  ngOnInit() {
    this.activatedRoute.queryParams.subscribe((params) => {
      var id = params["id"] ? params["id"] : null;
      if (id) {
        this.catagoryService.get(id).subscribe((data: any) => {
          this.catagoryForm.patchValue(data);
          this.setParentCatagory(this.catagoryForm.value.parentID);
        });
      }
      else {
        var parentID = params["parentID"] ? params["parentID"] : null;
        this.setParentCatagory(parentID);
      }

    });
  }
  setParentCatagory(parentID) {
    if (parentID) {
      this.catagoryForm.patchValue({ parentID: parentID });
      this.queryService.getCatagoryOption(parentID).subscribe((res) => {
        this.parentCatagory = res.displayValue;
      });
    }
    else
    this.parentCatagory="顶层分类";
  }
  save() {
    var self = this;
    if (this.catagoryForm.valid) {
      this.catagoryService.createAndUpdate(this.catagoryForm.value).subscribe(res => {
        self.router.navigate(["./"], { relativeTo: self.activatedRoute.parent, queryParams: { id: self.activatedRoute.snapshot.queryParams["parentID"] } });
      });
    }
  }
  back() {

    history.back();
  }
}

