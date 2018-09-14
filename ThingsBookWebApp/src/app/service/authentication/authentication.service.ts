import { Injectable } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  constructor(private oauthService: OAuthService, private router: Router) { }

  public get claims() {
    return this.oauthService.getIdentityClaims();
  }

  public get accessToken() {
    if (Date.now() > this.oauthService.getAccessTokenExpiration()) {
      this.login();
    }
    return this.oauthService.getAccessToken();
  }

  public get isAuthorized(): boolean {
    return (localStorage.getItem('name') !== null) && (Date.now() <= this.oauthService.getAccessTokenExpiration());
  }

  login() {
    this.oauthService.initImplicitFlow();
  }

  logout() {
    this.oauthService.logOut();
    localStorage.removeItem('name');
  }
}
