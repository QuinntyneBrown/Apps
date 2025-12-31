import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ProductService, NutritionInfoService, ComparisonService } from '../services';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule],
  template: `
    <div class="dashboard">
      <h1 class="dashboard__title">Dashboard</h1>
      <div class="dashboard__cards">
        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon class="dashboard__card-icon dashboard__card-icon--products">inventory_2</mat-icon>
            <mat-card-title>Products</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-count">{{ (productService.products$ | async)?.length || 0 }}</div>
            <p class="dashboard__card-description">Total scanned products</p>
          </mat-card-content>
          <mat-card-actions>
            <button mat-raised-button color="primary" (click)="navigateToProducts()">
              View Products
            </button>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon class="dashboard__card-icon dashboard__card-icon--nutrition">local_dining</mat-icon>
            <mat-card-title>Nutrition Info</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-count">{{ (nutritionInfoService.nutritionInfos$ | async)?.length || 0 }}</div>
            <p class="dashboard__card-description">Nutrition facts analyzed</p>
          </mat-card-content>
          <mat-card-actions>
            <button mat-raised-button color="primary" (click)="navigateToNutritionInfos()">
              View Nutrition Info
            </button>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon class="dashboard__card-icon dashboard__card-icon--comparisons">compare_arrows</mat-icon>
            <mat-card-title>Comparisons</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-count">{{ (comparisonService.comparisons$ | async)?.length || 0 }}</div>
            <p class="dashboard__card-description">Product comparisons</p>
          </mat-card-content>
          <mat-card-actions>
            <button mat-raised-button color="primary" (click)="navigateToComparisons()">
              View Comparisons
            </button>
          </mat-card-actions>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .dashboard {
      padding: 2rem;
      max-width: 1400px;
      margin: 0 auto;
    }

    .dashboard__title {
      margin: 0 0 2rem 0;
      font-size: 2rem;
      font-weight: 500;
    }

    .dashboard__cards {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
      gap: 1.5rem;
    }

    .dashboard__card {
      display: flex;
      flex-direction: column;
    }

    .dashboard__card mat-card-header {
      display: flex;
      align-items: center;
      gap: 1rem;
    }

    .dashboard__card-icon {
      font-size: 48px;
      width: 48px;
      height: 48px;
    }

    .dashboard__card-icon--products {
      color: #3f51b5;
    }

    .dashboard__card-icon--nutrition {
      color: #4caf50;
    }

    .dashboard__card-icon--comparisons {
      color: #ff9800;
    }

    .dashboard__card-count {
      font-size: 3rem;
      font-weight: 600;
      margin: 1rem 0 0.5rem 0;
      color: rgba(0, 0, 0, 0.87);
    }

    .dashboard__card-description {
      margin: 0;
      color: rgba(0, 0, 0, 0.6);
    }

    mat-card-actions {
      margin-top: auto;
      padding: 16px;
    }
  `]
})
export class Dashboard implements OnInit {
  private readonly router = inject(Router);
  public readonly productService = inject(ProductService);
  public readonly nutritionInfoService = inject(NutritionInfoService);
  public readonly comparisonService = inject(ComparisonService);

  ngOnInit(): void {
    this.productService.getAll().subscribe();
    this.nutritionInfoService.getAll().subscribe();
    this.comparisonService.getAll().subscribe();
  }

  navigateToProducts(): void {
    this.router.navigate(['/products']);
  }

  navigateToNutritionInfos(): void {
    this.router.navigate(['/nutrition-infos']);
  }

  navigateToComparisons(): void {
    this.router.navigate(['/comparisons']);
  }
}
