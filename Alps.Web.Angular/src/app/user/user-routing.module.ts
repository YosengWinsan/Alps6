import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { InfoComponent } from './info/info.component';
import { UserComponent } from './user/user.component';
import { UserListComponent } from './user-list/user-list.component';
import { AuthorizeComponent } from './authorize/authorize.component';

const routes: Routes = [
  {
    path: "", component: UserComponent, children: [
      { path: 'info', component: InfoComponent },
      { path: 'userlist', component: UserListComponent },
      { path: 'authorize', component:AuthorizeComponent}
    ]
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
