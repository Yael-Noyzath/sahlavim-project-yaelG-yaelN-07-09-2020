import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Program } from 'src/app/Classes/program';
import { MainServiceService, row } from 'src/app/services/MainService/main-service.service';

@Component({
  selector: 'app-program-details',
  templateUrl: './program-details.component.html',
  styleUrls: ['./program-details.component.css']
})

export class ProgramDetailsComponent implements OnInit {

  currentProgram: Program = new Program();
  formProgram: FormGroup;
  lProgramAgegroupsValue: Array<row>= new Array<row>();
  lProgramTypeValue:Array<row>= new Array<row>();
  selectAllProgramAgegroups: boolean = false;
  cancelAllProgramAgegroups: boolean = false;
  programTypes:any;
  constructor(private mainService: MainServiceService) {
    this.currentProgram = this.mainService.programForDetails;
    this.lProgramTypeValue = mainService.SysTableList[9];
    this.lProgramAgegroupsValue = mainService.SysTableList[6];
  }


  ngOnInit() {
    this.programTypes=  this.lProgramTypeValue.keys();
debugger
  }


  saveProgram() {

    this.mainService.post("ProgramInsertUpdate", { oProgram: this.currentProgram, iUserId: this.mainService.currentUser.iUserId }).then(
      res => {
        alert("update "+this.currentProgram.nvProgramName+" done!"); 
        this.mainService.getPrograms();
        this.mainService.serviceNavigate("/header-menu/programs/programs-table");
        this.mainService.serviceNavigate("/header-menu/operators/operator-table");

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
}
