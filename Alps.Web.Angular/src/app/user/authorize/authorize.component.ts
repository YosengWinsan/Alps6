import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../user.service';
import { ActivatedRoute } from '@angular/router';
import { QueryService } from '../../infrastructure/infrastructure.module';
import { MatSelectionList } from '@angular/material/list';
import { HttpBackend } from '@angular/common/http';

@Component({
  selector: 'app-authorize',
  templateUrl: './authorize.component.html',
  styleUrls: ['./authorize.component.css']
})
export class AuthorizeComponent implements OnInit {

  constructor(private userService: UserService, private activatedRoute: ActivatedRoute, private queryService: QueryService) {
    this.user = {};
  }

  user;
  roleOptions;
  @ViewChild('roles', { static: true })
  roleSelection: MatSelectionList;
  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(params => {
      let id = params["id"] ? params["id"] : "";
      if (id != "") {
        this.userService.getUser(id).subscribe(data => {
          this.user = data;
          this.roleSelection.writeValue(this.user.roles.split(","));

        });
      }
    });
    this.roleOptions = this.queryService.getRoleOptions();

    this.roleSelection.selectionChange.subscribe(d => {
      this.user.roles = this.roleSelection.selectedOptions.selected.map(o => o.value).join(",");
      //console.info(this.roleSelection.selectedOptions.selected);
    });

  }

  back() {
    history.back();
  }
  save() {
    this.userService.updateUserRole(this.user).subscribe(rst => {
      this.back();
    });
  }
}
