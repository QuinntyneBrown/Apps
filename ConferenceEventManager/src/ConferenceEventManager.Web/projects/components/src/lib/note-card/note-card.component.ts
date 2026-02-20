import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'lib-note-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './note-card.component.html',
  styleUrls: ['./note-card.component.scss']
})
export class NoteCardComponent {
  @Input({ required: true }) title!: string;
  @Input({ required: true }) body!: string;
  @Input({ required: true }) date!: string;
  @Input() badgeLabel: string = '';
  @Input() badgeColor: 'primary' | 'accent' | 'success' | 'warning' = 'primary';
}
