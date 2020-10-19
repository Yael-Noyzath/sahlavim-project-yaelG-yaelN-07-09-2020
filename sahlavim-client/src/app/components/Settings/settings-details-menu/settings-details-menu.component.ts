import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Setting } from 'src/app/Classes/setting';
import { MainServiceService } from 'src/app/services/MainService/main-service.service';

@Component({
  selector: 'app-settings-details-menu',
  templateUrl: './settings-details-menu.component.html',
  styleUrls: ['./settings-details-menu.component.css']
})
export class SettingsDetailsMenuComponent implements OnInit {

  idSetting: number;
  settingList: Array<Setting>;
  currentSetting: Setting = new Setting();
  formSetting: FormGroup;

  constructor(private route: ActivatedRoute, private mainService: MainServiceService) { }

  ngOnInit() {
    this.idSetting = parseInt(this.route.snapshot.paramMap.get('id'));

    this.SettingsGet();
  }

  SettingsGet() {
    this.mainService.post("SettingsGet", {}).then(
      res => {
        this.settingList = res;
        this.currentSetting = this.settingList.find(s => s.iSettingId == this.idSetting);
        this.settingControls();

      },
      err => {
        alert("SettingsGet err")
      }
    );
  }
  save() {

  }
  settingControls() {
    this.formSetting = new FormGroup({
      nvSettingName: new FormControl(this.currentSetting.nvSettingName),
      nvAddress: new FormControl(this.currentSetting.nvAddress),
      nvPhone: new FormControl(this.currentSetting.nvPhone)
    });
  }

  get nvUserName() {
    return this.formSetting.get("nvSettingName");
  }
  get nvPassword() {
    return this.formSetting.get("nvAddress");
  }
  get nvMail() {
    return this.formSetting.get("nvPhone");
  }
  saveChange(){
    alert("saveChange")
  }
}
