import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-contact-detail-hero',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './contact-detail-hero.component.html',
  styleUrls: ['./contact-detail-hero.component.scss']
})
export class ContactDetailHeroComponent {
  @Input({ required: true }) name!: string;
  @Input({ required: true }) role!: string;
  @Input({ required: true }) initials!: string;
  @Input() avatarColor: 'primary' | 'accent' | 'success' | 'warning' | 'danger' = 'primary';
  @Input() badges: { label: string; color: string }[] = [];
  @Input() infoRows: { icon: string; text: string; isLink?: boolean }[] = [];
  @Output() logInteraction = new EventEmitter<void>();
  @Output() addFollowUp = new EventEmitter<void>();

  onLogInteraction(): void {
    this.logInteraction.emit();
  }

  onAddFollowUp(): void {
    this.addFollowUp.emit();
  }
}
