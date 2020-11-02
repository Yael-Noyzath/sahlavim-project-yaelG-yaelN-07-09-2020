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

  hide = true;
  enterByUserName: boolean = true;
  user: User = new User();
  currentUser: User = new User();
  usersList: Array<User>;
  formLogin: FormGroup;
  userExist:boolean=false;
  ngOnInit() {
    this.UserLoginControls();
  }
  //כניסה
  enterToTheMenu() {
    //קבלה של הנתונים שהכניס בכניסה
    this.user = this.formLogin.value;
    //בדיקה אם הוא רשאי להכנס
    this.UserLogin(this.user.nvUserName, this.user.nvPassword, this.user.nvMail);
    //users קבלת רשימה של כל ה
    this.GetUsers();
  }

  GetUsers() {
    this.mainService.post("GetUsers", {})
      .then(
        res => {
          if (res) {
            this.usersList = res;
            //חיפוש המשתמש הזה בתוך הרשימה
            this.currentUser = this.usersList.find(u => u.nvUserName == this.user.nvUserName
              && u.nvPassword == this.user.nvPassword);
            //שנכנס למערכת לשמירה בסרויס user שליחה של ה
            this.mainService.saveUser(this.currentUser);
            if(this.userExist==true)
            {
              this.mainService.serviceNavigate("header-menu");
            }
          }
          else
            alert("GetUsers login error");
        },
        err => {
          alert("error");
        }
      );
  }

  UserLogin(UnvUserName: string, UnvPassword: string, UnvMail: string) {
    this.mainService.post("UserLogin", { nvUserName: UnvUserName, nvPassword: UnvPassword, nvMail: UnvMail })
      .then(
        res => {
          if (res.iUserId)
            this.userExist= true;
          else {
            alert("userLogin error");
            this.userExist= false;
          }
        },
        err => {
          alert("err")
          this.userExist= false;
        }
      );
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
      nvUserName: new FormControl(this.user.nvUserName),
      nvPassword: new FormControl(this.user.nvPassword),
      nvMail: new FormControl(this.user.nvMail)
    });
  }
  
  get nvUserName() {
    return this.formLogin.get("nvUserName");
  }
  get nvPassword() {
    return this.formLogin.get("nvPassword");
  }
  get nvMail() {
    return this.formLogin.get("nvMail");
  }
}
