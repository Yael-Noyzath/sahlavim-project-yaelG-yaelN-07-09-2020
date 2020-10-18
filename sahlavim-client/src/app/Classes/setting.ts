import { LongTapEvent } from 'ag-grid-community';

export class Setting {

    constructor(
        public iSettingId: number,
        public nvSettingName: string,
        public nvSettingCode: string,
        public nvSettingTypeValue: number,
        public nvLat: string,
        public nvLng: string,
        public nvAddress: string,
        public nvOperatingLocation: string,
        public nvNeighborhood: string,
        public nvPhone: string,
        public nvContactPerson: string,
        public nvContactPersonMail: string,
        public nvContactPersonPhone: string,
        public iCoordinatorId: number,
        public iCreateByUserId: number,
        public dCreateDate: Date,
        public iLastModifyUserId: number,//קוד מישתמש שינוי אחרון
        public iLastModifyDate: number,//לבדוק מה זה
        public iSysRowStatus: number,
        public bisMorningSetting: boolean,
        public bisNoonSetting: boolean,
        public bisAternoon: boolean,
        public bisJoint: boolean,
        public lSettingAgegroupsValue: number,

        
        public nvFullName : string,
        public nvPhoneCoordinator : string,
        public nvMail : string,
    ) {

    }
    
}