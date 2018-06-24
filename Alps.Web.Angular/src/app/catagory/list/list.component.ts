import { Component, OnInit, ViewChild } from '@angular/core';
import { CatagoryService } from '../catagory.service';
import { ActivatedRoute } from "@angular/router";
import { MatSort,MatTable } from "@angular/material";

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']

})
export class ListComponent implements OnInit {
  constructor(private catagoryService: CatagoryService, private activatedRoute: ActivatedRoute) {
  }
  @ViewChild(MatSort) sort: MatSort;
@ViewChild("table") table:MatTable<any>;
  
  catagoryPath;
  catagoryData;
  displayedData=[];
  displayedColumns;
  listID;
  ngOnInit() {
    this.displayedColumns = ["name", "action"];
    this.activatedRoute.queryParams.subscribe((params) => {
      var id = params["id"];
      if (!id) {
        id = "";
      }
      this.listID=id;
      this.catagoryService.getListByParentID(id).subscribe((data: any) => {
        this.catagoryPath = data.path;
        this.catagoryData = data.data;
        this.displayedData=this.catagoryData;
      }
      );
    });
    this.sort.sortChange.subscribe((data) => {
      this.displayedData=this.sortData([...this.catagoryData]);
      this.table.renderRows();
    });
  }
  sortData(data:any[]) {
    
    if (!this.sort.active || this.sort.direction === '') {
      return data;
    }

    return data.sort((a, b) => {
      const isAsc = this.sort.direction === 'asc';
      return compare(Reflect.get(a, this.sort.active), Reflect.get(b, this.sort.active), isAsc)
    });
  }

}
/** Simple sort comparator for example ID/Name columns (for client-side sorting). */
function compare(a, b, isAsc) {
  return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
}
