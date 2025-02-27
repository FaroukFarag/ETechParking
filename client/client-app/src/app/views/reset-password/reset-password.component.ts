import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth/auth.service'; 

@Component({
  selector: 'app-reset-password',
  standalone: true,
  imports: [],
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.scss'
})
export class ResetPasswordComponent {
  newPassword: string = '';
  confirmPassword: string = '';

  constructor(private authService: AuthService, private router: Router) { }


  resetPassword() {
    if (this.newPassword === this.confirmPassword) {
      // Call the service to reset the password
      this.authService.resetPassword(this.newPassword).subscribe(
        (response) => {
          alert('Password reset successful!');
          this.router.navigate(['/login']); // Redirect to login after successful reset
        },
        (error) => {
          console.error('Password reset failed', error);
          alert('Password reset failed. Please try again.');
        }
      );
    } else {
      alert('Passwords do not match. Please try again.');
    }
  }

}
