import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AlpsSelectorDialogComponent } from './alps-selector/alps-selector-dialog/alps-selector-dialog.component';
import { AlpsSelectorComponent } from './alps-selector/alps-selector.component';
import { MatDialogModule, MatButtonModule, MatIconModule } from '@angular/material';
import { AlpsEditToolbarComponent } from './alps-edit-toolbar/alps-edit-toolbar.component';
import { AlpsInfoChipComponent } from './alps-info-chip/alps-info-chip.component';
import { NgForObjPipe } from './pipe/ng-for-obj.pipe';

@NgModule({
  imports: [
    CommonModule,MatDialogModule,MatButtonModule ,MatIconModule
  ],
  exports:[AlpsSelectorComponent,AlpsEditToolbarComponent,AlpsInfoChipComponent,NgForObjPipe],
  declarations: [AlpsSelectorComponent,AlpsSelectorDialogComponent, AlpsEditToolbarComponent, AlpsInfoChipComponent, NgForObjPipe],
  entryComponents:[AlpsSelectorDialogComponent]
})
export class InfrastructureModule { }

export * from './repository/alpsActionResponse';
export {RepositoryService} from './repository/repository.service';
export {QueryService} from './query-service/query.service';