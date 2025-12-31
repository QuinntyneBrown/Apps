import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { combineLatest, map } from 'rxjs';
import { WineService, DrinkingWindowService } from '../../services';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, RouterLink],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard {
  private _wineService = inject(WineService);
  private _drinkingWindowService = inject(DrinkingWindowService);

  viewModel$ = combineLatest([
    this._wineService.wines$,
    this._drinkingWindowService.drinkingWindows$
  ]).pipe(
    map(([wines, drinkingWindows]) => {
      const now = new Date();
      const winesInWindow = drinkingWindows.filter(dw => {
        const start = new Date(dw.startDate);
        const end = new Date(dw.endDate);
        return now >= start && now <= end;
      });

      const totalValue = wines.reduce((sum, wine) => sum + (wine.purchasePrice || 0) * wine.bottleCount, 0);
      const totalBottles = wines.reduce((sum, wine) => sum + wine.bottleCount, 0);

      return {
        totalWines: wines.length,
        totalBottles,
        totalValue,
        winesInWindow: winesInWindow.length,
        recentWines: wines.slice(0, 5)
      };
    })
  );

  ngOnInit(): void {
    this._wineService.getWines().subscribe();
    this._drinkingWindowService.getDrinkingWindows().subscribe();
  }
}
