import { Component, OnInit } from '@angular/core';
import { CatagoryService } from '../catagory.service';
import { ActivatedRoute } from "@angular/router";
@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']

})
export class ListComponent implements OnInit {
  constructor(private catagoryService: CatagoryService, private activatedRoute: ActivatedRoute) {
  }

  catagoryPath;
  catagoryData;
  displayedColumns;
  ngOnInit() {
    this.displayedColumns = ["name", "action"];
    this.activatedRoute.queryParams.subscribe((params) => {
      var id = params["id"];
      if (!id) {
        id = "";
      }
      this.catagoryService.getListByParentID(id).subscribe((data: { path, data }) => {
        this.catagoryPath = data.path;
        this.catagoryData = data.data;
      }
      );
    });
  }

}
