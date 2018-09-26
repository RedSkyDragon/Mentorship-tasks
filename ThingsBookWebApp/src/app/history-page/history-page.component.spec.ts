import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HistoryPageComponent } from './history-page.component';
import { MatTableModule, MatSortModule, MatPaginatorModule, MatProgressSpinnerModule,
  MatFormFieldModule, MatInputModule } from '@angular/material';
import { HistoryApiService } from '../service/history-apiservice/history-api.service';
import { of, Observable } from 'rxjs';
import { History } from '../models/history';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('HistoryPageComponent', () => {
  let component: HistoryPageComponent;
  let fixture: ComponentFixture<HistoryPageComponent>;
  const historyApiServiceStub = {
    getHistory: (): Observable<History[]> => of([] as History[]),
    deleteHistory: () => of()
  };

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HistoryPageComponent ],
      imports: [
        BrowserModule,
        BrowserAnimationsModule,
        MatTableModule,
        MatSortModule,
        MatPaginatorModule,
        MatProgressSpinnerModule,
        MatFormFieldModule,
        MatInputModule
      ],
      providers: [
        { provide: HistoryApiService, useValue: historyApiServiceStub }
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HistoryPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create history page', () => {
    expect(component).toBeTruthy();
  });

  it('should call getHistory', () => {
    const apiService = TestBed.get(HistoryApiService);
    const getSpy = spyOn(apiService, 'getHistory');
    const initSpy = spyOn(component, 'ngOnInit').and.callFake(apiService.getHistory);
    component.ngOnInit();
    expect(getSpy).toHaveBeenCalled();
  });
});
