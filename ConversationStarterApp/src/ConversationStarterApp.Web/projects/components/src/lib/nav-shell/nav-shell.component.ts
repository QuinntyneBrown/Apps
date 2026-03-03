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
  @Input() activeTab: 'home' | 'prompts' | 'collections' | 'progress' | 'settings' = 'home';
  @Output() tabChange = new EventEmitter<string>();

  onTabClick(tab: string): void {
    this.tabChange.emit(tab);
  }
}
