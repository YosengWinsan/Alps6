import { Component, OnInit, Output, Input, EventEmitter } from '@angular/core';

@Component({
  selector: 'alps-edit-toolbar',
  templateUrl: './alps-edit-toolbar.component.html',
  styleUrls: ['./alps-edit-toolbar.component.css']
})
export class AlpsEditToolbarComponent implements OnInit {

  constructor() { }
  @Output()
  back: EventEmitter<any>=new EventEmitter();
  @Output()
  save: EventEmitter<any>=new EventEmitter();
  @Input()
  title: string;
  ngOnInit() {

  }
  doBack() {

    if (this.back.observers.length>0)
      this.back.emit();
    else
      history.back();
  }
  doSave() {
    if (this.save)
      this.save.emit();
  }
}
