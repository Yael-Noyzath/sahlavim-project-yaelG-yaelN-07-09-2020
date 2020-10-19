import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Operator } from 'src/app/classes/operator';
import { MainServiceService } from 'src/app/services/MainService/main-service.service';

@Component({
  selector: 'app-operator-menu',
  templateUrl: './operator-menu.component.html',
  styleUrls: ['./operator-menu.component.css']
})
export class OperatorMenuComponent implements OnInit {
  id: string;
  operator: Operator;

  constructor(private route: ActivatedRoute, private mainService: MainServiceService) { 
        this.operator=this.mainService.operatorForDetails;

  }

  ngOnInit() {
    
  }
}
