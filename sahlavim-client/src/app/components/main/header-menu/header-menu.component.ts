import { Component, OnInit } from '@angular/core';
import { MainServiceService } from 'src/app/services/MainService/main-service.service';

@Component({
  selector: 'app-header-menu',
  templateUrl: './header-menu.component.html',
  styleUrls: ['./header-menu.component.css']
})
export class HeaderMenuComponent implements OnInit {

  constructor(private mainService: MainServiceService) { }

  ngOnInit() {
  }

  setting() {
    this.mainService.serviceNavigate("/header-menu/setting-details-menu");
  }
  management() {
    this.mainService.serviceNavigate("/header-menu/management-menu");

  }
  operator() {
    this.mainService.serviceNavigate("/header-menu/operator-menu");
  }
  program() {
    this.mainService.serviceNavigate("/header-menu/program-details-menu");
  }

}
