export class FieldConfig {
  type!: string;
  label!: string;
  key!: string;
  required?: boolean;
  options?: any[];
  placeholder?: string;
  labelField?: string;
  valueField?: string;
  validation?: any;
  visible?: boolean;
  onChange?: any;
  completeMethod?: any;
  onSelect?: any;
}