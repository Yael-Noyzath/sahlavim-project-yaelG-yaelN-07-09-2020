import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'hebrewDate'
})
export class HebrewDatePipe implements PipeTransform {
  
  Hebcal = require('hebcal');

  transform(date: Date): any {

    var day = new this.Hebcal.HDate(date);
    console.log(day.getSedra('h'));
    console.log(day)
    debugger
    if(day.holidays(true).length>0)
    {
     console.log(day.holidays(true)[0].getDesc('h'))
    }
    if(day.getDay()==6)
    {
      console.log(day.getSedra('h')[0])
      return this.Hebcal.gematriya(day.getDate())+' '+day.getSedra('h')[0];

    }
    return this.Hebcal.gematriya(day.getDate());
    }

}
