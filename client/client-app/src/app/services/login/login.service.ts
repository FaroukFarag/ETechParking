import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseService } from '../shared/base.service';
import { Login } from '../../models/login/login.model';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LoginService  {
  protected baseUrl: string;
  protected http = inject(HttpClient);
  constructor() {
      this.baseUrl = `${environment.apiUrl}`;
   //   super();
  }


  login(loginModel: Login): Observable<any> {
    return this.http.post(`${this.baseUrl}/Users/login`, loginModel);
  }

  resetPassword(data: any): Observable<any> {
    return this.http.post(`${environment.apiUrl}/Users/ResetPassword`, data);
  }



}
