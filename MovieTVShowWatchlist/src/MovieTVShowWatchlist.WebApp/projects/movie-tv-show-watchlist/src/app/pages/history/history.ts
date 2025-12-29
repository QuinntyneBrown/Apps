import { Component, inject } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { map, startWith, switchMap } from 'rxjs';
import { HistoryService } from '../../services';
import { ViewingRecord } from '../../models';

@Component({
  selector: 'app-history',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatIconModule, MatChipsModule, DatePipe],
  templateUrl: './history.html',
  styleUrl: './history.scss'
})
export class History {
  private _historyService = inject(HistoryService);

  viewModel$ = this._historyService.loadHistory().pipe(
    switchMap(() => this._historyService.history$),
    map(records => ({ records })),
    startWith({ records: [] as ViewingRecord[] })
  );

  getStars(rating: number | undefined): string {
    if (!rating) return '';
    return '★'.repeat(rating) + '☆'.repeat(5 - rating);
  }

  formatRuntime(minutes: number): string {
    const hours = Math.floor(minutes / 60);
    const mins = minutes % 60;
    if (hours > 0) {
      return `${hours}h ${mins}m`;
    }
    return `${mins}m`;
  }
}
