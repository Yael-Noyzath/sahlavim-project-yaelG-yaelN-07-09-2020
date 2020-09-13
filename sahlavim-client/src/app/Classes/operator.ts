export class Operator {
    constructor(
        public OperatorId:number,
        public OperatorName:string,
        public CompanyName:string,
        public OperatorNamber:number,
        public OperatorType:number,
        public OperatorPaymentType:number,
        public IdentityNumber:string,
        public ContactPerson:string,
        public ContactPersonEmail:string,
        public ContactPersonPhone:string,
        public inProgramsDatabase:boolean,
        public Talan:boolean,
        public NumActivityDays:number,
        public NumActivityWeek:number,
        public FilePathTax:string,
        public FilePathBooks:string,
        public FilePathContract:string,
        public ActivityPriority:boolean,
        public CreateByUserId:number,
        public CreateDate:Date,
        public LastModifyUserId:number,//לבדוק מה זה
        public LastModifyDate:number,//לבדוק מה זה
        public SysRowStatus:number,//לבדוק מה זה
        public bookkeepingNum:number,
        public NoonActivity:boolean,
        public ActiveChanukaCamp:boolean,
        public ActivePesachCamp:boolean,
        public ActiveSummerCamp:boolean,
        public LeadersNum:boolean,

     

        ){

    }
}
