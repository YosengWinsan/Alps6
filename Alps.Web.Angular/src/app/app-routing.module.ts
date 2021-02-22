import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from "./dashboard/dashboard.component";
import { AuthGuard } from './auth/guard/auth.guard';
import { AppContainerComponent } from './app-container/app-container.component';
import { LoginComponent } from './auth/login/login.component';
import { ForgotPasswordComponent } from './auth/forgot-password/forgot-password.component';
import { RegisterComponent } from './auth/register/register.component';
import { ForbidComponent } from './auth/forbid/forbid.component';

const routes: Routes = [
  {
    path: '', component: AppContainerComponent, children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: "dashboard", component: DashboardComponent },
      { path: 'forbid', component: ForbidComponent },
      { path: "catagory", loadChildren: () => import("./catagory/catagory.module").then(m => m.CatagoryModule) },
      { path: "product", loadChildren: () => import("./product/product.module").then(m => m.ProductModule) },
      { path: "stock", loadChildren: () => import("./stock/stock.module").then(m => m.StockModule) },
      { path: "sale", loadChildren: () => import("./sale/sale.module").then(m => m.SaleModule) },
      { path: "logistics", loadChildren: () => import("./logistics/logistics.module").then(m => m.LogisticsModule) },
      { path: "loan", loadChildren: () => import("./loan/loan.module").then(m => m.LoanModule) },
      { path: "user", loadChildren: () => import("./user/user.module").then(m => m.UserModule) },
      { path: "purchase", loadChildren: () => import("./purchase/purchase.module").then(m => m.PurchaseModule) }
    ], canActivateChild: [AuthGuard], canActivate: [AuthGuard]
  },
  { path: 'login', component: LoginComponent },
  { path: 'forgotpassword', component: ForgotPasswordComponent },
  { path: 'register', component: RegisterComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
