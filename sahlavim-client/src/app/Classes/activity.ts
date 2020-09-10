export class Activity {
    constructor(public ActivityId:number,
        public OperatorId:number,
        public ActivityName:string,
        public CategoryType:number,
        public StatusType:number,
        public ActivityProduct:string,
        public Price:number,
        public ShortBreak:number,
        public LongBreak:number,
        public Points:number,
        public CreateByUserId:number,
        public CreateDate:Date,
        public LastModifyUserId:number,//לבדוק מה זה
        public LastModifyDate:number,//לבדוק מה זה
        public SysRowStatus:number){//לבדוק מה זה

    }
}
