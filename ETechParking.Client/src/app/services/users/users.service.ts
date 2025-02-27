import { Injectable } from '@angular/core';
import { BaseService } from '../shared/base.service';
import { Users } from '../../models/users/users.model'
@Injectable({
  providedIn: 'root'
})
export class UsersService extends BaseService<Users>  {

  constructor() {
    super()
  }
}
