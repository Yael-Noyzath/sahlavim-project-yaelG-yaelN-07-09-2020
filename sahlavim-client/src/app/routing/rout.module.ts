import { NgModule, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { combineLatest } from 'rxjs';

const appTable: Routes = [];


@NgModule({
  imports: [
    RouterModule.forRoot(appTable),
    CommonModule
  ],
  declarations: []
})
export class RoutModule { }
