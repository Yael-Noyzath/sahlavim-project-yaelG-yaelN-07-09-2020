import { User } from 'src/app/classes/user';
import { MainServiceService } from 'src/app/services/MainService/main-service.service';
import { AfterViewInit, ViewChild, Component, OnInit } from '@angular/core';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Operator } from 'src/app/Classes/operator';
import { MySearchPipe } from 'src/app/pipe/my-search.pipe';
import { from } from 'rxjs';

@Component({
  selector: 'app-managers-table',
  templateUrl: './managers-table.component.html',
  styleUrls: ['./managers-table.component.css']
})
export class ManagersTableComponent implements OnInit {


  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  displayedColumns: string[] = ['nvLastName', 'nvFirstName', 'iUserType', 'nvUserName', 'nvMobile', 'nvMail', 'details'];

  //סוג מקור הנתונים
  dataSource: MatTableDataSource<User>;
  //מערך מפעילים לטבלה
  usersList: Array<User>;

  currentUser: User = new User();

  constructor(private mainService: MainServiceService) {
    this.currentUser = mainService.getUser();
    this.GetUsers();
  }

  ngOnInit() {
    this.ngAfterViewInit();

  }


  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  GetUsers() {
    this.mainService.post("GetUsers", {})
      .then(
        res => {
          if (res) {
            this.usersList = res;
            // this.usersList.forEach(element => {
            //   switch(element.iUserType)
            //   {
            //     case 1:
            //   }
            // });

            this.dataSource = new MatTableDataSource(this.usersList);

          }
          else
            alert("GetUsers management error");
        },
        err => {
          alert("error");
        }
      );
  }

  DetailsUser(user: User) {
    alert(user.nvUserName);
  }

}
