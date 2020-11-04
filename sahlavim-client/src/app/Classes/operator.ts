import { Activity } from './activity';

export class Operator {
  
 
    public iCreateByUserId: number;
    public CreateDate: Date;
    public iLastModifyUserId: number;//לבדוק מה זה
    public iLastModifyDate: number;//לבדוק מה זה
    public iSysRowStatus: number;//לבדוק מה זה

    constructor(
        public lSchools:number[]=[],

        public iOperatorId: number=1,
        public nvOperatorName: string="",
        public nvCompanyName: string="",
        public nvOperatorNumber: string="",
        public iOperatorType: number=0,
        public iOperatorPaymentType: number=0,
        public nvIdentity: string="",
        public nvContactPerson: string="",
        public nvContactPersonMail: string="",
        public nvContactPersonPhone: string="",
        public bInProgramPool:boolean=true,
        public bTalan: boolean=true,
        public iNumOperationsDay: number=0,
        public iNumOperationsWeek: number=0,
        public nvFilePathTax: string="",
        public nvFilePathBooks: string="",
        public nvFilePathContract: string="",
        public iNumLeaders: number=0,
        public bActiveAfternoon: boolean=false,
       public bActiveChanukahCamp: boolean=false,
        public bActivePassoverCamp: boolean=false,
        public bActiveSummerCamp: boolean=false,
        public bActivityPriority: boolean=false,

        public lNeighborhoods:number[]=[],

        public lSchoolsExcude:number[]=[],
 
        public lActivity:Activity[]=[],

        public iNumBookkeeping: number=0,
        public gg: number=0



        // public binProgramsDatabase: boolean=true,
        // public nvOperatorTypeValue:string="",
        // public nvActivityies:string="",

        ) 
        {}
    }
      
