import { Component } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { Router } from '@angular/router';
import { UserApiService } from '../service/user-apiservice/user-apiservice';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  constructor(private oauthService: OAuthService, private router: Router, private userService: UserApiService) {
    this.oauthService.loadDiscoveryDocument().then(() => {
      this.oauthService.tryLogin({ onTokenReceived: context => {
        this.oauthService.loadUserProfile().then((profile) => {
          userService.getUser()
            .subscribe((data) => {
              if (!data) {
                userService.createUser()
                  .subscribe(() => {
                    this.login(profile);
                  });
              }
              this.login(profile);
            });
        });
      }});
    });
  }

  private login(profile) {
    if (profile['preferred_username']) {
      localStorage.setItem('name', profile['preferred_username']);
    } else {
      localStorage.setItem('name', profile['name']);
    }
    const returnUrl = localStorage.getItem('returnUrl');
    if (returnUrl) {
      localStorage.removeItem('returnUrl');
      this.router.navigate([returnUrl]);
    } else {
      this.router.navigate(['/']);
    }
  }
}
