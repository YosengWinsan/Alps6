import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AlpsSelectorDialogComponent } from './alps-selector/alps-selector-dialog/alps-selector-dialog.component';
import { AlpsSelectorComponent } from './alps-selector/alps-selector.component';

import{MatButtonModule} from '@angular/material/button';
import{ MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import{MatIconModule} from '@angular/material/icon';
import{MatExpansionModule} from '@angular/material/expansion';
import{MatTableModule} from '@angular/material/table';
import{MatFormFieldModule} from '@angular/material/form-field';
import{MatMenuModule} from '@angular/material/menu';
import{MatDialogModule} from '@angular/material/dialog';
import{ MatInputModule} from '@angular/material/input';
import{ MatAutocompleteModule} from '@angular/material/autocomplete';


import { AlpsEditToolbarComponent } from './alps-edit-toolbar/alps-edit-toolbar.component';
import { AlpsInfoChipComponent } from './alps-info-chip/alps-info-chip.component';
import { NgForObjPipe } from './pipe/ng-for-obj.pipe';
import { AlpsLoadingBarComponent } from './service/alps-loading-bar/alps-loading-bar.component';
import { EnterToTabDirective } from './directive/enter-to-tab.directive';
import { AlpsMenuSelectorComponent } from './alps-menu-selector/alps-menu-selector.component';
import { AlpsNavBarComponent } from './alps-nav-bar/alps-nav-bar.component';
import {RouterModule } from '@angular/router';
import { AlpsCommodityCatagorySelectorComponent } from './alps-commodity-catagory-selector/alps-commodity-catagory-selector.component';
import { AlpsShopcartComponent } from './alps-shopcart/alps-shopcart.component';
import { AlpsPrintComponent } from './alps-print/alps-print.component';
import { AlpsSearchSelectorComponent } from './alps-search-selector/alps-search-selector.component';
import { ReactiveFormsModule } from '@angular/forms';
import { EnumFormatPipe } from './pipe/enum-format.pipe';
import { AlpsActionToolbarComponent } from './alps-action-toolbar/alps-action-toolbar.component';
@NgModule({
  imports: [
    CommonModule,MatDialogModule,MatButtonModule ,MatIconModule,MatProgressSpinnerModule,MatMenuModule,RouterModule,MatExpansionModule,MatTableModule
    ,ReactiveFormsModule,MatAutocompleteModule,MatInputModule
  ],
  exports:[AlpsSelectorComponent,AlpsEditToolbarComponent,AlpsInfoChipComponent,NgForObjPipe,EnterToTabDirective,AlpsMenuSelectorComponent,AlpsNavBarComponent,AlpsCommodityCatagorySelectorComponent
  ,AlpsShopcartComponent,MatFormFieldModule,MatTableModule,MatIconModule,MatInputModule,MatButtonModule,AlpsPrintComponent,AlpsSearchSelectorComponent],
  declarations: [AlpsSelectorComponent,AlpsSelectorDialogComponent, AlpsEditToolbarComponent, AlpsInfoChipComponent, NgForObjPipe,AlpsLoadingBarComponent
,EnterToTabDirective, AlpsMenuSelectorComponent, AlpsNavBarComponent, AlpsCommodityCatagorySelectorComponent,  AlpsShopcartComponent, AlpsPrintComponent, AlpsSearchSelectorComponent, EnumFormatPipe,  AlpsActionToolbarComponent
  ],
  entryComponents:[AlpsSelectorDialogComponent,AlpsLoadingBarComponent]
})
export class InfrastructureModule { }

export * from './repository/alpsActionResponse';
export {RepositoryService} from './repository/repository.service';
export {QueryService} from './query-service/query.service';
export {ShopcartService,Icommodity,IShopcartItem} from './service/shopcart.service';
export {AlpsConst} from './alps-const';