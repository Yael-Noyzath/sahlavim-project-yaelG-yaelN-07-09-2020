import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { coordinator } from 'src/app/Classes/coordinator';
import { Setting } from 'src/app/Classes/setting';
import { MainServiceService, row } from 'src/app/services/MainService/main-service.service';


@Component({
  selector: 'app-settings-details',
  templateUrl: './settings-details.component.html',
  styleUrls: ['./settings-details.component.css']
})
export class SettingsDetailsComponent implements OnInit {


  lSettingAgegroupsValue:row[]=[];
  SettingAgegroupsListNg:row[]=[];
  dropdownSettingAgegroups: IDropdownSettings;

  panelOpenState = false;
  idSetting: number;
  settingList: Array<Setting>;
  currentSetting: Setting = new Setting();
  currentCoordinator: coordinator = new coordinator();
  coordinatorList: Array<coordinator>;
  formSetting: FormGroup;
  lSettingTypeValue: Map<number, string> = new Map<number, string>();
  lNeighborhoodTypeValue: Map<number, string> = new Map<number, string>();

  constructor(private mainService: MainServiceService) {
    this.lNeighborhoodTypeValue = mainService.SysTableList[4];
    this.lSettingTypeValue = mainService.SysTableList[5];
  }

  ngOnInit() {
    // this.idSetting = parseInt(this.route.snapshot.paramMap.get('id'));
    this.currentSetting = this.mainService.settingForDetails;
    this.CoordinatorsGet();
    this.lSettingAgegroupsValue = this.mainService.gItems[6].dParams;

    // איתחול רשימת הגילאים של התוכנית 
    if (this.currentSetting.lSettingAgegroups.length > 0) {
      for (let nlId of this.currentSetting.lSettingAgegroups) {
        this.SettingAgegroupsListNg.push(this.lSettingAgegroupsValue.find(x => x.Key == nlId));
      }
    }
debugger
    //הגדרות ה multi select
    this.dropdownSettingAgegroups = {
      singleSelection: false,
      idField: 'Key',
      textField: 'Value',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 3,
      allowSearchFilter: true
    };
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

  saveSetting() {
    this.currentSetting.lSettingAgegroups.splice(0,this.currentSetting.lSettingAgegroups.length)
    //  עידכון רשימת הגילאים שלא תוכנית לפי הרשימה שנבחרה 
     if (this.SettingAgegroupsListNg.length > 0) {
      for (let age of this.SettingAgegroupsListNg)//מעבר על הרשימה שנבחרה
      {
          this.currentSetting.lSettingAgegroups.push(age.Key);
      }
   }
    debugger
     //alert(this.currentSetting.lSettingAgegroups[0])
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
  // selected: boolean = false;
  // isSelected(s: any) {
  //   // alert(this.currentSetting.lSettingAgegroups.includes(s))
  //   this.selected = this.currentSetting.lSettingAgegroups.includes(s);
  //   return true;
  // }
}
