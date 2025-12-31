import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatCardModule } from '@angular/material/card';
import { ComparisonService } from '../services';

@Component({
  selector: 'app-comparison-list',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatCardModule
  ],
  template: `
    <div class="comparison-list">
      <div class="comparison-list__header">
        <h1 class="comparison-list__title">Comparisons</h1>
        <button mat-raised-button color="primary" (click)="navigateToCreate()">
          <mat-icon>add</mat-icon>
          Add Comparison
        </button>
      </div>

      <mat-card class="comparison-list__card">
        <div *ngIf="comparisonService.loading$ | async" class="comparison-list__loading">
          <mat-spinner></mat-spinner>
        </div>

        <div *ngIf="!(comparisonService.loading$ | async)">
          <table mat-table [dataSource]="(comparisonService.comparisons$ | async) || []" class="comparison-list__table">
            <ng-container matColumnDef="name">
              <th mat-header-cell *matHeaderCellDef>Name</th>
              <td mat-cell *matCellDef="let comparison">{{ comparison.name }}</td>
            </ng-container>

            <ng-container matColumnDef="productIds">
              <th mat-header-cell *matHeaderCellDef>Product IDs</th>
              <td mat-cell *matCellDef="let comparison">{{ comparison.productIds }}</td>
            </ng-container>

            <ng-container matColumnDef="results">
              <th mat-header-cell *matHeaderCellDef>Results</th>
              <td mat-cell *matCellDef="let comparison">{{ comparison.results || 'N/A' }}</td>
            </ng-container>

            <ng-container matColumnDef="winnerProductId">
              <th mat-header-cell *matHeaderCellDef>Winner Product ID</th>
              <td mat-cell *matCellDef="let comparison">{{ comparison.winnerProductId || 'N/A' }}</td>
            </ng-container>

            <ng-container matColumnDef="createdAt">
              <th mat-header-cell *matHeaderCellDef>Created At</th>
              <td mat-cell *matCellDef="let comparison">{{ comparison.createdAt | date:'short' }}</td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let comparison">
                <button mat-icon-button color="primary" (click)="navigateToEdit(comparison.comparisonId)">
                  <mat-icon>edit</mat-icon>
                </button>
                <button mat-icon-button color="warn" (click)="deleteComparison(comparison.comparisonId)">
                  <mat-icon>delete</mat-icon>
                </button>
              </td>
            </ng-container>

            <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
            <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
          </table>

          <div *ngIf="(comparisonService.comparisons$ | async)?.length === 0" class="comparison-list__empty">
            <mat-icon class="comparison-list__empty-icon">compare_arrows</mat-icon>
            <p>No comparisons found. Create your first comparison to get started!</p>
          </div>
        </div>
      </mat-card>
    </div>
  `,
  styles: [`
    .comparison-list {
      padding: 2rem;
      max-width: 1400px;
      margin: 0 auto;
    }

    .comparison-list__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .comparison-list__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 500;
    }

    .comparison-list__card {
      overflow: auto;
    }

    .comparison-list__loading {
      display: flex;
      justify-content: center;
      padding: 3rem;
    }

    .comparison-list__table {
      width: 100%;
    }

    .comparison-list__empty {
      text-align: center;
      padding: 3rem;
      color: rgba(0, 0, 0, 0.6);
    }

    .comparison-list__empty-icon {
      font-size: 64px;
      width: 64px;
      height: 64px;
      color: rgba(0, 0, 0, 0.26);
    }
  `]
})
export class ComparisonList implements OnInit {
  private readonly router = inject(Router);
  public readonly comparisonService = inject(ComparisonService);

  displayedColumns: string[] = ['name', 'productIds', 'results', 'winnerProductId', 'createdAt', 'actions'];

  ngOnInit(): void {
    this.comparisonService.getAll().subscribe();
  }

  navigateToCreate(): void {
    this.router.navigate(['/comparisons/new']);
  }

  navigateToEdit(id: string): void {
    this.router.navigate(['/comparisons', id]);
  }

  deleteComparison(id: string): void {
    if (confirm('Are you sure you want to delete this comparison?')) {
      this.comparisonService.delete(id).subscribe();
    }
  }
}
