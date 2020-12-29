import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { CalendarEvent, CalendarView, DAYS_OF_WEEK , CalendarDateFormatter} from 'angular-calendar';
import { CustomDateFormatter } from './custom-date-formatter.provider';


@Component({
  selector: 'app-operator-schedule',
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './operator-schedule.component.html',
  styleUrls: ['./operator-schedule.component.css'],
  providers: [
    {
      provide: CalendarDateFormatter,
      useClass: CustomDateFormatter,
    },
  ], 
})
export class OperatorScheduleComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }
  view: CalendarView = CalendarView.Month;

  viewDate = new Date();

  events: CalendarEvent[] = [];

  locale: string = 'fr';

  weekStartsOn: number = DAYS_OF_WEEK.MONDAY;

  weekendDays: number[] = [DAYS_OF_WEEK.FRIDAY, DAYS_OF_WEEK.SATURDAY];

  CalendarView = CalendarView;

  setView(view: CalendarView) {
    this.view = view;
  }
}
