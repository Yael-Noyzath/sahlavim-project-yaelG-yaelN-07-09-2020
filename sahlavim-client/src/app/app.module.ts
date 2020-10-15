import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {AgGridModule} from 'ag-grid-angular';
import {
  MatButtonModule,
  MatMenuModule,
  MatToolbarModule,
  MatIconModule,
  MatCardModule,
  MatFormFieldModule,
  MatInputModule,
  MatDatepickerModule,
  MatDatepicker,
  MatNativeDateModule,
  MatRadioModule,
  MatSelectModule,
  MatOptionModule,
  MatPaginatorModule,
  MatSortModule,
  MatGridListModule,
  MatSlideToggleModule, ErrorStateMatcher, ShowOnDirtyErrorStateMatcher, MatTableModule,MatProgressSpinnerModule
} from '@angular/material';


import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { RoutModule } from './routing/rout.module';
import { MatExpansionModule} from '@angular/material/expansion';
import { AfternoonDetailsComponent } from './components/Afternoons/afternoon-details/afternoon-details.component';
import { AfternoonDetailsMenuComponent } from './components/Afternoons/afternoon-details-menu/afternoon-details-menu.component';
import { AfternoonMainComponent } from './components/Afternoons/afternoon-main/afternoon-main.component';
import { AfternoonScheduleComponent } from './components/Afternoons/afternoon-schedule/afternoon-schedule.component';
import { WeeklySchedulingComponent } from './components/Afternoons/weekly-scheduling/weekly-scheduling.component';
import { LoginComponent } from './components/Login/login/login.component';
import { HeaderMenuComponent } from './components/Main/header-menu/header-menu.component';
import { MessageComponent } from './components/Main/message/message.component';
import { ManagersTableComponent } from './components/Management/managers-table/managers-table.component';
import { ManagementScheduleComponent } from './components/Management/management-schedule/management-schedule.component';
import { ManagementSettingsClustersComponent } from './components/Management/management-settings-clusters/management-settings-clusters.component';
import { ManagementSettingsJointComponent } from './components/Management/management-settings-joint/management-settings-joint.component';
import { ManagementMenuComponent }from'./components/Management/management-menu/management-menu.component';
import { OperatorActivityReportComponent } from './components/Operators/operator-activity-report/operator-activity-report.component';
import { OperatorCreditComponent } from './components/Operators/operator-credit/operator-credit.component';
import { OperatorDetailsComponent } from './components/Operators/operator-details/operator-details.component';
import { OperatorMessagesComponent } from './components/Operators/operator-messages/operator-messages.component';
import { OperatorReviewComponent } from './components/Operators/operator-review/operator-review.component';
import { OperatorScheduleComponent } from './components/Operators/operator-schedule/operator-schedule.component';
import { OperatorMenuComponent } from './components/Operators/operator-menu/operator-menu.component';
import { ProgramDetailsComponent } from './components/Programs/program-details/program-details.component';
import { ProgramDetailsMenuComponent } from './components/Programs/program-details-menu/program-details-menu.component';
import { ProgramReportComponent } from './components/Programs/program-report/program-report.component';
import { ProgramsComponent } from './components/Programs/programs/programs.component';
import { ProgramScheduleComponent } from './components/Programs/program-schedule/program-schedule.component';
import { SettingsComponent } from './components/Settings/settings/settings.component';
import { SettingsDetailsComponent } from './components/Settings/settings-details/settings-details.component';
import { SettingsDetailsMenuComponent } from './components/Settings/settings-details-menu/settings-details-menu.component';
//import {FlexLayoutModule} from '@angular/flex-layout';
import { from } from 'rxjs';
import { OperatorTableComponent } from './components/Operators/operator-table/operator-table.component';
import { OperatorsComponent } from './components/Operators/operators/operators.component';


@NgModule({
  declarations: [
    AppComponent,
    AppComponent,
    AppComponent,
    AfternoonDetailsComponent,
    AfternoonDetailsMenuComponent,
    AfternoonMainComponent,
    AfternoonScheduleComponent,
    WeeklySchedulingComponent,
    LoginComponent,
    HeaderMenuComponent,
    MessageComponent,
    ManagersTableComponent,
    ManagementScheduleComponent,
    ManagementSettingsClustersComponent,
    ManagementSettingsJointComponent,
    ManagementMenuComponent,
    OperatorActivityReportComponent,
    OperatorCreditComponent,
    OperatorDetailsComponent,
    OperatorMessagesComponent,
    OperatorReviewComponent,
    OperatorScheduleComponent,
    OperatorMenuComponent,
    ProgramDetailsComponent,
    ProgramDetailsMenuComponent,
    ProgramReportComponent,
    ProgramsComponent,
    ProgramScheduleComponent,
    SettingsComponent,
    SettingsDetailsComponent,
    SettingsDetailsMenuComponent,
<<<<<<< HEAD
    OperatorTableComponent
=======
    OperatorTableComponent,
    OperatorsComponent,
>>>>>>> parent of e8819d2... Merge pull request #45 from Yael-Noyzath/noyzath
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatMenuModule,
    MatToolbarModule,
    MatIconModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatRadioModule,
    MatSelectModule,
    MatOptionModule,
    MatSlideToggleModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatGridListModule,
    HttpClientModule,
    RouterModule,
    RoutModule,
    FormsModule,
    ReactiveFormsModule,
    MatExpansionModule,



   //FlexLayoutModule
   AgGridModule

  ],
  providers: [{ provide: ErrorStateMatcher, useClass: ShowOnDirtyErrorStateMatcher }],
  bootstrap: [AppComponent]
})
export class AppModule { }