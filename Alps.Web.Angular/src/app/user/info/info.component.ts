import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/auth/auth.service';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.css']
})

export class InfoComponent implements OnInit {
  userForm: FormGroup;
  constructor(private userService: UserService, private activatedRoute: ActivatedRoute, private authService: AuthService, private formBuilder: FormBuilder) {
    this.userForm = this.formBuilder.group({ "id": [], "idName": [], "name": [], "mobilePhoneNumber": [], "identityNumber": [], "roles": [], "password": [], "confirmpassword": [] });
    //this.user = {};
  }
  ngOnInit() {

    let idname = this.authService.IdName;

    this.userService.getUserByIdName(idname).subscribe(data => {
      this.userForm.patchValue(data);
      //this.user = data;

    });

  }

  back() {
    history.back();
  }
  save() {
    if (this.userForm.controls.password && this.userForm.controls.confirmpassword)
      if (this.userForm.controls.password.value != this.userForm.controls.confirmpassword.value) {
        alert("确认密码不一致");
        return;
      }


    this.userService.saveUser(this.userForm.value).subscribe(rst => {
      this.back();
    });
  }

}
