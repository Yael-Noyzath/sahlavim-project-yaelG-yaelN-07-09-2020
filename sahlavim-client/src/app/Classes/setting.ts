import { LongTapEvent } from 'ag-grid-community';

export class Setting {

    constructor(
        public iSettingId: number=-1,
        public nvSettingName: string="",
        public nvSettingCode: string="",
        public nvSettingTypeValue: number=-1,
        public nvLat: string="",
        public nvLng: string="",
        public nvAddress: string="",
        public nvOperatingLocation: string="",
        public nvNeighborhood: string="",
        public nvPhone: string="",
        public nvContactPerson: string="",
        public nvContactPersonMail: string="",
        public nvContactPersonPhone: string="",
        public iCoordinatorId: number=-1,
        public iCreateByUserId: number=-1,
        public dCreateDate: Date=new Date(),
        public iLastModifyUserId: number=-1,//קוד מישתמש שינוי אחרון
        public iLastModifyDate: number=-1,//לבדוק מה זה
        public iSysRowStatus: number=-1,
        public bSettingMorning: boolean=false,
        public bSettingNoon: boolean=false,
        public bActiveAfternoon: boolean=false,
        public bisJoint: boolean=false,
        public lSettingAgegroupsValue: Array<number>=new Array<number>(),
    ) { 
    }
    
}