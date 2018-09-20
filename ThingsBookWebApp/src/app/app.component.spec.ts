import { TestBed, async } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
describe('AppComponent', () => {
  const oAuthServiceStub = {
    configure: () => {},
    setStorage: () => {},
    loadDiscoveryDocument: () => {},
    setupAutomaticSilentRefresh: () => {}
  };
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        AppComponent
      ],
      schemas: [ NO_ERRORS_SCHEMA ],
      providers: [ { provide: OAuthService, useValue: oAuthServiceStub } ]

    }).compileComponents();
  }));
  it('should create the app', async(() => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  }));
});
