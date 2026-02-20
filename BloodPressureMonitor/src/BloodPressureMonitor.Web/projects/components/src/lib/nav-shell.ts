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
  templateUrl: './nav-shell.html',
  styleUrls: ['./nav-shell.scss']
})
export class NavShellComponent {
  @Input({ required: true }) navItems!: NavItem[];
  @Input() appName: string = 'BP Monitor';
  @Input() appIcon: string = 'monitor_heart';
}
