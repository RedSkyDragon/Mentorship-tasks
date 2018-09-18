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

  public get hasExpired(): boolean {
    return Date.now() > this.oauthService.getAccessTokenExpiration();
  }

  public get accessToken() {
    // console.log(this.oauthService.getAccessTokenExpiration());
    return this.oauthService.getAccessToken();
  }

  public get isAuthorized(): boolean {
    return (localStorage.getItem('name') !== null);
  }

  login(returnUrl?: string) {
    if (returnUrl) {
      localStorage.setItem('returnUrl', returnUrl);
    }
    this.oauthService.initImplicitFlow();
  }

  logout() {
    this.oauthService.logOut();
    localStorage.removeItem('name');
  }
}
