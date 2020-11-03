import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Program } from 'src/app/Classes/program';
import { MainServiceService } from 'src/app/services/MainService/main-service.service';

@Component({
  selector: 'app-program-details',
  templateUrl: './program-details.component.html',
  styleUrls: ['./program-details.component.css']
})
export class ProgramDetailsComponent implements OnInit {

  currentProgram: Program = new Program();
  formProgram: FormGroup;
  lProgramAgegroupsValue: Map<number, string> = new Map<number, string>();
  lProgramTypeValue: Map<number, string> = new Map<number, string>();
  selectAllProgramAgegroups: boolean = false;
  cancelAllProgramAgegroups: boolean = false;

  constructor(private mainService: MainServiceService) {
    this.currentProgram = this.mainService.programForDetails;
    this.lProgramTypeValue = mainService.SysTableList[9];
    this.lProgramAgegroupsValue = mainService.SysTableList[6];
    this.programControls();
  }


  ngOnInit() {
  }

  programControls() {
    this.formProgram = new FormGroup({
      iProgramId: new FormControl(this.currentProgram.iProgramId),
      iProgramType: new FormControl(this.currentProgram.iProgramType),
      nvProgramName: new FormControl(this.currentProgram.nvProgramName),
      //dFromDateFormat: new FormControl(this.currentProgram.dFromDate),
      //dToDateFormat: new FormControl(this.currentProgram.dToDateFormat),
      dFromDate: new FormControl(this.currentProgram.dFromDate),
      dToDate: new FormControl(this.currentProgram.dToDate),
      // lengthProgramSettings: new FormControl(this.currentProgram.lengthProgramSettings),
      // lProgramAgegroupsValue: new FormControl(this.currentProgram.lProgramAgegroupsValue),
      // lSettingAgegroupsValue: new FormControl(this.currentProgram.lSettingAgegroupsValue),
      nvBudgetItem: new FormControl(this.currentProgram.nvBudgetItem),
      lProgramAgegroups: new FormControl(this.currentProgram.lProgramAgegroups),
      iNumActivityMorning: new FormControl(this.currentProgram.iNumActivityMorning),
      iNumActivityAfternoon: new FormControl(this.currentProgram.iNumActivityAfternoon),
      iActivityPreferenceCount: new FormControl(this.currentProgram.iActivityPreferenceCount),
      iActivityPreferenceInWeekCount: new FormControl(this.currentProgram.iActivityPreferenceInWeekCount),
      tFromTimeMorning: new FormControl(this.currentProgram.tFromTimeMorning),
      tToTimeMorning: new FormControl(this.currentProgram.tToTimeMorning),
      tFromTimeAfternoon: new FormControl(this.currentProgram.tFromTimeAfternoon),
      tToTimeAfternoon: new FormControl(this.currentProgram.tToTimeAfternoon),
      bTwoActivitiesThatDay: new FormControl(this.currentProgram.bTwoActivitiesThatDay),
    });
  }
  get iProgramId() {
    return this.formProgram.get("iProgramType");
  }
  get iProgramType() {
    return this.formProgram.get("iProgramType");
  }
  get nvProgramName() {
    return this.formProgram.get("nvProgramName");
  }
  get dFromDate() {
    return this.formProgram.get("dFromDate");
  }
  get dToDate() {
    return this.formProgram.get("dToDate");
  }
  get nvBudgetItem() {
    return this.formProgram.get("nvBudgetItem");
  }
  get lProgramAgegroups() {
    return this.formProgram.get("lProgramAgegroups");
  }
  get iNumActivityMorning() {
    return this.formProgram.get("iNumActivityMorning");
  }
  get iNumActivityAfternoon() {
    return this.formProgram.get("iNumActivityAfternoon");
  }
  saveProgram() {
    this.mainService.post("ProgramInsertUpdate", { oProgram: this.currentProgram, iUserId: this.mainService.currentUser.iUserId }).then(
      res => {
        alert(res)
      },
      err => {
        alert("saveProgram err");
      }
    )
    //לאחר שעידכנו מיסגרת צריך לישלוף מחדש מהסרויס את המיסגרת המעודכנת.
    this.mainService.getPrograms();
  }


  testDate() {
    this.currentProgram = this.formProgram.value;
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
        this.selected= this.selected = this.currentProgram.lProgramAgegroups.includes(s)
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
}
