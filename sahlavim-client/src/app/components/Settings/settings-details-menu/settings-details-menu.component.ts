import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MainServiceService } from 'src/app/services/MainService/main-service.service';

@Component({
  selector: 'app-settings-details-menu',
  templateUrl: './settings-details-menu.component.html',
  styleUrls: ['./settings-details-menu.component.css']
})
export class SettingsDetailsMenuComponent implements OnInit {

  idSetting:string;
  constructor(private route: ActivatedRoute, private mainService: MainServiceService) { }

  ngOnInit() {
    this.idSetting = this.route.snapshot.paramMap.get('id')
    alert(this.idSetting)
  }
}
