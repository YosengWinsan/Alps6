import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StockInfoComponent } from './stock-info/stock-info.component';
import { StockInListComponent } from './stock-in-list/stock-in-list.component';
import { StockInComponent } from './stock-in/stock-in.component';
import { StockOutComponent } from './stock-out/stock-out.component';
import { StockOutListComponent } from './stock-out-list/stock-out-list.component';
import { StockRoutingModule } from './stock-routing.module';
import { MatButtonModule, MatGridListModule, MatTableModule, MatIconModule, MatSortModule, MatFormFieldModule, MatDialogModule, MatInputModule, MatDividerModule } from '@angular/material';
import { StockComponent } from './stock.component';
import { InfrastructureModule } from '../infrastructure/infrastructure.module';
import { ReactiveFormsModule } from '@angular/forms';
import { StockInItemEditComponent } from './stock-in/stock-in-item-edit/stock-in-item-edit.component';
import { StockInDetailComponent } from './stock-in-detail/stock-in-detail.component';
@NgModule({
  imports: [
    CommonModule, StockRoutingModule, MatButtonModule, MatGridListModule, MatTableModule, MatIconModule, MatSortModule, InfrastructureModule,MatFormFieldModule
    ,ReactiveFormsModule,MatDialogModule,MatInputModule,MatDividerModule
  ],
  declarations: [StockInfoComponent, StockInListComponent, StockInComponent, StockOutComponent, StockOutListComponent, StockComponent,  StockInItemEditComponent, StockInDetailComponent],
  entryComponents:[StockInItemEditComponent]

})
export class StockModule { }
