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
import { Program } from 'src/app/Classes/program';

@Component({
  selector: 'app-programs-table',
  templateUrl: './programs-table.component.html',
  styleUrls: ['./programs-table.component.css']
})
export class ProgramsTableComponent implements OnInit {

  displayedColumns: string[] = ['choose', 'edit', 'nvProgramTypeValue', 'nvProgramName', 'dFromDateFormat', 'dToDateFormat',
    'lengthProgramSettings', 'lProgramAgegroupsValue', 'nvBudgetItem'];

  currentUser: User = new User();
  programList: Array<Program>;
  dataSource: MatTableDataSource<Program>;

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;
  ngOnInit() {
    this.ngAfterViewInit();
  }


  constructor(private mainService: MainServiceService) {
    this.currentUser = this.mainService.getUser();
    this.ProgramsGet();
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }

  ProgramsGet() {
    this.mainService.post("ProgramsGet", {})
      .then(
        res => {
          this.programList = res;
          this.dataSource = new MatTableDataSource(this.programList);

        },
        err => {
          alert("err ProgramsGet")
        }
      )
  }
  EditProgram() {
    alert("EditProgram");
  }
}
