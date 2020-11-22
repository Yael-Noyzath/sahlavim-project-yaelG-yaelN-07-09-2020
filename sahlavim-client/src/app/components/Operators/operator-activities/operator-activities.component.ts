import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSelect, MatSort, MatTableDataSource } from '@angular/material';
import { Activity } from 'src/app/classes/activity';
import { MatTableModule } from '@angular/material/table';
import { forSelect, MainServiceService } from 'src/app/services/MainService/main-service.service';
import { FormControl } from '@angular/forms';
import { ReplaySubject, Subject } from 'rxjs';
import { take, takeUntil } from 'rxjs/operators';
import { AfterViewInit, OnDestroy } from '@angular/core';

@Component({
  selector: 'app-operator-activities',
  templateUrl: './operator-activities.component.html',
  styleUrls: ['./operator-activities.component.css']
})
export class OperatorActivitiesComponent implements OnInit {

  //מערך שמות העמודות
  displayedColumns: string[] = ['nvActivityName','iCategoryType','nvActivityProduct', 'lActivityAgegroups', 'nPrice', 'nShortBreak','nLongBreak','bActivityMorning','bActivityNoon','update'];
  //סוג מקור הנתונים
  dataSource: MatTableDataSource<Activity>;

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;


  //רשימת פעילויות
  Activities:Activity[]=[];
  ActivitiesType:forSelect[];
  CurrentActivity:Activity=new Activity();

  activControl:forSelect;
  activFilterCtrl:FormControl=new FormControl();
  /** list of activities filtered by search keyword */
  public filteredActivities: ReplaySubject<forSelect[]> = new ReplaySubject<forSelect[]>(1);

  @ViewChild('singleSelect', { static: true }) singleSelect: MatSelect;

  /** Subject that emits when the component has been destroyed. */
  protected _onDestroy = new Subject<void>();
  
  agesCategories:Map<number,string>=new  Map<number,string>();
  activityCategories:Map<number,string>=new  Map<number,string>();
  constructor(private mainService: MainServiceService) { }


  ngOnInit() {

    this.Activities = this.mainService.operatorForDetails.lActivity;
    this.dataSource = new MatTableDataSource(this.Activities);
    this.ngAfterViewInit();
    this.ActivitiesType=this.mainService.gItems[7].dParams;
    this.agesCategories=this.mainService.SysTableList[6];
    this.activityCategories=this.mainService.SysTableList[7];

    // load the initial bank list
    this.filteredActivities.next(this.ActivitiesType.slice());

    // listen for search field value changes
    this.activFilterCtrl.valueChanges
      .pipe(takeUntil(this._onDestroy))
      .subscribe(() => {
        this.filterActivities();
      });
  }

  ngAfterViewInit() {
    this.setInitialValue();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  protected filterActivities() {
    if (!this.ActivitiesType) {
      return;
    }
    // get the search keyword
    let search = this.activControl.Value;
    if (!search) {
      this.filteredActivities.next(this.ActivitiesType.slice());
      return;
    } else {
      search = search.toLowerCase();
    }
    // filter the banks
    this.filteredActivities.next(
      this.ActivitiesType.filter(activ => activ.Value.toLowerCase().indexOf(search) > -1)
    );
  }


  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }

  /**
   * Sets the initial value after the filteredBanks are loaded initially
   */
  protected setInitialValue() {
    this.filteredActivities
      .pipe(take(1), takeUntil(this._onDestroy))
      .subscribe(() => {
        // setting the compare With property to a comparison function
        // triggers initializing the selection according to the initial value of
        // the form control (i.e. _initializeSelection())
        // this needs to be done after the filteredBanks are loaded initially
        // and after the mat-option elements are available
        this.singleSelect.compareWith = (a: forSelect, b: forSelect) => a && b && a.Key === b.Key;
      });
  }
  
 





  EditActivity(Activity:Activity)
  {
    this.CurrentActivity=Activity;
    this.activControl=this.ActivitiesType.find(x=>x.Key== this.CurrentActivity.iCategoryType);
    debugger
  }
}
