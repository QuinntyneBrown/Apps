import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatCardModule } from '@angular/material/card';
import { NutritionInfoService } from '../services';

@Component({
  selector: 'app-nutrition-info-list',
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
    <div class="nutrition-info-list">
      <div class="nutrition-info-list__header">
        <h1 class="nutrition-info-list__title">Nutrition Info</h1>
        <button mat-raised-button color="primary" (click)="navigateToCreate()">
          <mat-icon>add</mat-icon>
          Add Nutrition Info
        </button>
      </div>

      <mat-card class="nutrition-info-list__card">
        <div *ngIf="nutritionInfoService.loading$ | async" class="nutrition-info-list__loading">
          <mat-spinner></mat-spinner>
        </div>

        <div *ngIf="!(nutritionInfoService.loading$ | async)">
          <table mat-table [dataSource]="(nutritionInfoService.nutritionInfos$ | async) || []" class="nutrition-info-list__table">
            <ng-container matColumnDef="calories">
              <th mat-header-cell *matHeaderCellDef>Calories</th>
              <td mat-cell *matCellDef="let info">{{ info.calories }}</td>
            </ng-container>

            <ng-container matColumnDef="totalFat">
              <th mat-header-cell *matHeaderCellDef>Total Fat (g)</th>
              <td mat-cell *matCellDef="let info">{{ info.totalFat }}</td>
            </ng-container>

            <ng-container matColumnDef="sodium">
              <th mat-header-cell *matHeaderCellDef>Sodium (mg)</th>
              <td mat-cell *matCellDef="let info">{{ info.sodium }}</td>
            </ng-container>

            <ng-container matColumnDef="totalCarbohydrates">
              <th mat-header-cell *matHeaderCellDef>Carbs (g)</th>
              <td mat-cell *matCellDef="let info">{{ info.totalCarbohydrates }}</td>
            </ng-container>

            <ng-container matColumnDef="protein">
              <th mat-header-cell *matHeaderCellDef>Protein (g)</th>
              <td mat-cell *matCellDef="let info">{{ info.protein }}</td>
            </ng-container>

            <ng-container matColumnDef="createdAt">
              <th mat-header-cell *matHeaderCellDef>Created At</th>
              <td mat-cell *matCellDef="let info">{{ info.createdAt | date:'short' }}</td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let info">
                <button mat-icon-button color="primary" (click)="navigateToEdit(info.nutritionInfoId)">
                  <mat-icon>edit</mat-icon>
                </button>
                <button mat-icon-button color="warn" (click)="deleteNutritionInfo(info.nutritionInfoId)">
                  <mat-icon>delete</mat-icon>
                </button>
              </td>
            </ng-container>

            <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
            <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
          </table>

          <div *ngIf="(nutritionInfoService.nutritionInfos$ | async)?.length === 0" class="nutrition-info-list__empty">
            <mat-icon class="nutrition-info-list__empty-icon">local_dining</mat-icon>
            <p>No nutrition info found. Add your first nutrition info to get started!</p>
          </div>
        </div>
      </mat-card>
    </div>
  `,
  styles: [`
    .nutrition-info-list {
      padding: 2rem;
      max-width: 1400px;
      margin: 0 auto;
    }

    .nutrition-info-list__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .nutrition-info-list__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 500;
    }

    .nutrition-info-list__card {
      overflow: auto;
    }

    .nutrition-info-list__loading {
      display: flex;
      justify-content: center;
      padding: 3rem;
    }

    .nutrition-info-list__table {
      width: 100%;
    }

    .nutrition-info-list__empty {
      text-align: center;
      padding: 3rem;
      color: rgba(0, 0, 0, 0.6);
    }

    .nutrition-info-list__empty-icon {
      font-size: 64px;
      width: 64px;
      height: 64px;
      color: rgba(0, 0, 0, 0.26);
    }
  `]
})
export class NutritionInfoList implements OnInit {
  private readonly router = inject(Router);
  public readonly nutritionInfoService = inject(NutritionInfoService);

  displayedColumns: string[] = ['calories', 'totalFat', 'sodium', 'totalCarbohydrates', 'protein', 'createdAt', 'actions'];

  ngOnInit(): void {
    this.nutritionInfoService.getAll().subscribe();
  }

  navigateToCreate(): void {
    this.router.navigate(['/nutrition-infos/new']);
  }

  navigateToEdit(id: string): void {
    this.router.navigate(['/nutrition-infos', id]);
  }

  deleteNutritionInfo(id: string): void {
    if (confirm('Are you sure you want to delete this nutrition info?')) {
      this.nutritionInfoService.delete(id).subscribe();
    }
  }
}
