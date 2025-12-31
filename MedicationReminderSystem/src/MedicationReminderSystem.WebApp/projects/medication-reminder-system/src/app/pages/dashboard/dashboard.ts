import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MedicationService, DoseScheduleService, RefillService } from '../../services';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  private medicationService = inject(MedicationService);
  private scheduleService = inject(DoseScheduleService);
  private refillService = inject(RefillService);

  medications$ = this.medicationService.medications$;
  schedules$ = this.scheduleService.doseSchedules$;
  refills$ = this.refillService.refills$;

  ngOnInit(): void {
    this.medicationService.getAll().subscribe();
    this.scheduleService.getAll().subscribe();
    this.refillService.getAll().subscribe();
  }
}
