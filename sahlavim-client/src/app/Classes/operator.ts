export class Operator {
    public iOperatorPaymentType: number;
    public nvContactPersonMail: string;
    public nvContactPersonPhone: string;
    public bTalan: boolean;
    public iNumActivityDays: number;
    public iNumActivityWeek: number;
    public nvFilePathTax: string;
    public nvFilePathBooks: string;
    public nvFilePathContract: string;
    public bActivityPriority: boolean;
    public iCreateByUserId: number;
    public CreateDate: Date;
    public iLastModifyUserId: number;//לבדוק מה זה
    public iLastModifyDate: number;//לבדוק מה זה
    public iSysRowStatus: number;//לבדוק מה זה
    public ibookkeepingNum: number;
    public bActiveAfternoon: boolean;
    public bActiveChanukahCamp: boolean;
    public bActivePesachCamp: boolean;
    public bActiveSummerCamp: boolean;
    public bLeadersNum: boolean;

    constructor(
        public iOperatorId: number=1,
        public nvOperatorName: string="",
        public nvContactPerson: string="",
        public iOperatorType: number=0,
        public nvCompanyName: string="",
        public nvIdentity: string="",
        public nvOperatorNumber: string="",
        public binProgramsDatabase: boolean=true,
        public nvOperatorTypeValue:string="",
        public nvActivityies:string="",
        public bInProgramPool:boolean=true) 
        {}
}
