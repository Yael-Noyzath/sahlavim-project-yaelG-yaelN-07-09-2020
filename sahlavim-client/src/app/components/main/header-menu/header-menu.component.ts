import { Component, OnInit } from '@angular/core';
import { MainServiceService } from 'src/app/services/MainService/main-service.service';

@Component({
  selector: 'app-header-menu',
  templateUrl: './header-menu.component.html',
  styleUrls: ['./header-menu.component.css']
})
export class HeaderMenuComponent implements OnInit {

  constructor(private mainService: MainServiceService) { }
showWelcome:boolean=true;
  ngOnInit() {
  }

  setting() {
    this.mainService.serviceNavigate("/header-menu/settings");
   this.showWelcome=false;

  }
  management() {
    this.mainService.serviceNavigate("/header-menu/managers-table");
    this.showWelcome=false;

  }
  operator() {
    this.mainService.serviceNavigate("/header-menu/operator-table");
    this.showWelcome=false;

  }
  program() {
    this.mainService.serviceNavigate("/header-menu/programs");
    this.showWelcome=false;

  }

}
