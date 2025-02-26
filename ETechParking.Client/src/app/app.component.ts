import { Component, ViewChild } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SideMenuComponent } from "./views/shared/components/side-menu/side-menu.component";
import { AppLayoutComponent } from './views/app-layout/app-layout.component';
import { SidebarComponent } from './views/app-layout/sidebar/sidebar.component';
import { HeaderComponent } from './views/app-layout/header/header.component';
import { ContentComponent } from './views/app-layout/content/content.component';
import { FooterComponent } from './views/app-layout/footer/footer.component';

@Component({
  selector: 'app-root',
  imports: [
    RouterOutlet, 
    SideMenuComponent,
    AppLayoutComponent,
    HeaderComponent,
    ContentComponent,
    FooterComponent,
    SidebarComponent
  ],
  
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  @ViewChild(SidebarComponent) sidebar!: SidebarComponent;

  title = 'ETechParking.Client';
}
