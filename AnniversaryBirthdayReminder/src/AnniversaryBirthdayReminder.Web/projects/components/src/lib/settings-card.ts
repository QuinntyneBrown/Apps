import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

export interface SettingsOption {
  label: string;
  value: string;
  enabled?: boolean;
}

@Component({
  selector: 'app-settings-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './settings-card.html',
  styleUrl: './settings-card.scss'
})
export class SettingsCard {
  @Input({ required: true }) title!: string;
  @Input() subtitle?: string;
  @Input({ required: true }) icon!: string;
  @Input() iconColor: 'primary' | 'info' | 'success' | 'warning' = 'primary';
  @Input() options: SettingsOption[] = [];
}
