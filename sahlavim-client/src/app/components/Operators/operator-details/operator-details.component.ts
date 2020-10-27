import { Component, OnInit, } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Operator } from 'src/app/classes/operator';
import { MainServiceService } from 'src/app/services/MainService/main-service.service';
import {MatCheckboxModule} from '@angular/material'
import { FormControl, FormGroup,NgForm } from '@angular/forms';
import { Setting } from 'src/app/Classes/setting';
@Component({
  selector: 'app-operator-details',
  templateUrl: './operator-details.component.html',
  styleUrls: ['./operator-details.component.css']
})
export class OperatorDetailsComponent implements OnInit {

  List = [];
  selectedItems = [];
  dropdownSettings = {};

  DetailsForm:FormGroup;
  operator: Operator;
  blNeighborhoods:boolean;//פעיל באיזורים מסויימים
  bSchoolsExclude:boolean;//לא פעיל במיסגרות מסויימות
  schools = new FormControl();
  neighborhoods=new FormControl();

  settingsList:Setting[]=[];

  constructor(private route: ActivatedRoute, private mainService: MainServiceService) { 
    this.settingsList=mainService.settingsList;
  }

  ngOnInit() {
    this.List = [
      {"id":1,"itemName":"India"},
      {"id":2,"itemName":"Singapore"},
      {"id":3,"itemName":"Australia"},
      {"id":4,"itemName":"Canada"},
      {"id":5,"itemName":"South Korea"},
      {"id":6,"itemName":"Germany"},
      {"id":7,"itemName":"France"},
      {"id":8,"itemName":"Russia"},
      {"id":9,"itemName":"Italy"},
      {"id":10,"itemName":"Sweden"}
    ];
    this.selectedItems = [
      {"id":2,"itemName":"Singapore"},
      {"id":3,"itemName":"Australia"},
      {"id":4,"itemName":"Canada"},
      {"id":5,"itemName":"South Korea"}
  ];
    this.operator = this.mainService.operatorForDetails;
    debugger
    this.blNeighborhoods= this.operator.lNeighborhoods.length>0?true:false;//
    this.bSchoolsExclude= this.operator.lSchoolsExcude.length>0?true:false;//

    this.dropdownSettings = { 
      singleSelection: false, 
      text:"Select Countries",
      selectAllText:'Select All',
      unSelectAllText:'UnSelect All',
      enableSearchFilter: true,
      classes:"myclass custom-class"
    };            

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
