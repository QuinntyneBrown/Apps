import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { EfficiencyReportsService } from '../services';
import { ReportDialog } from './report-dialog';

@Component({
  selector: 'app-reports',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatCardModule,
    MatDialogModule,
    MatSnackBarModule
  ],
  templateUrl: './reports.html',
  styleUrl: './reports.scss'
})
export class Reports implements OnInit {
  private reportsService = inject(EfficiencyReportsService);
  private dialog = inject(MatDialog);
  private snackBar = inject(MatSnackBar);

  reports$ = this.reportsService.reports$;
  displayedColumns = ['startDate', 'endDate', 'totalMiles', 'totalGallons', 'averageMPG', 'totalFuelCost', 'costPerMile', 'numberOfFillUps', 'actions'];

  ngOnInit(): void {
    this.loadReports();
  }

  loadReports(): void {
    this.reportsService.getAll().subscribe();
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(ReportDialog, {
      width: '600px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadReports();
      }
    });
  }

  deleteReport(id: string): void {
    if (confirm('Are you sure you want to delete this report?')) {
      this.reportsService.delete(id).subscribe({
        next: () => {
          this.snackBar.open('Report deleted successfully', 'Close', { duration: 3000 });
        },
        error: () => {
          this.snackBar.open('Error deleting report', 'Close', { duration: 3000 });
        }
      });
    }
  }
}
