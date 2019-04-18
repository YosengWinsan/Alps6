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
  permissionList: any;//{[resourceID:string]:{[roleID:string]:boolean}};
  totalCount;
  displayColumns = ['name', 'controller', 'actionx', 'updateTime'];
  ngOnInit() {
    this.loadList();

  }
  loadList() {
    forkJoin(this.userService.getResources(), this.userService.getRoles(), this.userService.getPermissions()).subscribe(rst => {
      this.resourceList = rst[0];
      this.permissionList = rst[2];
      // rst[2].forEach(p => {
      //   this.permissionList[p.resourceID][p.roleID]=true;
      // });
      // console.info(this.permissionList);
      this.totalCount = this.resourceList.length;
      this.roleList = rst[1];
      this.roleList.forEach(role => {
        this.displayColumns.push(role.name);
      });

    });
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

  getPermission(roleID, resourceID) {
    //console.info("get");
    return this.permissionList.findIndex(p => p.roleID == roleID && p.resourceID == resourceID) > -1;
  }
  updatePermission(roleID, resourceID) {
    console.info("update");
    let index = this.permissionList.findIndex(p => p.roleID == roleID && p.resourceID == resourceID);
    if (index > -1)
      this.permissionList.splice(index, 1);
    else
      this.permissionList.push({ resourceID, roleID });
  }
  savePermissions(){
    this.userService.savePermissions(this.permissionList).subscribe();
  }
}
