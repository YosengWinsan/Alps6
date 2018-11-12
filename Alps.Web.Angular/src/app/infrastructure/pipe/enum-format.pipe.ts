import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'enumFormat'
})
export class EnumFormatPipe implements PipeTransform {

  transform(value: any, args?: any): any {
    
    return null;
  }

}
