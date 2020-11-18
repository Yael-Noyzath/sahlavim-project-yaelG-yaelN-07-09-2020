import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { afternoon } from 'src/app/Classes/afternoon';
import { MainServiceService } from 'src/app/services/MainService/main-service.service';

@Component({
  selector: 'app-afternoon-table',
  templateUrl: './afternoon-table.component.html',
  styleUrls: ['./afternoon-table.component.css']
})
export class AfternoonTableComponent implements OnInit {

  displayedColumns: string[] = ['choose', 'edit', 'iafternoonType', 'nvafternoonName', 'dFromDateFormat', 'dToDateFormat',
    'lafternoonSettings', 'lafternoonAgegroups', 'nvBudgetItem'];

  afternoonList: Array<afternoon>;
  dataSource: MatTableDataSource<afternoon>;

  ngOnInit() {
    this.ngAfterViewInit();
  }

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  constructor(private mainService: MainServiceService) {
    this.afternoonList = this.mainService.afternoonsList;
    for (let p of this.afternoonList) {
      // p.dFromDate=new Date(parseInt(p.dFromDate.replace(/\/+Date\(([\d+-]+)\)\/+/, '$1'))).toString();
      // p.dToDate=new Date(parseInt(p.dToDate.replace(/\/+Date\(([\d+-]+)\)\/+/, '$1'))).toString();

      // // p.tFromTimeAfternoon=new Date(parseInt(p.tFromTimeAfternoon.replace(/\/+Date\(([\d+-]+)\)\/+/, '$1'))).toDateString();
    }


  }

  ngAfterViewInit() {

    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }


}
