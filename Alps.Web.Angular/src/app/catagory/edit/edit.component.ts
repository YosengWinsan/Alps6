import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { CatagoryService } from "../catagory.service";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  constructor(private activatedRoute: ActivatedRoute, private catagoryService: CatagoryService, private formBuilder: FormBuilder) {
    this.catagoryForm = formBuilder.group({ name: [, Validators.required], fullName: [, Validators.required] });

  }
  catagoryForm: FormGroup;
  ngOnInit() {

    this.activatedRoute.params.subscribe((params) => {
      var id = params["id"];
      if (!id) {
        id = "";
      }
      this.catagoryService.get(id).subscribe((data: any) => {
        this.catagoryForm.patchValue(data);
      }
      );
    });
  }
  save() {
    if (this.catagoryForm.valid) {
      console.info(this.catagoryForm.value);
    }
  }

}
