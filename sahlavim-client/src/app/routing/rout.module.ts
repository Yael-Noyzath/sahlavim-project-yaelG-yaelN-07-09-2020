import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from '../components/login/login/login.component';
import { HeaderMenuComponent } from '../components/main/header-menu/header-menu.component';
import { ManagementMenuComponent } from '../components/Management/management-menu/management-menu.component';
import { OperatorMenuComponent } from '../components/Operators/operator-menu/operator-menu.component';
import { ProgramDetailsMenuComponent } from '../components/Programs/program-details-menu/program-details-menu.component';
import { SettingsDetailsMenuComponent } from '../components/Settings/settings-details-menu/settings-details-menu.component';

const appTable: Routes = [
  { path: "", component: LoginComponent },
  {
    path: "header-menu", component: HeaderMenuComponent,
    children: [
      { path: "management-menu", component: ManagementMenuComponent },
      { path: "operator-menu", component: OperatorMenuComponent },
      { path: "program-details-menu", component: ProgramDetailsMenuComponent },
      { path: "setting-details-menu", component: SettingsDetailsMenuComponent }
    ]
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
