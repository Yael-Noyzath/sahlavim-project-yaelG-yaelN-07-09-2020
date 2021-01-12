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
import { CustomDateFormatter } from './../calendar/custom-date-formatter.provider';
import { formatDate } from '@angular/common';
import { MainServiceService } from 'src/app/services/MainService/main-service.service';

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
    { provide: NgbCalendar, useClass: NgbCalendarHebrew },
    { provide: NgbDatepickerI18n, useClass: NgbDatepickerI18nHebrew }
  ],
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit {


  model: NgbDateStruct;

  constructor(private mainService: MainServiceService, private calendar: NgbCalendar, public i18n: NgbDatepickerI18n) {
    this.dayTemplateData = this.dayTemplateData.bind(this);
  }

  dayTemplateData(date: NgbDate) {
    return {
      gregorian: (this.calendar as NgbCalendarHebrew).toGregorian(date)
    };
  }

  selectToday() {
    this.model = this.calendar.getToday();
  }


  view: string = 'month';

  viewDate: Date = new Date();

  locale: string = 'he';

  weekStartsOn: number = DAYS_OF_WEEK.SUNDAY;

  weekendDays: number[] = [DAYS_OF_WEEK.SATURDAY];

  viewChange = new EventEmitter<CalendarView>();

  viewDateChange = new EventEmitter<Date>();

  setView(view: CalendarView) {
    this.view = view;
  }

  CalendarView = CalendarView;
  @Input() calendarName: string;
  @Input() calendarId: number;


  ngOnInit() {
    alert(this.calendarId);
    alert(this.calendarName);
    this.mainService.post("SchedulesGet", { iOperatorId: this.calendarId, iSettingId: -1, iProgramId: -1, dDate: null })
      .then(
        res => {
          this.events = res;
          alert(this.events.length);
        },
        err => {
          alert("errrrrrrrr SchedulesGet")
        }
      )
  }

  dayViewHour({ date, locale }: DateFormatterParams): string {
    return formatDate(date, 'HH:mm', locale);
  }

  weekViewHour({ date, locale }: DateFormatterParams): string {
    return this.dayViewHour({ date, locale });
  }

  events: CalendarEvent[];

}