import { Injectable } from '@angular/core';
import { RepositoryService } from '../repository/repository.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class QueryService extends RepositoryService {

  constructor(http: HttpClient) {
    super(http);
    this.setBaseUrl("api/query");
  }
  initDatabase() {
    return this.query("InitDatabase");
  }
  GetCatagoryOptions()
  {
    return this.query("CatagoryOptions");
  }
}

