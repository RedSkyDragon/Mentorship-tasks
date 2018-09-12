import { TestBed, inject } from '@angular/core/testing';

import { CategoriesApiService } from './categories-api.service';

describe('ApiService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CategoriesApiService]
    });
  });

  it('should be created', inject([CategoriesApiService], (service: CategoriesApiService) => {
    expect(service).toBeTruthy();
  }));
});
