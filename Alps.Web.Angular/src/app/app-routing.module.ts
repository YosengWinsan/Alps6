import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from "./dashboard/dashboard.component";
import { AuthGuard } from './auth/guard/auth.guard';
import { AppContainerComponent } from './app-container/app-container.component';
import { LoginComponent } from './auth/login/login.component';
import { ForgotPasswordComponent } from './auth/forgot-password/forgot-password.component';
import { RegisterComponent } from './auth/register/register.component';
const routes: Routes = [
  //{path:"",redirectTo:"dashboard",pathMatch:"full"},
  {
    path: '', component: AppContainerComponent, children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: "dashboard", component: DashboardComponent },
      { path: "catagory", loadChildren: "./catagory/catagory.module#CatagoryModule" },
      { path: "product", loadChildren: "./product/product.module#ProductModule" },
      { path: "stock", loadChildren: "./stock/stock.module#StockModule" },
      { path: "sale", loadChildren: "./sale/sale.module#SaleModule" },
      { path: "logistics", loadChildren: "./logistics/logistics.module#LogisticsModule" },
      { path: "loan", loadChildren: "./loan/loan.module#LoanModule" },
      { path: "user", loadChildren: "./user/user.module#UserModule" },
      { path: "purchase", loadChildren: "./purchase/purchase.module#PurchaseModule" }
    ], canActivateChild: [AuthGuard], canActivate: [AuthGuard]
  },
  { path: 'login', component: LoginComponent },
  { path: 'forgotpassword', component: ForgotPasswordComponent },
  { path: 'register', component: RegisterComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
