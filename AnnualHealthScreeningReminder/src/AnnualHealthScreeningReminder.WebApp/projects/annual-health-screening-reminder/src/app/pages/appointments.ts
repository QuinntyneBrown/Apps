import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatCardModule } from '@angular/material/card';
import { AppointmentService } from '../services';

@Component({
  selector: 'app-appointments',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatChipsModule, MatCardModule],
  templateUrl: './appointments.html',
  styleUrl: './appointments.scss'
})
export class Appointments implements OnInit {
  private readonly appointmentService = inject(AppointmentService);

  appointments$ = this.appointmentService.appointments$;
  displayedColumns = ['appointmentDate', 'location', 'provider', 'status', 'notes', 'actions'];

  ngOnInit(): void {
    this.appointmentService.getAll().subscribe();
  }

  deleteAppointment(id: string): void {
    if (confirm('Are you sure you want to delete this appointment?')) {
      this.appointmentService.delete(id).subscribe();
    }
  }
}
