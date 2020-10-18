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

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})

export class SettingsComponent implements OnInit {
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  displayedColumns: string[] = ['iSettingId', 'nvSettingName', 'nvSettingCode', 'nvSettingTypeValue', 'nvAddress', 'nvPhone',
    'nvContactPerson', 'nvContactPersonMail', 'nvContactPersonPhone', 'lSettingAgegroupsValue', 'nvFullName', 
    'nvMail', 'nvPhoneCoordinator','edit','choose'];

  //סוג מקור הנתונים
  dataSource: MatTableDataSource<Setting>;
  //מערך מפעילים לטבלה
  settingList: Array<Setting>;
  coordinatorList: Array<coordinator>;
  currentUser: User = new User();
  coordinator: coordinator = new coordinator();

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
        //כדאי מאוד ליעל את זה
        this.settingList.forEach(s => {
          if (s.iCoordinatorId) {
            this.coordinator = this.coordinatorList.find(c => c.iCoordinatorId == s.iCoordinatorId);
            s.nvFullName = this.coordinator.nvFirstName + " " + this.coordinator.nvLastName;
            s.nvPhoneCoordinator = this.coordinator.nvPhone;
            s.nvMail = this.coordinator.nvMail;
          }
        });
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
}
