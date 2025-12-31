import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ReflectionService } from '../services';

@Component({
  selector: 'app-reflection-list',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule],
  template: `
    <div class="reflection-list">
      <div class="reflection-list__header">
        <h1 class="reflection-list__title">Reflections</h1>
        <a mat-raised-button color="primary" routerLink="/reflections/create">
          <mat-icon>add</mat-icon>
          Add Reflection
        </a>
      </div>

      <div class="reflection-list__table-container">
        <table mat-table [dataSource]="(reflectionService.reflections$ | async) || []" class="reflection-list__table">
          <ng-container matColumnDef="topic">
            <th mat-header-cell *matHeaderCellDef>Topic</th>
            <td mat-cell *matCellDef="let reflection">{{ reflection.topic || 'N/A' }}</td>
          </ng-container>

          <ng-container matColumnDef="text">
            <th mat-header-cell *matHeaderCellDef>Text</th>
            <td mat-cell *matCellDef="let reflection">{{ reflection.text }}</td>
          </ng-container>

          <ng-container matColumnDef="reflectionDate">
            <th mat-header-cell *matHeaderCellDef>Date</th>
            <td mat-cell *matCellDef="let reflection">{{ reflection.reflectionDate | date }}</td>
          </ng-container>

          <ng-container matColumnDef="createdAt">
            <th mat-header-cell *matHeaderCellDef>Created</th>
            <td mat-cell *matCellDef="let reflection">{{ reflection.createdAt | date:'short' }}</td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let reflection">
              <button mat-icon-button color="primary" [routerLink]="['/reflections/edit', reflection.reflectionId]">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="deleteReflection(reflection.reflectionId)">
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
    .reflection-list {
      padding: 2rem;
    }

    .reflection-list__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .reflection-list__title {
      margin: 0;
      color: #333;
    }

    .reflection-list__table-container {
      overflow-x: auto;
    }

    .reflection-list__table {
      width: 100%;
    }
  `]
})
export class ReflectionList implements OnInit {
  reflectionService = inject(ReflectionService);
  displayedColumns = ['topic', 'text', 'reflectionDate', 'createdAt', 'actions'];

  ngOnInit(): void {
    this.reflectionService.getAll().subscribe();
  }

  deleteReflection(id: string): void {
    if (confirm('Are you sure you want to delete this reflection?')) {
      this.reflectionService.delete(id).subscribe();
    }
  }
}
