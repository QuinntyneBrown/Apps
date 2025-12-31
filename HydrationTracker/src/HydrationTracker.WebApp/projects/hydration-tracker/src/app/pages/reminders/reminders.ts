import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { ReminderService } from '../../services';
import { ReminderDialog } from '../../components/reminder-dialog/reminder-dialog';
import { Reminder, CreateReminderCommand, UpdateReminderCommand } from '../../models';

@Component({
  selector: 'app-reminders',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatChipsModule,
    MatDialogModule
  ],
  templateUrl: './reminders.html',
  styleUrls: ['./reminders.scss']
})
export class Reminders implements OnInit {
  private reminderService = inject(ReminderService);
  private dialog = inject(MatDialog);

  reminders$ = this.reminderService.reminders$;
  loading$ = this.reminderService.loading$;

  ngOnInit(): void {
    this.reminderService.getReminders().subscribe();
  }

  onAdd(): void {
    const dialogRef = this.dialog.open(ReminderDialog, {
      width: '500px',
      data: {}
    });

    dialogRef.afterClosed().subscribe((result: CreateReminderCommand) => {
      if (result) {
        this.reminderService.createReminder(result).subscribe();
      }
    });
  }

  onEdit(reminder: Reminder): void {
    const dialogRef = this.dialog.open(ReminderDialog, {
      width: '500px',
      data: { reminder }
    });

    dialogRef.afterClosed().subscribe((result: UpdateReminderCommand) => {
      if (result) {
        this.reminderService.updateReminder(reminder.reminderId, result).subscribe();
      }
    });
  }

  onDelete(id: string): void {
    if (confirm('Are you sure you want to delete this reminder?')) {
      this.reminderService.deleteReminder(id).subscribe();
    }
  }
}
