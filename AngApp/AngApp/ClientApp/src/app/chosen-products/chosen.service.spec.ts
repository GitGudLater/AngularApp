import { TestBed, inject } from '@angular/core/testing';

import { ChosenService } from './chosen.service';

describe('ChosenService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ChosenService]
    });
  });

  it('should be created', inject([ChosenService], (service: ChosenService) => {
    expect(service).toBeTruthy();
  }));
});
