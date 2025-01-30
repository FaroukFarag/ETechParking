import { BaseModel } from "../shared/base-model.model";

export class Location extends BaseModel<number> {
  country!: string;
  city!: string;
}
