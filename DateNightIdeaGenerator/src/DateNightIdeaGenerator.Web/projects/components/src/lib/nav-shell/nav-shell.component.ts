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
    { route: 'discover', label: 'Discover', icon: 'explore' },
    { route: 'saved', label: 'Saved', icon: 'bookmark' },
    { route: 'calendar', label: 'Calendar', icon: 'calendar_month' },
    { route: 'memories', label: 'Memories', icon: 'photo_library' },
    { route: 'settings', label: 'Settings', icon: 'settings' }
  ];

  tabLinks = [
    { route: 'dashboard', label: 'Home', icon: 'home' },
    { route: 'discover', label: 'Discover', icon: 'explore' },
    { route: 'saved', label: 'Saved', icon: 'bookmark' },
    { route: 'calendar', label: 'Calendar', icon: 'calendar_month' },
    { route: 'memories', label: 'Memories', icon: 'photo_library' }
  ];

  onNavigate(route: string): void {
    this.navigate.emit(route);
  }
}
