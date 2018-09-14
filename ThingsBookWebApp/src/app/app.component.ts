import { Component } from '@angular/core';
import { OAuthService, JwksValidationHandler } from 'angular-oauth2-oidc';
import { AuthConfiguration } from './service/authentication/AuthConfiguration';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(private oauthService: OAuthService) {
    this.ConfigureAuthentication();
  }

  private ConfigureAuthentication() {
    this.oauthService.configure(AuthConfiguration);
    this.oauthService.setStorage(localStorage);
    this.oauthService.tokenValidationHandler = new JwksValidationHandler();
    this.oauthService.loadDiscoveryDocument();
  }
}
