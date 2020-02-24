import { Component, OnInit } from '@angular/core';
import { trigger, state, style, transition, animate } from '@angular/animations';

@Component({
  selector: 'app-alps-loading-bar',
  templateUrl: './alps-loading-bar.component.html',
  styleUrls: ['./alps-loading-bar.component.css']
})
export class AlpsLoadingBarComponent implements OnInit {
  error: boolean = false;
  constructor() { }

  ngOnInit() {
  }
  toggleError() {
    this.error = true;
  }
}
