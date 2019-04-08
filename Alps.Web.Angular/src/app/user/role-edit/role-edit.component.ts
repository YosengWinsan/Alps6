import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { UserService } from '../user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AlpsActionResponse } from 'src/app/infrastructure/infrastructure.module';

@Component({
  selector: 'app-role-edit',
  templateUrl: './role-edit.component.html',
  styleUrls: ['./role-edit.component.css']
})
export class RoleEditComponent implements OnInit {
  roleForm: FormGroup;
  constructor(private formBuilder: FormBuilder, private userService: UserService, private activatedRoute: ActivatedRoute, private router: Router) {
    this.roleForm = formBuilder.group({ name: [], id: [], timestamp: [], description: [] });
  }

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(params => {
      let id = params["id"] ? params["id"] : "";
      if (id != "") {
        this.userService.getRole(id).subscribe(data => {
          this.roleForm.patchValue(data);
        });
      }
    })
  }

  save() {
    this.userService.saveRole(this.roleForm.value).subscribe((d: AlpsActionResponse) => {
      if (d.resultCode == 1)
        this.router.navigate(["../rolelist"],{relativeTo:this.activatedRoute});
      //this.router.navigate(["./productdetail"], { relativeTo: this.activatedRoute.parent,queryParams:{id:this.productForm.controls["id"].value}});
    });
  }
}
