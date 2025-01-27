import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-locations',
  imports: [ReactiveFormsModule],
  templateUrl: './locations.component.html',
  styleUrl: './locations.component.css'
})
export class LocationsComponent implements OnInit {
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
