import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule, MatButtonModule, MatSidenavModule, MatIconModule, MatListModule, MatGridListModule, MatCardModule, MatMenuModule,MatDialogModule } from '@angular/material';
import {  AppRoutingModule} from "./app-routing.module";
import { DashboardComponent } from './dashboard/dashboard.component';
import { HttpClientModule } from "@angular/common/http";
import { AlpsSelectorComponent } from './infrastructure/alps-selector/alps-selector.component';
import { FormsModule } from "@angular/forms";
import { AlpsSelectorDialogComponent } from './infrastructure/alps-selector/alps-selector-dialog/alps-selector-dialog.component';
@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    AlpsSelectorComponent,
    AlpsSelectorDialogComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,AppRoutingModule, MatGridListModule, MatCardModule, MatMenuModule,HttpClientModule,FormsModule,MatDialogModule
  ],
  entryComponents:[AlpsSelectorDialogComponent],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
