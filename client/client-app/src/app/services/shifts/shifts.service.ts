
import { Injectable } from '@angular/core';
import { BaseService } from '../shared/base.service';
import { Shifts } from '../../models/shifts/shifts.model';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class ShiftsService extends BaseService<Shifts> {
  constructor() {
    super();
  }


  getShiftTickets(shiftId: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/Shifts/GetAllShiftTickets?shiftId=${shiftId}`);
  }
}
