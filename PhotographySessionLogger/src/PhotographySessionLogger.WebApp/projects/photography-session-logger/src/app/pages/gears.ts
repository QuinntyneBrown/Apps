import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { GearService } from '../services';

@Component({
  selector: 'app-gears',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule, MatCardModule],
  template: `
    <div class="gears">
      <div class="gears__header">
        <h1 class="gears__title">Photography Gear</h1>
        <a mat-raised-button color="primary" routerLink="/gears/new" class="gears__add-btn">
          <mat-icon>add</mat-icon>
          New Gear
        </a>
      </div>

      <mat-card class="gears__card">
        <mat-card-content>
          <table mat-table [dataSource]="(gears$ | async) || []" class="gears__table">
            <ng-container matColumnDef="name">
              <th mat-header-cell *matHeaderCellDef>Name</th>
              <td mat-cell *matCellDef="let gear">{{ gear.name }}</td>
            </ng-container>

            <ng-container matColumnDef="gearType">
              <th mat-header-cell *matHeaderCellDef>Type</th>
              <td mat-cell *matCellDef="let gear">{{ gear.gearType }}</td>
            </ng-container>

            <ng-container matColumnDef="brand">
              <th mat-header-cell *matHeaderCellDef>Brand</th>
              <td mat-cell *matCellDef="let gear">{{ gear.brand || '-' }}</td>
            </ng-container>

            <ng-container matColumnDef="model">
              <th mat-header-cell *matHeaderCellDef>Model</th>
              <td mat-cell *matCellDef="let gear">{{ gear.model || '-' }}</td>
            </ng-container>

            <ng-container matColumnDef="purchaseDate">
              <th mat-header-cell *matHeaderCellDef>Purchase Date</th>
              <td mat-cell *matCellDef="let gear">{{ gear.purchaseDate ? (gear.purchaseDate | date:'shortDate') : '-' }}</td>
            </ng-container>

            <ng-container matColumnDef="purchasePrice">
              <th mat-header-cell *matHeaderCellDef>Price</th>
              <td mat-cell *matCellDef="let gear">{{ gear.purchasePrice ? (gear.purchasePrice | currency) : '-' }}</td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let gear">
                <button mat-icon-button color="primary" [routerLink]="['/gears', gear.gearId]">
                  <mat-icon>edit</mat-icon>
                </button>
                <button mat-icon-button color="warn" (click)="deleteGear(gear.gearId)">
                  <mat-icon>delete</mat-icon>
                </button>
              </td>
            </ng-container>

            <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
            <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
          </table>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .gears {
      padding: 2rem;
    }

    .gears__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .gears__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 500;
    }

    .gears__add-btn {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .gears__card {
      width: 100%;
    }

    .gears__table {
      width: 100%;
    }
  `]
})
export class Gears implements OnInit {
  private readonly gearService = inject(GearService);

  gears$ = this.gearService.gears$;
  displayedColumns = ['name', 'gearType', 'brand', 'model', 'purchaseDate', 'purchasePrice', 'actions'];

  ngOnInit(): void {
    this.gearService.getAll().subscribe();
  }

  deleteGear(id: string): void {
    if (confirm('Are you sure you want to delete this gear?')) {
      this.gearService.delete(id).subscribe();
    }
  }
}
