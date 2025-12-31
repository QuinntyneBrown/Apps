import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatChipsModule } from '@angular/material/chips';
import { TaxYearService } from '../../services';
import { TaxYear } from '../../models';
import { TaxYearFormDialog, TaxYearFormDialogData } from '../../components';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-tax-years',
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
  templateUrl: './tax-years.html',
  styleUrl: './tax-years.scss'
})
export class TaxYears implements OnInit {
  private _taxYearService = inject(TaxYearService);
  private _dialog = inject(MatDialog);

  taxYears$!: Observable<TaxYear[]>;
  displayedColumns: string[] = ['year', 'totalDeductions', 'isFiled', 'filingDate', 'actions'];

  ngOnInit(): void {
    this._taxYearService.getAll().subscribe();
    this.taxYears$ = this._taxYearService.taxYears$;
  }

  openAddDialog(): void {
    const dialogRef = this._dialog.open(TaxYearFormDialog, {
      width: '600px',
      data: {} as TaxYearFormDialogData
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._taxYearService.create(result).subscribe();
      }
    });
  }

  openEditDialog(taxYear: TaxYear): void {
    const dialogRef = this._dialog.open(TaxYearFormDialog, {
      width: '600px',
      data: { taxYear } as TaxYearFormDialogData
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._taxYearService.update(result).subscribe();
      }
    });
  }

  deleteTaxYear(id: string): void {
    if (confirm('Are you sure you want to delete this tax year?')) {
      this._taxYearService.delete(id).subscribe();
    }
  }
}
