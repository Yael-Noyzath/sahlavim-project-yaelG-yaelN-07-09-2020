import { Component, OnInit } from '@angular/core';
import {

  ChangeDetectionStrategy,
  ViewChild,
  TemplateRef,
  EventEmitter,
  Input,
  Output,
} from '@angular/core';


import {
  startOfDay,
  endOfDay,
  subDays,
  addDays,
  endOfMonth,
  isSameDay,
  isSameMonth,
  addHours,
} from 'date-fns';

import {
  MAT_MOMENT_DATE_FORMATS,
  MomentDateAdapter,
  MAT_MOMENT_DATE_ADAPTER_OPTIONS,
} from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';

import { Subject } from 'rxjs';
import { NgbCalendar, NgbCalendarHebrew, NgbDate, NgbDatepickerI18n, NgbDatepickerI18nHebrew, NgbDateStruct, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {
  CalendarEvent,
  CalendarEventAction,
  CalendarEventTimesChangedEvent,
  CalendarView,
  DateFormatterParams,
} from 'angular-calendar';
import {
  CalendarDateFormatter,

  DAYS_OF_WEEK,
} from 'angular-calendar';
import { CustomDateFormatter } from '../Operators/operator-schedule/custom-date-formatter.provider';
import { formatDate, Time } from '@angular/common';
import { MainServiceService } from 'src/app/services/MainService/main-service.service';
import { th } from 'date-fns/locale';
import { schedule } from 'src/app/Classes/schedule';
import { CalendarEventActionsComponent } from 'angular-calendar/modules/common/calendar-event-actions.component';
import { element } from 'protractor';
import { Program } from 'src/app/Classes/program';
import { Operator } from 'src/app/classes/operator';
import { Setting } from 'src/app/classes/setting';

const colors: any = {
  red: {
    primary: '#ad2121',
    secondary: '#FAE3E3',
  },
  blue: {
    primary: '#1e90ff',
    secondary: '#D1E8FF',
  },
  yellow: {
    primary: '#e3bc08',
    secondary: '#FDF1BA',
  },
};

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  providers: [
    {
      provide: CalendarDateFormatter,
      useClass: CustomDateFormatter
    },

    { provide: MAT_DATE_LOCALE, useValue: 'he-IL' },
    {
      provide: DateAdapter,
      useClass: MomentDateAdapter,
      deps: [MAT_DATE_LOCALE, MAT_MOMENT_DATE_ADAPTER_OPTIONS]
    },
    { provide: MAT_DATE_FORMATS, useValue: MAT_MOMENT_DATE_FORMATS },

    { provide: NgbCalendar, useClass: NgbCalendarHebrew },
    { provide: NgbDatepickerI18n, useClass: NgbDatepickerI18nHebrew }
  ],
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit {


  model: NgbDateStruct;
  programsList: Program[] = [];
  view: string = 'month';
  operator: Operator = new Operator();
  operatorList: Operator[] = [];
  operatorSettings: Setting[] = [];
  settingsList: Setting[] = [];
  viewDate: Date = new Date();

  locale: string = 'he';

  weekStartsOn: number = DAYS_OF_WEEK.SUNDAY;

  weekendDays: number[] = [DAYS_OF_WEEK.SATURDAY];

  viewChange = new EventEmitter<CalendarView>();

  viewDateChange = new EventEmitter<Date>();

  events: CalendarEvent[] = [];

  eventsFromSer: schedule[] = [];
  currentSetting: Setting=new Setting();
currentProgram:Program=new Program();

  setView(view: CalendarView) {
    this.view = view;
  }

  CalendarView = CalendarView;
  @Input() type: number;
  @Input() calendarId: number;

  types = {
    iOperatorId: -1,
    iSettingId: -1,
    iProgramId: -1,
    dDate: null
    // dDate:new Date('11/08/2020')

  };

  program: Program = new Program();
  settingId: number = 0;
  activity: number;
  date: string;
  time: Time;

  // constructor(private mainService: MainServiceService, private calendar: NgbCalendar, public i18n: NgbDatepickerI18n) {
  //   this.dayTemplateData = this.dayTemplateData.bind(this);
  // }
  constructor(private mainService: MainServiceService) {
  }

  // dayTemplateData(date: NgbDate) {
  //   return {
  //     gregorian: (this.calendar as NgbCalendarHebrew).toGregorian(date)
  //   };
  // }

  // selectToday() {
  //   this.model = this.calendar.getToday();
  // }

objName:string;
  ngOnInit() {
    this.operatorList = this.mainService.operatorsList;
    this.programsList = this.mainService.programsList;
    this.settingsList = this.mainService.settingsList;
    this.types[this.type] = this.calendarId;

    if (this.types["iOperatorId"] != -1) {//import the operator by the id
      this.operator = this.mainService.operatorsList.find(x => x.iOperatorId == this.types["iOperatorId"]);
      this.objName=this.operator.nvOperatorName;
    }
    if (this.types["iSettingId"] != -1) {//import the setting by the id
      this.currentSetting = this.mainService.settingsList.find(x => x.iSettingId == this.types["iSettingId"]);
      this.objName='מסגרת '+this.currentSetting.nvSettingName;
    }
    if (this.types["iProgramId"] != -1) {//import the program by the id
      this.currentProgram = this.mainService.programsList.find(x => x.iProgramId ==this.types["iProgramId"]);
      this.objName='תוכנית '+this.currentProgram.nvProgramName;

    }


    this.mainService.post("SchedulesGet", this.types)
      .then(
        res => {

          this.eventsFromSer = res;

          this.eventsFromSer.forEach(element => {
            element.dtStartTime = new Date(parseInt(element.dtStartTime.substr(6))).toString();
            this.events.push({
              title: element.nvProgramValue,
              start: new Date(element.dtStartTime),
            });
          });

          console.log(this.events);

        },
        err => {
          alert("err SchedulesGet")
        }
      )


    



    this.mainService.settingsList.forEach(element => {//fill the settings list where the op active.
      if (!this.operator.lSchoolsExcude.find(x => x == element.iSettingId)) {
        this.operatorSettings.push(element);
      }
    });


  }
  flag: number = 0;
  ps:Setting;
  fillLists(str:string) {
    debugger
    //מופעל רק בתוכניות וצהרונים
    //מילוי רשימת מפעילים שעובדים במיסגרת מסוימת שנבחרה לתוכנית
    if (this.settingId != 0 && this.types["iOperatorId"] == -1) {
      //מאתחל את הרשימה הנוכחית
      this.operatorList = new Array<Operator>();
      //עובר על הרשימה של כל המפעילים
      this.mainService.operatorsList.forEach(operator => {
        //בודק לכל מפעיל אם עובד במסגרת הספציפית שנבחרה
        this.flag = operator.lSchoolsExcude.findIndex(s => s == this.settingId);
        if (this.flag == -1) {
          //מוסיף לרשימה הנוכחית
          this.operatorList.push(operator);
        }
      });
    }
    //מופעל רק במפעילים
    //מילוי רשימת מסגרות שתואמות לתוכנית שנבחרה
    if (this.eventToEdit.iProgramId != -1 && str=='program' && this.types["iSettingId"] == -1) {
      //מאתחל את הרשימה הנוכחית
      this.settingsList = new Array<Setting>();
      //של המסגרות שמתאימות לתוכנית Idעובר על רשימת ה
      this.program.lProgramSettings.forEach(p => {
        //מקבל את המסגרת עצמה בתור אוביקט
        this.ps=this.mainService.settingsList.find(s => s.iSettingId == p);
        if (this.ps.iSettingId != -1) {
          //מוסיף לרשימה הנוכחית
          this.settingsList.push(this.ps);
        }
      }
      );
    }

  }
  dtStartTime:string="";
  editEvent(e:schedule){
    debugger
    this.eventToEdit=e;
    this.dtStartTime=this.eventToEdit.dtStartTime.substr(16,5);
    alert(this.eventToEdit.iActivityId);
  }
  
  // resetArray() {
  //   this.eventsArrayByDate = new Array<schedule>();
  // }

  eventsArrayByDate: schedule[] = [];
  eventToEdit:schedule=new schedule();
  dayDetails: string;

  getShortDate(date: Date) {
    let hours = date.getHours();
    debugger
    let mnth = ("0" + (date.getMonth() + 1)).slice(-2),
      day = ("0" + date.getDate()).slice(-2);
    return [date.getFullYear(), mnth, day].join("-");
  }
  createArrayForDetails(date: Date)//יצירת מערך להצגת פרטי אירועים ליום מסויים שנבחר
  {
    debugger
    this.dayDetails = this.getShortDate(date);
      this.eventsArrayByDate=[];

    this.eventsFromSer.forEach(element => {

      if (this.getShortDate(new Date(element.dtStartTime)) == this.dayDetails)
        this.eventsArrayByDate.push(element);

    });

    console.log(this.eventsArrayByDate);

  }

  dayViewHour({ date, locale }: DateFormatterParams): string {
    return formatDate(date, 'HH:mm', locale);
  }

  weekViewHour({ date, locale }: DateFormatterParams): string {
    return this.dayViewHour({ date, locale });
  }

  addEvent() {
  }
}
