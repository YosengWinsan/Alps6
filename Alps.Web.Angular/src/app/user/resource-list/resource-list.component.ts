import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-resource-list',
  templateUrl: './resource-list.component.html',
  styleUrls: ['./resource-list.component.css']
})
export class ResourceListComponent implements OnInit {

  constructor(private userService: UserService) { }
  resourceList;
  roleList;
  totalCount;
  displayColumns=['name','controller','actionx','updateTime'];
  ngOnInit() {
    this.loadList();

  }
  loadList() {
    forkJoin(this.userService.getResources(),this.userService.getRoles()).subscribe(      rst=>{
      this.resourceList=rst[0];
      this.totalCount=this.resourceList.length;
      this.roleList=rst[1];
      this.roleList.forEach(role => {
        this.displayColumns.push(role.name);
      });
      }    );
    // this.resourceList = this.userService.getResources().subscribe(rst => {
    //   this.resourceList = rst;
    //   this.totalCount = this.resourceList.length;
    // });
  }
  updateResource() {
    this.userService.updateResource().subscribe(rst => {
      if (rst)
        this.loadList();
    });
  }
  updatePermission(roleID,resourceID){
    console.info(roleID);
    console.info(resourceID);
  }
}
