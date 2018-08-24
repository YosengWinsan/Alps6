import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../auth.service';
import { Subscription } from 'rxjs';
import { QueryService } from '../../infrastructure/infrastructure.module';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private activatedRoute: ActivatedRoute, private router: Router, private formBuilder: FormBuilder, private authService: AuthService, private queryService: QueryService) {
    this.loginForm = formBuilder.group({ username: [,Validators.required], password: [,Validators.required] });
  }
  url = "";
  loginForm: FormGroup;
  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(params => {
      this.url = params['url'] ? params['url'] : "";
    });
  }
  keyup(e:KeyboardEvent)
  {
    if(e.keyCode==13)
    this.login();
  }
  loginSubscription: Subscription = null;
  errorMsg = "";
  login() {
    this.errorMsg = "";
    if (this.loginForm.valid) {
      this.loginForm.markAsUntouched();
      if (this.loginSubscription)
        this.loginSubscription.unsubscribe();
      this.loginSubscription = this.authService.login(this.loginForm.controls.username.value, this.loginForm.controls.password.value).subscribe
        (data => {
          if (data) {
            this.router.navigateByUrl(this.url);
          }
          else
            this.errorMsg = "用户名或密码错误";
        });

    }
  }
  clearWarn(){this.errorMsg="";}
  initDatabase() {
    if (confirm("确定要初始化？会爆哦！")) {
      this.queryService.clearCache();
      this.queryService.initDatabase().subscribe((d) => {if(d) alert("初始化成功"); });
    }
  }
  clearCache(){
    this.queryService.clearCache();
  }
}
