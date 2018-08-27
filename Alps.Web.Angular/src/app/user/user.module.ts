import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { InfoComponent } from './info/info.component';
import { UserListComponent } from './user-list/user-list.component';
import { UserComponent } from './user/user.component';
import { InfrastructureModule } from '../infrastructure/infrastructure.module';
import { AuthorizeComponent } from './authorize/authorize.component';
import { MatListModule } from '@angular/material';

@NgModule({
  imports: [
    CommonModule,
    UserRoutingModule,InfrastructureModule,MatListModule
  ],
  declarations: [InfoComponent, UserListComponent, UserComponent, AuthorizeComponent]
})
export class UserModule { }
