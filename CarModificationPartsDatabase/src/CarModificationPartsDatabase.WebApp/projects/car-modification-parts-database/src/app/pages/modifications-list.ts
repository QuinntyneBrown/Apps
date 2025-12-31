import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { ModificationsService } from '../services';
import { MOD_CATEGORY_LABELS } from '../models';

@Component({
  selector: 'app-modifications-list',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule, MatChipsModule],
  template: `
    <div class="modifications-list">
      <div class="modifications-list__header">
        <h1>Modifications</h1>
        <a mat-raised-button color="primary" routerLink="/modifications/new">
          <mat-icon>add</mat-icon>
          Add Modification
        </a>
      </div>

      <table mat-table [dataSource]="modifications$ | async" class="modifications-list__table">
        <ng-container matColumnDef="name">
          <th mat-header-cell *matHeaderCellDef>Name</th>
          <td mat-cell *matCellDef="let mod">{{ mod.name }}</td>
        </ng-container>

        <ng-container matColumnDef="category">
          <th mat-header-cell *matHeaderCellDef>Category</th>
          <td mat-cell *matCellDef="let mod">
            <mat-chip>{{ getCategoryLabel(mod.category) }}</mat-chip>
          </td>
        </ng-container>

        <ng-container matColumnDef="manufacturer">
          <th mat-header-cell *matHeaderCellDef>Manufacturer</th>
          <td mat-cell *matCellDef="let mod">{{ mod.manufacturer || 'N/A' }}</td>
        </ng-container>

        <ng-container matColumnDef="estimatedCost">
          <th mat-header-cell *matHeaderCellDef>Est. Cost</th>
          <td mat-cell *matCellDef="let mod">{{ mod.estimatedCost ? (mod.estimatedCost | currency) : 'N/A' }}</td>
        </ng-container>

        <ng-container matColumnDef="difficultyLevel">
          <th mat-header-cell *matHeaderCellDef>Difficulty</th>
          <td mat-cell *matCellDef="let mod">{{ mod.difficultyLevel ? mod.difficultyLevel + '/5' : 'N/A' }}</td>
        </ng-container>

        <ng-container matColumnDef="actions">
          <th mat-header-cell *matHeaderCellDef>Actions</th>
          <td mat-cell *matCellDef="let mod">
            <a mat-icon-button [routerLink]="['/modifications', mod.modificationId]">
              <mat-icon>visibility</mat-icon>
            </a>
            <a mat-icon-button [routerLink]="['/modifications', mod.modificationId, 'edit']">
              <mat-icon>edit</mat-icon>
            </a>
            <button mat-icon-button (click)="deleteModification(mod.modificationId)" color="warn">
              <mat-icon>delete</mat-icon>
            </button>
          </td>
        </ng-container>

        <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
        <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
      </table>
    </div>
  `,
  styles: [`
    .modifications-list {
      padding: 2rem;

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 2rem;
      }

      &__table {
        width: 100%;
      }
    }
  `]
})
export class ModificationsList implements OnInit {
  modifications$ = this.modificationsService.modifications$;
  displayedColumns = ['name', 'category', 'manufacturer', 'estimatedCost', 'difficultyLevel', 'actions'];

  constructor(private modificationsService: ModificationsService) {}

  ngOnInit() {
    this.modificationsService.getAll().subscribe();
  }

  getCategoryLabel(category: number): string {
    return MOD_CATEGORY_LABELS[category];
  }

  deleteModification(id: string) {
    if (confirm('Are you sure you want to delete this modification?')) {
      this.modificationsService.delete(id).subscribe();
    }
  }
}
