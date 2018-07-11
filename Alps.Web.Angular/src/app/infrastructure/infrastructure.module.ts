import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AlpsSelectorDialogComponent } from './alps-selector/alps-selector-dialog/alps-selector-dialog.component';
import { AlpsSelectorComponent } from './alps-selector/alps-selector.component';
import { MatDialogModule, MatButtonModule, MatIconModule, MatProgressSpinnerModule, MatMenuModule } from '@angular/material';
import { AlpsEditToolbarComponent } from './alps-edit-toolbar/alps-edit-toolbar.component';
import { AlpsInfoChipComponent } from './alps-info-chip/alps-info-chip.component';
import { NgForObjPipe } from './pipe/ng-for-obj.pipe';
import { AlpsLoadingBarComponent } from './service/alps-loading-bar/alps-loading-bar.component';
import { EnterToTabDirective } from './directive/enter-to-tab.directive';
import { AlpsMenuSelectorComponent } from './alps-menu-selector/alps-menu-selector.component';
import { AlpsNavBarComponent } from './alps-nav-bar/alps-nav-bar.component';
import {RouterModule } from '@angular/router';
@NgModule({
  imports: [
    CommonModule,MatDialogModule,MatButtonModule ,MatIconModule,MatProgressSpinnerModule,MatMenuModule,RouterModule
  ],
  exports:[AlpsSelectorComponent,AlpsEditToolbarComponent,AlpsInfoChipComponent,NgForObjPipe,EnterToTabDirective,AlpsMenuSelectorComponent,AlpsNavBarComponent],
  declarations: [AlpsSelectorComponent,AlpsSelectorDialogComponent, AlpsEditToolbarComponent, AlpsInfoChipComponent, NgForObjPipe,AlpsLoadingBarComponent
,EnterToTabDirective, AlpsMenuSelectorComponent, AlpsNavBarComponent
  ],
  entryComponents:[AlpsSelectorDialogComponent,AlpsLoadingBarComponent]
})
export class InfrastructureModule { }

export * from './repository/alpsActionResponse';
export {RepositoryService} from './repository/repository.service';
export {QueryService} from './query-service/query.service';