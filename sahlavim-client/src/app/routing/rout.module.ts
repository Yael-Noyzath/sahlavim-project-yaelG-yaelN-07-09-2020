import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from '../components/Login/login/login.component';
import { HeaderMenuComponent } from '../components/Main/header-menu/header-menu.component';
import { ManagementMenuComponent } from '../components/Management/management-menu/management-menu.component';
import { OperatorMenuComponent } from '../components/Operators/operator-menu/operator-menu.component';
import { ProgramDetailsMenuComponent } from '../components/Programs/program-details-menu/program-details-menu.component';
import { SettingsDetailsMenuComponent } from '../components/Settings/settings-details-menu/settings-details-menu.component';
import { ManagersTableComponent } from '../components/Management/managers-table/managers-table.component';
import { OperatorTableComponent } from '../components/Operators/operator-table/operator-table.component';
import { ProgramsComponent } from '../components/Programs/programs/programs.component';
import { SettingsComponent } from '../components/Settings/settings/settings.component';
<<<<<<< HEAD
<<<<<<< HEAD
=======
import { OperatorsComponent } from '../components/Operators/operators/operators.component';
>>>>>>> parent of e8819d2... Merge pull request #45 from Yael-Noyzath/noyzath
=======
>>>>>>> parent of a190a819... edit header menu links

const appTable: Routes = [
  { path: "", component: LoginComponent },
  {
    path: "header-menu", component: HeaderMenuComponent,
    children: [
      { path: "managers-table", component: ManagersTableComponent },
<<<<<<< HEAD
<<<<<<< HEAD
      { path: "operator-table", component: OperatorTableComponent },
      { path: "programs", component: ProgramsComponent },
      { path: "settings", component: SettingsComponent }
    ]
=======
      { path: "operators", component:OperatorsComponent  },
=======
      { path: "operator-table", component: OperatorTableComponent },
>>>>>>> parent of a190a819... edit header menu links
      { path: "programs", component: ProgramsComponent },
      { path: "settings", component: SettingsComponent }
    ]
<<<<<<< HEAD

>>>>>>> parent of e8819d2... Merge pull request #45 from Yael-Noyzath/noyzath
=======
>>>>>>> parent of a190a819... edit header menu links
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(appTable),
    CommonModule
  ],
  declarations: []
})
export class RoutModule { }
