import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, observable } from 'rxjs';
import { Operator } from 'src/app/classes/operator';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from 'src/app/classes/user';
import { Setting } from 'src/app/Classes/setting';
import { Program } from 'src/app/Classes/program';

@Injectable({
  providedIn: 'root'
})
export class MainServiceService {

  constructor(private router: Router, private http: HttpClient) {

    this.getAllOperators();
    this.getSettings();
    this.ProgramsGet();
  }

  operatorsList: Operator[] = [];
  settingsList: Setting[] = [];
  programsList: Program[] = [];
  //משתמש שנכנס למערכת
  currentUser: User = new User();
  // לעריכת מפעיל
  operatorForDetails: Operator = new Operator();
  //לעריכת מסגרת
  settingForDetails: Setting = new Setting();
  
  //לעריכת תוכנית
  programForDetails: Program;

// http://qa.webit-track.com/SachlavimQA/Service/Service1.svc/ שרת בדיקות מרוחק
  sahlavimUrl ="http://localhost:53070/Service1.svc/";//שרת מקומי
  post(url: string, data: any): Promise<any> {
    console.log(url);

    return this.http.post(`${this.sahlavimUrl}${url}`, data).toPromise();
  }

  get(url: string): Promise<any> {
    console.log(url);
    return this.http.get(`${this.sahlavimUrl}${url}`).toPromise();
  }
  
  ProgramsGet() {
    this.post("ProgramsGet", {}).then(
      res => {
        this.programsList = res;
      },
      err => {
        alert("ProgramsGet err")
      }
    );
  }

  getAllOperators() {

    this.post("GetOperators", {})
      .then(
        res => {
          if (res) {
            this.operatorsList = res;
          }
          else
            alert("get all operators error")
        }
        , err => {
          alert("err");
        }
      );
  }


  getSettings() {
     this.post("SettingsGet", {}).then(
      res => {
        this.settingsList = res;
      },
      err => {
        alert("SettingsGet err")
      }
    );
  }

  serviceNavigate(path: string) {
    this.router.navigate([path]);
  }
  serviceNavigateForOperatorEdit(path: string, id: number) {
    this.router.navigate([path, id]);
  }

  saveUser(u: User) {
    //alert("saveUser  " + u.nvUserName);
    this.currentUser = u;
  }
  getUser() {
    //alert("getUser " + this.currentUser.nvUserName);
    return this.currentUser;
  }

}