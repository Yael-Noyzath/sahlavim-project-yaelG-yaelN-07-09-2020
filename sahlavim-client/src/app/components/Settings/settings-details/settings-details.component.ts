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
  selector: 'app-settings-details',
  templateUrl: './settings-details.component.html',
  styleUrls: ['./settings-details.component.css']
})
export class SettingsDetailsComponent implements OnInit {
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  // displayedColumns: string[] = ['iSettingId', 'nvSettingName', 'nvSettingCode', 'nvSettingTypeValue', 'nvAddress', 'nvPhone',
  //   'nvContactPerson', 'nvContactPersonMail', 'nvContactPersonPhone', 'lSettingAgegroupsValue', 'nvFullName',
  //   'nvMail', 'nvPhoneCoordinator','edit','choose'];
  displayedColumns: string[] = ['iSettingId', 'nvSettingName', 'nvSettingCode', 'nvSettingTypeValue', 'nvAddress', 'nvPhone',
    'nvContactPerson', 'nvContactPersonMail', 'nvContactPersonPhone', 'lSettingAgegroupsValue', 'CoordinatorDetails'
    , 'edit', 'choose'];

  //סוג מקור הנתונים
  dataSource: MatTableDataSource<Setting>;
  //מערך מפעילים לטבלה
  settingList: Array<Setting>;
  currentSetting:Setting=new Setting();
  coordinatorList: Array<coordinator>;
  currentUser: User = new User();
  coordinator: coordinator = new coordinator();
  openDetails: boolean = false;

  constructor(private mainService: MainServiceService) {
    this.currentUser = mainService.getUser();
    this.CoordinatorsGet();
    this.SettingsGet();

  }

  ngOnInit() {
    this.ngAfterViewInit();
  }


  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  SettingsGet() {
    this.mainService.post("SettingsGet", {}).then(
      res => {
        this.settingList = res;
        
        this.dataSource = new MatTableDataSource(this.settingList);

      },
      err => {
        alert("SettingsGet err")
      }
    );
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

  CoordinatorDetails(sett:Setting,CoordinatorId: number) {
    this.currentSetting=sett;
    if (this.openDetails == true)
      this.openDetails = false;
    else
      this.openDetails = true;

    if (CoordinatorId) {
      this.coordinator = this.coordinatorList.find(c => c.iCoordinatorId == CoordinatorId);
    }
  }
  EditSetting(idSetting: number) {
    this.mainService.serviceNavigateForOperatorEdit('/header-menu/settings/settings-details-menu', idSetting)
  }
}
