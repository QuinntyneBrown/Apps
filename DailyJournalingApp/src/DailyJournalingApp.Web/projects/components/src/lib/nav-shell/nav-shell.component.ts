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
    { route: 'entries', label: 'Entries', icon: 'book' },
    { route: 'prompts', label: 'Prompts', icon: 'edit_note' },
    { route: 'analytics', label: 'Analytics', icon: 'analytics' },
    { route: 'tags', label: 'Tags', icon: 'label' },
    { route: 'settings', label: 'Settings', icon: 'settings' }
  ];

  tabLinks = [
    { route: 'dashboard', label: 'Home', icon: 'home' },
    { route: 'entries', label: 'Entries', icon: 'book' },
    { route: 'prompts', label: 'Prompts', icon: 'edit_note' },
    { route: 'analytics', label: 'Insights', icon: 'analytics' }
  ];

  onNavigate(route: string): void {
    this.navigate.emit(route);
  }
}
