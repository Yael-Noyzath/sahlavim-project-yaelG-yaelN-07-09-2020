import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Operator } from 'src/app/classes/operator';
import { MainServiceService } from 'src/app/services/MainService/main-service.service';

@Component({
  selector: 'app-operator-details',
  templateUrl: './operator-details.component.html',
  styleUrls: ['./operator-details.component.css']
})
export class OperatorDetailsComponent implements OnInit {

  operator: Operator;
  constructor(private route: ActivatedRoute, private mainService: MainServiceService) { }

  ngOnInit() {

    this.operator = this.mainService.operatorForDetails;
    debugger
  }

}
