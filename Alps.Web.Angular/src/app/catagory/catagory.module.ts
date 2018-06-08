import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from "@angular/forms";
import { CatagoryRoutingModule } from './catagory-routing.module';
import { ListComponent } from './list/list.component';
import { EditComponent } from './edit/edit.component';
import { MatTableModule, MatPaginatorModule, MatSortModule,MatFormFieldModule,MatInputModule,MatIconModule ,MatButtonModule 

} from '@angular/material';
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
