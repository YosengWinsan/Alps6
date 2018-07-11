import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { StockInfoComponent } from './stock-info/stock-info.component';
import { StockInListComponent } from './stock-in-list/stock-in-list.component';
import { StockInComponent } from './stock-in/stock-in.component';
import { StockOutComponent } from './stock-out/stock-out.component';
import { StockOutListComponent } from './stock-out-list/stock-out-list.component';
import { StockComponent } from './stock.component';
import { StockInDetailComponent } from './stock-in-detail/stock-in-detail.component';
import { StockOutDetailComponent } from './stock-out-detail/stock-out-detail.component';


const routes: Routes = [
    {
        path: '', component: StockComponent, children: [
            { path: '', redirectTo: "stockinfo" },
            { path: 'stockinfo', component: StockInfoComponent },
            { path: 'stockinlist', component: StockInListComponent },
            { path: 'stockoutlist', component: StockOutListComponent },
            { path: 'stockout', component: StockOutComponent },
            { path: 'stockin', component: StockInComponent },
            { path: 'stockindetail', component: StockInDetailComponent },
            { path: 'stockoutdetail', component: StockOutDetailComponent }
        ]
    }
   
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class StockRoutingModule { }
