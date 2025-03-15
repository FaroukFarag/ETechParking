import { Injectable } from '@angular/core';
import { BaseService } from '../shared/base.service';
import { Tickets } from '../../models/tickets/tickets.model'
import { HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class TicketsService extends BaseService <Tickets>  {

  constructor() {
    super()
  }

  getTotalTickets(req: any) {
    return this.http.post<number>(`${this.baseUrl}/Tickets/GetTotalTickets`, req);
  }

  getTicketsTransactionType(req: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/Tickets/GetTransactionTypeStatistics`, req);
  }

  getTicketsClientType(req: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/Tickets/GetClientTypeStatistics`, req);
  }
}
