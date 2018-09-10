import { Injectable } from '@angular/core';
import { AuthConfiguration } from './AuthConfiguration';
import { OAuthService, JwksValidationHandler } from 'angular-oauth2-oidc';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  constructor(private oauthService: OAuthService) {
    this.oauthService.configure(AuthConfiguration);
    this.oauthService.setStorage(localStorage);
    this.oauthService.tokenValidationHandler = new JwksValidationHandler();
    this.oauthService.loadDiscoveryDocument().then(() => {
      this.oauthService.tryLogin({ onTokenReceived: context => {
        this.oauthService.loadUserProfile().then((profile) => {
          if (profile['preferred_username']) {
            localStorage.setItem('name', profile['preferred_username']);
          } else {
            localStorage.setItem('name', profile['name']);
          }
        });
      }});
    });
   }

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
