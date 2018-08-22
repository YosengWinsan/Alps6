import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthService } from '../auth.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private activatedRoute: ActivatedRoute, private router: Router, private formBuilder: FormBuilder, private authService: AuthService) {
    this.loginForm = formBuilder.group({ username: [], password: [], confirmPassword: [] });
  }
  url = "";
  loginForm: FormGroup;
  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(params => {
      this.url = params['url'] ? params['url'] : "";
    })
  }
  loginSubscription: Subscription = null;
  errorMsg="";
  login() {
    this.errorMsg="";
    if (this.loginForm.valid) {
      if (this.loginSubscription)
        this.loginSubscription.unsubscribe();
      this.loginSubscription = this.authService.login(this.loginForm.controls.username.value, this.loginForm.controls.password.value).subscribe
        (data => {
          if (data) {
            this.router.navigateByUrl(this.url);
          }
          else
          this.errorMsg="用户名或密码错误";
        });

    }
  }
}
