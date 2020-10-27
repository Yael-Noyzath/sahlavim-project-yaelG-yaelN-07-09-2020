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

  currentProgram: Program;
  formProgram: FormGroup;

  constructor(mainService: MainServiceService) {
    this.currentProgram = mainService.programForDetails;
    this.programControls();
  }

  ngOnInit() {
  }
  
  programControls() {
    this.formProgram = new FormGroup({
      iProgramType: new FormControl(this.currentProgram.iProgramType),
      //nvProgramTypeValue: new FormControl(this.currentProgram.nvProgramTypeValue),
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
  saveChange() {

  }
}
