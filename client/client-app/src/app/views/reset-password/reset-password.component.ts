import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth/auth.service'
import { LoginService } from '../../services/login/login.service';; 
import { FormsModule } from '@angular/forms'
import { ResetPasswordService } from '../../services/reset-password/reset-password.service';
@Component({
  selector: 'app-reset-password',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.scss'
})
export class ResetPasswordComponent {
  @Input() canAccessMainLayout: boolean = false;

  newPassword: string = '';
  confirmPassword: string = '';
  resetModel = {

    userName: '',

    oldPassword: '',

    newPassword: ''

  };

  constructor(private authService: AuthService, private router: Router, private loginService: LoginService, private resetPasswordService: ResetPasswordService) { }


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




  onResetPassword() {



    this.resetPasswordService.resetPassword('Users/ResetPassword', this.resetModel).subscribe({

      next: (response: any) => {

        console.log('Password reset successful', response);

        this.router.navigate(['/login']); 

      },

      error: (error) => {

        console.error('Password reset failed', error);

        alert('Password reset failed. Please try again.');

      }

    });

  }

}
