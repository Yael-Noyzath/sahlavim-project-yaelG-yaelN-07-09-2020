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

  visible:number=1;

  currentUser: User=new User();
  //מערך מפעילים לטבלה
  operators: Array<Operator>;
   //מערך שמות העמודות
   displayedColumns: string[] = ['nvOperatorName', 'nvContactPerson', 'nvOperatorTypeValue', 'nvCompanyName', 'nvActivityies', 'nvIdentity', 'nvContactPersonPhone', 'nvContactPersonMail', 'bInProgramPool', 'update', 'delete','choose'];
   //סוג מקור הנתונים
   dataSource: MatTableDataSource<Operator>;

  ngOnInit() {
    this.currentUser = this.mainService.getUser();
    this.getAllOperators();
    this.ngAfterViewInit();
  }

    //קבלת כל המפעילים
    getAllOperators() {

      this.dataSource = new MatTableDataSource(this.mainService.operatorsList); 

    }

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



  //מחיקת מפעיל
  DeleteOperator(oper: Operator) {
    alert("DeleteOperator  "+oper.iOperatorId)
    alert("האם אתה בטוח שברצונך למחוק מפעיל זה ?");
    this.mainService.post("DeleteOperator", { iOperatorId: oper.iOperatorId, iUserId: this.currentUser.iUserId });
  }
  //עריכת מפעיל
  EditOperator(operator: Operator) {
    // settingsActiveTab = 0;
    // bNeighborhood = false;
    // bSchoolsExcude = false;
    // iOperatorId = oper.iOperatorId;
    this.mainService.operatorForDetails=operator;
    this.mainService.serviceNavigateForOperatorEdit("/header-menu/operators/operator-menu/",operator.iOperatorId);
    
    //מעבר לעמוד של עריכה
    
  }
}





