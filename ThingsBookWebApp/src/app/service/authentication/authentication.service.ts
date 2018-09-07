import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { AuthConfiguration } from './AuthConfiguration';
import { OAuthService, JwksValidationHandler } from 'angular-oauth2-oidc';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  constructor(private http: HttpClient, private oauthService: OAuthService) {
    this.oauthService.configure(AuthConfiguration);
    this.oauthService.tokenValidationHandler = new JwksValidationHandler();
    this.oauthService.loadDiscoveryDocument();
    // .then((doc) => {
    //   this.oauthService.tryLogin()
    //     .catch(err => {
    //       console.error(err);
    //     })
    //     .then(() => {
    //       if (!this.oauthService.hasValidAccessToken()) {
    //         this.oauthService.initImplicitFlow();
    //       }
    //     });
    //   });
   }

  public get claims() { return this.oauthService.getIdentityClaims(); }

  public get accessToken() { return this.oauthService.getAccessToken(); }

  login() {
    this.oauthService.initImplicitFlow();
  }

  logout() {
    this.oauthService.logOut();
  }
}
