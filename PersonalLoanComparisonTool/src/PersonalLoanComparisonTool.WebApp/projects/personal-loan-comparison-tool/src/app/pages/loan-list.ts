import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { LoanService } from '../services';
import { LoanTypeLabels } from '../models';

@Component({
  selector: 'app-loan-list',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule, MatCardModule],
  template: `
    <div class="loan-list">
      <div class="loan-list__header">
        <h1 class="loan-list__title">Loans</h1>
        <button mat-raised-button color="primary" routerLink="/loans/new" class="loan-list__add-button">
          <mat-icon>add</mat-icon>
          Add Loan
        </button>
      </div>

      <mat-card class="loan-list__card">
        <mat-card-content>
          <table mat-table [dataSource]="(loanService.loans$ | async) || []" class="loan-list__table">
            <ng-container matColumnDef="name">
              <th mat-header-cell *matHeaderCellDef>Name</th>
              <td mat-cell *matCellDef="let loan">{{ loan.name }}</td>
            </ng-container>

            <ng-container matColumnDef="loanType">
              <th mat-header-cell *matHeaderCellDef>Type</th>
              <td mat-cell *matCellDef="let loan">{{ getLoanTypeLabel(loan.loanType) }}</td>
            </ng-container>

            <ng-container matColumnDef="requestedAmount">
              <th mat-header-cell *matHeaderCellDef>Requested Amount</th>
              <td mat-cell *matCellDef="let loan">{{ loan.requestedAmount | currency }}</td>
            </ng-container>

            <ng-container matColumnDef="purpose">
              <th mat-header-cell *matHeaderCellDef>Purpose</th>
              <td mat-cell *matCellDef="let loan">{{ loan.purpose }}</td>
            </ng-container>

            <ng-container matColumnDef="creditScore">
              <th mat-header-cell *matHeaderCellDef>Credit Score</th>
              <td mat-cell *matCellDef="let loan">{{ loan.creditScore }}</td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let loan">
                <button mat-icon-button color="primary" [routerLink]="['/loans', loan.loanId, 'edit']" class="loan-list__action-button">
                  <mat-icon>edit</mat-icon>
                </button>
                <button mat-icon-button color="warn" (click)="deleteLoan(loan.loanId)" class="loan-list__action-button">
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
    .loan-list {
      padding: 24px;
    }

    .loan-list__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 24px;
    }

    .loan-list__title {
      margin: 0;
      font-size: 32px;
      font-weight: 400;
    }

    .loan-list__add-button {
      display: flex;
      align-items: center;
      gap: 8px;
    }

    .loan-list__card {
      overflow-x: auto;
    }

    .loan-list__table {
      width: 100%;
    }

    .loan-list__action-button {
      margin-right: 8px;
    }
  `]
})
export class LoanList implements OnInit {
  loanService = inject(LoanService);
  displayedColumns = ['name', 'loanType', 'requestedAmount', 'purpose', 'creditScore', 'actions'];
  loanTypeLabels = LoanTypeLabels;

  ngOnInit(): void {
    this.loanService.getAll().subscribe();
  }

  getLoanTypeLabel(loanType: number): string {
    return this.loanTypeLabels[loanType] || '';
  }

  deleteLoan(id: string): void {
    if (confirm('Are you sure you want to delete this loan?')) {
      this.loanService.delete(id).subscribe();
    }
  }
}
