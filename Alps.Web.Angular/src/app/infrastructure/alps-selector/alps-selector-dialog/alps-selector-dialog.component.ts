import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA,MatDialogRef } from "@angular/material";
@Component({
  selector: 'app-alps-selector-dialog',
  templateUrl: './alps-selector-dialog.component.html',
  styleUrls: ['./alps-selector-dialog.component.css']
})
export class AlpsSelectorDialogComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA)private options:AlpsSelectorOption[],private dialog:MatDialogRef<AlpsSelectorDialogComponent>) { 
    this.currentOptions=this.options;
  }

  ngOnInit() {
    
  }
  selectedOptions:AlpsSelectorOption[]=[];
  currentOptions;
  select(option:AlpsSelectorOption)
  {
    if(option.children && option.children.length>0)
      {
        this.selectedOptions.push(option);
        this.currentOptions=option.children;
      }
      else
      this.dialog.close(option);
    
  }
  selectPath(path:AlpsSelectorOption)
  {
    var i=this.selectedOptions.indexOf(path);
    this.selectedOptions.splice(i,this.selectedOptions.length-i);
    if(i===0)
    this.currentOptions=this.options;
    else
    this.currentOptions=this.selectedOptions[i-1].children;
    
  }
}


interface AlpsSelectorOption
{
  value:string;
  displayValue:string;
  children:AlpsSelectorOption[];
  
}