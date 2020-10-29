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
    this.globalObj();
    this.getAllOperators();
    this.getSettings();
    this.ProgramsGet();
    // let dict = new Dictionary<Number>();

  }

  gItems:any = [];

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


//פונקציה המחזירה לתוך אובייקט את נתוני טבלת SysTable
  ProgramsGet() {
    this.post("ProgramsGet", {}).then(
      res => {
        this.programsList = res;
        debugger
      },
      err => {
        alert("ProgramsGet err")
      }
    );
  }

//רשימת המפעילים
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

 
//רשימת המיסגרות
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

  serviceNavigateForId(path: string, id: number) {
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

 globalObj() {
    this.post("SysTableListGet", {}).then(
      res => {
        this.gItems = res;
        debugger
        //alert(this.gItems[0].dParams[0].Value)
      },
      err => {
        alert("globalObj err");
      }
    )
  }

  getGItems(){
    return this.gItems;
  }
}