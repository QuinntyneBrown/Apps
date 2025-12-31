import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { Observable } from 'rxjs';
import { ValueAssessmentService, VehicleService } from '../../services';
import { ValueAssessment } from '../../models';
import { ValueAssessmentDialog } from '../../components';

@Component({
  selector: 'app-value-assessments',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatDialogModule,
    MatSnackBarModule
  ],
  templateUrl: './value-assessments.html',
  styleUrl: './value-assessments.scss'
})
export class ValueAssessments implements OnInit {
  private _assessmentService = inject(ValueAssessmentService);
  private _vehicleService = inject(VehicleService);
  private _dialog = inject(MatDialog);
  private _snackBar = inject(MatSnackBar);

  assessments$!: Observable<ValueAssessment[]>;
  displayedColumns: string[] = ['assessmentDate', 'vehicleId', 'estimatedValue', 'mileageAtAssessment', 'conditionGrade', 'actions'];

  ngOnInit(): void {
    this.assessments$ = this._assessmentService.assessments$;
    this._assessmentService.getValueAssessments().subscribe();
    this._vehicleService.getVehicles().subscribe();
  }

  openAddDialog(): void {
    const dialogRef = this._dialog.open(ValueAssessmentDialog, {
      width: '800px',
      data: {}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._assessmentService.createValueAssessment(result).subscribe({
          next: () => {
            this._snackBar.open('Value assessment added successfully', 'Close', { duration: 3000 });
          },
          error: () => {
            this._snackBar.open('Error adding value assessment', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  openEditDialog(assessment: ValueAssessment): void {
    const dialogRef = this._dialog.open(ValueAssessmentDialog, {
      width: '800px',
      data: { assessment }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._assessmentService.updateValueAssessment(assessment.valueAssessmentId, result).subscribe({
          next: () => {
            this._snackBar.open('Value assessment updated successfully', 'Close', { duration: 3000 });
          },
          error: () => {
            this._snackBar.open('Error updating value assessment', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  deleteAssessment(assessment: ValueAssessment): void {
    if (confirm('Are you sure you want to delete this value assessment?')) {
      this._assessmentService.deleteValueAssessment(assessment.valueAssessmentId).subscribe({
        next: () => {
          this._snackBar.open('Value assessment deleted successfully', 'Close', { duration: 3000 });
        },
        error: () => {
          this._snackBar.open('Error deleting value assessment', 'Close', { duration: 3000 });
        }
      });
    }
  }

  formatDate(date: string): string {
    return new Date(date).toLocaleDateString();
  }
}
