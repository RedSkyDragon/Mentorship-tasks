import { TestBed, inject } from '@angular/core/testing';

import { LendsApiService } from './lends-api.service';

describe('LendsApiService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LendsApiService]
    });
  });

  it('should be created', inject([LendsApiService], (service: LendsApiService) => {
    expect(service).toBeTruthy();
  }));
});
