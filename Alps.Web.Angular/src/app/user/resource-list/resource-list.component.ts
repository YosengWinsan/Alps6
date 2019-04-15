import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';

@Component({
  selector: 'app-resource-list',
  templateUrl: './resource-list.component.html',
  styleUrls: ['./resource-list.component.css']
})
export class ResourceListComponent implements OnInit {

  constructor(private userService: UserService) { }
  resourceList;
  totalCount;
  ngOnInit() {
    this.loadList();

  }
  loadList() {
    this.resourceList = this.userService.getResources().subscribe(rst => {
      this.resourceList = rst;
      this.totalCount = this.resourceList.length;
    });
  }
  updateResource() {
    this.userService.updateResource().subscribe(rst => {
      if (rst)
        this.loadList();
    });
  }
}
