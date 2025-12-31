import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { ContributionService } from '../services';
import { Contribution } from '../models';

@Component({
  selector: 'app-contributions',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule, MatCardModule],
  template: `
    <div class="contributions">
      <div class="contributions__header">
        <h1 class="contributions__title">Contributions</h1>
        <button mat-raised-button color="primary" routerLink="/contributions/new" class="contributions__add-btn">
          <mat-icon>add</mat-icon>
          New Contribution
        </button>
      </div>

      <mat-card class="contributions__card">
        <mat-card-content>
          <table mat-table [dataSource]="(contributions$ | async) || []" class="contributions__table">
            <ng-container matColumnDef="contributionDate">
              <th mat-header-cell *matHeaderCellDef>Date</th>
              <td mat-cell *matCellDef="let contribution">{{ contribution.contributionDate | date:'short' }}</td>
            </ng-container>

            <ng-container matColumnDef="accountName">
              <th mat-header-cell *matHeaderCellDef>Account Name</th>
              <td mat-cell *matCellDef="let contribution">{{ contribution.accountName }}</td>
            </ng-container>

            <ng-container matColumnDef="amount">
              <th mat-header-cell *matHeaderCellDef>Amount</th>
              <td mat-cell *matCellDef="let contribution">{{ contribution.amount | currency }}</td>
            </ng-container>

            <ng-container matColumnDef="isEmployerMatch">
              <th mat-header-cell *matHeaderCellDef>Employer Match</th>
              <td mat-cell *matCellDef="let contribution">
                <mat-icon [class.contributions__match-icon--yes]="contribution.isEmployerMatch">
                  {{ contribution.isEmployerMatch ? 'check_circle' : 'cancel' }}
                </mat-icon>
              </td>
            </ng-container>

            <ng-container matColumnDef="notes">
              <th mat-header-cell *matHeaderCellDef>Notes</th>
              <td mat-cell *matCellDef="let contribution">{{ contribution.notes || '-' }}</td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let contribution">
                <button mat-icon-button color="primary" [routerLink]="['/contributions', contribution.contributionId]">
                  <mat-icon>edit</mat-icon>
                </button>
                <button mat-icon-button color="warn" (click)="deleteContribution(contribution)">
                  <mat-icon>delete</mat-icon>
                </button>
              </td>
            </ng-container>

            <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
            <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
          </table>

          <div *ngIf="(contributions$ | async)?.length === 0" class="contributions__empty">
            <p>No contributions found. Add your first contribution to track your retirement savings!</p>
          </div>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .contributions {
      padding: 2rem;
    }

    .contributions__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .contributions__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 500;
      color: #333;
    }

    .contributions__add-btn {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .contributions__card {
      overflow-x: auto;
    }

    .contributions__table {
      width: 100%;
    }

    .contributions__match-icon--yes {
      color: #4caf50;
    }

    .contributions__empty {
      padding: 3rem;
      text-align: center;
      color: #666;
    }
  `]
})
export class Contributions implements OnInit {
  private contributionService = inject(ContributionService);
  private router = inject(Router);

  contributions$ = this.contributionService.contributions$;
  displayedColumns = ['contributionDate', 'accountName', 'amount', 'isEmployerMatch', 'notes', 'actions'];

  ngOnInit(): void {
    this.contributionService.loadContributions().subscribe();
  }

  deleteContribution(contribution: Contribution): void {
    if (confirm(`Are you sure you want to delete this contribution of ${contribution.amount}?`)) {
      this.contributionService.deleteContribution(contribution.contributionId).subscribe();
    }
  }
}
