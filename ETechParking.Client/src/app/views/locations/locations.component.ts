import { Component, inject, OnInit } from '@angular/core';
import { LocationService } from '../../services/locations/location.service';
import { FieldConfig } from '../../models/shared/field-config.model';
import { DynamicGridComponent } from "../shared/dynamic-grid/dynamic-grid.component";

@Component({
  selector: 'app-locations',
  imports: [DynamicGridComponent],
  templateUrl: './locations.component.html',
  styleUrl: './locations.component.css'
})
export class LocationsComponent implements OnInit {
  locationService = inject(LocationService);

  locationColumns = [
    { field: 'name', header: 'Name' },
    { field: 'country', header: 'Country' },
    { field: 'city', header: 'City' }
  ];

  locationFields: FieldConfig[] = [
    { type: 'text', label: 'Name', key: 'name', required: true, placeholder: 'Enter name' },
    { type: 'text', label: 'Country', key: 'country', required: true, placeholder: 'Enter country' },
    { type: 'text', label: 'City', key: 'city', required: true, placeholder: 'Enter city' },
    { type: 'number', label: 'Longitude', key: 'longitude', required: true, placeholder: 'Enter longitude' },
    { type: 'number', label: 'Latitude', key: 'latitude', required: true, placeholder: 'Enter latitude' },
  ];

  ngOnInit(): void {
  }
}
