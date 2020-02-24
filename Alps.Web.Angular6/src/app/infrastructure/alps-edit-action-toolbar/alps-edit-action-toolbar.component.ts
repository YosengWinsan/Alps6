import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'alps-edit-action-toolbar',
  templateUrl: './alps-edit-action-toolbar.component.html',
  styleUrls: ['./alps-edit-action-toolbar.component.css']
})
export class AlpsEditActionToolbarComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }
  doSave() { }
  doBack() { history.back(); }

}
