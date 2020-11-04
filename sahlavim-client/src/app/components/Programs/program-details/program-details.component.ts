import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { Program } from 'src/app/Classes/program';
import { MainServiceService, row } from 'src/app/services/MainService/main-service.service';

@Component({
  selector: 'app-program-details',
  templateUrl: './program-details.component.html',
  styleUrls: ['./program-details.component.css']
})

export class ProgramDetailsComponent implements OnInit {

  ProgramAgegroupsList:row[]=[];
  dropdownProgramAgegroups: IDropdownSettings;


  currentProgram: Program = new Program();
  formProgram: FormGroup;
  lProgramAgegroupsValue: Map<number, string> = new Map<number, string>();
  lProgramTypeValue: Map<number, string> = new Map<number, string>();
  selectAllProgramAgegroups: boolean = false;
  cancelAllProgramAgegroups: boolean = false;
  constructor(private mainService: MainServiceService) {
    this.currentProgram = this.mainService.programForDetails;
    this.lProgramTypeValue = mainService.SysTableList[9];
  }


  ngOnInit() { 
    this.lProgramAgegroupsValue = this.mainService.gItems[6].dParams;

    //הגדרות ה multi select
    this.dropdownProgramAgegroups = {
      singleSelection: false,
      idField: 'iProgramId',
      textField: 'nvProgramName',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 3,
      allowSearchFilter: true
    };
  }



  saveProgram() {
    this.mainService.post("ProgramInsertUpdate", { oProgram: this.currentProgram, iUserId: this.mainService.currentUser.iUserId }).then(
      res => {
        this.mainService.getPrograms();
        alert("update " + this.currentProgram.nvProgramName + " done!");
        this.mainService.serviceNavigate("/header-menu/programs/programs-table");

      },
      err => {
        alert("saveProgram err");
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
    console.log(this.ProgramAgegroupsList);
  }
  OnItemDeSelect(item: Program) {
    //this.operator.lSchoolsExcude.splice(item.iSettingId, 1);//מחיקה מהרשימה של האופרטור

    console.log(item);
    console.log(this.ProgramAgegroupsList);
  }
  onSelectAll(items: any) {

    console.log(items);
  }
  onDeSelectAll(items: any) {
    console.log(items);
  }
}
