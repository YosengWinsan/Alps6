import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule, MatButtonModule, MatSidenavModule, MatIconModule, MatListModule, MatGridListModule, MatCardModule, MatMenuModule,MatDialogModule, MatPaginator, MatPaginatorIntl } from '@angular/material';
import {  AppRoutingModule} from "./app-routing.module";
import { DashboardComponent } from './dashboard/dashboard.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { InfrastructureModule } from "./infrastructure/infrastructure.module";
//import { AlpsSelectorComponent } from './infrastructure/alps-selector/alps-selector.component';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { AppContainerComponent } from './app-container/app-container.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { ForgotPasswordComponent } from './auth/forgot-password/forgot-password.component';
import { AuthHttpInterceptor } from './auth/auth-http-interceptor';
import { MatPaginatorLocal } from './extends/MatPaginatorLocal';
import { ForbidComponent } from './auth/forbid/forbid.component';
@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    AppContainerComponent,
    LoginComponent,
    RegisterComponent,
    ForgotPasswordComponent,
    ForbidComponent
    //,
  //  AlpsSelectorComponent,
  //  AlpsSelectorDialogComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,AppRoutingModule, MatGridListModule, MatCardModule, MatMenuModule,HttpClientModule,FormsModule,MatDialogModule,InfrastructureModule,ReactiveFormsModule
  ],
  entryComponents:[],
  providers: [{provide:HTTP_INTERCEPTORS,useClass:AuthHttpInterceptor,multi:true},{provide:MatPaginatorIntl,useClass:MatPaginatorLocal}],
  bootstrap: [AppComponent]
})
export class AppModule { }
