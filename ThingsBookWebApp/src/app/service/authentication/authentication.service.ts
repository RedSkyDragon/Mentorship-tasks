import { Injectable } from '@angular/core';
import { OAuthService, JwksValidationHandler } from 'angular-oauth2-oidc';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  constructor(private oauthService: OAuthService) { }

  public get claims() {
    return this.oauthService.getIdentityClaims();
  }

  public get accessToken() {
    return this.oauthService.getAccessToken();
  }

  public get isAuthorized(): boolean {
    return localStorage.getItem('name') !== null;
  }

  login() {
    this.oauthService.initImplicitFlow();
  }

  logout() {
    this.oauthService.logOut();
    localStorage.removeItem('name');
  }
}
