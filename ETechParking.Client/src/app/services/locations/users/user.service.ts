import { Injectable } from '@angular/core';
import { BaseService } from '../../shared/base.service';
import { User } from '../../../models/locations/users/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService<User> {
  constructor() {
    super();
  }
}
