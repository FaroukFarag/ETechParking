import { Injectable } from '@angular/core';
import { BaseService } from '../shared/base.service';
import { Location } from '../../models/locations/location.model';

@Injectable({
  providedIn: 'root'
})
export class LocationService extends BaseService<Location> {
  constructor() { 
    super();
  }

  getTotalLocations() {
    return this.http.get<number>(`${this.baseUrl}/Locations/GetTotalLocations`);
  }
}
