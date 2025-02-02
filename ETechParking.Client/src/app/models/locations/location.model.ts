import { BaseModel } from "../shared/base-model.model";

export class Location extends BaseModel<number> {
  name!: string;
  country!: string;
  city!: string;
  longitude!: number;
  latitude!: number;
}
