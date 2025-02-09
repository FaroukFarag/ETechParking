import { Component, inject, OnInit } from '@angular/core';
import { FieldConfig } from '../../../models/shared/field-config.model';
import { UserService } from '../../../services/locations/users/user.service';
import { DynamicGridComponent } from "../../shared/dynamic-grid/dynamic-grid.component";
import { LocationService } from '../../../services/locations/location.service';
import { Location } from '../../../models/locations/location.model';

@Component({
  selector: 'app-users',
  imports: [DynamicGridComponent],
  templateUrl: './users.component.html',
  styleUrl: './users.component.css'
})
export class UsersComponent implements OnInit {
  userService = inject(UserService);
  locationService = inject(LocationService);

  locations!: Location[];

  userColumns = [
    { field: 'userName', header: 'User Name' },
    { field: 'email', header: 'Email' },
    { field: 'locationName', header: 'Location' },
    { field: 'roleName', header: 'Role' }
  ];

  userFields: FieldConfig[] = [
    { type: 'text', label: 'Username', key: 'userName', required: true, placeholder: 'Enter username' },
    { type: 'text', label: 'Email', key: 'email', required: true, placeholder: 'Enter email' },
    { type: 'password', label: 'Password', key: 'password', required: true, placeholder: 'Enter password' },
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

  ngOnInit(): void {
    this.getLocations();
  }

  getLocations() {
    this.locationService.getAll('Locations/GetAll').subscribe({
      next: data => {
        this.locations = data;

        const locationField = this.userFields.find(u => u.key === 'locationId');

        if (locationField) {
          locationField.options = this.locations;
        }
      }
    });
  }

  onCompleteMethod(event: any, field: FieldConfig) {
    const query = event.query;

    field.options = this.locations.filter(location =>
      location['name'].toLowerCase().includes(query.toLowerCase())
    );
  }
}
