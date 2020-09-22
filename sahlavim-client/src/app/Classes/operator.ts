export class Operator {
    public OperatorPaymentType: number;
    public ContactPersonEmail: string;
    public ContactPersonPhone: string;
    public Talan: boolean;
    public NumActivityDays: number;
    public NumActivityWeek: number;
    public FilePathTax: string;
    public FilePathBooks: string;
    public FilePathContract: string;
    public ActivityPriority: boolean;
    public CreateByUserId: number;
    public CreateDate: Date;
    public LastModifyUserId: number;//לבדוק מה זה
    public LastModifyDate: number;//לבדוק מה זה
    public SysRowStatus: number;//לבדוק מה זה
    public bookkeepingNum: number;
    public NoonActivity: boolean;
    public ActiveChanukaCamp: boolean;
    public ActivePesachCamp: boolean;
    public ActiveSummerCamp: boolean;
    public LeadersNum: boolean;

    constructor(
        public OperatorId: number,
        public OperatorName: string,
        public ContactPerson: string,
        public OperatorType: number,
        public CompanyName: string,
        public IdentityNumber: string,
        public OperatorNamber: string,
        public inProgramsDatabase: boolean,
        public OperatorNamber: string
    ) {

    }
}
