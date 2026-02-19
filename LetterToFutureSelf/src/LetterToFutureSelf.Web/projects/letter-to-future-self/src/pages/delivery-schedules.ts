import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTooltipModule } from '@angular/material/tooltip';
import { DeliveryScheduleService } from '../services';
import { DeliverySchedule } from '../models';

@Component({
  selector: 'app-delivery-schedules',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatChipsModule,
    MatProgressSpinnerModule,
    MatTooltipModule
  ],
  templateUrl: './delivery-schedules.html',
  styleUrl: './delivery-schedules.scss'
})
export class DeliverySchedules implements OnInit {
  private deliveryScheduleService = inject(DeliveryScheduleService);
  private router = inject(Router);

  deliverySchedules$ = this.deliveryScheduleService.deliverySchedules$;
  loading$ = this.deliveryScheduleService.loading$;

  displayedColumns: string[] = ['letterId', 'scheduledDateTime', 'deliveryMethod', 'recipientContact', 'isActive', 'actions'];

  ngOnInit(): void {
    this.deliveryScheduleService.getAll().subscribe();
  }

  onEdit(schedule: DeliverySchedule): void {
    this.router.navigate(['/delivery-schedules', schedule.deliveryScheduleId]);
  }

  onDelete(schedule: DeliverySchedule): void {
    if (confirm('Are you sure you want to delete this delivery schedule?')) {
      this.deliveryScheduleService.delete(schedule.deliveryScheduleId).subscribe();
    }
  }

  formatDateTime(dateString: string): string {
    return new Date(dateString).toLocaleString();
  }

  formatLetterId(letterId: string): string {
    return letterId.substring(0, 8);
  }
}
