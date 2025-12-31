import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { CompensationService } from '../../services';
import { Compensation } from '../../models';
import { CompensationFormDialog } from '../../components/compensation-form-dialog/compensation-form-dialog';

@Component({
  selector: 'app-compensations',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatTableModule, MatDialogModule],
  templateUrl: './compensations.html',
  styleUrl: './compensations.scss'
})
export class Compensations implements OnInit {
  private _compensationService = inject(CompensationService);
  private _dialog = inject(MatDialog);

  compensations$!: Observable<Compensation[]>;
  displayedColumns: string[] = ['employer', 'jobTitle', 'compensationType', 'baseSalary', 'totalCompensation', 'effectiveDate', 'actions'];

  ngOnInit(): void {
    this._compensationService.getCompensations().subscribe();
    this.compensations$ = this._compensationService.compensations$;
  }

  openCreateDialog(): void {
    const dialogRef = this._dialog.open(CompensationFormDialog, {
      width: '600px',
      data: null
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._compensationService.createCompensation(result).subscribe();
      }
    });
  }

  openEditDialog(compensation: Compensation): void {
    const dialogRef = this._dialog.open(CompensationFormDialog, {
      width: '600px',
      data: compensation
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._compensationService.updateCompensation(compensation.compensationId, result).subscribe();
      }
    });
  }

  deleteCompensation(id: string): void {
    if (confirm('Are you sure you want to delete this compensation record?')) {
      this._compensationService.deleteCompensation(id).subscribe();
    }
  }
}
