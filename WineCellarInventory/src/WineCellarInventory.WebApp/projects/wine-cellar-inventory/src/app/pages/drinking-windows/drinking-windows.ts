import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { combineLatest, map } from 'rxjs';
import { WineService, DrinkingWindowService } from '../../services';
import { DrinkingWindow } from '../../models';

@Component({
  selector: 'app-drinking-windows',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatTableModule, MatButtonModule, MatIconModule, MatChipsModule],
  templateUrl: './drinking-windows.html',
  styleUrl: './drinking-windows.scss'
})
export class DrinkingWindows {
  private _wineService = inject(WineService);
  private _drinkingWindowService = inject(DrinkingWindowService);

  displayedColumns: string[] = ['wineName', 'startDate', 'endDate', 'status', 'notes', 'actions'];

  viewModel$ = combineLatest([
    this._wineService.wines$,
    this._drinkingWindowService.drinkingWindows$
  ]).pipe(
    map(([wines, drinkingWindows]) => {
      const now = new Date();

      return drinkingWindows.map(dw => {
        const wine = wines.find(w => w.wineId === dw.wineId);
        const start = new Date(dw.startDate);
        const end = new Date(dw.endDate);

        let status: 'upcoming' | 'current' | 'past';
        if (now < start) {
          status = 'upcoming';
        } else if (now >= start && now <= end) {
          status = 'current';
        } else {
          status = 'past';
        }

        return {
          ...dw,
          wineName: wine?.name || 'Unknown',
          status
        };
      }).sort((a, b) => new Date(a.startDate).getTime() - new Date(b.startDate).getTime());
    })
  );

  ngOnInit(): void {
    this._wineService.getWines().subscribe();
    this._drinkingWindowService.getDrinkingWindows().subscribe();
  }

  onDelete(drinkingWindow: DrinkingWindow): void {
    if (confirm('Are you sure you want to delete this drinking window?')) {
      this._drinkingWindowService.deleteDrinkingWindow(drinkingWindow.drinkingWindowId).subscribe();
    }
  }

  getStatusClass(status: string): string {
    return `drinking-windows__status--${status}`;
  }

  getStatusLabel(status: string): string {
    return status.charAt(0).toUpperCase() + status.slice(1);
  }
}
