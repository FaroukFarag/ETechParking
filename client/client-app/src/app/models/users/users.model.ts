import { BaseModel } from "../shared/base-model.model";

export class Users extends BaseModel<number> {
  userName!: any;
  email!: any;
  password!: any;
  phoneNumber!: any;
  locationId!: any;
  locationName!: any;
  roleId!: any;
  roleName!: any;
}

