import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { CatagoryService } from "../catagory.service";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { AlpsActionResponse, AlpsActionResultCode,QueryService } from "../../infrastructure/infrastructure.module";
@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  constructor(private activatedRoute: ActivatedRoute, private router: Router, private catagoryService: CatagoryService, private formBuilder: FormBuilder
    ,private queryService:QueryService    ) {
    this.catagoryForm = formBuilder.group({ name: [, Validators.required], id: [], parentID: [] });

  }
  catagoryOptions;
  catagoryForm: FormGroup;
  ngOnInit() {
    this.activatedRoute.queryParams.subscribe((params) => {
      var id = params["id"];
      if (!id) {
        id = "";
      }
      this.catagoryService.get(id).subscribe((data: any) => {
        this.catagoryForm.patchValue(data);
        this.queryService.GetCatagoryOptions().subscribe((res)=>{
          this.catagoryOptions=res;
        });
      }  );
    });
  }
  save() {
    var self = this;
    if (this.catagoryForm.valid) {
      this.catagoryService.createAndUpdate(this.catagoryForm.value).subscribe(
        (res: AlpsActionResponse) => {
          if (res.resultCode == AlpsActionResultCode.Ok)
            self.router.navigate(["./"], { relativeTo: self.activatedRoute.parent, queryParams: { id: self.activatedRoute.snapshot.queryParams["listID"] } });
        }
      );
    }
  }
  back() {

    history.back();
  }
}

