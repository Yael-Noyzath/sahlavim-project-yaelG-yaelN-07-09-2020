import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, observable } from 'rxjs';
import { Operator } from 'src/app/classes/operator';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MainServiceService {

  constructor(private router: Router, private http: HttpClient) { }

  sahlavimUrl = "http://qa.webit-track.com/SachlavimQA/Service/Service1.svc/"

  serviceNavigate(path: string) {
    this.router.navigate([path]);
  }

  getOperators(): Observable<Array<Operator>> {
    return this.http.post<Array<Operator>>(this.sahlavimUrl+"GetOperators",{});
  }

}