import { Injectable } from '@angular/core';
import { BaseModel } from '../../shared/base-model.model';

@Injectable({
  providedIn: 'root'
})
export class Fare extends BaseModel<number> {
  amount!: number;
  fareType!: number;
  fareTypeName!: string;
  enterGracePeriod!: number;
  exitGracePeriod!: number;
  locationId!: number;
}
