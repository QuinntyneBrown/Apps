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
  @Input() appName: string = 'ChoreTracker';
  @Input() navItems: NavItem[] = [
    { icon: 'home', label: 'Home', route: '/', active: true },
    { icon: 'chores', label: 'Chores', route: '/chores' },
    { icon: 'board', label: 'Board', route: '/leaderboard' },
    { icon: 'rewards', label: 'Rewards', route: '/rewards' }
  ];
}
