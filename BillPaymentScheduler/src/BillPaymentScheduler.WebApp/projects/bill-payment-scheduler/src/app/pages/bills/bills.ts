import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { combineLatest, map } from 'rxjs';
import { BillsService, PayeesService, PaymentsService } from '../../services';
import { BillCard, BillDialog, PaymentDialog } from '../../components';
import { Bill } from '../../models';

@Component({
  selector: 'app-bills',
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatSnackBarModule,
    BillCard,
  ],
  templateUrl: './bills.html',
  styleUrl: './bills.scss',
})
export class Bills implements OnInit {
  private readonly _billsService = inject(BillsService);
  private readonly _payeesService = inject(PayeesService);
  private readonly _paymentsService = inject(PaymentsService);
  private readonly _dialog = inject(MatDialog);
  private readonly _snackBar = inject(MatSnackBar);

  viewModel$ = combineLatest([
    this._billsService.bills$,
    this._payeesService.payees$,
  ]).pipe(
    map(([bills, payees]) => ({ bills, payees }))
  );

  ngOnInit(): void {
    this._billsService.getAll().subscribe();
    this._payeesService.getAll().subscribe();
  }

  onCreateBill(): void {
    combineLatest([this._payeesService.payees$]).subscribe(([payees]) => {
      const dialogRef = this._dialog.open(BillDialog, {
        width: '500px',
        data: { payees },
      });

      dialogRef.afterClosed().subscribe((result) => {
        if (result) {
          this._billsService.create(result).subscribe({
            next: () => {
              this._snackBar.open('Bill created successfully', 'Close', { duration: 3000 });
            },
            error: (error) => {
              this._snackBar.open('Failed to create bill', 'Close', { duration: 3000 });
              console.error('Error creating bill:', error);
            },
          });
        }
      });
    });
  }

  onEditBill(bill: Bill): void {
    combineLatest([this._payeesService.payees$]).subscribe(([payees]) => {
      const dialogRef = this._dialog.open(BillDialog, {
        width: '500px',
        data: { bill, payees },
      });

      dialogRef.afterClosed().subscribe((result) => {
        if (result) {
          this._billsService.update(bill.billId, result).subscribe({
            next: () => {
              this._snackBar.open('Bill updated successfully', 'Close', { duration: 3000 });
            },
            error: (error) => {
              this._snackBar.open('Failed to update bill', 'Close', { duration: 3000 });
              console.error('Error updating bill:', error);
            },
          });
        }
      });
    });
  }

  onDeleteBill(bill: Bill): void {
    if (confirm(`Are you sure you want to delete the bill "${bill.name}"?`)) {
      this._billsService.delete(bill.billId).subscribe({
        next: () => {
          this._snackBar.open('Bill deleted successfully', 'Close', { duration: 3000 });
        },
        error: (error) => {
          this._snackBar.open('Failed to delete bill', 'Close', { duration: 3000 });
          console.error('Error deleting bill:', error);
        },
      });
    }
  }

  onPayBill(bill: Bill): void {
    combineLatest([this._billsService.bills$]).subscribe(([bills]) => {
      const dialogRef = this._dialog.open(PaymentDialog, {
        width: '500px',
        data: {
          payment: {
            billId: bill.billId,
            amount: bill.amount,
            paymentDate: new Date().toISOString(),
          },
          bills,
        },
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
}
