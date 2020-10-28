import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { coordinator } from 'src/app/Classes/coordinator';
import { Setting } from 'src/app/Classes/setting';
import { MainServiceService } from 'src/app/services/MainService/main-service.service';


@Component({
  selector: 'app-settings-details',
  templateUrl: './settings-details.component.html',
  styleUrls: ['./settings-details.component.css']
})
export class SettingsDetailsComponent implements OnInit {

  idSetting: number;
  settingList: Array<Setting>;
  currentSetting: Setting = new Setting();
  currentCoordinator: coordinator = new coordinator();
  coordinatorList: Array<coordinator>;
  formSetting: FormGroup;

  constructor(private mainService: MainServiceService) {
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
          this.currentCoordinator = this.coordinatorList.find(c => c.iCoordinatorId == this.currentSetting.iCoordinatorId)
          this.settingControls();
      },
      err => {
        alert("CoordinatorsGet err")
      }
    );
  }

  // SettingsGet() {
  //   this.mainService.post("SettingsGet", {}).then(
  //     res => {
  //       this.settingList = res;
  //       this.currentSetting = this.settingList.find(s => s.iSettingId == this.idSetting);
  //       this.settingControls();
  //     },
  //     err => {
  //       alert("SettingsGet err")
  //     }
  //   );
  // }
  save() {

  }
  settingControls() {
    this.formSetting = new FormGroup({
      nvSettingName: new FormControl(this.currentSetting.nvSettingName),
      nvSettingCode: new FormControl(this.currentSetting.nvSettingCode),
      iSettingId: new FormControl(this.currentSetting.iSettingId),
      nvSettingTypeValue: new FormControl(this.currentSetting.iSettingType),
      nvAddress: new FormControl(this.currentSetting.nvAddress),
      nvPhone: new FormControl(this.currentSetting.nvPhone),
      lSettingAgegroupsValue: new FormControl(this.currentSetting.lSettingAgegroups),
      nvOperatingLocation: new FormControl(this.currentSetting.nvOperatingLocation),
      nvContactPerson: new FormControl(this.currentSetting.nvContactPerson),
      nvContactPersonPhone: new FormControl(this.currentSetting.nvContactPersonPhone),
      nvContactPersonMail: new FormControl(this.currentSetting.nvContactPersonMail),
      bSettingMorning: new FormControl(this.currentSetting.bSettingMorning),
      bSettingNoon: new FormControl(this.currentSetting.bSettingNoon),
      bActiveAfternoon: new FormControl(this.currentSetting.bActiveAfternoon),
    });
  }

  get nvUserName() {
    return this.formSetting.get("nvSettingName");
  }
  get nvSettingCode() {
    return this.formSetting.get("nvSettingCode");
  }
  get nvPassword() {
    return this.formSetting.get("nvAddress");
  }
  get nvMail() {
    return this.formSetting.get("nvPhone");
  }
  get lSettingAgegroupsValue() {
    return this.formSetting.get("lSettingAgegroupsValue");
  }
  get iSettingId() {
    return this.formSetting.get("iSettingId");
  }
  get iSettingType() {
    return this.formSetting.get("iSettingType");
  }
  get nvOperatingLocation() {
    return this.formSetting.get("nvOperatingLocation");
  }
  get nvContactPerson() {
    return this.formSetting.get("nvContactPerson");
  }
  get nvContactPersonPhone() {
    return this.formSetting.get("nvContactPersonPhone");
  }
  get nvContactPersonMail() {
    return this.formSetting.get("nvContactPersonMail");
  }
  get bSettingMorning() {
    return this.formSetting.get("bSettingMorning");
  }
  get bSettingNoon() {
    return this.formSetting.get("bSettingNoon");
  }
  get bActiveAfternoon() {
    return this.formSetting.get("bActiveAfternoon");
  }
  saveChange() {
    alert("saveChange");
    //לאחר שעידכנו מיסגרת צריך לישלוף מחדש מהסרויס את המיסגרת המעודכנת.
    this.mainService.getSettings();
  }
}
