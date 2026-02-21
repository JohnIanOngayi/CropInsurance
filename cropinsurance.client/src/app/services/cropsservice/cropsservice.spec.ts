import { TestBed } from '@angular/core/testing';

import { Cropsservice } from '../cropsservice';

describe('Cropsservice', () => {
  let service: Cropsservice;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Cropsservice);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
