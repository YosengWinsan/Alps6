import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LogisticsRoutingModule } from './logistics-routing.module';
import { LogisticsComponent } from './logistics/logistics.component';
import { LogisticsInfoComponent } from './logistics-info/logistics-info.component';
import { InfrastructureModule } from '../infrastructure/infrastructure.module';
import { DistributionComponent } from './distribution/distribution.component';
import { DispatchComponent } from './dispatch/dispatch.component';
import { MatCardModule } from '@angular/material';

@NgModule({
  imports: [
    CommonModule,
    LogisticsRoutingModule,InfrastructureModule,MatCardModule
  ],
  declarations: [LogisticsComponent, LogisticsInfoComponent, DistributionComponent, DispatchComponent]
})
export class LogisticsModule { }
