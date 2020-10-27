export class Activity {
    constructor(
        public iActivityId:number,
        public iOperatorId:number,
        public nvActivityName:string,
        public iCategoryType:number,
        public iStatusType:number,
        public nvActivityProduct:string,
        public nPrice:string,
        public nShortBreak:string,
        public nLongBreak:string,
        // public Points:number,
        // public CreateByUserId:number,
        // public CreateDate:Date,
        // public LastModifyUserId:number,//לבדוק מה זה
        // public LastModifyDate:number,//לבדוק מה זה
        // public SysRowStatus:number,//לבדוק מה זה
       public bActivityPreference:boolean,
    //    public lActivityAgegroups:ActivityAgeGroup[]=[],
       public bActivityMorning:boolean,
      public bActivityNoon:boolean,

      

        ){

    }
}
