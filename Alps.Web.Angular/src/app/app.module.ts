import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

//Angular Material
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';
import { MatMenuModule } from '@angular/material/menu';
import { MatDialogModule } from '@angular/material/dialog';
import { MatPaginatorIntl } from '@angular/material/paginator';
//Angular
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
//Alps

import { InfrastructureModule } from "./infrastructure/infrastructure.module";
import { DashboardComponent } from './dashboard/dashboard.component';
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
    AppComponent,
    DashboardComponent,
    AppContainerComponent,
    LoginComponent,
    RegisterComponent,
    ForgotPasswordComponent,
    ForbidComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule, AppRoutingModule, MatGridListModule, MatCardModule, MatMenuModule, HttpClientModule, FormsModule,
     MatDialogModule, InfrastructureModule, ReactiveFormsModule
  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: AuthHttpInterceptor, multi: true }, { provide: MatPaginatorIntl, useClass: MatPaginatorLocal }],
  bootstrap: [AppComponent]
})
export class AppModule { }
