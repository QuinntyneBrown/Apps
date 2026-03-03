import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-nav-shell',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './nav-shell.component.html',
  styleUrls: ['./nav-shell.component.scss']
})
export class NavShellComponent {
  @Input() activeRoute: string = 'dashboard';
  @Output() navigate = new EventEmitter<string>();

  sidebarLinks = [
    { route: 'dashboard', label: 'Dashboard', icon: 'dashboard' },
    { route: 'claims', label: 'Claims', icon: 'description' },
    { route: 'expenses', label: 'Expenses', icon: 'receipt_long' },
    { route: 'employees', label: 'Employees', icon: 'group' },
    { route: 'categories', label: 'Categories', icon: 'category' },
    { route: 'settings', label: 'Settings', icon: 'settings' }
  ];

  tabLinks = [
    { route: 'dashboard', label: 'Home', icon: 'home' },
    { route: 'claims', label: 'Claims', icon: 'description' },
    { route: 'expenses', label: 'Expenses', icon: 'receipt_long' },
    { route: 'profile', label: 'Profile', icon: 'person' }
  ];

  onNavigate(route: string): void {
    this.navigate.emit(route);
  }
}
