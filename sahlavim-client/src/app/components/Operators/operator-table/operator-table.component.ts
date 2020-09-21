import { Component, OnInit } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { MainServiceService } from 'src/app/services/MainService/main-service.service';
import { Operator } from 'src/app/classes/operator';

@Component({
  selector: 'app-operator-table',
  templateUrl: './operator-table.component.html',
  styleUrls: ['./operator-table.component.css']
})
export class OperatorTableComponent implements OnInit {

  constructor(private mainService: MainServiceService) { }
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



}
