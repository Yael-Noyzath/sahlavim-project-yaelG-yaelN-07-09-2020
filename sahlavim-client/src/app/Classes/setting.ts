export class Setting {
    
    constructor(
        public SettingId:number,
        public SettingName:string,
        public SettingCode:string,
        public SettingType:number,
        public Lat:string,
        public Lng:string,
        public Adress:string,
        public OperatingLocation:string,
        public Neighborhood:string,
        public Phone:string,
        public ContactPerson:string,
        public ContactPersonEmail:string,
        public ContactPersonPhone:string,
        public CoordinatorId:number,
        public CreateByUserId:number,
        public CreateDate:Date,
        public LastModifyUserId:number,//קוד מישתמש שינוי אחרון
        public LastModifyDate:number,//לבדוק מה זה
        public SysRowStatus:number,
        public isMorningSetting:boolean,
        public isNoonSetting:boolean,
        public isAternoon:boolean,
        public isJoint:boolean

    ){

    }
}
