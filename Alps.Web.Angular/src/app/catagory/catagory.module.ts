import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from "@angular/forms";
import { CatagoryRoutingModule } from './catagory-routing.module';
import { ListComponent } from './list/list.component';
import { EditComponent } from './edit/edit.component';
import { MatTableModule, MatPaginatorModule, MatSortModule,MatFormFieldModule,MatInputModule,MatIconModule ,MatButtonModule,MatDividerModule,MatToolbarModule
,MatBottomSheetModule,MatDialogModule
} from '@angular/material';
import { ParentSelectorComponent } from './edit/parent-selector/parent-selector.component';

@NgModule({
  imports: [
    CommonModule,
    CatagoryRoutingModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,MatFormFieldModule,MatInputModule,MatIconModule,ReactiveFormsModule,MatButtonModule,MatDividerModule,MatToolbarModule,MatBottomSheetModule,MatDialogModule
  ],
  declarations: [ListComponent, EditComponent, ParentSelectorComponent],
  entryComponents:[ParentSelectorComponent]
})
export class CatagoryModule { }
