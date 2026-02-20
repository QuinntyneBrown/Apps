import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-contact-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './contact-card.component.html',
  styleUrls: ['./contact-card.component.scss']
})
export class ContactCardComponent {
  @Input({ required: true }) name!: string;
  @Input({ required: true }) role!: string;
  @Input({ required: true }) initials!: string;
  @Input() avatarColor: 'primary' | 'accent' | 'success' | 'warning' = 'primary';
  @Input() badgeLabel: string = '';
  @Input() badgeColor: 'primary' | 'accent' | 'success' | 'warning' = 'success';
}
