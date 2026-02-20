import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-nav-shell',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './nav-shell.component.html',
  styleUrls: ['./nav-shell.component.scss']
})
export class NavShellComponent {
  @Input() activeTab: 'home' | 'contacts' | 'interactions' | 'follow-ups' | 'settings' = 'home';
}
