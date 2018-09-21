import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../service/authentication/authentication.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {

  constructor(private authService: AuthenticationService) { }

  public get isAuth() {
    return this.authService.isAuthorized;
  }

  public get userName() {
    return localStorage.getItem('name');
  }

  ngOnInit() {
  }

  Login() {
    this.authService.login();
  }

  Logout() {
    this.authService.logout();
  }
}
