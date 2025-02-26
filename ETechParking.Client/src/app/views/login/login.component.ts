import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../../services/login/login.service'; 
import { Login } from '../../models/login/login.model'; 
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; 

@Component({
  selector: 'app-login',
  imports: [
    CommonModule,
    FormsModule, 
  ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  @Input() canAccessMainLayout: boolean = false;
  loginData: Login = new Login(); 

  constructor(private loginService: LoginService, private router: Router) { }

  onLogin() {
    this.loginService.create('Users/login', this.loginData).subscribe((response:any) => {
        console.log('Login successful', response);
        this.router.navigate(['/locations']);
      },
    
    );
  }

  navigate() {
    this.router.navigate(['/register']);
  }
}
