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
  id: string;
  setting: Setting;

  constructor(private route: ActivatedRoute, private mainService: MainServiceService) {
    this.setting = this.mainService.settingForDetails;
  }
  ngOnInit() {

  }
}
