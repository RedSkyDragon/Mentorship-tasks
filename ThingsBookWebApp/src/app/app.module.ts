import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { OAuthModule } from 'angular-oauth2-oidc';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MainNavComponent } from './main-nav/main-nav.component';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule, MatButtonModule, MatSidenavModule, MatIconModule, MatListModule, MatTableModule } from '@angular/material';
import { HistoryPageComponent } from './history-page/history-page.component';
import { CategoriesPageComponent } from './categories-page/categories-page.component';
import { FriendsPageComponent } from './friends-page/friends-page.component';
import { ThingsPageComponent } from './things-page/things-page.component';
import { HomePageComponent } from './home-page/home-page.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { routing } from './app.routing';
import { AuthComponent } from './auth/auth.component';

@NgModule({
  declarations: [
    AppComponent,
    MainNavComponent,
    HistoryPageComponent,
    CategoriesPageComponent,
    FriendsPageComponent,
    ThingsPageComponent,
    HomePageComponent,
    UnauthorizedComponent,
    AuthComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    LayoutModule,
    routing,
    OAuthModule.forRoot(),
    HttpClientModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatTableModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
