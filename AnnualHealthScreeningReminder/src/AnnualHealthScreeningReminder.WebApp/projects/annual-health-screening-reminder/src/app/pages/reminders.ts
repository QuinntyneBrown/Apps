import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatCardModule } from '@angular/material/card';
import { ReminderService } from '../services';

@Component({
  selector: 'app-reminders',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatChipsModule, MatCardModule],
  templateUrl: './reminders.html',
  styleUrl: './reminders.scss'
})
export class Reminders implements OnInit {
  private readonly reminderService = inject(ReminderService);

  reminders$ = this.reminderService.reminders$;
  displayedColumns = ['reminderDate', 'message', 'status', 'actions'];

  ngOnInit(): void {
    this.reminderService.getAll().subscribe();
  }

  markAsSent(id: string): void {
    this.reminderService.markAsSent(id).subscribe();
  }

  deleteReminder(id: string): void {
    if (confirm('Are you sure you want to delete this reminder?')) {
      this.reminderService.delete(id).subscribe();
    }
  }
}
