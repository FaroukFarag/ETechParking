import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class RoleService {
  constructor() { }

  isAdmin(): boolean {
    const roleName = localStorage.getItem('roleName');
    return roleName === 'Admin';
  }

  isCashier(): boolean {
    const roleName = localStorage.getItem('roleName');
    return roleName === 'Cashier'; 
  } 
   isAccountant(): boolean {
    const roleName = localStorage.getItem('roleName');
    return roleName === 'Accountant'; 
  }

}
