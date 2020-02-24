import { Directive, ElementRef, HostListener, Input } from '@angular/core';

@Directive({
  selector: '[enterToTab]'
})
export class EnterToTabDirective {
  constructor(private elementRef: ElementRef) {
  }
  @Input() enterToTab: string;
  @HostListener('focusin', ['$event'])
  onFocus(e)
  {
    let control=e.target;
    if (control.tagName == "INPUT" && control.type == "text")
    control.select();
  }
  @HostListener('keyup.enter', ['$event'])
  onEnter(e:KeyboardEvent) { 
    e.preventDefault();
    let controls = this.elementRef.nativeElement.querySelectorAll("input,button");
    let moveNext = false;
    for (let i = 0; i < controls.length; i++) {
      if (moveNext) {        
          controls[i].focus();  
        break;
      }
      if (controls[i] == e.srcElement)
        moveNext = true;
    };


  }
}
