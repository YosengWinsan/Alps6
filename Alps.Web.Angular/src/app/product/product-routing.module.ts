import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductListComponent } from './product-list/product-list.component';
import { ProductEditComponent } from './product-edit/product-edit.component';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { ProductSkuEditComponent } from './product-sku-edit/product-sku-edit.component';
import { CatagoryListComponent } from './catagory-list/catagory-list.component';
import { CatagoryEditComponent } from './catagory-edit/catagory-edit.component';
import { ProductComponent } from './product/product.component';

const routes: Routes = [
  {
    path: "", component: ProductComponent, children: [
      { path: "", redirectTo: "productlist", pathMatch: "full" },
      { path: "productlist", component: ProductListComponent },
      { path: "productedit", component: ProductEditComponent },
      { path: "productdetail", component: ProductDetailComponent },
      { path: "productskuedit", component: ProductSkuEditComponent },
      { path: "catagorylist", component: CatagoryListComponent },
      { path: "catagoryedit", component: CatagoryEditComponent }]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductRoutingModule { }
