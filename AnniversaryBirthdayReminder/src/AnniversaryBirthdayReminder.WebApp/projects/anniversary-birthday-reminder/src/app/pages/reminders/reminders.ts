import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatListModule } from '@angular/material/list';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { map } from 'rxjs/operators';
import { ReminderService } from '../../services';
import { DeliveryChannel, ReminderSettings } from '../../models';

@Component({
  selector: 'app-reminders',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatCheckboxModule,
    MatButtonModule,
    MatIconModule,
    MatDividerModule,
    MatListModule,
    MatSnackBarModule
  ],
  templateUrl: './reminders.html',
  styleUrl: './reminders.scss'
})
export class Reminders {
  private readonly reminderService = inject(ReminderService);
  private readonly snackBar = inject(MatSnackBar);

  DeliveryChannel = DeliveryChannel;

  viewModel$ = this.reminderService.settings$.pipe(
    map(settings => ({ settings }))
  );

  updateOneWeekBefore(settings: ReminderSettings, checked: boolean): void {
    this.reminderService.updateLocalSettings({
      oneWeekBefore: checked,
      threeDaysBefore: settings.threeDaysBefore,
      oneDayBefore: settings.oneDayBefore,
      channels: settings.channels
    });
  }

  updateThreeDaysBefore(settings: ReminderSettings, checked: boolean): void {
    this.reminderService.updateLocalSettings({
      oneWeekBefore: settings.oneWeekBefore,
      threeDaysBefore: checked,
      oneDayBefore: settings.oneDayBefore,
      channels: settings.channels
    });
  }

  updateOneDayBefore(settings: ReminderSettings, checked: boolean): void {
    this.reminderService.updateLocalSettings({
      oneWeekBefore: settings.oneWeekBefore,
      threeDaysBefore: settings.threeDaysBefore,
      oneDayBefore: checked,
      channels: settings.channels
    });
  }

  toggleChannel(settings: ReminderSettings, channel: DeliveryChannel): void {
    const channels = settings.channels.includes(channel)
      ? settings.channels.filter(c => c !== channel)
      : [...settings.channels, channel];

    this.reminderService.updateLocalSettings({
      oneWeekBefore: settings.oneWeekBefore,
      threeDaysBefore: settings.threeDaysBefore,
      oneDayBefore: settings.oneDayBefore,
      channels
    });
  }

  hasChannel(settings: ReminderSettings, channel: DeliveryChannel): boolean {
    return settings.channels.includes(channel);
  }

  saveSettings(settings: ReminderSettings): void {
    this.reminderService.updateSettings(settings).subscribe(() => {
      this.snackBar.open('Settings saved successfully', 'Close', {
        duration: 3000
      });
    });
  }
}
