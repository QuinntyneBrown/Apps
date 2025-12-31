import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatCardModule } from '@angular/material/card';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { InjuryService } from '../services';
import { Injury, INJURY_TYPE_LABELS, INJURY_SEVERITY_LABELS } from '../models';

@Component({
  selector: 'app-injuries',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatChipsModule, MatCardModule, MatProgressBarModule],
  template: `
    <div class="injuries">
      <div class="injuries__header">
        <h1>Injuries</h1>
        <button mat-raised-button color="primary">
          <mat-icon>add</mat-icon>
          Log Injury
        </button>
      </div>

      <mat-card>
        <table mat-table [dataSource]="(injuries$ | async) || []" class="injuries__table">
          <ng-container matColumnDef="bodyPart">
            <th mat-header-cell *matHeaderCellDef>Body Part</th>
            <td mat-cell *matCellDef="let injury">{{ injury.bodyPart }}</td>
          </ng-container>

          <ng-container matColumnDef="type">
            <th mat-header-cell *matHeaderCellDef>Type</th>
            <td mat-cell *matCellDef="let injury">{{ getTypeLabel(injury.injuryType) }}</td>
          </ng-container>

          <ng-container matColumnDef="severity">
            <th mat-header-cell *matHeaderCellDef>Severity</th>
            <td mat-cell *matCellDef="let injury">
              <mat-chip [class]="'severity--' + injury.severity">
                {{ getSeverityLabel(injury.severity) }}
              </mat-chip>
            </td>
          </ng-container>

          <ng-container matColumnDef="date">
            <th mat-header-cell *matHeaderCellDef>Date</th>
            <td mat-cell *matCellDef="let injury">{{ injury.injuryDate | date:'mediumDate' }}</td>
          </ng-container>

          <ng-container matColumnDef="progress">
            <th mat-header-cell *matHeaderCellDef>Progress</th>
            <td mat-cell *matCellDef="let injury">
              <div class="progress-cell">
                <mat-progress-bar mode="determinate" [value]="injury.progressPercentage"></mat-progress-bar>
                <span>{{ injury.progressPercentage }}%</span>
              </div>
            </td>
          </ng-container>

          <ng-container matColumnDef="status">
            <th mat-header-cell *matHeaderCellDef>Status</th>
            <td mat-cell *matCellDef="let injury">{{ injury.status }}</td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let injury">
              <button mat-icon-button color="primary" title="Edit">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" title="Delete" (click)="deleteInjury(injury)">
                <mat-icon>delete</mat-icon>
              </button>
            </td>
          </ng-container>

          <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
          <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
        </table>
      </mat-card>
    </div>
  `,
  styles: [`
    .injuries {
      padding: 1.5rem;
    }
    .injuries__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 1.5rem;
    }
    .injuries__table {
      width: 100%;
    }
    .progress-cell {
      display: flex;
      align-items: center;
      gap: 0.5rem;
      mat-progress-bar {
        width: 100px;
      }
    }
    .severity--0 { background-color: #e8f5e9; }
    .severity--1 { background-color: #fff3e0; }
    .severity--2 { background-color: #ffebee; }
    .severity--3 { background-color: #f44336; color: white; }
  `]
})
export class Injuries implements OnInit {
  private _injuryService = inject(InjuryService);

  injuries$ = this._injuryService.injuries$;
  displayedColumns = ['bodyPart', 'type', 'severity', 'date', 'progress', 'status', 'actions'];

  ngOnInit(): void {
    this._injuryService.getAll().subscribe();
  }

  getTypeLabel(type: number): string {
    return INJURY_TYPE_LABELS[type as keyof typeof INJURY_TYPE_LABELS] || 'Unknown';
  }

  getSeverityLabel(severity: number): string {
    return INJURY_SEVERITY_LABELS[severity as keyof typeof INJURY_SEVERITY_LABELS] || 'Unknown';
  }

  deleteInjury(injury: Injury): void {
    if (confirm(`Are you sure you want to delete this injury record?`)) {
      this._injuryService.delete(injury.injuryId).subscribe();
    }
  }
}
