import { Component, OnInit, } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Operator } from 'src/app/classes/operator';
import { MainServiceService } from 'src/app/services/MainService/main-service.service';
import {MatCheckboxModule} from '@angular/material'
import { FormControl, FormGroup } from '@angular/forms';
@Component({
  selector: 'app-operator-details',
  templateUrl: './operator-details.component.html',
  styleUrls: ['./operator-details.component.css']
})
export class OperatorDetailsComponent implements OnInit {
  DetailsForm:FormGroup;
  operator: Operator;

  constructor(private route: ActivatedRoute, private mainService: MainServiceService) { }

  ngOnInit() {

    this.operator = this.mainService.operatorForDetails;
    
    this.DetailsForm = new FormGroup({
      firstName: new FormControl()
   });

  }


}
