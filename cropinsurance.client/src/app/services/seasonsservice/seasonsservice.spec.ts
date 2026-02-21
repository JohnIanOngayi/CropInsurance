import { TestBed } from '@angular/core/testing';

import { Seasonsservice } from './seasonsservice';

describe('Seasonsservice', () => {
  let service: Seasonsservice;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Seasonsservice);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
