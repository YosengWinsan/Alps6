import { Component, OnInit, OnDestroy } from '@angular/core';
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
export class AppContainerComponent implements OnDestroy {
  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches)
    );
  menuData: MenuItem[];
  loginStatus = false;
  loginStatusObserver;
  constructor(private breakpointObserver: BreakpointObserver, private authService: AuthService, private router: Router) {
    this.menuData = MENU_DATA;
    // this.loginStatusObserver = this.authService.loginStatus.subscribe(status => {
    //   this.loginStatus = status;

    //   if (!this.loginStatus)
    //     this.router.navigateByUrl('login');
    // });
  }
  logout() {
    this.authService.logout();
  }
  ngOnDestroy() {
    //this.loginStatusObserver.unsubscribe();
  }
}

interface MenuItem {
  name: string;
  path: string;
  children?: MenuItem[];
}
const MENU_DATA: MenuItem[] = [
  { path: "dashboard", name: "实时动态" },
  { path: "crm", name: "客户关系" },
  { path: "product", name: "产品管理" },
  { path: "stock", name: "仓库管理" },
  { path: "purchase", name: "采购管理" },
  { path: "sale", name: "销售管理" },
  { path: "logistics", name: "物流管理" },
  { path: "loan", name: "财务管理" },
  { path: "user", name: "用户管理" }
]