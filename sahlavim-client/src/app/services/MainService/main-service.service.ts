import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, observable } from 'rxjs';
import { Operator } from 'src/app/classes/operator';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from 'src/app/classes/user';

@Injectable({
  providedIn: 'root'
})
export class MainServiceService {

  constructor(private router: Router, private http: HttpClient) { }

  //משתמש שנכנס למערכת
  currentUser: User = new User();
  sahlavimUrl = "http://qa.webit-track.com/SachlavimQA/Service/Service1.svc/"

  post(url: string, data): Promise<any> {
    console.log(url);
    return this.http.post(`${this.sahlavimUrl}${url}`, data).toPromise();
  }

  get(url: string): Promise<any> {
    console.log(url);
    return this.http.get(`${this.sahlavimUrl}${url}`).toPromise();

  }

  serviceNavigate(path: string) {
    this.router.navigate([path]);
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