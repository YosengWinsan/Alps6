import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }
  links = {"用户概况":"info","用户列表":"userlist","身份列表":"rolelist","资源列表":"resourcelist"};
}
