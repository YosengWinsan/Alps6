import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'alps-commodity-catagory-selector',
  templateUrl: './alps-commodity-catagory-selector.component.html',
  styleUrls: ['./alps-commodity-catagory-selector.component.css']
})
export class AlpsCommodityCatagorySelectorComponent implements OnInit {

  @Input()
  set catagories(value) {
    if (value) {
      this.levelOneCatagories = value;
      if (this.levelOneCatagories.length > 0)
        this.select(this.levelOneCatagories[0], 1);
    }
  }
  @Output()
  selectionChanged: EventEmitter<string> = new EventEmitter<string>();

  levelOneCatagories
  levelTwoCatagories;
  levelThreeCatagories;
  levelOneSelectedCatagory;
  levelTwoSelectedCatagory;
  levelThreeSelectedCatagory;
  constructor() { }

  ngOnInit() {

  }
  select(catagory, level) {
    switch (level) {
      case 1:
        if (this.levelOneSelectedCatagory != catagory) {
          this.levelOneSelectedCatagory = catagory;
          if (catagory.children && catagory.children.length > 0) {
            this.levelTwoCatagories = catagory.children;
            if (this.levelTwoCatagories.length > 0)
              this.select(this.levelTwoCatagories[0], 2)
          }
          else {
            this.levelThreeCatagories = [];
            this.levelThreeSelectedCatagory = null;
            this.levelTwoCatagories = [];
            this.levelTwoSelectedCatagory = null;
          }
        }
        break;
      case 2:
        if (this.levelTwoSelectedCatagory != catagory) {
          this.levelTwoSelectedCatagory = catagory;
          if (catagory.children && catagory.children.length > 0) {
            this.levelThreeCatagories = catagory.children;
            if (this.levelThreeCatagories.length > 0)
              this.select(this.levelThreeCatagories[0], 3);
          }
          else {
            this.levelThreeCatagories = [];
            this.levelThreeSelectedCatagory = null;
          }
        }
        break;
      case 3:
        if (this.levelThreeSelectedCatagory != catagory) {
          this.levelThreeSelectedCatagory = catagory;
          this.selectionChanged.emit(catagory.value);
        }
        break;

    }

  }
}
