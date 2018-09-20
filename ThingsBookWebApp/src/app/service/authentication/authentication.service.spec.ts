import { TestBed, inject } from '@angular/core/testing';

import { AuthenticationService } from './authentication.service';
import { OAuthService } from 'angular-oauth2-oidc';
import { Router } from '@angular/router';

describe('AuthenticationService', () => {
  const authServiceStub = {
    getAccessTokenExpiration: () => {},
    getIdentityClaims: () => {},
    getAccessToken: () => true,
    initImplicitFlow: () => {},
    logOut: () => {}
  };
  const routerStub = { };

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        AuthenticationService,
        { provide: OAuthService, useValue: authServiceStub },
        { provide: Router, useValue: routerStub }
      ]
    });
  });

  it('should be created', inject([AuthenticationService], (service: AuthenticationService) => {
    expect(service).toBeTruthy();
  }));

  it('should call getIdentityClaims', inject([AuthenticationService, OAuthService],
      (service: AuthenticationService, oauth: OAuthService) => {
    const spy = spyOn(oauth, 'getIdentityClaims');
    const res = service.claims;
    expect(spy).toHaveBeenCalled();
  }));

  it('should call getAccessTokenExpiration', inject([AuthenticationService, OAuthService],
    (service: AuthenticationService, oauth: OAuthService) => {
    const spy = spyOn(oauth, 'getAccessTokenExpiration');
    const res = service.hasExpired;
    expect(spy).toHaveBeenCalled();
  }));

  it('should call getAccessToken', inject([AuthenticationService, OAuthService],
    (service: AuthenticationService, oauth: OAuthService) => {
    const spy = spyOn(oauth, 'getAccessToken');
    const res = service.accessToken;
    expect(spy).toHaveBeenCalled();
  }));

  it('should call initImplicitFlow', inject([AuthenticationService, OAuthService],
    (service: AuthenticationService, oauth: OAuthService) => {
    const spy = spyOn(oauth, 'initImplicitFlow');
    service.login();
    expect(spy).toHaveBeenCalled();
  }));

  it('should call logOut', inject([AuthenticationService, OAuthService],
    (service: AuthenticationService, oauth: OAuthService) => {
    const spy = spyOn(oauth, 'logOut');
    service.logout();
    expect(spy).toHaveBeenCalled();
  }));
});
