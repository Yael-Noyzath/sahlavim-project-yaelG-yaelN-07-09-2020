import { Activity } from './activity';

export class Operator {
  
 
    public bActivityPriority: boolean;
    public iCreateByUserId: number;
    public CreateDate: Date;
    public iLastModifyUserId: number;//לבדוק מה זה
    public iLastModifyDate: number;//לבדוק מה זה
    public iSysRowStatus: number;//לבדוק מה זה
    public bActiveAfternoon: boolean;
    public bActiveChanukahCamp: boolean;
    public bActivePassoverCamp: boolean;
    public bActiveSummerCamp: boolean;
    public iNumLeaders: number;

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
        public bInProgramPool:boolean=true,
        public iNumBookkeeping: number=0,
        public iNumOperationsDay: number=0,
        public iNumOperationsWeek: number=0,
        public lActivity:Activity[]=[],
        public lNeighborhoods:number[]=[],
        public lSchoolsExcude:number[]=[],
        public lSchools:number[]=[],
        public bTalan: boolean=true,
        public iOperatorPaymentType: number=0,
        public nvContactPersonMail: string="",
        public nvContactPersonPhone: string="",
        public nvFilePathTax: string="",
        public nvFilePathBooks: string="",
        public nvFilePathContract: string="",



        ) 
        {}

      
      
        // lActivity: (2) [{…}, {…}]
        // lNeighborhoods: []
        // lSchools: []
        // lSchoolsExcude: []
        // missing: null
        // numactivities: null
        // nvActivityies: "חיות, "
        // nvCompanyName: "חיות וחיוכים"
        // nvContactPerson: "אירנה"
        // nvContactPersonMail: "irena-a@bezeqint.net"
        // nvContactPersonPhone: "050-5633522"
        // nvFilePathBooks: null
        // nvFilePathContract: null
        // nvFilePathTax: null
        // nvIdentity: "023762280"
        // nvOperatorName: "אירנה אפרייט"
        // nvOperatorNumber: "615698"
        // weekday: null
}
