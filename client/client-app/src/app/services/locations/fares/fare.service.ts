import { Injectable } from '@angular/core';
import { BaseService } from '../../shared/base.service';
import { Fare } from '../../../models/locations/fares/fare.model';

@Injectable({
  providedIn: 'root'
})
export class FareService extends BaseService<Fare> {
  constructor() {
    super();
  }
}
