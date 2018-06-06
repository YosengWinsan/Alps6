import { Component, OnInit } from '@angular/core';
import { CatagoryService } from '../../catagory.service';
import { MatDialogRef } from "@angular/material";
@Component({
  selector: 'app-parent-selector',
  templateUrl: './parent-selector.component.html',
  styleUrls: ['./parent-selector.component.css']
})
export class ParentSelectorComponent implements OnInit {

  constructor(private catagoryService: CatagoryService, private dialogRef: MatDialogRef<ParentSelectorComponent>) { }

  catagoryPath;
  catagoryList;
  ngOnInit() {
    this.select("");
  }
  select(id: string) {
    this.catagoryService.getListByParentID(id).subscribe((res: any) => {
      if (res.data.length === 0)
        this.dialogRef.close(id);
      else {
        this.catagoryPath = res.path;
        this.catagoryList = res.data;
      }

    });
  }


}
