import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { UserService } from '../user.service';

@Component({
  selector: 'app-role-edit',
  templateUrl: './role-edit.component.html',
  styleUrls: ['./role-edit.component.css']
})
export class RoleEditComponent implements OnInit {
roleForm:FormGroup;
  constructor(formBuilder:FormBuilder,userService:UserService) {
    this.roleForm=formBuilder.group({name:[]});
   }

  ngOnInit() {
  }
save(){

}
}
