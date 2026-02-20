import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-section-header',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './section-header.html',
  styleUrl: './section-header.scss'
})
export class SectionHeader {
  @Input({ required: true }) title!: string;
  @Input() badgeText?: string;
  @Input() badgeColor: 'primary' | 'success' | 'info' | 'warning' = 'success';
}
