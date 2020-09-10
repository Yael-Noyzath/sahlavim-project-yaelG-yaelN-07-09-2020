import { Time } from '@angular/common';

export class Program {
    constructor(public ProgramId:number,
        public ProgramType:number,
        public ProgramName:string,
        public BudgetItem:string,//סעיף תקציב
        public FromDate:Date,
        public ToDate:Date,
        public NumMorningActivities:number,//מיספר הפעלות בוקר
        public NumAfternoonActivities:number,//מיספר הפעלות צהרים
        public NumPreferenceActivities:number,//מספר הפעלות מועדפות
        public NumPreferenceActivitiesInWeek:number,//מספר הפעלות מועדפות לשבוע
        public StartTimeMorning:Time,
        public ToTimeMorning:Time,
        public StartTimeAfternoon:Time,
        public ToTimeAfternoon:Time,
        public areTwoActivitiesInDay:boolean,
        public StatusType:number,
        public CreateByUserId:number,
        public CreateDate:Date,
        public 


    ){}
  
}
