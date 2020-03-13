import { Injectable, Injector } from '@angular/core';
import { RepositoryService } from '../infrastructure/infrastructure.module';

@Injectable({
  providedIn: 'root'
})
export class UserService extends RepositoryService {

  constructor(injector: Injector) {
    super(injector);
    this.setBaseUrl("api/Users");
  }
  //private setRoles(){this.setBaseUrl()}
  getUsers(){
    return this.getall();
  }
  getUser(id){
    return this.get(id);
  }
  saveUser(user){
    return this.action("saveuser",user);
  }
  getRoles(){

    return this.query("getroles");
  }
  getRole(id:string){
    return this.query("getrole/"+id);
  }
  saveRole(role)
  {
    this.checkAndFillID(role);
    return this.action("saverole",role);
  }
  getResources(){
    return this.query('getresources')
  }
  updateResource(){
    return this.action('updateresources');
  }
  getPermissions(){
    return this.query("getPermissions");
  }
  savePermissions(permissions)
  {
    return this.action("savepermissions",permissions);
  }
}
