/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { GalerieService } from './galerie.service';

describe('Service: Galerie', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GalerieService]
    });
  });

  it('should ...', inject([GalerieService], (service: GalerieService) => {
    expect(service).toBeTruthy();
  }));
});
