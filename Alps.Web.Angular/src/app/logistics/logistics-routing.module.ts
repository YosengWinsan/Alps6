import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LogisticsComponent } from './logistics/logistics.component';
import { LogisticsInfoComponent } from './logistics-info/logistics-info.component';
import { DistributionComponent } from './distribution/distribution.component';

const routes: Routes = [
  {path:"",component:LogisticsComponent,children:
  [{path:"",redirectTo:"logisticsinfo",pathMatch:"full"},
  {path:"logisticsinfo",component:LogisticsInfoComponent},
  {path:"distribution",component:DistributionComponent}
]

}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LogisticsRoutingModule { }
