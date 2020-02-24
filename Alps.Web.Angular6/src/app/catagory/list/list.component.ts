import { Component, OnInit, ViewChild } from '@angular/core';
import { CatagoryService } from '../catagory.service';
import { ActivatedRoute } from "@angular/router";
import { MatSort} from "@angular/material/sort";
import {  MatTable } from "@angular/material/table";
import {MatTableDataSource } from "@angular/material/table";

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']

})
export class ListComponent implements OnInit {
  constructor(private catagoryService: CatagoryService, private activatedRoute: ActivatedRoute) {
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
//     this.sort.sortChange.subscribe((data) => {
//       this.displayedData=this.sortData([...this.catagoryData]);
//       this.table.renderRows();
//     });
//   }
//   sortData(data:any[]) {

//     if (!this.sort.active || this.sort.direction === '') {
//       return data;
//     }

//     return data.sort((a, b) => {
//       const isAsc = this.sort.direction === 'asc';
//       return compare(Reflect.get(a, this.sort.active), Reflect.get(b, this.sort.active), isAsc)
//     });
//   }

// }
// /** Simple sort comparator for example ID/Name columns (for client-side sorting). */
// function compare(a, b, isAsc) {
//   return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
// }
