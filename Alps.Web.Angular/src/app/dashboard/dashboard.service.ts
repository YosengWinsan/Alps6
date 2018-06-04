import { Injectable } from '@angular/core';
import { RepositoryService } from "../infrastructure/repository.service";
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class DashboardService extends RepositoryService {

  constructor(http: HttpClient) {
    super(http);
    this.setBaseUrl("api/query");
  }
  initDatabase()
  {
    return this.query("InitDatabase");
  }

}
