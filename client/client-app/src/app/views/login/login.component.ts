import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../../services/login/login.service'; 
import { Login } from '../../models/login/login.model'; 
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; 
import { AuthService } from '../../services/auth/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,

  imports: [
    CommonModule,
    FormsModule, 
  ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  @Input() canAccessMainLayout: boolean = false;
  //@Output() loginSuccess = new EventEmitter<void>();
  @Output() loginClicked = new EventEmitter<void>();
  loginData: Login = new Login(); 
  loginModel: Login = new Login();

  constructor(private loginService: LoginService, private router: Router, private authService: AuthService) { }

  onLogin() {

    this.loginService.login(this.loginModel).subscribe({
      next: (response) => {
        console.log('Token:', response.token);
        localStorage.setItem('token', response.token);
        
        if (response.isFirstLogin) {
          this.router.navigate(['/reset-password']);
        } else {
          this.loginClicked.emit();
          this.router.navigate(['/locations']);
        }
        
      },
      error: (error) => {
        console.error('Login failed', error);
        alert('Login failed. Please check your credentials.');
      }
    });
  }


}
