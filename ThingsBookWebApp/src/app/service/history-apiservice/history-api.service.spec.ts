import { TestBed, inject } from '@angular/core/testing';

import { HistoryApiService } from './history-api.service';

describe('HistoryApiService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HistoryApiService]
    });
  });

  it('should be created', inject([HistoryApiService], (service: HistoryApiService) => {
    expect(service).toBeTruthy();
  }));
});
