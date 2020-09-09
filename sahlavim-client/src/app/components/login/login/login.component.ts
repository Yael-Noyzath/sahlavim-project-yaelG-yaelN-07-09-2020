import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private routr: Router) { }

  ngOnInit() {
  }
  headerMenu(){
    this.routr.navigate(["/header-menu"]);
  }
}
