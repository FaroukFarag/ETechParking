import { Injectable } from '@angular/core';
import { BaseService } from '../shared/base.service';
import { Login } from '../../models/login/login.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService extends BaseService<Login>  {
  constructor() {
    super();
  }

  login(loginData: Login): Observable<any> {
    return this.create('Users/login', loginData); 
  }
}
