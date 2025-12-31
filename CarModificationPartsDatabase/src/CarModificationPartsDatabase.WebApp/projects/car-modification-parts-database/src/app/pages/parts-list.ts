import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { PartsService } from '../services';
import { MOD_CATEGORY_LABELS } from '../models';

@Component({
  selector: 'app-parts-list',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule, MatChipsModule],
  template: `
    <div class="parts-list">
      <div class="parts-list__header">
        <h1>Parts</h1>
        <a mat-raised-button color="primary" routerLink="/parts/new">
          <mat-icon>add</mat-icon>
          Add Part
        </a>
      </div>

      <table mat-table [dataSource]="parts$ | async" class="parts-list__table">
        <ng-container matColumnDef="name">
          <th mat-header-cell *matHeaderCellDef>Name</th>
          <td mat-cell *matCellDef="let part">{{ part.name }}</td>
        </ng-container>

        <ng-container matColumnDef="manufacturer">
          <th mat-header-cell *matHeaderCellDef>Manufacturer</th>
          <td mat-cell *matCellDef="let part">{{ part.manufacturer }}</td>
        </ng-container>

        <ng-container matColumnDef="category">
          <th mat-header-cell *matHeaderCellDef>Category</th>
          <td mat-cell *matCellDef="let part">
            <mat-chip>{{ getCategoryLabel(part.category) }}</mat-chip>
          </td>
        </ng-container>

        <ng-container matColumnDef="price">
          <th mat-header-cell *matHeaderCellDef>Price</th>
          <td mat-cell *matCellDef="let part">{{ part.price | currency }}</td>
        </ng-container>

        <ng-container matColumnDef="inStock">
          <th mat-header-cell *matHeaderCellDef>In Stock</th>
          <td mat-cell *matCellDef="let part">
            <mat-chip [class.parts-list__chip--in-stock]="part.inStock" [class.parts-list__chip--out-of-stock]="!part.inStock">
              {{ part.inStock ? 'Yes' : 'No' }}
            </mat-chip>
          </td>
        </ng-container>

        <ng-container matColumnDef="actions">
          <th mat-header-cell *matHeaderCellDef>Actions</th>
          <td mat-cell *matCellDef="let part">
            <a mat-icon-button [routerLink]="['/parts', part.partId]">
              <mat-icon>visibility</mat-icon>
            </a>
            <a mat-icon-button [routerLink]="['/parts', part.partId, 'edit']">
              <mat-icon>edit</mat-icon>
            </a>
            <button mat-icon-button (click)="deletePart(part.partId)" color="warn">
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
    .parts-list {
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

      &__chip--in-stock {
        background-color: #4caf50 !important;
        color: white;
      }

      &__chip--out-of-stock {
        background-color: #f44336 !important;
        color: white;
      }
    }
  `]
})
export class PartsList implements OnInit {
  parts$ = this.partsService.parts$;
  displayedColumns = ['name', 'manufacturer', 'category', 'price', 'inStock', 'actions'];

  constructor(private partsService: PartsService) {}

  ngOnInit() {
    this.partsService.getAll().subscribe();
  }

  getCategoryLabel(category: number): string {
    return MOD_CATEGORY_LABELS[category];
  }

  deletePart(id: string) {
    if (confirm('Are you sure you want to delete this part?')) {
      this.partsService.delete(id).subscribe();
    }
  }
}
