import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SideMenuComponent } from "./views/shared/side-menu/side-menu.component";

@Component({
  selector: 'app-root',
  imports: [
    RouterOutlet, 
    SideMenuComponent
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'ETechParking.Client';
}
