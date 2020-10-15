import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/classes/user';
import { MainServiceService } from 'src/app/services/MainService/main-service.service';

@Component({
  selector: 'app-header-menu',
  templateUrl: './header-menu.component.html',
  styleUrls: ['./header-menu.component.css']
})
export class HeaderMenuComponent implements OnInit {

  showWelcome: boolean = true;
  currentUser: User = new User();

  ngOnInit() {
  }

  constructor(private mainService: MainServiceService) {
    this.currentUser = this.mainService.getUser();
  }
  setting() {
    this.showWelcome = false;
    this.mainService.serviceNavigate("/header-menu/settings");
<<<<<<< HEAD

  }
  management() {
    this.mainService.serviceNavigate("/header-menu/managers-table");
    this.showWelcome = false;

  }
  operator() {
    this.mainService.serviceNavigate("/header-menu/operator-table");
    this.showWelcome = false;

  }
  program() {
    this.mainService.serviceNavigate("/header-menu/programs");
    this.showWelcome = false;

  }
=======
>>>>>>> parent of a190a81... edit header menu links

  }
  management() {
    this.mainService.serviceNavigate("/header-menu/managers-table");
    this.showWelcome = false;

  }
  operator() {
    this.mainService.serviceNavigate("/header-menu/operator-table");
    this.showWelcome = false;

  }
  program() {
    this.mainService.serviceNavigate("/header-menu/programs");
    this.showWelcome = false;

  }

}
