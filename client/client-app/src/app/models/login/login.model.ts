import { BaseModel } from "../shared/base-model.model";

export class Login extends BaseModel<number> {
  username!: string;
  password!: string;
}
