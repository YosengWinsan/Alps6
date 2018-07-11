import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'alps-nav-bar',
  templateUrl: './alps-nav-bar.component.html',
  styleUrls: ['./alps-nav-bar.component.css']
})
export class AlpsNavBarComponent implements OnInit {

  constructor() { }
  @Input()
  set links(newLinks) {
    this._links=newLinks;
    this._linkKeys=Object.keys(this._links);
    // for(let l of Object.keys(newLinks))
    // {
    //   this._links.push({path:l,name:newLinks[l]});
    // }
  }
  _linkKeys;
  _links;
  ngOnInit() {
  }

}
