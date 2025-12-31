import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatChipsModule } from '@angular/material/chips';
import { DeductionService, TaxYearService } from '../../services';
import { Deduction, DeductionCategory } from '../../models';
import { DeductionFormDialog, DeductionFormDialogData } from '../../components';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-deductions',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatDialogModule,
    MatChipsModule
  ],
  templateUrl: './deductions.html',
  styleUrl: './deductions.scss'
})
export class Deductions implements OnInit {
  private _deductionService = inject(DeductionService);
  private _taxYearService = inject(TaxYearService);
  private _dialog = inject(MatDialog);

  deductions$!: Observable<Deduction[]>;
  displayedColumns: string[] = ['date', 'description', 'category', 'amount', 'hasReceipt', 'actions'];

  categoryNames: { [key: number]: string } = {
    [DeductionCategory.MedicalExpenses]: 'Medical Expenses',
    [DeductionCategory.CharitableDonations]: 'Charitable Donations',
    [DeductionCategory.MortgageInterest]: 'Mortgage Interest',
    [DeductionCategory.StateAndLocalTaxes]: 'State and Local Taxes',
    [DeductionCategory.BusinessExpenses]: 'Business Expenses',
    [DeductionCategory.EducationExpenses]: 'Education Expenses',
    [DeductionCategory.HomeOffice]: 'Home Office',
    [DeductionCategory.Other]: 'Other'
  };

  ngOnInit(): void {
    this._deductionService.getAll().subscribe();
    this._taxYearService.getAll().subscribe();
    this.deductions$ = this._deductionService.deductions$;
  }

  openAddDialog(): void {
    const taxYears = this._taxYearService.taxYears$.value;
    const dialogRef = this._dialog.open(DeductionFormDialog, {
      width: '600px',
      data: { taxYears } as DeductionFormDialogData
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._deductionService.create(result).subscribe();
      }
    });
  }

  openEditDialog(deduction: Deduction): void {
    const taxYears = this._taxYearService.taxYears$.value;
    const dialogRef = this._dialog.open(DeductionFormDialog, {
      width: '600px',
      data: { deduction, taxYears } as DeductionFormDialogData
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._deductionService.update(result).subscribe();
      }
    });
  }

  deleteDeduction(id: string): void {
    if (confirm('Are you sure you want to delete this deduction?')) {
      this._deductionService.delete(id).subscribe();
    }
  }

  getCategoryName(category: DeductionCategory): string {
    return this.categoryNames[category];
  }
}
