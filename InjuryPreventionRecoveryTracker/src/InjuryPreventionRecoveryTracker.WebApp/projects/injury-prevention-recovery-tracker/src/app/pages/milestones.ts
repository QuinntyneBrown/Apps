import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatCardModule } from '@angular/material/card';
import { MilestoneService } from '../services';
import { Milestone } from '../models';

@Component({
  selector: 'app-milestones',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatChipsModule, MatCardModule],
  template: `
    <div class="milestones">
      <div class="milestones__header">
        <h1>Recovery Milestones</h1>
        <button mat-raised-button color="primary">
          <mat-icon>add</mat-icon>
          Add Milestone
        </button>
      </div>

      <mat-card>
        <table mat-table [dataSource]="(milestones$ | async) || []" class="milestones__table">
          <ng-container matColumnDef="name">
            <th mat-header-cell *matHeaderCellDef>Name</th>
            <td mat-cell *matCellDef="let milestone">{{ milestone.name }}</td>
          </ng-container>

          <ng-container matColumnDef="description">
            <th mat-header-cell *matHeaderCellDef>Description</th>
            <td mat-cell *matCellDef="let milestone">{{ milestone.description || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="targetDate">
            <th mat-header-cell *matHeaderCellDef>Target Date</th>
            <td mat-cell *matCellDef="let milestone">{{ milestone.targetDate ? (milestone.targetDate | date:'mediumDate') : '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="achievedDate">
            <th mat-header-cell *matHeaderCellDef>Achieved Date</th>
            <td mat-cell *matCellDef="let milestone">{{ milestone.achievedDate ? (milestone.achievedDate | date:'mediumDate') : '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="status">
            <th mat-header-cell *matHeaderCellDef>Status</th>
            <td mat-cell *matCellDef="let milestone">
              <mat-chip [color]="milestone.isAchieved ? 'primary' : 'accent'">
                {{ milestone.isAchieved ? 'Achieved' : 'In Progress' }}
              </mat-chip>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let milestone">
              <button mat-icon-button color="primary" title="Edit">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" title="Delete" (click)="deleteMilestone(milestone)">
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
    .milestones {
      padding: 1.5rem;
    }
    .milestones__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 1.5rem;
    }
    .milestones__table {
      width: 100%;
    }
  `]
})
export class Milestones implements OnInit {
  private _milestoneService = inject(MilestoneService);

  milestones$ = this._milestoneService.milestones$;
  displayedColumns = ['name', 'description', 'targetDate', 'achievedDate', 'status', 'actions'];

  ngOnInit(): void {
    this._milestoneService.getAll().subscribe();
  }

  deleteMilestone(milestone: Milestone): void {
    if (confirm(`Are you sure you want to delete "${milestone.name}"?`)) {
      this._milestoneService.delete(milestone.milestoneId).subscribe();
    }
  }
}
