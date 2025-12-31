import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { PaymentScheduleService } from '../services';

@Component({
  selector: 'app-payment-schedule-list',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule, MatCardModule],
  template: `
    <div class="payment-schedule-list">
      <div class="payment-schedule-list__header">
        <h1 class="payment-schedule-list__title">Payment Schedules</h1>
        <button mat-raised-button color="primary" routerLink="/payment-schedules/new" class="payment-schedule-list__add-button">
          <mat-icon>add</mat-icon>
          Add Payment Schedule
        </button>
      </div>

      <mat-card class="payment-schedule-list__card">
        <mat-card-content>
          <table mat-table [dataSource]="(paymentScheduleService.paymentSchedules$ | async) || []" class="payment-schedule-list__table">
            <ng-container matColumnDef="paymentNumber">
              <th mat-header-cell *matHeaderCellDef>Payment #</th>
              <td mat-cell *matCellDef="let schedule">{{ schedule.paymentNumber }}</td>
            </ng-container>

            <ng-container matColumnDef="dueDate">
              <th mat-header-cell *matHeaderCellDef>Due Date</th>
              <td mat-cell *matCellDef="let schedule">{{ schedule.dueDate | date }}</td>
            </ng-container>

            <ng-container matColumnDef="paymentAmount">
              <th mat-header-cell *matHeaderCellDef>Payment Amount</th>
              <td mat-cell *matCellDef="let schedule">{{ schedule.paymentAmount | currency }}</td>
            </ng-container>

            <ng-container matColumnDef="principalAmount">
              <th mat-header-cell *matHeaderCellDef>Principal</th>
              <td mat-cell *matCellDef="let schedule">{{ schedule.principalAmount | currency }}</td>
            </ng-container>

            <ng-container matColumnDef="interestAmount">
              <th mat-header-cell *matHeaderCellDef>Interest</th>
              <td mat-cell *matCellDef="let schedule">{{ schedule.interestAmount | currency }}</td>
            </ng-container>

            <ng-container matColumnDef="remainingBalance">
              <th mat-header-cell *matHeaderCellDef>Remaining Balance</th>
              <td mat-cell *matCellDef="let schedule">{{ schedule.remainingBalance | currency }}</td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let schedule">
                <button mat-icon-button color="primary" [routerLink]="['/payment-schedules', schedule.paymentScheduleId, 'edit']" class="payment-schedule-list__action-button">
                  <mat-icon>edit</mat-icon>
                </button>
                <button mat-icon-button color="warn" (click)="deletePaymentSchedule(schedule.paymentScheduleId)" class="payment-schedule-list__action-button">
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
    .payment-schedule-list {
      padding: 24px;
    }

    .payment-schedule-list__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 24px;
    }

    .payment-schedule-list__title {
      margin: 0;
      font-size: 32px;
      font-weight: 400;
    }

    .payment-schedule-list__add-button {
      display: flex;
      align-items: center;
      gap: 8px;
    }

    .payment-schedule-list__card {
      overflow-x: auto;
    }

    .payment-schedule-list__table {
      width: 100%;
    }

    .payment-schedule-list__action-button {
      margin-right: 8px;
    }
  `]
})
export class PaymentScheduleList implements OnInit {
  paymentScheduleService = inject(PaymentScheduleService);
  displayedColumns = ['paymentNumber', 'dueDate', 'paymentAmount', 'principalAmount', 'interestAmount', 'remainingBalance', 'actions'];

  ngOnInit(): void {
    this.paymentScheduleService.getAll().subscribe();
  }

  deletePaymentSchedule(id: string): void {
    if (confirm('Are you sure you want to delete this payment schedule?')) {
      this.paymentScheduleService.delete(id).subscribe();
    }
  }
}
