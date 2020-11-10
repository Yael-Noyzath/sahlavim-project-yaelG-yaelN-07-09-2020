import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { Activity } from 'src/app/classes/activity';
import { MatTableModule } from '@angular/material/table';
import { MainServiceService } from 'src/app/services/MainService/main-service.service';

@Component({
  selector: 'app-operator-activities',
  templateUrl: './operator-activities.component.html',
  styleUrls: ['./operator-activities.component.css']
})
export class OperatorActivitiesComponent implements OnInit {


  //רשימת פעילויות
  Activities:Activity[]=[];

  //מערך שמות העמודות
  displayedColumns: string[] = ['nvActivityName','iCategoryType','nvActivityProduct', 'lActivityAgegroups', 'nPrice', 'nShortBreak','nLongBreak', 'bActivityNoon','bActivityMorning'];
  //סוג מקור הנתונים
  dataSource: MatTableDataSource<Activity>;

  activityCategories:Map<number,string>=new  Map<number,string>();
  agesCategories:Map<number,string>=new  Map<number,string>();

  constructor(private mainService: MainServiceService) { }

  ngOnInit() {

    this.Activities = this.mainService.operatorForDetails.lActivity;
    this.dataSource = new MatTableDataSource(this.Activities);
    this.ngAfterViewInit();
    this.activityCategories=this.mainService.SysTableList[7];
    this.agesCategories=this.mainService.SysTableList[6];


     debugger
  }

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;


  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

}
