import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { DxButtonModule, DxTextBoxModule } from 'devextreme-angular';

@Component({
  selector: 'app-register',
  imports: [FormsModule, DxTextBoxModule, DxButtonModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  user = {
    userName: '',
    email: '',
    password: '',
    phoneNumber: '',
    locationId: 0
  };

  constructor(private http: HttpClient, private router: Router) { }


  onRegister() {
    this.http.post('/api/Users/register', this.user).subscribe(() => {
      this.router.navigate(['/login']); // Redirect to login after successful registration
    });


  }


  onSignIn() { }

}
