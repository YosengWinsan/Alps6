import { Component } from '@angular/core';
import { BreakpointObserver, Breakpoints, BreakpointState } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})


export class AppComponent {
  title = 'app';
  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches)
    );
  menuData: MenuItem[];
  constructor(private breakpointObserver: BreakpointObserver) {
    this.menuData = MENU_DATA;
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
  { path: "sale", name: "销售管理" }  ,
  { path: "logistics", name: "物流管理" }
]
