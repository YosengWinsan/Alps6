import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { forkJoin } from 'rxjs';
import { p } from '@angular/core/src/render3';

@Component({
  selector: 'app-resource-list',
  templateUrl: './resource-list.component.html',
  styleUrls: ['./resource-list.component.css']
})
export class ResourceListComponent implements OnInit {

  constructor(private userService: UserService) { }
  resourceList;
  roleList;
  permissionList: any = [];//{[resourceID:string]:{[roleID:string]:boolean}};
  totalCount;
  resourcedDisplayColumns = ['name', 'controller', 'actionx', 'updateTime'];
  displayColumns=[];
  ngOnInit() {
    this.loadList();

  }
  loadList() {
    forkJoin(this.userService.getResources(), this.userService.getRoles(), this.userService.getPermissions()).subscribe(rst => {
      this.displayColumns.splice(0,this.displayColumns.length);
      this.resourcedDisplayColumns.forEach(c=>{this.displayColumns.push(c);});
      this.resourceList = rst[0];
      this.totalCount = this.resourceList.length;
      this.roleList = rst[1];
      this.roleList.forEach(role => {
        this.displayColumns.push(role.name);
      });
      for (let i = 0; i < this.resourceList.length; i++) {
        this.permissionList[i] = [];
        for (let j = 0; j < this.roleList.length; j++) {
          this.permissionList[i].push(false);
          for (let k = 0; k < rst[2].length; k++) {
            if (rst[2][k].resourceID == this.resourceList[i].id && rst[2][k].roleID == this.roleList[j].id) {
              this.permissionList[i][j] = true;
              (<any[]>rst[2]).splice(k, 1);
              break;
            }
          }
        }
      }
    });
  }
  updateResource() {
    this.userService.updateResource().subscribe(rst => {
      if (rst)
        this.loadList();
    });
  }

  savePermissions() {
    let list = [];
    for (let i = 0; i < this.resourceList.length; i++) {
      for (let j = 0; j < this.roleList.length; j++) {
        if (this.permissionList[i][j]) {
          list.push({ "resourceID": this.resourceList[i].id, "roleID": this.roleList[j].id });
        }
      }
    }
    this.userService.savePermissions(list).subscribe();
  }
}
