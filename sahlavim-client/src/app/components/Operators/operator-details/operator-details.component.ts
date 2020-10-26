import { Component, OnInit, } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Operator } from 'src/app/classes/operator';
import { MainServiceService } from 'src/app/services/MainService/main-service.service';
import {MatCheckboxModule} from '@angular/material'
import { FormControl, FormGroup,NgForm } from '@angular/forms';
@Component({
  selector: 'app-operator-details',
  templateUrl: './operator-details.component.html',
  styleUrls: ['./operator-details.component.css']
})
export class OperatorDetailsComponent implements OnInit {
  DetailsForm:FormGroup;
  operator: Operator;
  blNeighborhoods:boolean;//פעיל באיזורים מסויימים
  bSchoolsExclude:boolean;//לא פעיל במיסגרות מסויימות
  schools = new FormControl();
  neighborhoods=new FormControl();

  constructor(private route: ActivatedRoute, private mainService: MainServiceService) { }

  ngOnInit() {
    this.operator = this.mainService.operatorForDetails;
    this.blNeighborhoods= this.operator.lNeighborhoods.length>0?true:false;//

  }

 save(){

   console.log(this.operator);

  alert(this.operator.nvCompanyName);

    this.mainService.post("UpdateOperator", {data:this.operator})
      .then(
        res => {
          debugger
          if (res) {
           alert(res);
          }
          else
            alert("UpdateOperator error")
        }
        , err => {
          alert("err");
        }
      );
      //עידכון רשימת המפעילים  ע"י קבלתה מחדש מהסרויס
      this.mainService.getAllOperators();
  }
}
