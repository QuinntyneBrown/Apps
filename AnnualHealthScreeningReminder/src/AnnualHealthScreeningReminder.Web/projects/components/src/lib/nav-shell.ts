import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

export interface NavItem {
  icon: string;
  label: string;
  route: string;
}

@Component({
  selector: 'app-nav-shell',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './nav-shell.html',
  styleUrl: './nav-shell.scss'
})
export class NavShell {
  @Input({ required: true }) appName!: string;
  @Input() appIcon = 'health_and_safety';
  @Input({ required: true }) navItems: NavItem[] = [];
}
