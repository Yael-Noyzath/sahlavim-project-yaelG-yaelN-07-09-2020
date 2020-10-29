import { Component, OnInit, } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Operator } from 'src/app/classes/operator';
import { MainServiceService } from 'src/app/services/MainService/main-service.service';
import {MatCheckboxModule} from '@angular/material'
import { FormControl, FormGroup,NgForm } from '@angular/forms';
import { Setting } from 'src/app/Classes/setting';
import {  IDropdownSettings } from 'ng-multiselect-dropdown';

@Component({
  selector: 'app-operator-details',
  templateUrl: './operator-details.component.html',
  styleUrls: ['./operator-details.component.css']
})
export class OperatorDetailsComponent implements OnInit {

  List = [];
  dropdownSettings:IDropdownSettings;

  DetailsForm:FormGroup;
  operator: Operator;
  blNeighborhoods:boolean;//פעיל באיזורים מסויימים
  bSchoolsExclude:boolean;//לא פעיל במיסגרות מסויימות
  schools = new FormControl();
  neighborhoods=new FormControl();
 
  schoolsExcludeList:Setting[] = [];//רשימת המיסגרות בהן המפעיל לא פעיל
  settingsList:Setting[]=[];//רשימת המיסגרות

  constructor(private route: ActivatedRoute, private mainService: MainServiceService) { 
  }

  ngOnInit() {
 
    this.operator = this.mainService.operatorForDetails;
    debugger
    this.blNeighborhoods= this.operator.lNeighborhoods.length>0?true:false;//האם באיזורים מסויימם
    this.bSchoolsExclude= this.operator.lSchoolsExcude.length>0?true:false;//האם לא פועל במיסגרות מסויימות
    this.settingsList=this.mainService.settingsList;
    
    // איתחול רשימת schoolsExcludeList 
    if(this.operator.lSchoolsExcude.length>0)
    {
       for ( let schoolId of this.operator.lSchoolsExcude)
        { 
          this.schoolsExcludeList.push(this.settingsList.find(x=>x.iSettingId == schoolId));
        }
    }
   

    this.dropdownSettings = {
      singleSelection: false,
     idField: 'iSettingId',
    textField: 'nvSettingName',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 3,
      allowSearchFilter: true
    };         

  }

 save(){

   console.log(this.operator);

   //  עידכון רשימת הבתי ספר שלא פעיל לפי הרשימה שנבחרה 
   if(this.schoolsExcludeList.length>0)
   {
     for (let school of this.schoolsExcludeList)//מעבר על הרשימה שנבחרה
       { 
       if(this.operator.lSchoolsExcude.indexOf(school.iSettingId)==-1)//אם המיסגרת לא כלולה ברשימה אז הוסף אותה
       {
          this.operator.lSchoolsExcude.push(school.iSettingId);
       }
      }
  }

  debugger

    this.mainService.post("UpdateOperator", {data:this.operator})
      .then(
        res => {
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

//בינתיים
  onItemSelect(item:Setting){
//this.operator.lSchoolsExcude.push(item.iSettingId);//הוספה לרשימה של האופרטור
    console.log(item);
    console.log(this.schoolsExcludeList);
}
OnItemDeSelect(item:Setting){
  //this.operator.lSchoolsExcude.splice(item.iSettingId, 1);//מחיקה מהרשימה של האופרטור
 
    console.log(item);
    console.log(this.schoolsExcludeList);
}
onSelectAll(items: any){

    console.log(items);
}
onDeSelectAll(items: any){
    console.log(items);
}
}
