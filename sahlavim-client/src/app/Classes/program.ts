import { time } from 'console';

export class Program {
    constructor(public iProgramId: number = 0,
        public iProgramType: number = 0,
        public nvProgramName: string = "",
        public nvBudgetItem: string = "",
        public dFromDate: string ="1/1/2020",
        public dToDate: string="1/1/20",
        public iNumActivityMorning: number = 0,
        public iNumActivityAfternoon: number = 0,
        public iActivityPreferenceCount: number = 0,
        public iActivityPreferenceInWeekCount: number = 0,
        public tFromTimeMorning: string = new Date("00:00").toTimeString(),
        public tToTimeMorning: string = new Date("00:00").toTimeString(),
        public tFromTimeAfternoon: string =  new Date("00:00").toTimeString(),
        public tToTimeAfternoon: string = new Date("00:00").toTimeString(),
        public bTwoActivitiesThatDay: boolean = false,
        public iStatusType: number = 0,
        public CreateByUserId: number = 0,
        public CreateDate: string ="",
        public LastModifyUserId: number = 0,
        public iSysRowStatus: number = 0,
        public bProgramAfternoon: boolean = false,
        public iSemesterType: number =94,
        public tFirstActivity: string= new Date("00:00").toTimeString(),
        public tSecondActivity: string= new Date("00:00").toTimeString(),
        public iYearType: number = 64,
        public iNumActivityInWeek: number = 0,
        public lProgramAgegroups: Array<number> = new Array<number>(),
        public lProgramSettings: Array<number> = new Array<number>(),
    ) { }

}