import { Injectable } from '@angular/core';
import { BaseService } from '../shared/base.service';
import { Tickets } from '../../models/tickets/tickets.model'
@Injectable({
  providedIn: 'root'
})
export class TicketsService extends BaseService <Tickets>  {

  constructor() {
    super()
  }
}
