
import {AfterViewInit,ViewChild, Component, OnInit } from '@angular/core';
import {MatTableModule,MatTableDataSource} from '@angular/material/table';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import { Operator } from 'src/app/Classes/operator';



import { MainServiceService } from 'src/app/services/MainService/main-service.service';

@Component({
  selector: 'app-operator-table',
  templateUrl: './operator-table.component.html',
  styleUrls: ['./operator-table.component.css']
})
export class OperatorTableComponent implements OnInit {



  constructor(private mainService: MainServiceService) { 
      // Assign the data to the data source for the table to render
    this.dataSource = new MatTableDataSource(this.operators);
  
 }
  
  operatorsList: Array<Operator>;
  ngOnInit() {
    this.getOperatorList();
  }
  
  getOperatorList() {
    this.mainService.getOperators().subscribe(
      myList => {
        this.operatorsList = myList;
        alert(this.operatorsList);
      },
      error => {
        alert("errrrorrrr");
      }
    );
  }



  displayedColumns: string[] = ['id','name','kind','companyName','category','identity','phoneNumber','update','delete' ];
  dataSource: MatTableDataSource<Operator>;
  operators:Array<Operator>=[
    new Operator(1,"Yael","aa","0533145141"),
    new Operator(2,"Shira","aa","0533145141"),
    new Operator(3,"Michal","aa","0533145141"),
    new Operator(1,"Yael","aa","0533145141"),
    new Operator(2,"Shira","aa","0533145141"),
    new Operator(3,"Michal","aa","0533145141"),
    new Operator(1,"Yael","aa","0533145141"),
    new Operator(2,"Shira","aa","0533145141"),
    new Operator(3,"Michal","aa","0533145141"),
  ];
    
  // @ViewChild(MatPaginator) paginator: MatPaginator;
  // @ViewChild(MatSort) sort: MatSort;



  // ngAfterViewInit() {
  //   this.dataSource.paginator = this.paginator;
  //   this.dataSource.sort = this.sort;
  // }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
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
}



  

