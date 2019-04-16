import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { InfoComponent } from './info/info.component';
import { UserListComponent } from './user-list/user-list.component';
import { UserComponent } from './user/user.component';
import { InfrastructureModule } from '../infrastructure/infrastructure.module';
import { AuthorizeComponent } from './authorize/authorize.component';
import { MatListModule, MatCheckboxModule } from '@angular/material';
import { RoleListComponent } from './role-list/role-list.component';
import { RoleEditComponent } from './role-edit/role-edit.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ResourceListComponent } from './resource-list/resource-list.component';

@NgModule({
  imports: [
    CommonModule,
    UserRoutingModule,InfrastructureModule,MatListModule,ReactiveFormsModule,MatCheckboxModule
  ],
  declarations: [InfoComponent, UserListComponent, UserComponent, AuthorizeComponent, RoleListComponent, RoleEditComponent, ResourceListComponent]
})
export class UserModule { }
