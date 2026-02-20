import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

export interface NavItem {
  icon: string;
  label: string;
  route: string;
  active?: boolean;
}

@Component({
  selector: 'lib-nav-shell',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './nav-shell.component.html',
  styleUrls: ['./nav-shell.component.scss']
})
export class NavShellComponent {
  @Input() appName: string = 'GiveTracker';
  @Input() navItems: NavItem[] = [
    { icon: 'home', label: 'Home', route: '/', active: true },
    { icon: 'donate', label: 'Donate', route: '/donations' },
    { icon: 'orgs', label: 'Orgs', route: '/organizations' },
    { icon: 'tax', label: 'Tax', route: '/tax-reports' }
  ];
}
