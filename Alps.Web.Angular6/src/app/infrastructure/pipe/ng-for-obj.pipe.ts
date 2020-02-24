import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'ngForObj'
})
export class NgForObjPipe implements PipeTransform {

  transform(value: any, args?: any): any {
    return Object.keys(value);
  }

}
