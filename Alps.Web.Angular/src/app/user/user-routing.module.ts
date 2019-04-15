import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { InfoComponent } from './info/info.component';
import { UserComponent } from './user/user.component';
import { UserListComponent } from './user-list/user-list.component';
import { AuthorizeComponent } from './authorize/authorize.component';
import { RoleListComponent } from './role-list/role-list.component';
import { RoleEditComponent } from './role-edit/role-edit.component';
import { ResourceListComponent } from './resource-list/resource-list.component';

const routes: Routes = [
  {
    path: "", component: UserComponent, children: [
      { path: '', redirectTo:'userlist',pathMatch:"full" },
      { path: 'info', component: InfoComponent },
      { path: 'userlist', component: UserListComponent },
      { path: 'authorize', component:AuthorizeComponent},
      {path:'rolelist',component:RoleListComponent},
      {path:'roleedit',component:RoleEditComponent},
      {path:'resourcelist',component:ResourceListComponent}
    ]
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
