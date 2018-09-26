import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { LoginComponent } from './login.component';
import { MatProgressSpinnerModule } from '@angular/material';
import { OAuthService } from 'angular-oauth2-oidc';
import { Router } from '@angular/router';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  const oAuthServiceStub = {
    loadDiscoveryDocument: () => new Promise<object>(() => {})
  };
  const routerStub = { };

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LoginComponent ],
      imports: [ MatProgressSpinnerModule, HttpClientTestingModule ],
      providers: [
        { provide: OAuthService, useValue: oAuthServiceStub },
        { provide: Router, useValue: routerStub }
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create login component', () => {
    expect(component).toBeTruthy();
  });
});
