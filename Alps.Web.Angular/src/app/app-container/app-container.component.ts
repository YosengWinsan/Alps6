import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';
import { map } from 'rxjs/operators';
import { AuthService } from '../auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-app-container',
  templateUrl: './app-container.component.html',
  styleUrls: ['./app-container.component.css']
})
export class AppContainerComponent {
  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches)
    );
  menuData: MenuItem[];
  loginStatus = false;
  constructor(private breakpointObserver: BreakpointObserver, private authService: AuthService, private router: Router) {
    this.menuData = MENU_DATA;
    this.authService.loginStatus.subscribe(status => {
      this.loginStatus = status;
      if (!this.loginStatus)
        this.router.navigateByUrl('login');
    });
  }
  logout() {
    this.authService.logout();
  }
}

interface MenuItem {
  name: string;
  path: string;
  children?: MenuItem[];
}
const MENU_DATA: MenuItem[] = [
  { path: "dashboard", name: "实时动态" },
  { path: "product", name: "产品管理" },
  { path: "stock", name: "仓库管理" },
  { path: "sale", name: "销售管理" },
  { path: "logistics", name: "物流管理" },
  { path: "loan", name: "财务管理" },
  { path: "user", name: "用户管理" }
]