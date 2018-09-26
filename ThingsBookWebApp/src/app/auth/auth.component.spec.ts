import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AuthComponent } from './auth.component';
import { MatButtonModule } from '@angular/material';
import { AuthenticationService } from '../service/authentication/authentication.service';

describe('AuthComponent', () => {
  let component: AuthComponent;
  let fixture: ComponentFixture<AuthComponent>;
  const authServiceStub = {
    isAuthorized: () => true,
    login: () => {},
    logout: () => {}
  };

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AuthComponent ],
      imports: [ MatButtonModule ],
      providers: [
        { provide: AuthenticationService, useValue: authServiceStub }
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create auth component', () => {
    expect(component).toBeTruthy();
  });
});
