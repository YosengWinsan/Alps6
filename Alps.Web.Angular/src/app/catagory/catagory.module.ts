import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CatagoryRoutingModule } from './catagory-routing.module';
import { ListComponent } from './list/list.component';
import { EditComponent } from './edit/edit.component';

@NgModule({
  imports: [
    CommonModule,
    CatagoryRoutingModule
  ],
  declarations: [ListComponent, EditComponent]
})
export class CatagoryModule { }
