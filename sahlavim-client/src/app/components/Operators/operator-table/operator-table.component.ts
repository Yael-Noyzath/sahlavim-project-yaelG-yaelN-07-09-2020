import { AfterViewInit, ViewChild, Component, OnInit } from '@angular/core';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Operator } from 'src/app/Classes/operator';
import { MainServiceService } from 'src/app/services/MainService/main-service.service';
import { MySearchPipe } from 'src/app/pipe/my-search.pipe';
import { from } from 'rxjs';
import { FormControl } from '@angular/forms';


@Component({
  selector: 'app-operator-table',
  templateUrl: './operator-table.component.html',
  styleUrls: ['./operator-table.component.css']
})
export class OperatorTableComponent implements OnInit {

  ContactNameFilter= new FormControl('');
  nameFilter = new FormControl('');
  OperatorTypeFilter = new FormControl(''); 
  CompanyNameFilter= new FormControl(''); 
  categoryFilter=new FormControl(''); 
  IdentityFilter=new FormControl('');
  ContactPersonPhoneFilter=new FormControl('');
  ContactPersonMailFilter=new FormControl('');
  //מערך מפעילים לטבלה
  operators: Operator[];
  //מערך שמות העמודות
  displayedColumns: string[] = ['iOperatorType','nvOperatorName', 'nvContactPerson', 'nvCompanyName', 'nvActivityies', 'nvIdentity', 'nvContactPersonPhone', 'nvContactPersonMail', 'bInProgramPool', 'update', 'delete', 'choose'];
  //סוג מקור הנתונים
  dataSource: MatTableDataSource<Operator>;
 
operatorTypes: Map<number, string> = new Map<number, string>();
//array of the filter colomns
filterValues = {
  nvOperatorName: '',
  nvContactPerson: '',
  iOperatorType: '',
  nvCompanyName: '',
  nvActivityies:'',
  nvIdentity:'',
   nvContactPersonPhone:'',
   nvContactPersonMail:''
  // bInProgramPool:''
};
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  constructor(private mainService: MainServiceService) {
this.operatorTypes=this.mainService.SysTableList[2];
debugger
    this.operators = this.mainService.operatorsList
    this.dataSource = new MatTableDataSource(this.operators);
    this.dataSource.filterPredicate = this.createFilter();

  }


  ngOnInit() {
    
    this.ngAfterViewInit();
   
    this.nameFilter.valueChanges.subscribe(
      name => {
        this.filterValues.nvOperatorName = name;
        this.dataSource.filter = JSON.stringify(this.filterValues);
      }
    )

     this.ContactNameFilter.valueChanges.subscribe(
      cname => {
        this.filterValues.nvContactPerson = cname;
        this.dataSource.filter = JSON.stringify(this.filterValues);
      }
    )
    
    this.OperatorTypeFilter.valueChanges.subscribe(
      name => {
        this.filterValues.iOperatorType = name;
        this.dataSource.filter = JSON.stringify(this.filterValues);
      }
    )

    this.CompanyNameFilter.valueChanges.subscribe(
      name => {
        this.filterValues.nvCompanyName = name;
        this.dataSource.filter = JSON.stringify(this.filterValues);
      }
    )

    this.categoryFilter.valueChanges.subscribe(
      name => {
        this.filterValues.nvActivityies = name;
        this.dataSource.filter = JSON.stringify(this.filterValues);
      }
    )

    this.IdentityFilter.valueChanges.subscribe(
      name => {
        this.filterValues.nvIdentity = name;
        this.dataSource.filter = JSON.stringify(this.filterValues);
      }
    )
    this.ContactPersonPhoneFilter.valueChanges.subscribe(
      name => {
        this.filterValues.nvContactPersonPhone = name;
        this.dataSource.filter = JSON.stringify(this.filterValues);
      }
    )

    this.ContactPersonMailFilter.valueChanges.subscribe(
      name => {
        this.filterValues.nvContactPersonMail = name;
        this.dataSource.filter = JSON.stringify(this.filterValues);
      }
    )
    
  }

  createFilter(): (data: any, filter: string) => boolean {
    
    let filterFunction = function(data, filter): boolean {
      let searchTerms = JSON.parse(filter);debugger
      return data.nvOperatorName.toLowerCase().indexOf(searchTerms.nvOperatorName) !== -1
         && data.nvContactPerson.toLowerCase().indexOf(searchTerms.nvContactPerson) !== -1
          //  && this.operatorTypes.get(data.iOperatorType).toLowerCase().indexOf(searchTerms.iOperatorType) !== -1
          && data.nvCompanyName.toLowerCase().indexOf(searchTerms.nvCompanyName) !== -1
          && data.nvActivityies.toLowerCase().indexOf(searchTerms.nvActivityies) !== -1
          && data.nvIdentity.toLowerCase().indexOf(searchTerms.nvIdentity) !== -1
          && data.nvContactPersonPhone.toLowerCase().indexOf(searchTerms.nvContactPersonPhone) !== -1
          && data.nvContactPersonMail.toLowerCase().indexOf(searchTerms.nvContactPersonMail) !== -1;
    }
    return filterFunction;
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
  // applyFilter(event: Event, prop: string) {
  //   //alert(prop);
  //   const filterValue = (event.target as HTMLInputElement).value;
  //   //העתקת הרשימה
  //   var list = this.operators;
  //   //מציאת הרשימה העונה על הדרישות
  //   switch (prop) {
  //     case 'nvOperatorName': this.operators = this.operators.filter((m) => (m.nvOperatorName.indexOf(filterValue) > -1));
  //       break;
  //     case 'nvContactPerson': this.operators = this.operators.filter((m) => (m.nvContactPerson.indexOf(filterValue) > -1));
  //       break;
  //     //iOperatorType צריך לעשות לו המרה מ 
  //     case 'nvOperatorTypeValue': this.operators = this.operators.filter((m) => (m.nvOperatorTypeValue.indexOf(filterValue) > -1));
  //       break;
  //     case 'nvCompanyName': this.operators = this.operators.filter((m) => (m.nvCompanyName.indexOf(filterValue) > -1));
  //       break;
  //     case 'nvActivityies': this.operators = this.operators.filter((m) => (m.nvActivityies.indexOf(filterValue) > -1));
  //       break;
  //     case 'nvIdentity': this.operators = this.operators.filter((m) => (m.nvIdentity.indexOf(filterValue) > -1));
  //       break;
  //     case 'nvContactPersonPhone': this.operators = this.operators.filter((m) => (m.nvContactPersonPhone.indexOf(filterValue) > -1));
  //       break;
  //     case 'nvContactPersonMail': this.operators = this.operators.filter((m) => (m.nvContactPersonMail.indexOf(filterValue) > -1));
  //       break;
  //     // case 'bInProgramPool': this.operators = this.operators.filter((m) => (m.bInProgramPool.indexOf(filterValue) > -1));
  //     //   break;
  //   }
  //   //שמירת הרשימה שנמצאה
  //   this.dataSource.data = this.operators;
  //   //החזרת הרשימה הראשונה
  //   this.operators = list;
  // }



  //מחיקת מפעיל
  DeleteOperator(oper: Operator) {

    if (confirm("Are you sure to delete " + oper.nvOperatorName + "?")) {
      this.mainService.post("DeleteOperator", { iOperatorId: oper.iOperatorId, iUserId: this.mainService.currentUser.iUserId }).then(
        res => {
          this.mainService.operatorsList = res;
        },
        err => {
          alert(err);
        }
      );
    }

  }

  //עריכת מפעיל
  EditOperator(operator: Operator) {
    // settingsActiveTab = 0;
    // bNeighborhood = false;
    // bSchoolsExcude = false;
    // iOperatorId = oper.iOperatorId;
    this.mainService.operatorForDetails = operator;
    this.mainService.serviceNavigateForId("/header-menu/operators/operator-menu/", operator.iOperatorId);

    //מעבר לעמוד של עריכה

  }
}





