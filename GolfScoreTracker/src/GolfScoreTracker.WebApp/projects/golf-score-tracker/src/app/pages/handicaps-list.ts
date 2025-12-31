import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { HandicapsService } from '../services';

@Component({
  selector: 'app-handicaps-list',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule, MatTableModule],
  template: `
    <div class="handicaps-list">
      <div class="handicaps-list__header">
        <h1>Handicaps</h1>
        <a mat-raised-button color="primary" routerLink="/handicaps/new">
          <mat-icon>add</mat-icon>
          Add Handicap
        </a>
      </div>

      <mat-card class="handicaps-list__card">
        <mat-card-content>
          <div *ngIf="(handicaps$ | async) as handicaps">
            <p *ngIf="handicaps.length === 0" class="handicaps-list__empty">No handicaps found. Add your first handicap!</p>

            <table mat-table [dataSource]="handicaps" *ngIf="handicaps.length > 0" class="handicaps-list__table">
              <ng-container matColumnDef="index">
                <th mat-header-cell *matHeaderCellDef>Handicap Index</th>
                <td mat-cell *matCellDef="let handicap">{{ handicap.handicapIndex | number:'1.1-1' }}</td>
              </ng-container>

              <ng-container matColumnDef="calculated">
                <th mat-header-cell *matHeaderCellDef>Calculated Date</th>
                <td mat-cell *matCellDef="let handicap">{{ handicap.calculatedDate | date:'shortDate' }}</td>
              </ng-container>

              <ng-container matColumnDef="rounds">
                <th mat-header-cell *matHeaderCellDef>Rounds Used</th>
                <td mat-cell *matCellDef="let handicap">{{ handicap.roundsUsed }}</td>
              </ng-container>

              <ng-container matColumnDef="notes">
                <th mat-header-cell *matHeaderCellDef>Notes</th>
                <td mat-cell *matCellDef="let handicap">{{ handicap.notes || 'N/A' }}</td>
              </ng-container>

              <ng-container matColumnDef="actions">
                <th mat-header-cell *matHeaderCellDef>Actions</th>
                <td mat-cell *matCellDef="let handicap">
                  <a mat-button color="primary" [routerLink]="['/handicaps', handicap.handicapId]">View</a>
                  <a mat-button color="accent" [routerLink]="['/handicaps', handicap.handicapId, 'edit']">Edit</a>
                  <button mat-button color="warn" (click)="deleteHandicap(handicap.handicapId)">Delete</button>
                </td>
              </ng-container>

              <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
              <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
            </table>
          </div>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .handicaps-list {
      padding: 2rem;
      max-width: 1400px;
      margin: 0 auto;
    }

    .handicaps-list__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .handicaps-list__header h1 {
      margin: 0;
    }

    .handicaps-list__card {
      margin-top: 1rem;
    }

    .handicaps-list__empty {
      text-align: center;
      padding: 2rem;
      color: #666;
    }

    .handicaps-list__table {
      width: 100%;
    }
  `]
})
export class HandicapsList implements OnInit {
  handicaps$ = this.handicapsService.handicaps$;
  displayedColumns: string[] = ['index', 'calculated', 'rounds', 'notes', 'actions'];

  constructor(private handicapsService: HandicapsService) {}

  ngOnInit(): void {
    this.handicapsService.getHandicaps().subscribe();
  }

  deleteHandicap(id: string): void {
    if (confirm('Are you sure you want to delete this handicap?')) {
      this.handicapsService.deleteHandicap(id).subscribe();
    }
  }
}
