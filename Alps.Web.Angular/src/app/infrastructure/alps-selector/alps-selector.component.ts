import { NG_VALUE_ACCESSOR, ControlValueAccessor } from '@angular/forms';
import { Component, Input, OnInit, forwardRef } from '@angular/core';
import { MatDialog } from "@angular/material/dialog";
import { AlpsSelectorDialogComponent, AlpsSelectorOption, AlpsSelectorData } from './alps-selector-dialog/alps-selector-dialog.component';
import { AlpsConst } from '../alps-const';

export const COMPONENT_NAME_VALUE_ACCESSOR: any = {
  provide: NG_VALUE_ACCESSOR,
  useExisting: forwardRef(() => AlpsSelectorComponent),
  multi: true
};



@Component({
  selector: 'alps-selector',
  templateUrl: './alps-selector.component.html',
  styleUrls: ['./alps-selector.component.css'],
  providers: [COMPONENT_NAME_VALUE_ACCESSOR]
})
export class AlpsSelectorComponent implements OnInit, ControlValueAccessor {

  private _selectedNode: AlpsSelectorOption;
  private _value: any = "";
  _displayValue: string = "请选择";
  private _options: any[] = [];
  _placeholder: string = "";
  _optionsPath = [];
_selectedOption;
  @Input()
  set options(newOptions) {
    if (newOptions)
      if (this._options !== newOptions) {
        this._options = newOptions;
        this.initDisplayValue();
      }
  }
  @Input()
  set placeholder(placeholder) {
    if (placeholder)
      if (this._placeholder !== placeholder) {
        this._placeholder = placeholder;
      }
  }
  //get options(): AlpsSelectItem[] { return this._options; }
  constructor(private matDialog: MatDialog) {
  }

  open() {
    var data=new AlpsSelectorData();
    data.options=this._options;
    data.optionsPath=this._optionsPath;
    data.selectedOption=this._selectedOption;
    this.matDialog.open(AlpsSelectorDialogComponent, { data:data, minWidth: "90vw" }).afterClosed().subscribe(
      (result) => {
        if (result) {
          this._selectedOption=result;
          this._displayValue = result.displayValue;  
          this.value = result.value;
        
        }
      }
    );
  }
  getDisplayValue() {
    return this._displayValue;
  }
  getOptionValue(p:string)
  {
    return this._selectedOption[p];
  }
  private initDisplayValue() {
    if (this._value && this._value !== "" && this._value!==AlpsConst.GUID_EMPTY) {

      if (this._options && this._options.length > 0) {
        var displayValue = this.searchOption(this._options, this._value);
        this._displayValue = displayValue === "" ? this._value : displayValue; 
      }
      // else
      // this._displayValue=this._value;
    }

  }
  searchOption(options: any[], value: string) {
    var result = "";
    for (let option of options) {
      if (option && option.children && option.children.length > 0)
        result = this.searchOption(option.children, value);
      if (result !== "") {
        this._optionsPath.unshift(option);
        return result;
      }
      if (option.isOption && option.value == value) {
        this._selectedOption=option;
        return option.displayValue;
      }
    }
    return "";
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

    this.initDisplayValue();
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

