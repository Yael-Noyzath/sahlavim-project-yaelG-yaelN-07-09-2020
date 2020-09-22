import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, observable } from 'rxjs';
import { Operator } from 'src/app/classes/operator';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MainServiceService {

  constructor(private router: Router, private http: HttpClient) { }

  sahlavimUrl = "http://qa.webit-track.com/SachlavimQA/Service/Service1.svc/"

/////////////////////ניסיון לגשת לשרת שלא הצליח
  // post(url: string, data): Promise<any> {
  //   console.log(url);
  //   return this.http.post(`${this.sahlavimUrl}${url}`, data).toPromise();
  // }

  // get(url: string): Promise<any> {
  //   console.log(url);
  //   return this.http.get(`${this.sahlavimUrl}${url}`).toPromise();

  // }

  serviceNavigate(path: string) {
    this.router.navigate([path]);
  }

  // getOperators(): Observable<Array<Operator>> {
  //   return this.http.post<Array<Operator>>(this.sahlavimUrl + "GetOperators", {});
  // }

  userLogin(UnvUserName: string, UnvPassword: string, UnvMail: string) {
     return this.http.post<Array<Operator>>(this.sahlavimUrl + "UserLogin", { nvUserName: UnvUserName, nvPassword: UnvPassword, nvMail: UnvMail },{});
   }

}