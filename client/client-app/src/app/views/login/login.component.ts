import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../../services/login/login.service';
import { Login } from '../../models/login/login.model';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth/auth.service';
import { ResetPasswordService } from '../../services/reset-password/reset-password.service';
import { DxButtonModule, DxLoadIndicatorModule, DxTemplateModule } from 'devextreme-angular';


@Component({
  selector: 'app-login',
  standalone: true,

  imports: [
    CommonModule,
    FormsModule,
    DxButtonModule,
    DxLoadIndicatorModule,
    DxTemplateModule
  ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  @Input() canAccessMainLayout: boolean = false;
  @Output() loginClicked = new EventEmitter<boolean>();
  loginData: Login = new Login();
  loginModel: Login = new Login();
  resetModel: any = {}; 
  showResetPasswordForm: boolean = false;
  loading: boolean = false;
  constructor(private loginService: LoginService, private router: Router, private authService: AuthService, private resetPasswordService: ResetPasswordService) { }

  onLogin() {
    this.loading = true;
    this.loginService.login(this.loginModel).subscribe({
      next: (response) => {
        console.log('Token:', response.token);
        localStorage.setItem('token', response.token);
        localStorage.setItem('roleName', response.roleName);
        if (response.isFirstLogin === true) {
          this.showResetPasswordForm = true; 
        } else if (response.isFirstLogin === false) {
          this.loginClicked.emit();
          this.router.navigate(['/tickets']);
        }
      },
      error: (error) => {
        console.error('Login failed', error);
        alert('Login failed. Please check your credentials.');
      },
      complete: () => {
        this.loading = false;
      }
    });
  }
  onResetPassword() {
    this.resetPasswordService.resetPassword('Users/ResetPassword', this.resetModel).subscribe({
      next: (response: any) => {
        console.log('Password reset successful', response);
        this.showResetPasswordForm = false; 
      },
      error: (error) => {
        console.error('Password reset failed', error);
        alert('Password reset failed. Please try again.');
      }
    });

  }

}
