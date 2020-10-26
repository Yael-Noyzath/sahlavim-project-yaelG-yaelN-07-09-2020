import { User } from 'src/app/classes/user';
import { MainServiceService } from 'src/app/services/MainService/main-service.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Program } from 'src/app/Classes/program';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';

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

  ngOnInit() {
    this.ngAfterViewInit();
  }

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  constructor(private mainService: MainServiceService) {
    this.currentUser = this.mainService.getUser();
    this.ProgramsGet();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
  i: number = 0;
  ProgramsGet() {
    this.mainService.post("ProgramsGet", {}).then(
      res => {
        this.programList = res;
//חיתוך הרשימה במקום שלא צריך
        this.programList.forEach(p => {
          if (p.iProgramType == -1)
            this.programList.splice(this.i, 1);
          this.i++;

        });
        this.dataSource = new MatTableDataSource(this.programList);
      },
      err => {
        alert("ProgramsGet err")
      }
    );
  }

  EditProgram(prog: Program) {
    alert("EditProgram");
  }
}
