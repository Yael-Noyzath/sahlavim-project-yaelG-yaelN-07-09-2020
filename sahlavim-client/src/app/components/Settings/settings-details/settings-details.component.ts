import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
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
  formSetting: FormGroup;

  constructor(private route: ActivatedRoute, private mainService: MainServiceService) { }

  ngOnInit() {
    // this.idSetting = parseInt(this.route.snapshot.paramMap.get('id'));
    this.currentSetting = this.mainService.settingForDetails;
    this.settingControls();
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
      nvSettingTypeValue: new FormControl(this.currentSetting.nvSettingTypeValue),
      nvAddress: new FormControl(this.currentSetting.nvAddress),
      nvPhone: new FormControl(this.currentSetting.nvPhone),
      lSettingAgegroupsValue: new FormControl(this.currentSetting.lSettingAgegroupsValue),
      nvOperatingLocation: new FormControl(this.currentSetting.nvOperatingLocation),
      nvContactPerson: new FormControl(this.currentSetting.nvContactPerson),
      nvContactPersonPhone: new FormControl(this.currentSetting.nvContactPersonPhone),
      nvContactPersonMail: new FormControl(this.currentSetting.nvContactPersonMail)

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
  saveChange() {
    alert("saveChange")
  }
}
