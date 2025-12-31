import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatGridListModule } from '@angular/material/grid-list';
import { Observable, combineLatest, map } from 'rxjs';
import { PetService, MedicationService, VetAppointmentService } from '../../services';
import { Pet, Medication, VetAppointment } from '../../models';

interface DashboardStats {
  totalPets: number;
  upcomingAppointments: number;
  activeMedications: number;
}

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatGridListModule
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  private petService = inject(PetService);
  private medicationService = inject(MedicationService);
  private appointmentService = inject(VetAppointmentService);

  stats$!: Observable<DashboardStats>;
  recentPets$!: Observable<Pet[]>;
  upcomingAppointments$!: Observable<VetAppointment[]>;

  ngOnInit(): void {
    this.petService.getPets().subscribe();
    this.medicationService.getMedications().subscribe();
    this.appointmentService.getAppointments().subscribe();

    this.stats$ = combineLatest([
      this.petService.pets$,
      this.medicationService.medications$,
      this.appointmentService.appointments$
    ]).pipe(
      map(([pets, medications, appointments]) => ({
        totalPets: pets.length,
        upcomingAppointments: this.getUpcomingCount(appointments),
        activeMedications: this.getActiveCount(medications)
      }))
    );

    this.recentPets$ = this.petService.pets$.pipe(
      map(pets => pets.slice(0, 3))
    );

    this.upcomingAppointments$ = this.appointmentService.appointments$.pipe(
      map(appointments => {
        const now = new Date();
        return appointments
          .filter(a => new Date(a.appointmentDate) >= now)
          .sort((a, b) => new Date(a.appointmentDate).getTime() - new Date(b.appointmentDate).getTime())
          .slice(0, 3);
      })
    );
  }

  private getUpcomingCount(appointments: VetAppointment[]): number {
    const now = new Date();
    return appointments.filter(a => new Date(a.appointmentDate) >= now).length;
  }

  private getActiveCount(medications: Medication[]): number {
    const now = new Date();
    return medications.filter(m => {
      if (!m.endDate) return true;
      return new Date(m.endDate) >= now;
    }).length;
  }
}
