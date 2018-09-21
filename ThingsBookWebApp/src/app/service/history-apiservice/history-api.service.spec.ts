import { TestBed, inject } from '@angular/core/testing';

import { HistoryApiService } from './history-api.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AuthenticationService } from '../authentication/authentication.service';
import { History } from '../../models/history';

describe('HistoryApiService', () => {
  let http: HttpTestingController;
  const authServiceStub = {
    accessToken: () => {}
  };
  const expectedData: History[] = [
    { Id: '1', LendDate: new Date(), ReturnDate: new Date(), Comment: 'Comment', Thing: null , Friend: null },
    { Id: '2', LendDate: new Date(), ReturnDate: new Date(), Comment: 'Comment', Thing: null , Friend: null },
    { Id: '3', LendDate: new Date(), ReturnDate: new Date(), Comment: 'Comment', Thing: null , Friend: null },
  ];

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ HttpClientTestingModule ],
      providers: [
        HistoryApiService,
        { provide: AuthenticationService, useValue: authServiceStub }
      ]
    });
    http = TestBed.get(HttpTestingController);
  });

  afterEach(() => {
      http.verify();
  });

  it('should be created', inject([HistoryApiService], (service: HistoryApiService) => {
    expect(service).toBeTruthy();
  }));

  it('should get data', inject([HistoryApiService], (service: HistoryApiService) => {
    service.getHistory().subscribe((data) => {
      expect(data).toEqual(expectedData);
    });
    const req = http.expectOne(service.baseUrl + 'history');
    expect(req.request.method).toEqual('GET');
    req.flush(expectedData);
  }));
});
