import { TestBed, inject } from '@angular/core/testing';

import { AlpsLoadingBarService } from './alps-loading-bar.service';

describe('AlpsLoadingBarService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AlpsLoadingBarService]
    });
  });

  it('should be created', inject([AlpsLoadingBarService], (service: AlpsLoadingBarService) => {
    expect(service).toBeTruthy();
  }));
});
