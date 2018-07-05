import { Directive, ElementRef, HostListener, Input } from '@angular/core';

@Directive({
  selector: '[enterToTab]'
})
export class EnterToTabDirective {
  constructor(private elementRef: ElementRef) {
  }
  @Input() enterToTab: string;

  @HostListener('keydown.enter', ['$event'])
  onEnter(e) {
    e.preventDefault();
    let controls = this.elementRef.nativeElement.querySelectorAll("input,button");
    let moveNext = false;
    for (let i = 0; i < controls.length; i++) {
      if (moveNext) {
        if (controls[i].tagName == "INPUT" && controls[i].type == "text")
          controls[i].select();
        controls[i].focus();
        break;
      }
      if (controls[i] == e.srcElement)
        moveNext = true;
    };


  }
}
