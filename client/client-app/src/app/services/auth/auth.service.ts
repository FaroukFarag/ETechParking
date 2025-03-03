
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://8.208.12.34:8080/api'
  constructor(private http: HttpClient,private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const token = localStorage.getItem('token');
    if (!token) {
      return true; 
    }
    return false;
  }
  isLoggedIn(): boolean {
    return !!localStorage.getItem('token'); 
  }
  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
    window.location.reload();
  }

  isAuthenticated() {

    return !!localStorage.getItem('token');

  }
  // Method to reset the password
  resetPassword(newPassword: string): Observable<any> {
    const userId = localStorage.getItem('userId'); 
    const payload = {
      userId: userId,
      newPassword: newPassword
    };

    return this.http.post(`${this.apiUrl}/users/reset-password`, payload);
  }
}
