import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { User } from 'src/app/classes/user';
import { MainServiceService } from 'src/app/services/MainService/main-service.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private mainService: MainServiceService) { }
  enterByUserName: boolean = true;
  user:User=new User();
  formLogin:FormGroup;
  ngOnInit() {
    this.UserLoginControls();
  }
  //כניסה
  enterToTheMenu() {
    this.user=this.formLogin.value;
    alert(this.user.nvUserName)
    this.mainService.userLogin(this.user.nvUserName,this.user.nvPassword,this.user.nvMail).subscribe(
      data=>{
        alert("exelent");
      },
      erorr=>{
        alert("errrorrr");
      }
    )
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
  sentMailToResetPassword() {
    alert(this.user.nvMail + " we are sorry but our mail dose not work!")
  }
  UserLoginControls() {
    this.formLogin = new FormGroup({
      userName:new FormControl(this.user.nvUserName),
      userPassword:new FormControl(this.user.nvPassword),
      userMail:new FormControl(this.user.nvMail)
    });
  }
  get userName() {
    return this.formLogin.get("userName");
  }
  get userPassword() {
    return this.formLogin.get("userPassword");
  }
  get userMail() {
    return this.formLogin.get("userMail");
  }
}
