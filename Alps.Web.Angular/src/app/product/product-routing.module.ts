import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductListComponent } from './product-list/product-list.component';
import { ProductEditComponent } from './product-edit/product-edit.component';

const routes: Routes = [
  { path: "", redirectTo: "productlist", pathMatch: "full" },
  { path: "productlist", component: ProductListComponent },
  { path: "productedit", component: ProductEditComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductRoutingModule { }
