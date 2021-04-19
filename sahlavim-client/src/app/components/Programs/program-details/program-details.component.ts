import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { da } from 'date-fns/locale';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { Program } from 'src/app/Classes/program';
import { Setting } from 'src/app/Classes/setting';
import { MainServiceService, forSelect } from 'src/app/services/MainService/main-service.service';

@Component({
  selector: 'app-program-details',
  templateUrl: './program-details.component.html',
  styleUrls: ['./program-details.component.css']
})

export class ProgramDetailsComponent implements OnInit {

  lProgramAgegroupsValue: forSelect[] = [];
  ProgramAgegroupsListNg: forSelect[] = [];
  dropdownProgramAgegroups: IDropdownSettings;


  currentProgram: Program = new Program();
  formProgram: FormGroup;
  lProgramTypeValue: forSelect[] = [];

  //מקור הנתונים לטבלה של המסגרות
  dataSource: MatTableDataSource<Setting>;
  displayedColumns: string[] = ['check', 'nvSettingName', 'nvAddress', 'lSettingAgegroups'];
  settingList: Array<Setting>;
  lProgramAgegroupsValueForTable: Map<number, string> = new Map<number, string>();
  elementRef: any;

  constructor(private mainService: MainServiceService) {
    this.currentProgram = this.mainService.programForDetails;
    debugger
    this.lProgramTypeValue = mainService.gItems[9].dParams;
    this.lProgramAgegroupsValueForTable = mainService.SysTableList[6];
    this.settingList = mainService.settingsList;
    this.dataSource = new MatTableDataSource(this.settingList);
  }

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;


  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnInit() {
    this.lProgramAgegroupsValue = this.mainService.gItems[6].dParams;

    // איתחול רשימת הגילאים של התוכנית 
    if (this.currentProgram.lProgramAgegroups.length > 0) {
      for (let nlId of this.currentProgram.lProgramAgegroups) {
        this.ProgramAgegroupsListNg.push(this.lProgramAgegroupsValue.find(x => x.Key == nlId));
      }
    }

    //הגדרות ה multi select
    this.dropdownProgramAgegroups = {
      singleSelection: false,
      idField: 'Key',
      textField: 'Value',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 5,
      allowSearchFilter: true
    };

    this.currentProgram.tFromTimeMorning = new Date(parseInt(this.currentProgram.tFromTimeMorning.replace(/\/+Date\(([\d+-]+)\)\/+/, '$1'))).toString();
    this.currentProgram.tToTimeMorning = new Date(parseInt(this.currentProgram.tToTimeMorning.replace(/\/+Date\(([\d+-]+)\)\/+/, '$1'))).toString();
    this.currentProgram.tFromTimeAfternoon = new Date(parseInt(this.currentProgram.tFromTimeAfternoon.replace(/\/+Date\(([\d+-]+)\)\/+/, '$1'))).toString();
    this.currentProgram.tToTimeAfternoon = new Date(parseInt(this.currentProgram.tToTimeAfternoon.replace(/\/+Date\(([\d+-]+)\)\/+/, '$1'))).toString();

    this.ngAfterViewInit();

  }
  Hebcal = require('hebcal');
  rightFromDate: string = null;

  rightToDate: string = null;
  checkValidDate(fromTo: number, date: Date) {//30  קייטנת חנוכה 
    //31 פסח
    //32 קיץ
    //  date= new Date(5780, 5, 7, new HebrewCalendar())
    let newDate = new Date(date);

    //בדיקת תאריך קייטנת חנוכה, 23/3
    if (this.currentProgram.iProgramType == 30) {
      {
        //get the hebrew date
        let day = new this.Hebcal.HDate(newDate);
        //if not Kislev month
        if (day.getMonth() != 3) {
          // date =  this.Hebcal.HDate.hebrew2abs('1 Kislev');
          if (fromTo == 1)// אם תאריך התחלתי
          {
            this.rightFromDate = "נא הזן תאריך בחודש כסליו"

          }
          else {
            this.rightToDate = "נא הזן תאריך בחודש כסליו"

          }
        }

        debugger

        //convert the number date to letters 
        // return this.Hebcal.gematriya(day.getDate());et rightFromDate;
      }
    }



  }


  saveProgram() {
    debugger
    alert(this.currentProgram.lProgramSettings.length)
    this.currentProgram.lProgramAgegroups.splice(0, this.currentProgram.lProgramAgegroups.length)
    //  עידכון רשימת הבתי ספר שלא פעיל לפי הרשימה שנבחרה 
    if (this.ProgramAgegroupsListNg.length > 0) {
      for (let age of this.ProgramAgegroupsListNg)//מעבר על הרשימה שנבחרה
      {
        this.currentProgram.lProgramAgegroups.push(age.Key);
      }
    }
    var lSettingMorning: number[];
    var lSettingNoon: number[];

    this.mainService.post("ProgramSettingsInsertUpdate", {
      iProgramId: this.currentProgram.iProgramId,
      lProgramSettings: this.currentProgram.lProgramSettings,
      lSettingMorning: lSettingMorning,
      lSettingNoon: lSettingNoon,
      iUserId: this.mainService.currentUser.iUserId
    }).then(
      res => {
        alert(res)
      },
      err => {
        alert("err ProgramSettingsInsertUpdate")
      }
    )

    if (this.currentProgram.tFromTimeMorning != null)
      this.currentProgram.tFromTimeMorning = this.currentProgram.tFromTimeMorning.toString();
    if (this.currentProgram.tToTimeMorning != null)
      this.currentProgram.tToTimeMorning = this.currentProgram.tToTimeMorning.toString();
    if (this.currentProgram.tFromTimeAfternoon != null)
      this.currentProgram.tFromTimeAfternoon = this.currentProgram.tFromTimeAfternoon.toString();
    if (this.currentProgram.tToTimeAfternoon != null)
      this.currentProgram.tToTimeAfternoon = this.currentProgram.tToTimeAfternoon.toString();

    this.mainService.post("ProgramInsertUpdate", { oProgram: this.currentProgram, iUserId: this.mainService.currentUser.iUserId }).then(
      res => {
        this.mainService.getPrograms();

      },
      err => {
        alert("saveProgram err+\n+צריך לסדר את השעה והתאריך");
      }
    )
    //לאחר שעידכנו מיסגרת צריך לישלוף מחדש מהסרויס את המיסגרת המעודכנת.
    this.mainService.getPrograms();
  }


  testDate() {
    // if (this.currentProgram.iProgramId > -1 && (this.currentProgram.dFromDate > $scope.dFromDate || this.currentProgram.dToDate < $scope.dToDate))
    //   alert("שים לב  <br />בשמירה ימחקו הפעילויות שהוגדרו מחוץ לטווח התאריכים שצומצם <br /> האם בכל אופן הינך מעונין לשמור ?" + "אזהרה")
    // function () { $scope.saveProgram(); }, function () { return; });
    // else
    this.saveProgram();
  }
  selected: boolean = false;
  isSelected(s: any) {
    // alert(this.currentSetting.lSettingAgegroups.includes(s))
    // if (this.selectAllProgramAgegroups)
    //   return true;
    // else
    //   if (this.cancelAllProgramAgegroups)
    //     return false;
    //   else
    //    {
    this.selected = this.selected = this.currentProgram.lProgramAgegroups.includes(s)
    return true;
    // } 
  }
  // selectAll() {
  //   //alert(this.selectAllProgramAgegroups)
  //   this.selectAllProgramAgegroups = true;
  //   this.cancelAllProgramAgegroups = false;

  // }
  // cancelAll() {
  //   //alert(this.selectAllProgramAgegroups)
  //   this.cancelAllProgramAgegroups = true;
  //   this.selectAllProgramAgegroups = false;

  // }
  //בינתיים
  onItemSelect(item: Program) {
    //this.operator.lSchoolsExcude.push(item.iSettingId);//הוספה לרשימה של האופרטור
    console.log(item);
    //console.log(this.ProgramAgegroupsListNg);
  }
  OnItemDeSelect(item: Program) {
    //this.operator.lSchoolsExcude.splice(item.iSettingId, 1);//מחיקה מהרשימה של האופרטור

    console.log(item);
    //console.log(this.ProgramAgegroupsListNg);
  }
  onSelectAll(items: any) {

    console.log(items);
  }
  onDeSelectAll(items: any) {
    console.log(items);
  }
  checkedSettings(settingId: number) {
    //בודק אם כבר קיים
    var index = this.currentProgram.lProgramSettings.findIndex(x => x == settingId);

    if (index != -1) {//אם קיים סימן שרוצה להסיר ולכן מוציא מהמערך
      this.currentProgram.lProgramSettings.splice(index, 1);
      alert(this.currentProgram.lProgramSettings.length + " remove")
    }
    else {  //אם לא קיים סימן שרוצה להוסיף ולכן מכניס למערך
      this.currentProgram.lProgramSettings.push(settingId);
      alert(this.currentProgram.lProgramSettings.length + " add")
    }
    debugger
  }
  removeAlSetting() {
    //צריך לטפל במקרה הזה
    if (confirm("שים לב" + "\n" + "באם תשמור שינוי זה ימחקו כל הפעילויות המשובצות למסגרת זו לצהריים בתוכנית זו ובשאר תוכניות" + "\n האם אתה בטוח?"))
      alert("נמחק")
  }
  ifChecked(id: number) {
    if (this.currentProgram.lProgramSettings.findIndex(x => x == id) == -1)
      return false;
    return true;
  }
  h: boolean = false;



  checkFormValid() {
    //check if no mat-hint with context 
    const list = document.querySelectorAll<HTMLInputElement>("mat-hint");

    list.forEach(function (Item) {
      if (Item.innerHTML != '') {
        alert('נא שים לב לתוכן תקין');
        this.h = true;
        return false
      }
    });

    debugger

    if (this.h == false) {
      this.testDate()
    }
  }
}
