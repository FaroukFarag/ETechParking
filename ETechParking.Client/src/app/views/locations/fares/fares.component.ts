import { Component, inject, OnInit } from '@angular/core';
import { DynamicGridComponent } from "../../shared/dynamic-grid/dynamic-grid.component";
import { FareService } from '../../../services/locations/fares/fare.service';
import { LocationService } from '../../../services/locations/location.service';
import { Location } from '../../../models/locations/location.model';
import { FieldConfig } from '../../../models/shared/field-config.model';

@Component({
  selector: 'app-fares',
  imports: [DynamicGridComponent],
  providers: [],
  templateUrl: './fares.component.html',
  styleUrl: './fares.component.css'
})
export class FaresComponent implements OnInit {
  fareService = inject(FareService);
  locationService = inject(LocationService);

  locations!: Location[];

  fareFields!: FieldConfig[];

  fareColumns = [
    { field: 'amount', header: 'Amount' },
    { field: 'fareTypeName', header: 'Type' }
  ];

  ngOnInit(): void {
    this.getLocations();

    this.loadFieldsConfig();
  }

  getLocations() {
    this.locationService.getAll('Locations/GetAll').subscribe({
      next: data => {
        this.locations = data;

        const locationField = this.fareFields.find(f => f.key === 'locationId');

        if (locationField) {
          locationField.options = this.locations;
        }
      }
    });
  }

  loadFieldsConfig() {
    this.fareFields = [
      { type: 'number', label: 'Amount', key: 'amount', required: true, placeholder: 'Enter amount' },
      {
        type: 'select',
        label: 'Type',
        key: 'fareType',
        required: true,
        placeholder: 'Select a type',
        options: [
          { label: 'Hourly', value: 1 },
          { label: 'Daily', value: 2 }
        ],
        onChange: this.onFareTypeChange.bind(this),
      },
      { type: 'number', label: 'Entering Grace Period', key: 'enterGracePeriod', required: true, placeholder: 'Enter entering grace period' },
      { type: 'number', label: 'Exit Grace Period', key: 'exitGracePeriod', required: true, placeholder: 'Enter exit grace period' },
      {
        type: 'number',
        label: 'Max Daily Limit',
        key: 'maxLimit',
        required: false,
        placeholder: 'Enter max limit',
        visible: false,
        onReset: () => {
          const fieldConfig = this.fareFields.find(fc => fc.key === 'maxLimit');
          
          if (fieldConfig) {
            fieldConfig.visible = false;
            fieldConfig.required = false;
          }
        }
      },
      {
        type: 'autocomplete',
        label: 'Location',
        key: 'locationId',
        required: true,
        placeholder: 'Select a location',
        options: this.locations,
        labelField: 'name',
        valueField: 'id',
        completeMethod: this.onCompleteMethod.bind(this)
      }
    ];
  }

  onFareTypeChange(event: any) {
    const fieldConfig = this.fareFields?.find(fc => fc.key === 'maxLimit');

    if (fieldConfig) {
      fieldConfig.visible = event.value === 1;
      fieldConfig.required = event.value === 1;
    }
  }

  onCompleteMethod(event: any, field: FieldConfig) {
    const query = event.query;

    field.options = this.locations.filter(location =>
      location['name'].toLowerCase().includes(query.toLowerCase())
    );
  }
}
