import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatSidenavModule,
    MatListModule
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'Anniversary Birthday Reminder';

  navItems = [
    { path: '/', icon: 'dashboard', label: 'Dashboard' },
    { path: '/dates', icon: 'event', label: 'Dates' },
    { path: '/reminders', icon: 'notifications', label: 'Reminders' },
    { path: '/celebrations', icon: 'celebration', label: 'Celebrations' }
  ];
}
