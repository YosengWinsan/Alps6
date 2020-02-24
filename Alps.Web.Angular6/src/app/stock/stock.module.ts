import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StockInfoComponent } from './stock-info/stock-info.component';
import { StockInListComponent } from './stock-in-list/stock-in-list.component';
import { StockInComponent } from './stock-in/stock-in.component';
import { StockOutComponent } from './stock-out/stock-out.component';
import { StockOutListComponent } from './stock-out-list/stock-out-list.component';
import { StockRoutingModule } from './stock-routing.module';
//import { MatButtonModule, MatGridListModule, MatTableModule, MatIconModule, MatSortModule, MatFormFieldModule, MatDialogModule, MatInputModule, MatDividerModule } from '@angular/material';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule} from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule} from '@angular/material/button';
import { MatGridListModule} from '@angular/material/grid-list';
import { MatDividerModule} from '@angular/material/divider';
import { MatSortModule } from '@angular/material/sort';
import { MatDialogModule } from '@angular/material/dialog';

import { StockComponent } from './stock.component';
import { InfrastructureModule } from '../infrastructure/infrastructure.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { StockInItemEditComponent } from './stock-in/stock-in-item-edit/stock-in-item-edit.component';
import { StockInDetailComponent } from './stock-in-detail/stock-in-detail.component';
import { StockOutDetailComponent } from './stock-out-detail/stock-out-detail.component';
import { StockOutItemEditComponent } from './stock-out/stock-out-item-edit/stock-out-item-edit.component';
import { PositionListComponent } from './position-list/position-list.component';
import { PositionEditComponent } from './position-edit/position-edit.component';
@NgModule({
  imports: [
    CommonModule, StockRoutingModule, MatButtonModule, MatGridListModule, MatTableModule, MatIconModule, MatSortModule, InfrastructureModule,MatFormFieldModule
    ,ReactiveFormsModule,MatDialogModule,MatInputModule,MatDividerModule,FormsModule
  ],
  declarations: [StockInfoComponent, StockInListComponent, StockInComponent, StockOutComponent, StockOutListComponent, StockComponent,  StockInItemEditComponent, StockInDetailComponent, StockOutDetailComponent, StockOutItemEditComponent, PositionListComponent, PositionEditComponent],
  entryComponents:[StockInItemEditComponent,StockOutItemEditComponent]

})
export class StockModule { }
