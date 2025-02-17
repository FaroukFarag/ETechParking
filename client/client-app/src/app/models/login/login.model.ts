import { BaseModel } from "../shared/base-model.model";

export class Login extends BaseModel<number> {
  userName!: string;
  password!: string;
}
