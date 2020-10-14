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
  constructor(private route: ActivatedRoute, private mainService: MainServiceService) { }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id')

    this.mainService.post("GetOperator", { iOperatorId: Number(this.id)  })
      .then(
        res => {
          if (res) {
            this.operator = res;
            // }
            // else
            //   alert("There is an Error")
          }
        }
        , err => {
          alert("There is an Error")
        }
      );
  }
}
