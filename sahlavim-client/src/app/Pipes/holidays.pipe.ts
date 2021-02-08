import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'holidays'
})
export class HolidaysPipe implements PipeTransform {

  Hebcal = require('hebcal');

  transform(date: Date): any {

    var day = new this.Hebcal.HDate(date);

    if (day.holidays(true).length > 1) {
      for (let i = 0; i < day.holidays(true).length; i++) {
        console.log(day.holidays(true)[i].getDesc('h')) //ההדפסה מאיטה את ההרצה
      }
      if(day.holidays(true)[0].getDesc('h')!='ערב שבת')
    {
            return day.holidays(true)[0].getDesc('h');

    }
      debugger

    }
  }

}
