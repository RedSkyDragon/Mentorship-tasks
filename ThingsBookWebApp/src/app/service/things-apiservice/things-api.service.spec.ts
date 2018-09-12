import { TestBed, inject } from '@angular/core/testing';

import { ThingsApiService } from './things-api.service';

describe('ThingsApiService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ThingsApiService]
    });
  });

  it('should be created', inject([ThingsApiService], (service: ThingsApiService) => {
    expect(service).toBeTruthy();
  }));
});
