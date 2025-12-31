import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { IncomeService, BusinessService } from '../../services';
import { IncomeFormDialog } from '../../components/income-form-dialog/income-form-dialog';
import { Income } from '../../models';
import { map } from 'rxjs';

@Component({
  selector: 'app-incomes',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatDialogModule
  ],
  templateUrl: './incomes.html',
  styleUrl: './incomes.scss'
})
export class Incomes implements OnInit {
  private _incomeService = inject(IncomeService);
  private _businessService = inject(BusinessService);
  private _dialog = inject(MatDialog);

  incomes$ = this._incomeService.incomes$;
  businesses$ = this._businessService.businesses$;

  displayedColumns: string[] = ['description', 'business', 'amount', 'incomeDate', 'client', 'invoiceNumber', 'isPaid', 'actions'];

  ngOnInit(): void {
    this._incomeService.getAll().subscribe();
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
    const dialogRef = this._dialog.open(IncomeFormDialog, {
      width: '600px',
      data: {}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        // Data is automatically refreshed via the service
      }
    });
  }

  openEditDialog(income: Income): void {
    const dialogRef = this._dialog.open(IncomeFormDialog, {
      width: '600px',
      data: { income }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        // Data is automatically refreshed via the service
      }
    });
  }

  deleteIncome(income: Income): void {
    if (confirm(`Are you sure you want to delete this income entry?`)) {
      this._incomeService.delete(income.incomeId).subscribe();
    }
  }
}
