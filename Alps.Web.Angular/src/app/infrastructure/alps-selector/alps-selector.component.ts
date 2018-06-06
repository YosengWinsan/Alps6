import { NG_VALUE_ACCESSOR, ControlValueAccessor } from '@angular/forms';
import { Component, Input, OnInit, forwardRef } from '@angular/core';
import { MatDialog } from "@angular/material";
import { AlpsSelectorDialogComponent } from './alps-selector-dialog/alps-selector-dialog.component';

export const COMPONENT_NAME_VALUE_ACCESSOR: any = {
  provide: NG_VALUE_ACCESSOR,
  useExisting: forwardRef(() => AlpsSelectorComponent),
  multi: true
};

interface AlpsSelectorNode
{
  id:string;
  name:string;
  children:AlpsSelectorNode[];
}

@Component({
  selector: 'alps-selector',
  templateUrl: './alps-selector.component.html',
  styleUrls: ['./alps-selector.component.css'],
  providers: [COMPONENT_NAME_VALUE_ACCESSOR]
})
export class AlpsSelectorComponent implements OnInit, ControlValueAccessor {

  private _selectedNode:AlpsSelectorNode;
  private _value: any;
constructor(private matDialog:MatDialog)
{

}

open()
{
this.matDialog.open(AlpsSelectorDialogComponent).afterClosed().subscribe(
(result)=>console.info(result)
);
}

  set value(value: any) {
    this._value = value;
    this.notifyValueChange();
  }

  get value() {
    return this._value;
  }

  onChange: (value) => {};
  onTouched: () => {};


  notifyValueChange() {
    if (this.onChange) {
      this.onChange(this.value);
    }
  }

  ngOnInit(): void {
    
  }

  writeValue(obj: any): void {
    this.value = obj;
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean): void {
  }
}

