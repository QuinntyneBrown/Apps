import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { PayeesService } from '../../services';
import { PayeeDialog } from '../../components';
import { Payee } from '../../models';

@Component({
  selector: 'app-payees',
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatDialogModule,
    MatSnackBarModule,
  ],
  templateUrl: './payees.html',
  styleUrl: './payees.scss',
})
export class Payees implements OnInit {
  private readonly _payeesService = inject(PayeesService);
  private readonly _dialog = inject(MatDialog);
  private readonly _snackBar = inject(MatSnackBar);

  payees$ = this._payeesService.payees$;

  ngOnInit(): void {
    this._payeesService.getAll().subscribe();
  }

  onCreatePayee(): void {
    const dialogRef = this._dialog.open(PayeeDialog, {
      width: '500px',
      data: {},
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this._payeesService.create(result).subscribe({
          next: () => {
            this._snackBar.open('Payee created successfully', 'Close', { duration: 3000 });
          },
          error: (error) => {
            this._snackBar.open('Failed to create payee', 'Close', { duration: 3000 });
            console.error('Error creating payee:', error);
          },
        });
      }
    });
  }

  onEditPayee(payee: Payee): void {
    const dialogRef = this._dialog.open(PayeeDialog, {
      width: '500px',
      data: { payee },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this._payeesService.update(payee.payeeId, result).subscribe({
          next: () => {
            this._snackBar.open('Payee updated successfully', 'Close', { duration: 3000 });
          },
          error: (error) => {
            this._snackBar.open('Failed to update payee', 'Close', { duration: 3000 });
            console.error('Error updating payee:', error);
          },
        });
      }
    });
  }

  onDeletePayee(payee: Payee): void {
    if (confirm(`Are you sure you want to delete the payee "${payee.name}"?`)) {
      this._payeesService.delete(payee.payeeId).subscribe({
        next: () => {
          this._snackBar.open('Payee deleted successfully', 'Close', { duration: 3000 });
        },
        error: (error) => {
          this._snackBar.open('Failed to delete payee', 'Close', { duration: 3000 });
          console.error('Error deleting payee:', error);
        },
      });
    }
  }
}
