import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class MainServiceService {

  constructor(private router: Router) { }

  serviceNavigate(path:string)
  {
    this.router.navigate([path]);
  }

}
