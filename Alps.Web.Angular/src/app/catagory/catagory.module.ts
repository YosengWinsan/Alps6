import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from "@angular/forms";
import { CatagoryRoutingModule } from './catagory-routing.module';
import { ListComponent } from './list/list.component';
import { EditComponent } from './edit/edit.component';
import { TtComponent } from './tt/tt.component';
import { MatTableModule, MatPaginatorModule, MatSortModule,MatFormFieldModule,MatInputModule,MatIconModule ,MatButtonModule} from '@angular/material';

@NgModule({
  imports: [
    CommonModule,
    CatagoryRoutingModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,MatFormFieldModule,MatInputModule,MatIconModule,ReactiveFormsModule,MatButtonModule
  ],
  declarations: [ListComponent, EditComponent, TtComponent]
})
export class CatagoryModule { }
