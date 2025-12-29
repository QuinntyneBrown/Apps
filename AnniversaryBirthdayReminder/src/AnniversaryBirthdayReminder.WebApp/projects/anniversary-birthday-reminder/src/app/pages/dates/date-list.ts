import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatChipsModule } from '@angular/material/chips';
import { MatMenuModule } from '@angular/material/menu';
import { map } from 'rxjs/operators';
import { DateService } from '../../services';

@Component({
  selector: 'app-date-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatChipsModule,
    MatMenuModule
  ],
  templateUrl: './date-list.html',
  styleUrl: './date-list.scss'
})
export class DateList {
  private readonly dateService = inject(DateService);

  displayedColumns = ['icon', 'personName', 'dateType', 'date', 'relationship', 'actions'];

  viewModel$ = this.dateService.getDates().pipe(
    map(dates => ({
      dates: dates.map(date => ({
        ...date,
        icon: this.dateService.getDateTypeIcon(date.dateType),
        formattedDate: new Date(date.dateValue).toLocaleDateString('en-US', {
          month: 'long',
          day: 'numeric'
        }),
        daysUntil: this.dateService.getDaysUntil(date)
      }))
    }))
  );

  deleteDate(dateId: string): void {
    this.dateService.deleteDate(dateId).subscribe();
  }
}
