import { TestBed, inject } from '@angular/core/testing';

import { FriendsApiService } from './friends-api.service';

describe('FriendsApiService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FriendsApiService]
    });
  });

  it('should be created', inject([FriendsApiService], (service: FriendsApiService) => {
    expect(service).toBeTruthy();
  }));
});
