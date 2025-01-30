import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-edit-location',
  imports: [ReactiveFormsModule],
  templateUrl: './edit-location.component.html',
  styleUrl: './edit-location.component.css'
})
export class EditLocationComponent {
  private formBuilder = inject(FormBuilder);
  locationsForm: FormGroup;
  
  constructor() {
    this.locationsForm = this.createForm();
  }

  ngOnInit(): void {
  }

  createForm() {
    return this.formBuilder.group({
      country: [''],
      city: ['']
    });
  }

  onSubmit() {
    console.log(this.locationsForm.value)
  }
}
