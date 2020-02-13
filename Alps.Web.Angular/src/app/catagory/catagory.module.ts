import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from "@angular/forms";
import { CatagoryRoutingModule } from './catagory-routing.module';
import { ListComponent } from './list/list.component';
import { EditComponent } from './edit/edit.component';
import { MatTableModule} from '@angular/material/table';
import { MatPaginatorModule} from '@angular/material/paginator';
import {  MatSortModule } from '@angular/material/sort';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule} from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';


import { InfrastructureModule  } from "../infrastructure/infrastructure.module";

@NgModule({
  imports: [
    CommonModule,
    CatagoryRoutingModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,MatFormFieldModule,MatInputModule,MatIconModule,ReactiveFormsModule,MatButtonModule,InfrastructureModule
  ],
  declarations: [ListComponent, EditComponent]
})
export class CatagoryModule { }
