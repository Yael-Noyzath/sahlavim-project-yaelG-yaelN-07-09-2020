import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

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
  MatSlideToggleModule, ErrorStateMatcher, ShowOnDirtyErrorStateMatcher, MatTableModule
} from '@angular/material';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { MatExpansionModule} from '@angular/material/expansion';
import { AfternoonDetailsComponent } from './components/Afternoons/afternoon-details/afternoon-details.component';
import { AfternoonDetailsMenuComponent } from './components/Afternoons/afternoon-details-menu/afternoon-details-menu.component';
import { AfternoonMainComponent } from './components/Afternoons/afternoon-main/afternoon-main.component';
import { AfternoonScheduleComponent } from './components/Afternoons/afternoon-schedule/afternoon-schedule.component';
import { WeeklySchedulingComponent } from './components/Afternoons/weekly-scheduling/weekly-scheduling.component';
import { LoginComponent } from './components/login/login/login.component';
import { HeaderMenuComponent } from './components/main/header-menu/header-menu.component';
import { MessageComponent } from './components/main/message/message.component';
import { ManagersTableComponent } from './components/Management/managers-table/managers-table.component';
import { ManagementScheduleComponent } from './components/Management/management-schedule/management-schedule.component';
import { ManagementSettingsClustersComponent } from './components/Management/management-settings-clusters/management-settings-clusters.component';
import { ManagementSettingsJointComponent } from './components/Management/management-settings-joint/management-settings-joint.component';
import { OperatorActivityComponent } from './components/Operators/operator-activity/operator-activity.component';
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
    OperatorActivityComponent,
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
    ProgramScheduleComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatMenuModule,
    MatToolbarModule,
    MatIconModule,
    MatCardModule,
    BrowserAnimationsModule,
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
    FormsModule,
    ReactiveFormsModule,
    MatExpansionModule,
  ],
  providers: [{ provide: ErrorStateMatcher, useClass: ShowOnDirtyErrorStateMatcher }],
  bootstrap: [AppComponent]
})
export class AppModule { }