import { BaseModel } from "../shared/base-model.model";

export class resetPassword extends BaseModel<number> {
  userName!: any;
  email!: any;
  oldPassword!: any;
  newPassword!: any;

}

