
import { Injectable } from '@angular/core';
import { BaseService } from '../shared/base.service';
import { Shift } from '../../models/shifts/users.model';
@Injectable({
  providedIn: 'root'
})
export class ShiftsService extends BaseService<Shift> {

  constructor() {
    super();
  }
}
