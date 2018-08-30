import { Component, OnInit,  Input } from '@angular/core';

@Component({
  selector: 'alps-print',
  templateUrl: './alps-print.component.html',
  styleUrls: ['./alps-print.component.css']
})
export class AlpsPrintComponent implements OnInit {
  @Input() color: string;
  @Input() section: string;
  constructor() {
    if (this.section === undefined)
      this.section = '';
  }

  ngOnInit() {
  }
  print() {
    if (this.section && this.section !== undefined && this.section != '') {
      let printContent = document.getElementById(this.section).innerHTML;
      let styleContent = "@media print{.noprint{display:none}}";
      for (let i = 0; i < document.styleSheets.length; i++) {
        if (document.styleSheets[i].href == null) {
          let rules = (<any>document.styleSheets[i]).rules;
          if (rules)
            for (let j = 0; j < rules.length; j++) {
              styleContent = styleContent + rules[j].cssText;
            }
        }
        else
          styleContent = styleContent + '<link href="' + document.styleSheets[i].href + '" rel="stylesheet">'
      }

      if (window) {
        if (navigator.userAgent.toLowerCase().indexOf('chrome') > -1) {
          let popup = window.open('', '_blank', 'width=600,height=600,scrollbars=no,menubar=no,toolbar=no,location=no,status=no,titlebar=no');
          popup.window.focus();
          popup.document.write("<!DOCTYPE html><html><head><style>" + styleContent + "</style></head><body onload='window.print();window.close();'><div>" + printContent + "</div></body></html>");
          popup.onbeforeunload = (event) => { popup.close(); return '.\n'; };
          popup.onabort = (event) => { popup.document.close(); popup.close(); };
          popup.document.close();

        }
        else {
          let popup = window.open('', '_blank', 'width=600,height=600,scrollbars=no,menubar=no,toolbar=no,location=no,status=no,titlebar=no');
          popup.window.focus();
          popup.document.write('<!DOCTYPE html><html><head></head><body onload="window.print();">' + printContent + "</body></html>");
          popup.onbeforeunload = (event) => { popup.close(); return '.\n'; };
          popup.onabort = (event) => { popup.document.close(); popup.close(); };

        }
        return true;
      }

    }
  }

}
