import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { LetterService } from '../services/letter.service';
import { DeliveryScheduleService } from '../services/delivery-schedule.service';
import { DeliveryStatus, DeliveryStatusLabels } from '../models';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  private letterService = inject(LetterService);
  private deliveryScheduleService = inject(DeliveryScheduleService);

  letters$ = this.letterService.letters$;
  deliverySchedules$ = this.deliveryScheduleService.deliverySchedules$;
  loading$ = this.letterService.loading$;

  ngOnInit(): void {
    this.letterService.getAll().subscribe();
    this.deliveryScheduleService.getAll().subscribe();
  }

  getPendingLettersCount(letters: any[]): number {
    return letters?.filter(l => l.deliveryStatus === DeliveryStatus.Pending).length || 0;
  }

  getDeliveredLettersCount(letters: any[]): number {
    return letters?.filter(l => l.deliveryStatus === DeliveryStatus.Delivered).length || 0;
  }

  getUnreadLettersCount(letters: any[]): number {
    return letters?.filter(l => l.deliveryStatus === DeliveryStatus.Delivered && !l.hasBeenRead).length || 0;
  }

  getDueForDeliveryCount(letters: any[]): number {
    return letters?.filter(l => l.isDueForDelivery).length || 0;
  }

  getActiveSchedulesCount(schedules: any[]): number {
    return schedules?.filter(s => s.isActive).length || 0;
  }
}
