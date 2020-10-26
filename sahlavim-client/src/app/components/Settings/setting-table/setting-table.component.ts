import { User } from 'src/app/classes/user';
import { MainServiceService } from 'src/app/services/MainService/main-service.service';
import { AfterViewInit, ViewChild, Component, OnInit } from '@angular/core';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Operator } from 'src/app/Classes/operator';
import { MySearchPipe } from 'src/app/pipe/my-search.pipe';
import { from } from 'rxjs';
import { Setting } from 'src/app/Classes/setting';
import { coordinator } from 'src/app/Classes/coordinator';
import { flatten } from '@angular/compiler';

@Component({
  selector: 'app-setting-table',
  templateUrl: './setting-table.component.html',
  styleUrls: ['./setting-table.component.css']
})
export class SettingTableComponent implements OnInit {



  // displayedColumns: string[] = ['iSettingId', 'nvSettingName', 'nvSettingCode', 'nvSettingTypeValue', 'nvAddress', 'nvPhone',
  //   'nvContactPerson', 'nvContactPersonMail', 'nvContactPersonPhone', 'lSettingAgegroupsValue', 'nvFullName',
  //   'nvMail', 'nvPhoneCoordinator','edit','choose'];
  displayedColumns: string[] = [    'choose','edit','iSettingId', 'nvSettingName', 'nvSettingCode', 'nvSettingTypeValue', 'nvAddress', 'nvPhone',
    'nvContactPerson', 'nvContactPersonMail', 'nvContactPersonPhone', 'lSettingAgegroupsValue', 'CoordinatorDetails'
  ];


  //סוג מקור הנתונים
  dataSource: MatTableDataSource<Setting>;
  //מערך מפעילים לטבלה
  settingList: Array<Setting>;
  currentSetting: Setting = new Setting();
  coordinatorList: Array<coordinator>;
  currentUser: User = new User();
  coordinator: coordinator = new coordinator();
  openDetails: boolean = false;


  ngOnInit() {

    this.ngAfterViewInit();
  }
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  constructor(private mainService: MainServiceService) {
    this.currentUser = this.mainService.getUser();
    this.CoordinatorsGet();
    this.settingList=mainService.settingsList;
    this.dataSource = new MatTableDataSource(this.settingList);

  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }


  CoordinatorsGet() {
    this.mainService.post("CoordinatorsGet", {}).then(
      res => {
        this.coordinatorList = res;
      },
      err => {
        alert("CoordinatorsGet err")
      }
    );
  }

  CoordinatorDetails(sett: Setting, CoordinatorId: number) {
    this.currentSetting = sett;
    if (this.openDetails == true)
      this.openDetails = false;
    else
      this.openDetails = true;

    if (CoordinatorId) {
      this.coordinator = this.coordinatorList.find(c => c.iCoordinatorId == CoordinatorId);
    }
  }
  EditSetting(setting: Setting) {
    this.mainService.settingForDetails = setting;
    this.mainService.serviceNavigateForOperatorEdit('/header-menu/settings/settings-details-menu/', setting.iSettingId);
  }
  chooseAllSettings(){
alert("select all doesnt work")
  }
}
