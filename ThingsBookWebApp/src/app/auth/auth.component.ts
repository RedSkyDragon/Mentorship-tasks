import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../service/authentication/authentication.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {
  isAuthorized: boolean;
  userName = 'Sample';
  constructor(private authService: AuthenticationService) { }

  ngOnInit() {
  }

  Login() {
    this.authService.login();
  }

  Logout() {
    this.authService.logout();
  }

}
