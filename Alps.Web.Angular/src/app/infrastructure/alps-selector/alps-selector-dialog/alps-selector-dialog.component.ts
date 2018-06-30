import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material";
@Component({
  selector: 'app-alps-selector-dialog',
  templateUrl: './alps-selector-dialog.component.html',
  styleUrls: ['./alps-selector-dialog.component.css']
})
export class AlpsSelectorDialogComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) private optionsData:AlpsSelectorData, private dialog: MatDialogRef<AlpsSelectorDialogComponent>) {
if(this.optionsData.selectedOption)
this.selectedValue=this.optionsData.selectedOption.value;
    this.optionsPath.push( ...this.optionsData.optionsPath);
    this.setCurrentOptions();
  }

  ngOnInit() {
  }
  selectedValue;
  optionsPath: AlpsSelectorOption[] = [];
  optionsList;
  select(option: AlpsSelectorOption) {
    if (option.children && option.children.length > 0) {
      this.optionsPath.push(option);
      this.optionsList = option.children;
    }
    else {
      if (option.isOption)
      {
        this.optionsData.optionsPath.splice(0,this.optionsData.optionsPath.length,...this.optionsPath);
        this.dialog.close(option);
      }
    }

  }
  selectPath(path: AlpsSelectorOption) {
    var i = this.optionsPath.indexOf(path);
    this.optionsPath.splice(i, this.optionsPath.length - i);
    this.setCurrentOptions();

  }
  private setCurrentOptions() {
    if (this.optionsPath.length === 0)
      this.optionsList = this.optionsData.options;
    else
      this.optionsList = this.optionsPath[this.optionsPath.length - 1].children;
  }
}
export class AlpsSelectorData
{
  options:AlpsSelectorOption[];
  selectedOption:AlpsSelectorOption;  
  optionsPath:AlpsSelectorOption[];
}
export interface AlpsSelectorOption {
  value: string;
  displayValue: string;
  isOption:boolean;
  children: AlpsSelectorOption[];
}
