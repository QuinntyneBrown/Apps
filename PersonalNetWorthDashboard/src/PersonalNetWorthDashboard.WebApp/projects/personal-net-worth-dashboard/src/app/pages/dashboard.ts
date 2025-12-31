import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { RouterLink } from '@angular/router';
import { AssetService, LiabilityService } from '../services';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, RouterLink],
  template: `
    <div class="dashboard">
      <h1 class="dashboard__title">Net Worth Dashboard</h1>

      <div class="dashboard__cards">
        <mat-card class="dashboard__card dashboard__card--assets">
          <mat-card-header>
            <mat-card-title>Total Assets</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__amount">
              {{ (totalAssets$ | async) | currency }}
            </div>
            <div class="dashboard__count">
              {{ (activeAssetsCount$ | async) }} active assets
            </div>
          </mat-card-content>
          <mat-card-actions>
            <button mat-button color="primary" routerLink="/assets">View Assets</button>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card dashboard__card--liabilities">
          <mat-card-header>
            <mat-card-title>Total Liabilities</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__amount">
              {{ (totalLiabilities$ | async) | currency }}
            </div>
            <div class="dashboard__count">
              {{ (activeLiabilitiesCount$ | async) }} active liabilities
            </div>
          </mat-card-content>
          <mat-card-actions>
            <button mat-button color="primary" routerLink="/liabilities">View Liabilities</button>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card dashboard__card--networth">
          <mat-card-header>
            <mat-card-title>Net Worth</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__amount dashboard__amount--large">
              {{ (netWorth$ | async) | currency }}
            </div>
          </mat-card-content>
          <mat-card-actions>
            <button mat-button color="primary" routerLink="/snapshots">View Snapshots</button>
          </mat-card-actions>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .dashboard {
      padding: 2rem;

      &__title {
        margin-bottom: 2rem;
        color: #333;
      }

      &__cards {
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
        gap: 2rem;
      }

      &__card {
        &--assets {
          border-left: 4px solid #4caf50;
        }

        &--liabilities {
          border-left: 4px solid #f44336;
        }

        &--networth {
          border-left: 4px solid #2196f3;
        }
      }

      &__amount {
        font-size: 2rem;
        font-weight: 700;
        margin: 1rem 0;
        color: #333;

        &--large {
          font-size: 2.5rem;
          color: #2196f3;
        }
      }

      &__count {
        color: #666;
        font-size: 0.875rem;
      }
    }
  `]
})
export class Dashboard implements OnInit {
  private assetService = inject(AssetService);
  private liabilityService = inject(LiabilityService);

  totalAssets$ = this.assetService.assets$.pipe(
    map(assets => assets.filter(a => a.isActive).reduce((sum, a) => sum + a.currentValue, 0))
  );

  activeAssetsCount$ = this.assetService.assets$.pipe(
    map(assets => assets.filter(a => a.isActive).length)
  );

  totalLiabilities$ = this.liabilityService.liabilities$.pipe(
    map(liabilities => liabilities.filter(l => l.isActive).reduce((sum, l) => sum + l.currentBalance, 0))
  );

  activeLiabilitiesCount$ = this.liabilityService.liabilities$.pipe(
    map(liabilities => liabilities.filter(l => l.isActive).length)
  );

  netWorth$ = this.totalAssets$.pipe(
    map((assets, index) => {
      let liabilities = 0;
      this.totalLiabilities$.subscribe(l => liabilities = l);
      return assets - liabilities;
    })
  );

  ngOnInit(): void {
    this.assetService.getAssets().subscribe();
    this.liabilityService.getLiabilities().subscribe();
  }
}
