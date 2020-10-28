import { Time } from '@angular/common';

export class Program {
    constructor(public iProgramId: number=-1,
        public iProgramType: number=-1,
        public nvProgramName: string="",
        public nvBudgetItem: string="",//סעיף תקציב
        public dFromDate: Date=new Date('01/01/01'),
        public dToDate: Date=new Date(),
        public iNumActivityMorning: number=-1,//מיספר הפעלות בוקר
        public iNumActivityAfternoon: number=-1,//מיספר הפעלות צהרים
        public iActivityPreferenceCount: number=-1,//מספר הפעלות מועדפות
        public iActivityPreferenceInWeekCount: number=-1,//מספר הפעלות מועדפות לשבוע
        public tFromTimeMorning: Date=new Date(),
        public tToTimeMorning: Date=new Date(),
        public tFromTimeAfternoon: Date=new Date(),
        public tToTimeAfternoon: Date=new Date(),
        public bTwoActivitiesThatDay: boolean=false,
        public iStatusType: number=-1,
        public CreateByUserId: number=-1,
        public CreateDate: Date=new Date(),
        public LastModifyUserId: number=-1,//קוד מישתמש שינוי אחרון
        public iSysRowStatus: number=-1,//לבדוק מה זה
        public bProgramAfternoon: boolean=false,//האם תוכנית צהרים
        public iSemesterType: number=-1,
       // public tFirstActivity: Date=new Date(),
       // public tSecondActivity: Date=new Date(),
        public iYearType: number=-1,//לבדוק
        public iNumActivityInWeek: number=-1,
        public lProgramAgegroups: Array<number>=new Array<number>(),

    ) { }
   
}
