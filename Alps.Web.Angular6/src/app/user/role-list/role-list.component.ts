import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';

@Component({
  selector: 'app-role-list',
  templateUrl: './role-list.component.html',
  styleUrls: ['./role-list.component.css']
})
export class RoleListComponent implements OnInit {

  constructor(private userService:UserService) { }
  roleList;
  ngOnInit() {
    this.roleList=this.userService.getRoles();
  }

}
