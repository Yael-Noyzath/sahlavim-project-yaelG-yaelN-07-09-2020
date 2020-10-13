import { AfterViewInit, ViewChild, Component, OnInit } from '@angular/core';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Operator } from 'src/app/Classes/operator';
import { MainServiceService } from 'src/app/services/MainService/main-service.service';
import { MySearchPipe } from 'src/app/pipe/my-search.pipe';
import { from } from 'rxjs';
import { User } from 'src/app/classes/user';


@Component({
  selector: 'app-operator-table',
  templateUrl: './operator-table.component.html',
  styleUrls: ['./operator-table.component.css']
})
export class OperatorTableComponent implements OnInit {

  ngOnInit() {
    this.getAllOperators();
    this.thisUser = this.mainService.getUser();
  }

  //מערך שמות העמודות
  displayedColumns: string[] = ['nvOperatorName', 'nvContactPerson', 'nvOperatorTypeValue', 'nvCompanyName', 'nvActivityies', 'nvIdentity', 'nvContactPersonPhone', 'nvContactPersonMail', 'bInProgramPool', 'update', 'delete'];
  index = 8;
  //סוג מקור הנתונים
  dataSource: MatTableDataSource<Operator>;
  //מערך מפעילים לטבלה
  operators: Array<Operator>;
  thisUser: User=new User();

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  constructor(private mainService: MainServiceService) {

    // Assign the data to the data source for the table to render
    // if (this.operators[0].OperatorName)
    //   this.dataSource = new MatTableDataSource(this.operators);
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  // applyFilter(event: Event) {
  //   const filterValue = (event.target as HTMLInputElement).value;
  //   this.dataSource.filter = filterValue.trim().toLowerCase();
  //   if (this.dataSource.paginator) {
  //     this.dataSource.paginator.firstPage();
  //   }
  // }

  //פונקצית חיפוש לכל עמודה
  applyFilter(event: Event, prop: string) {
    //alert(prop);
    const filterValue = (event.target as HTMLInputElement).value;
    //העתקת הרשימה
    var list = this.operators;
    //מציאת הרשימה העונה על הדרישות
    switch (prop) {
      case 'nvOperatorName': this.operators = this.operators.filter((m) => (m.nvOperatorName.indexOf(filterValue) > -1));
        break;
      case 'nvContactPerson': this.operators = this.operators.filter((m) => (m.nvContactPerson.indexOf(filterValue) > -1));
        break;
      //iOperatorType צריך לעשות לו המרה מ 
      case 'nvOperatorTypeValue': this.operators = this.operators.filter((m) => (m.nvOperatorTypeValue.indexOf(filterValue) > -1));
        break;
      case 'nvCompanyName': this.operators = this.operators.filter((m) => (m.nvCompanyName.indexOf(filterValue) > -1));
        break;
      case 'nvActivityies': this.operators = this.operators.filter((m) => (m.nvActivityies.indexOf(filterValue) > -1));
        break;
      case 'nvIdentity': this.operators = this.operators.filter((m) => (m.nvIdentity.indexOf(filterValue) > -1));
        break;
      case 'nvContactPersonPhone': this.operators = this.operators.filter((m) => (m.nvContactPersonPhone.indexOf(filterValue) > -1));
        break;
      case 'nvContactPersonMail': this.operators = this.operators.filter((m) => (m.nvContactPersonMail.indexOf(filterValue) > -1));
        break;
      // case 'bInProgramPool': this.operators = this.operators.filter((m) => (m.bInProgramPool.indexOf(filterValue) > -1));
      //   break;
    }
    //שמירת הרשימה שנמצאה
    this.dataSource.data = this.operators;
    //החזרת הרשימה הראשונה
    this.operators = list;
  }

  //קבלת כל המפעילים
  getAllOperators() {
    this.mainService.post("GetOperators", {})
      .then(
        res => {
          if (res) {
            this.operators = res;
            this.dataSource = new MatTableDataSource(this.operators);
          }
          else
            alert("get all operators error")
        }
        , err => {
          alert("err");
        }
      );
  }

  //מחיקת מפעיל
  DeleteOperator(oper: Operator) {
    alert(oper.iOperatorId)
    alert("האם אתה בטוח שברצונך למחוק מפעיל זה ?");
    this.mainService.post("DeleteOperator", { iOperatorId: oper.iOperatorId, iUserId: this.thisUser.iUserId });
  }
  //עריכת מפעיל
  EditOperator(oper: Operator) {
    // settingsActiveTab = 0;
    // bNeighborhood = false;
    // bSchoolsExcude = false;
    // iOperatorId = oper.iOperatorId;
    this.mainService.post("GetOperator", { iOperatorId: oper.iOperatorId });
    //מעבר לעמוד של עריכה
  }
}

// const operators = Array.from({length: 100}, (_, k) => createNewUser(k + 1));

// /** Builds and returns a new User. */
// function createNewUser(id: number): UserData {
//   const name = NAMES[Math.round(Math.random() * (NAMES.length - 1))] + ' ' +
//       NAMES[Math.round(Math.random() * (NAMES.length - 1))].charAt(0) + '.';

//   return {
//     id: id.toString(),
//     name: name,
//     progress: Math.round(Math.random() * 100).toString(),
//     color: COLORS[Math.round(Math.random() * (COLORS.length - 1))]
//   };
//}





