import { Injectable } from '@angular/core';
import { BaseModel } from '../../shared/base-model.model';

@Injectable({
  providedIn: 'root'
})
export class User extends BaseModel<number> {
  userName!: string;
  email!: string;
  password!: string;
  locationId!: number;
  roleId!: number;
}
