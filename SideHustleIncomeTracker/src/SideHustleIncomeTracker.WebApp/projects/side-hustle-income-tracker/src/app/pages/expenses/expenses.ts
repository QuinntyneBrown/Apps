import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { ExpenseService, BusinessService } from '../../services';
import { ExpenseFormDialog } from '../../components/expense-form-dialog/expense-form-dialog';
import { Expense } from '../../models';

@Component({
  selector: 'app-expenses',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatDialogModule
  ],
  templateUrl: './expenses.html',
  styleUrl: './expenses.scss'
})
export class Expenses implements OnInit {
  private _expenseService = inject(ExpenseService);
  private _businessService = inject(BusinessService);
  private _dialog = inject(MatDialog);

  expenses$ = this._expenseService.expenses$;
  businesses$ = this._businessService.businesses$;

  displayedColumns: string[] = ['description', 'business', 'amount', 'expenseDate', 'category', 'vendor', 'isTaxDeductible', 'actions'];

  ngOnInit(): void {
    this._expenseService.getAll().subscribe();
    this._businessService.getAll().subscribe();
  }

  getBusinessName(businessId: string): string {
    let businessName = '';
    this.businesses$.subscribe(businesses => {
      const business = businesses.find(b => b.businessId === businessId);
      businessName = business?.name || 'Unknown';
    });
    return businessName;
  }

  openAddDialog(): void {
    const dialogRef = this._dialog.open(ExpenseFormDialog, {
      width: '600px',
      data: {}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        // Data is automatically refreshed via the service
      }
    });
  }

  openEditDialog(expense: Expense): void {
    const dialogRef = this._dialog.open(ExpenseFormDialog, {
      width: '600px',
      data: { expense }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        // Data is automatically refreshed via the service
      }
    });
  }

  deleteExpense(expense: Expense): void {
    if (confirm(`Are you sure you want to delete this expense entry?`)) {
      this._expenseService.delete(expense.expenseId).subscribe();
    }
  }
}
