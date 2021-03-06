import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { InfoComponent } from './info/info.component';
import { UserListComponent } from './user-list/user-list.component';
import { UserComponent } from './user/user.component';
import { InfrastructureModule } from '../infrastructure/infrastructure.module';
import { AuthorizeComponent } from './authorize/authorize.component';
import { MatListModule } from '@angular/material/list';
import { MatCheckboxModule } from '@angular/material/checkbox'
import { RoleListComponent } from './role-list/role-list.component';
import { RoleEditComponent } from './role-edit/role-edit.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ResourceListComponent } from './resource-list/resource-list.component';


@NgModule({
  imports: [
    CommonModule,
    UserRoutingModule, InfrastructureModule, MatListModule, ReactiveFormsModule, MatCheckboxModule, FormsModule
  ],
  declarations: [InfoComponent, UserListComponent, UserComponent, AuthorizeComponent, RoleListComponent, RoleEditComponent, ResourceListComponent]
})
export class UserModule { }
