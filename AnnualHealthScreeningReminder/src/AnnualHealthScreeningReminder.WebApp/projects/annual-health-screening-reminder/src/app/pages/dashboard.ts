import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { ScreeningService, AppointmentService, ReminderService } from '../services';
import { map, combineLatest } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  private readonly screeningService = inject(ScreeningService);
  private readonly appointmentService = inject(AppointmentService);
  private readonly reminderService = inject(ReminderService);

  viewModel$ = combineLatest({
    screenings: this.screeningService.screenings$,
    appointments: this.appointmentService.appointments$,
    reminders: this.reminderService.reminders$
  }).pipe(
    map(({ screenings, appointments, reminders }) => ({
      dueSoonScreenings: screenings.filter(s => s.isDueSoon),
      upcomingAppointments: appointments.filter(a => a.isUpcoming && !a.isCompleted),
      pendingReminders: reminders.filter(r => !r.isSent),
      totalScreenings: screenings.length,
      totalAppointments: appointments.length,
      totalReminders: reminders.length
    }))
  );

  ngOnInit(): void {
    this.screeningService.getAll().subscribe();
    this.appointmentService.getAll().subscribe();
    this.reminderService.getAll().subscribe();
  }
}
