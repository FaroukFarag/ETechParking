
import { Injectable } from '@angular/core';
import { BaseService } from '../shared/base.service';
import { Shifts } from '../../models/shifts/shifts.model';
@Injectable({
  providedIn: 'root'
})
export class ShiftsService extends BaseService<Shifts> {

  constructor() {
    super();
  }
}
