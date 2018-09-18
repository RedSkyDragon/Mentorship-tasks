import { Component } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  constructor(private oauthService: OAuthService, private router: Router) {
    this.oauthService.loadDiscoveryDocument().then(() => {
      this.oauthService.tryLogin({ onTokenReceived: context => {
        this.oauthService.loadUserProfile().then((profile) => {
          if (profile['preferred_username']) {
            localStorage.setItem('name', profile['preferred_username']);
          } else {
            localStorage.setItem('name', profile['name']);
          }
          const returnUrl = localStorage.getItem('returnUrl');
          if (returnUrl) {
            localStorage.removeItem('returnUrl');
            router.navigate([returnUrl]);
          } else {
            router.navigate(['/']);
          }
        });
      }});
    });
  }
}
