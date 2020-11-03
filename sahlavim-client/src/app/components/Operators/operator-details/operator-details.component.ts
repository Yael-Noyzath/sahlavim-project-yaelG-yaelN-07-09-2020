import { Component, OnInit, } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Operator } from 'src/app/classes/operator';
import { MainServiceService,row } from 'src/app/services/MainService/main-service.service';
import { MatCheckboxModule } from '@angular/material'
import { FormControl, FormGroup, NgForm } from '@angular/forms';
import { Setting } from 'src/app/Classes/setting';
import { IDropdownSettings } from 'ng-multiselect-dropdown';

@Component({
  selector: 'app-operator-details',
  templateUrl: './operator-details.component.html',
  styleUrls: ['./operator-details.component.css']
})
export class OperatorDetailsComponent implements OnInit {

  dropdownSettings: IDropdownSettings;
  dropdownNeighborhoods: IDropdownSettings;

  //רשימת שכונות
  NeighborhoodsList:row[]=[];
  operatorNeighborhoods:row[]=[];;
  DetailsForm: FormGroup;
  operator: Operator;
  blNeighborhoods: boolean;//פעיל באיזורים מסויימים
  bSettingslsExclude: boolean;//לא פעיל במיסגרות מסויימות
  schoolListforTalan: Setting[] = [];//רשימת בתי הספר לחוגי תל"ן
  lschool: Setting[] = [];//בתי הספר בהם מפעיל מפעיל חוגי תל"ן
  schoolsExcludeList: Setting[] = [];//רשימת המיסגרות בהן המפעיל לא פעיל
  settingsList: Setting[] = [];//רשימת המיסגרות

  constructor(private route: ActivatedRoute, private mainService: MainServiceService) {
  }

  ngOnInit() {

    this.operator = this.mainService.operatorForDetails;//פרטי המפעיל לטופס ערכיה
    this.blNeighborhoods = this.operator.lNeighborhoods.length > 0 ? true : false;//האם פעיל באיזורים מסויימם
    this.bSettingslsExclude = this.operator.lSchoolsExcude.length > 0 ? true : false;//האם לא פועל במיסגרות מסויימות
    this.settingsList = this.mainService.settingsList;//רשימת המיסגרות לבחירת לא פעיל במיסגרות מסויימות

    //שליפת רשימת מיסגרות מסוג ביה"ס- לחוגי תל"ן
    this.schoolListforTalan = this.settingsList.filter(x => x.iSettingType === 18);
    if (this.operator.lSchools.length > 0)//talan schools where operates for the form input
    {
      for (let schoolId of this.operator.lSchools)
        this.lschool.push(this.settingsList.find(x => x.iSettingId == schoolId));
    }

    // איתחול רשימת schoolsExcludeList 
    if (this.operator.lSchoolsExcude.length > 0) {
      for (let schoolId of this.operator.lSchoolsExcude) {
        this.schoolsExcludeList.push(this.settingsList.find(x => x.iSettingId == schoolId));
      }
    }

    this.NeighborhoodsList=this.mainService.SysTableList[4];

        // איתחול רשימת שכונות של המפעיל 
        if (this.operator.lNeighborhoods.length > 0) {
          for (let nlId of this.operator.lNeighborhoods) {
            this.operatorNeighborhoods.push(this.NeighborhoodsList.find(x => x.key == nlId));
          }
        }

    //הגדרות ה multi select
    this.dropdownSettings = {
      singleSelection: false,
      idField: 'iSettingId',
      textField: 'nvSettingName',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 3,
      allowSearchFilter: true
    };

    //הגדרות ה multi select
    this.dropdownNeighborhoods = {
      singleSelection: false,
      idField: 'key',
      textField: 'value',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 3,
      allowSearchFilter: true
    };

  }



  save() {

    console.log(this.operator);

    //  עידכון רשימת הבתי ספר שלא פעיל לפי הרשימה שנבחרה 
    if (this.schoolsExcludeList.length > 0) {
      for (let school of this.schoolsExcludeList)//מעבר על הרשימה שנבחרה
      {
        if (this.operator.lSchoolsExcude.indexOf(school.iSettingId) == -1)//אם המיסגרת לא כלולה ברשימה אז הוסף אותה
        {
          this.operator.lSchoolsExcude.push(school.iSettingId);
        }
      }
    }

    //  עידכון בתי הספר בהם מפעיל חוגי תל"ן
    if (this.lschool.length > 0) {
      for (let schoolid of this.lschool) {
        if (this.operator.lSchools.indexOf(schoolid.iSettingId) == -1)//אם הבי"הס לא כלול ברשימה 
        {
          this.operator.lSchools.push(schoolid.iSettingId);
        }
      }
    }

        //  עידכון רשימת פעיל בשכונות מסויימות 
        if (this.operatorNeighborhoods.length > 0) {
          for (let id of this.operatorNeighborhoods) {
            if (this.operator.lNeighborhoods.indexOf(id.key) == -1)//אם השכונה לא כלולה כבר ברשימה 
            {
              this.operator.lNeighborhoods.push(id.key);
            }
          }
        }
    

debugger
    this.mainService.post("UpdateOperator", { oOperator: this.operator })
      .then(
        res => {
            alert("update "+this.operator.nvOperatorName+" done!"); 
               //קבלה מהשרת את רשימת מפעילים המעודכנת
                this.mainService.getAllOperators();
                this.mainService.serviceNavigate("/header-menu/operators/operator-table");

        }
        , err => {
          alert("err");
        }
      );

  }

  //בינתיים
  onItemSelect(item: Setting) {
    //this.operator.lSchoolsExcude.push(item.iSettingId);//הוספה לרשימה של האופרטור
    console.log(item);
    console.log(this.schoolsExcludeList);
  }
  OnItemDeSelect(item: Setting) {
    //this.operator.lSchoolsExcude.splice(item.iSettingId, 1);//מחיקה מהרשימה של האופרטור

    console.log(item);
    console.log(this.schoolsExcludeList);
  }
  onSelectAll(items: any) {

    console.log(items);
  }
  onDeSelectAll(items: any) {
    console.log(items);
  }
}
