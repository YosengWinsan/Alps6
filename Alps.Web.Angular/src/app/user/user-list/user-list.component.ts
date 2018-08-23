import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { UserService } from '../user.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  constructor(private userService: UserService) { }
  userList: MatTableDataSource<any>;
  displayedColumns=["name","idName","mobilePhoneNumber","roles","action"];
  ngOnInit() {
    this.userService.getUsers().subscribe(data => {
      this.userList = new MatTableDataSource(data);
    });
  }
}
