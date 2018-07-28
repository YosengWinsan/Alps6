import { Component, OnInit, ViewChild } from '@angular/core';
import { ProductService } from '../product.service';
import { ActivatedRoute } from '@angular/router';
import { MatSort, MatTable, MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-catagory-list',
  templateUrl: './catagory-list.component.html',
  styleUrls: ['./catagory-list.component.css']
})
export class CatagoryListComponent implements OnInit {
  constructor(private catagoryService: ProductService, private activatedRoute: ActivatedRoute) {
  }
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild("table") table: MatTable<any>;

  isLeaf:boolean=false;
  catagoryPath;
  //catagoryData:MatTableDataSource<any>;
  displayedData: MatTableDataSource<any>;
  displayedColumns;
  listID;
  ngOnInit() {
    this.displayedColumns = ["name", "action"];
    this.activatedRoute.queryParams.subscribe((params) => {
      this.listID = params["id"] ? params["id"] : "";
      this.catagoryService.getListByParentID(this.listID).subscribe((data: any) => {
        this.catagoryPath = data.path;
        this.isLeaf=(this.catagoryPath.length>=2);
        this.displayedData = new MatTableDataSource(data.data);
        this.displayedData.sort = this.sort;
      }
      );
    });
  }
}