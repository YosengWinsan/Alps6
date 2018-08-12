import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from "./dashboard/dashboard.component";
const routes: Routes = [
{path:"",redirectTo:"dashboard",pathMatch:"full"},
{path:"dashboard",component:DashboardComponent},
{path:"catagory",loadChildren:"./catagory/catagory.module#CatagoryModule"},
{path:"product",loadChildren:"./product/product.module#ProductModule"},
{path:"stock",loadChildren:"./stock/stock.module#StockModule"},
{path:"sale",loadChildren:"./sale/sale.module#SaleModule"},
{path:"logistics",loadChildren:"./logistics/logistics.module#LogisticsModule"},
{path:"loan",loadChildren:"./loan/loan.module#LoanModule"}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
