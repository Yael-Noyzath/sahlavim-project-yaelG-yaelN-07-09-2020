import { Component, OnInit } from '@angular/core';
import { MainServiceService } from 'src/app/servies/main-service.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private mainService: MainServiceService) { }

  ngOnInit() {
  }
  headerMenu(){
    this.mainService.serviceNavigate("/header-menu");
  }
}
