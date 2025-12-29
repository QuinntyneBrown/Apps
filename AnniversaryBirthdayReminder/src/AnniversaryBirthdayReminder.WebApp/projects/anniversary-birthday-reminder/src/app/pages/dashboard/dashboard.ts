import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { map } from 'rxjs/operators';
import { DateService } from '../../services';
import { DateType } from '../../models';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard {
  private readonly dateService = inject(DateService);

  viewModel$ = this.dateService.getUpcomingDates().pipe(
    map(dates => ({
      upcomingDates: dates.map(date => ({
        ...date,
        daysUntil: this.dateService.getDaysUntil(date),
        icon: this.dateService.getDateTypeIcon(date.dateType),
        formattedDate: this.formatDate(new Date(date.dateValue))
      }))
    }))
  );

  private formatDate(date: Date): string {
    return date.toLocaleDateString('en-US', { month: 'long', day: 'numeric' });
  }

  getDaysLabel(days: number): string {
    if (days === 0) return 'Today';
    if (days === 1) return 'Tomorrow';
    return `In ${days} days`;
  }
}
