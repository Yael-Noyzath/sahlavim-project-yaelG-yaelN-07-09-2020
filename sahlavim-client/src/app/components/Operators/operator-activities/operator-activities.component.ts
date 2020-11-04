import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { Activity } from 'src/app/classes/activity';
import { MainServiceService } from 'src/app/services/MainService/main-service.service';

@Component({
  selector: 'app-operator-activities',
  templateUrl: './operator-activities.component.html',
  styleUrls: ['./operator-activities.component.css']
})
export class OperatorActivitiesComponent implements OnInit {

  constructor(private mainService: MainServiceService) { }

  //רשימת פעילויות
  Activities:Activity[]=[];

  //מערך שמות העמודות
  displayedColumns: string[] = ['nvActivityName','iCategoryType','nvActivityProduct', 'lActivityAgegroups', 'nPrice', 'nShortBreak','nLongBreak', 'bActivityNoon','bActivityMorning'];
  //סוג מקור הנתונים
  dataSource: MatTableDataSource<Activity>;

  ngOnInit() {
  
    this.dataSource = new MatTableDataSource(this.Activities);
    this.ngAfterViewInit();
  }

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;


  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

}
