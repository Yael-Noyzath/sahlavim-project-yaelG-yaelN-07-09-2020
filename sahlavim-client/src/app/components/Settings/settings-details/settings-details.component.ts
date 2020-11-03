import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { coordinator } from 'src/app/Classes/coordinator';
import { Setting } from 'src/app/Classes/setting';
import { MainServiceService, row } from 'src/app/services/MainService/main-service.service';


@Component({
  selector: 'app-settings-details',
  templateUrl: './settings-details.component.html',
  styleUrls: ['./settings-details.component.css']
})
export class SettingsDetailsComponent implements OnInit {

  panelOpenState = false;
  idSetting: number;
  settingList: Array<Setting>;
  currentSetting: Setting = new Setting();
  currentCoordinator: coordinator = new coordinator();
  coordinatorList: Array<coordinator>;
  formSetting: FormGroup;
  lSettingAgegroupsValue:Array<row>= new Array<row>();
  lSettingTypeValue: Array<row>= new Array<row>();
  lNeighborhoodTypeValue: Array<row>= new Array<row>();

  constructor(private mainService: MainServiceService) {
    this.lNeighborhoodTypeValue = mainService.SysTableList[4];
    this.lSettingTypeValue = mainService.SysTableList[5];
    this.lSettingAgegroupsValue = mainService.SysTableList[6];
    debugger
  }

  ngOnInit() {
    // this.idSetting = parseInt(this.route.snapshot.paramMap.get('id'));
    this.CoordinatorsGet();
  }
  CoordinatorsGet() {
    this.mainService.post("CoordinatorsGet", {}).then(
      res => {
        this.coordinatorList = res;
        this.currentSetting = this.mainService.settingForDetails;
        if (this.currentSetting.iCoordinatorId)
          this.currentCoordinator = this.coordinatorList.find(c => c.iCoordinatorId == this.currentSetting.iCoordinatorId);
      },
      err => {
        alert("CoordinatorsGet err")
      }
    );
  }

  save() {

  }

  saveSetting() {

    // alert(this.currentSetting.lSettingAgegroups.includes())
    this.mainService.post("SettingInsertUpdate", { oSetting: this.currentSetting, iUserId: this.mainService.currentUser.iUserId }).then(
      res => {
        //קבלה מהשרת את רשימת מפעילים המעודכנת
        this.mainService.getSettings();
        alert("update " + this.currentSetting.nvSettingName + " done!");

        this.mainService.serviceNavigate("/header-menu/settings/setting-table");
      },
      err => {
        alert("saveSetting err");
      }
    )
  }
  selected: boolean = false;
  isSelected(s: any) {
    // alert(this.currentSetting.lSettingAgegroups.includes(s))
    this.selected = this.currentSetting.lSettingAgegroups.includes(s);
    return true;
  }
}
