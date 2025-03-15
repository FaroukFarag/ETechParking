
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


  getTotalShifts(req: any) {
    return this.http.post<number>(`${this.baseUrl}/Shifts/GetTotalShifts`, req);
  }

  getShiftTickets(shiftId: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/Shifts/GetAllShiftTickets?shiftId=${shiftId}`);
  }
}
