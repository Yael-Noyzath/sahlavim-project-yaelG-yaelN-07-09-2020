import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header-menu',
  templateUrl: './header-menu.component.html',
  styleUrls: ['./header-menu.component.css']
})
export class HeaderMenuComponent implements OnInit {

  constructor(private routr: Router) { }

  ngOnInit() {
  }

  setting() {
    this.routr.navigate(["/header-menu/setting-details-menu"]);
  }
  management() {
    this.routr.navigate(["/header-menu/management-menu"]);

  }
  operator() {
    this.routr.navigate(["/header-menu/operator-menu"]);
  }
  program() {
    this.routr.navigate(["/header-menu/program-details-menu"]);
  }

}
