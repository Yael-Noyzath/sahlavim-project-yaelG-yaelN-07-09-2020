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
import { OperatorsComponent } from '../components/Operators/operators/operators.component';
import { SettingsDetailsComponent } from '../components/Settings/settings-details/settings-details.component';
import { OperatorDetailsComponent } from '../components/Operators/operator-details/operator-details.component';
import { OperatorActivitiesComponent } from '../components/Operators/operator-activities/operator-activities.component';
import { OperatorActivityReportComponent } from '../components/Operators/operator-activity-report/operator-activity-report.component';
import { OperatorCreditComponent } from '../components/Operators/operator-credit/operator-credit.component';
import { OperatorMessagesComponent } from '../components/Operators/operator-messages/operator-messages.component';
import { OperatorReviewComponent } from '../components/Operators/operator-review/operator-review.component';
import { OperatorScheduleComponent } from '../components/Operators/operator-schedule/operator-schedule.component';
import { SettingTableComponent } from '../components/Settings/setting-table/setting-table.component';
import { SettingsScheduleComponent } from '../components/Settings/settings-schedule/settings-schedule.component';
import { SettingsMessagesComponent } from '../components/Settings/settings-messages/settings-messages.component';
import { ProgramsTableComponent } from '../components/Programs/programs-table/programs-table.component';

const appTable: Routes = [
 
  { path: "", component: LoginComponent },
  {
    path: "header-menu", component: HeaderMenuComponent,
    children: [
      { path: "managers-table", component: ManagersTableComponent },
      {
        path: "operators", component: OperatorsComponent,
        children: [
          { path: "operator-table", component: OperatorTableComponent },
          {
            path: 'operator-menu/:id', component: OperatorMenuComponent,
            children: [
              { path: "operator-details", component: OperatorDetailsComponent },
              { path: "operator-schedule", component: OperatorScheduleComponent },
              { path: "operator-activities", component: OperatorActivitiesComponent },
              { path: "operator-activity-report", component: OperatorActivityReportComponent },
              { path: "operator-credit", component: OperatorCreditComponent },
              { path: "operator-messages", component: OperatorMessagesComponent },
              { path: "operator-review", component: OperatorReviewComponent },
            ]
          }
        ]
      },
      {
        path: "programs", component: ProgramsComponent,
        children: [
          { path: "programs-table", component: ProgramsTableComponent },
          {
            path: "programs-details-menu/:id", component: ProgramDetailsMenuComponent,
            children: [
              // { path: "settings-details", component: SettingsDetailsComponent },
              // { path: "settings-schedule", component: SettingsScheduleComponent },
              // { path: "settings-messages", component: SettingsMessagesComponent },
            ]
          },
        ]
      },
      {
        path: "settings", component: SettingsComponent,
        children: [
          { path: "setting-table", component: SettingTableComponent },
          {
            path: "settings-details-menu/:id", component: SettingsDetailsMenuComponent,
            children: [
              { path: "settings-details", component: SettingsDetailsComponent },
              { path: "settings-schedule", component: SettingsScheduleComponent },
              { path: "settings-messages", component: SettingsMessagesComponent },
            ]
          },
        ]
      },
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
