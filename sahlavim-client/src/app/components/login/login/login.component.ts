import { Component, OnInit } from '@angular/core';
import { MainServiceService } from 'src/app/services/MainService/main-service.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private mainService: MainServiceService) { }
  enterByUserName: boolean = true;
  ngOnInit() {
  }
  //כניסה
  enterToTheMenu() {
    //בדיקה אם השם משתמש והסיסמא נכונים
    this.mainService.serviceNavigate("/header-menu");
  }
  //אתחול סיסמא
  resetUser() {
    if (this.enterByUserName)
      this.enterByUserName = false;
    else
      this.enterByUserName = true;
  }
  //שליחת מייל לאיפוס הסיסמא
  sentMailToResetPassword(userMail: string) {
    alert(userMail + " we are sorry but our mail dose not work!")
  }
}
