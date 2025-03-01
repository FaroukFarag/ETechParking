import { Injectable } from '@angular/core';
import { BaseService } from '../shared/base.service';
import { resetPassword } from '../../models/reset-password/reset-password.model';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class ResetPasswordService extends BaseService<resetPassword>{

  constructor() {
    super();
  }



}
