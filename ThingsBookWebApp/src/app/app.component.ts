import { Component } from '@angular/core';
import { OAuthService, JwksValidationHandler } from 'angular-oauth2-oidc';
import { AuthConfiguration } from './service/authentication/AuthConfiguration';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ThingsBookWebApp';
  constructor(private oauthService: OAuthService) {
    this.ConfigureAuthentication();
  }

  private ConfigureAuthentication() {
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
}
