import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatTableModule } from '@angular/material/table';
import { MatChipsModule } from '@angular/material/chips';
import { combineLatest, map } from 'rxjs';
import { ExpenseClaimsService, EmployeesService } from '../../services';
import { ExpenseClaimDialog } from '../../components';

@Component({
  selector: 'app-expense-claims',
  imports: [CommonModule, MatButtonModule, MatIconModule, MatDialogModule, MatTableModule, MatChipsModule],
  templateUrl: './expense-claims.html',
  styleUrl: './expense-claims.scss',
})
export class ExpenseClaims implements OnInit {
  private readonly _expenseClaimsService = inject(ExpenseClaimsService);
  private readonly _employeesService = inject(EmployeesService);
  private readonly _dialog = inject(MatDialog);

  displayedColumns = ['title', 'employeeName', 'amount', 'categoryType', 'expenseDate', 'status', 'actions'];

  viewModel$ = combineLatest([
    this._expenseClaimsService.expenseClaims$,
    this._employeesService.employees$,
  ]).pipe(
    map(([expenseClaims, employees]) => ({
      expenseClaims,
      employees,
    }))
  );

  ngOnInit(): void {
    this._expenseClaimsService.getAll().subscribe();
    this._employeesService.getAll().subscribe();
  }

  openDialog(expenseClaim?: any): void {
    this.viewModel$.subscribe(vm => {
      const dialogRef = this._dialog.open(ExpenseClaimDialog, {
        width: '600px',
        data: { expenseClaim, employees: vm.employees },
      });

      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          if (expenseClaim) {
            this._expenseClaimsService.update(expenseClaim.expenseClaimId, result).subscribe();
          } else {
            this._expenseClaimsService.create(result).subscribe();
          }
        }
      });
    });
  }

  deleteExpenseClaim(id: string): void {
    if (confirm('Are you sure you want to delete this expense claim?')) {
      this._expenseClaimsService.delete(id).subscribe();
    }
  }

  getStatusColor(status: string): string {
    switch (status) {
      case 'Draft':
        return '';
      case 'Submitted':
        return 'primary';
      case 'UnderReview':
        return 'accent';
      case 'Approved':
        return 'primary';
      case 'Rejected':
        return 'warn';
      case 'Paid':
        return 'primary';
      default:
        return '';
    }
  }
}
