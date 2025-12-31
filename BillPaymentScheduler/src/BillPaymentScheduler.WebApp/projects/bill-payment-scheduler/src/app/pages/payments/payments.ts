import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { combineLatest, map } from 'rxjs';
import { PaymentsService, BillsService } from '../../services';
import { PaymentDialog } from '../../components';
import { Payment } from '../../models';

@Component({
  selector: 'app-payments',
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatDialogModule,
    MatSnackBarModule,
  ],
  templateUrl: './payments.html',
  styleUrl: './payments.scss',
})
export class Payments implements OnInit {
  private readonly _paymentsService = inject(PaymentsService);
  private readonly _billsService = inject(BillsService);
  private readonly _dialog = inject(MatDialog);
  private readonly _snackBar = inject(MatSnackBar);

  viewModel$ = combineLatest([
    this._paymentsService.payments$,
    this._billsService.bills$,
  ]).pipe(
    map(([payments, bills]) => ({ payments, bills }))
  );

  ngOnInit(): void {
    this._paymentsService.getAll().subscribe();
    this._billsService.getAll().subscribe();
  }

  onCreatePayment(): void {
    combineLatest([this._billsService.bills$]).subscribe(([bills]) => {
      const dialogRef = this._dialog.open(PaymentDialog, {
        width: '500px',
        data: { bills },
      });

      dialogRef.afterClosed().subscribe((result) => {
        if (result) {
          this._paymentsService.create(result).subscribe({
            next: () => {
              this._snackBar.open('Payment recorded successfully', 'Close', { duration: 3000 });
            },
            error: (error) => {
              this._snackBar.open('Failed to record payment', 'Close', { duration: 3000 });
              console.error('Error recording payment:', error);
            },
          });
        }
      });
    });
  }

  onEditPayment(payment: Payment): void {
    combineLatest([this._billsService.bills$]).subscribe(([bills]) => {
      const dialogRef = this._dialog.open(PaymentDialog, {
        width: '500px',
        data: { payment, bills },
      });

      dialogRef.afterClosed().subscribe((result) => {
        if (result) {
          this._paymentsService.update(payment.paymentId, result).subscribe({
            next: () => {
              this._snackBar.open('Payment updated successfully', 'Close', { duration: 3000 });
            },
            error: (error) => {
              this._snackBar.open('Failed to update payment', 'Close', { duration: 3000 });
              console.error('Error updating payment:', error);
            },
          });
        }
      });
    });
  }

  onDeletePayment(payment: Payment): void {
    if (confirm(`Are you sure you want to delete this payment?`)) {
      this._paymentsService.delete(payment.paymentId).subscribe({
        next: () => {
          this._snackBar.open('Payment deleted successfully', 'Close', { duration: 3000 });
        },
        error: (error) => {
          this._snackBar.open('Failed to delete payment', 'Close', { duration: 3000 });
          console.error('Error deleting payment:', error);
        },
      });
    }
  }
}
