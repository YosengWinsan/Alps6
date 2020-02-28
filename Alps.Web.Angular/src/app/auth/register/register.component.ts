import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  constructor(formBuilder: FormBuilder, private authService: AuthService, private router: Router) {
    this.registerForm = formBuilder.group({
      username: [, Validators.required], realname: [, Validators.required],
      pass: formBuilder.group({
        password: [, Validators.required], confirmPassword: [, Validators.required]
      }, { validator: RegisterComponent.MatchPassword }), identityNumber: [, Validators.required], mobilePhoneNumber: [, Validators.required]
    })
  }
  static MatchPassword(control: AbstractControl) {
    let password = control.get('password').value;
    let confirmPassword = control.get('confirmPassword').value;

    if (password != confirmPassword) {
      return { ConfirmPassword: true };
    } else {
      return null
    }
  }
  ngOnInit() {
  }
  register() {
    this.authService.register(this.registerForm.controls.username.value, this.registerForm.controls.pass.value.password, this.registerForm.controls.realname.value,
      this.registerForm.controls.identityNumber.value, this.registerForm.controls.mobilePhoneNumber.value).subscribe((rst) => {
        if (rst) {
          alert("注册成功，请前往登录");
          this.router.navigate(["/login"]);
        }
      })
  }
}
