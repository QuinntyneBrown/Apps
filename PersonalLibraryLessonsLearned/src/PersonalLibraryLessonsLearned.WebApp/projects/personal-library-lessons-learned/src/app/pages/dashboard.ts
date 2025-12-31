import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { LessonService, SourceService, LessonReminderService } from '../services';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  private lessonService = inject(LessonService);
  private sourceService = inject(SourceService);
  private reminderService = inject(LessonReminderService);

  lessons$ = this.lessonService.lessons$;
  sources$ = this.sourceService.sources$;
  reminders$ = this.reminderService.reminders$;

  ngOnInit(): void {
    this.lessonService.getLessons().subscribe();
    this.sourceService.getSources().subscribe();
    this.reminderService.getReminders().subscribe();
  }
}
