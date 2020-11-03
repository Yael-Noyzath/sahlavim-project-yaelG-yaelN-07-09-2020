import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, observable } from 'rxjs';
import { Operator } from 'src/app/classes/operator';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from 'src/app/classes/user';
import { Setting } from 'src/app/Classes/setting';
import { Program } from 'src/app/Classes/program';

export class row{
  key:number;
  value:string; 
  constructor(key:number,value:string){
    this.key=key;
    this.value=value;
  }
}


@Injectable({
  providedIn: 'root'
})


export class MainServiceService {


  constructor(private router: Router, private http: HttpClient) {

    this.globalObj();

    this.getAllOperators();
    this.getSettings();
    this.getPrograms();
    // let dict = new Dictionary<Number>();

  }

  gItems: any = [];

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

  //מערך של כל הטבלאות
  SysTableList: Array<Array<row>> = new Array<Array<row>>();

  // http://qa.webit-track.com/SachlavimQA/Service/Service1.svc/ שרת בדיקות מרוחק
  sahlavimUrl = "http://localhost:53070/Service1.svc/";//שרת מקומי

  post(url: string, data: any): Promise<any> {
    console.log(url);
    return this.http.post(`${this.sahlavimUrl}${url}`, data).toPromise();
  }

  get(url: string): Promise<any> {
    console.log(url);
    return this.http.get(`${this.sahlavimUrl}${url}`).toPromise();
  }

  getPrograms() {
    //פונקציה המחזירה לתוך אובייקט את נתוני טבלת SysTable
    this.post("ProgramsGet", {}).then(
      res => {
        if (res)
          this.programsList = res;
        else
          alert(" get programs err");
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

getUser()
{
return this.currentUser;
}


  //שכונות sysTableUd=5
  globalObj() {

    this.post("SysTableListGet", {}).then(
      res => {
        //קבלת כל הטבלאות בפורמט של הסרבר
        this.gItems = res;

        //alert(this.gItems[0].dParams[0].Value)
        //מעברת על כל הטבלאות
        this.gItems.forEach(g => {
          //במערך של הטבלאות MAP עבור כל טבלה יצירת 
          //בפורמט מתאים לאנגולר 8 PARAMS בשביל שמירת הנתונים של 
          this.SysTableList[g.iListId - 1] = new Array<row>();
          //שלה PARAMS לכל טבלה עובר על 
          //KEY,VALUE את הנתונים בצורה של MAP ומכניס לתוך ה
          //MAP ומכניס את ה
          //של הטבלה ID למערך במקום של ה
          g.dParams.forEach(p => {
            this.SysTableList[g.iListId - 1].push(new row(p.Key,p.Value));

          });

        });
debugger
      },
      err => {
        alert("globalObj err");
      }
    )
  }


}