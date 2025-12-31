import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { GratitudeService } from '../services';

@Component({
  selector: 'app-gratitude-list',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule],
  template: `
    <div class="gratitude-list">
      <div class="gratitude-list__header">
        <h1 class="gratitude-list__title">Gratitudes</h1>
        <a mat-raised-button color="primary" routerLink="/gratitudes/create">
          <mat-icon>add</mat-icon>
          Add Gratitude
        </a>
      </div>

      <div class="gratitude-list__table-container">
        <table mat-table [dataSource]="(gratitudeService.gratitudes$ | async) || []" class="gratitude-list__table">
          <ng-container matColumnDef="text">
            <th mat-header-cell *matHeaderCellDef>Text</th>
            <td mat-cell *matCellDef="let gratitude">{{ gratitude.text }}</td>
          </ng-container>

          <ng-container matColumnDef="gratitudeDate">
            <th mat-header-cell *matHeaderCellDef>Date</th>
            <td mat-cell *matCellDef="let gratitude">{{ gratitude.gratitudeDate | date }}</td>
          </ng-container>

          <ng-container matColumnDef="createdAt">
            <th mat-header-cell *matHeaderCellDef>Created</th>
            <td mat-cell *matCellDef="let gratitude">{{ gratitude.createdAt | date:'short' }}</td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let gratitude">
              <button mat-icon-button color="primary" [routerLink]="['/gratitudes/edit', gratitude.gratitudeId]">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="deleteGratitude(gratitude.gratitudeId)">
                <mat-icon>delete</mat-icon>
              </button>
            </td>
          </ng-container>

          <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
          <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
        </table>
      </div>
    </div>
  `,
  styles: [`
    .gratitude-list {
      padding: 2rem;
    }

    .gratitude-list__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .gratitude-list__title {
      margin: 0;
      color: #333;
    }

    .gratitude-list__table-container {
      overflow-x: auto;
    }

    .gratitude-list__table {
      width: 100%;
    }
  `]
})
export class GratitudeList implements OnInit {
  gratitudeService = inject(GratitudeService);
  displayedColumns = ['text', 'gratitudeDate', 'createdAt', 'actions'];

  ngOnInit(): void {
    this.gratitudeService.getAll().subscribe();
  }

  deleteGratitude(id: string): void {
    if (confirm('Are you sure you want to delete this gratitude?')) {
      this.gratitudeService.delete(id).subscribe();
    }
  }
}
