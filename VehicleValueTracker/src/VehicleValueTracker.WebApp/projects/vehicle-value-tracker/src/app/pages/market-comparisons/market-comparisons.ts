import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatChipsModule } from '@angular/material/chips';
import { Observable } from 'rxjs';
import { MarketComparisonService, VehicleService } from '../../services';
import { MarketComparison } from '../../models';
import { MarketComparisonDialog } from '../../components';

@Component({
  selector: 'app-market-comparisons',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatDialogModule,
    MatSnackBarModule,
    MatChipsModule
  ],
  templateUrl: './market-comparisons.html',
  styleUrl: './market-comparisons.scss'
})
export class MarketComparisons implements OnInit {
  private _comparisonService = inject(MarketComparisonService);
  private _vehicleService = inject(VehicleService);
  private _dialog = inject(MatDialog);
  private _snackBar = inject(MatSnackBar);

  comparisons$!: Observable<MarketComparison[]>;
  displayedColumns: string[] = ['comparisonDate', 'listingSource', 'comparableVehicle', 'askingPrice', 'comparableMileage', 'isActive', 'actions'];

  ngOnInit(): void {
    this.comparisons$ = this._comparisonService.comparisons$;
    this._comparisonService.getMarketComparisons().subscribe();
    this._vehicleService.getVehicles().subscribe();
  }

  openAddDialog(): void {
    const dialogRef = this._dialog.open(MarketComparisonDialog, {
      width: '800px',
      data: {}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._comparisonService.createMarketComparison(result).subscribe({
          next: () => {
            this._snackBar.open('Market comparison added successfully', 'Close', { duration: 3000 });
          },
          error: () => {
            this._snackBar.open('Error adding market comparison', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  openEditDialog(comparison: MarketComparison): void {
    const dialogRef = this._dialog.open(MarketComparisonDialog, {
      width: '800px',
      data: { comparison }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._comparisonService.updateMarketComparison(comparison.marketComparisonId, result).subscribe({
          next: () => {
            this._snackBar.open('Market comparison updated successfully', 'Close', { duration: 3000 });
          },
          error: () => {
            this._snackBar.open('Error updating market comparison', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  deleteComparison(comparison: MarketComparison): void {
    if (confirm('Are you sure you want to delete this market comparison?')) {
      this._comparisonService.deleteMarketComparison(comparison.marketComparisonId).subscribe({
        next: () => {
          this._snackBar.open('Market comparison deleted successfully', 'Close', { duration: 3000 });
        },
        error: () => {
          this._snackBar.open('Error deleting market comparison', 'Close', { duration: 3000 });
        }
      });
    }
  }

  formatDate(date: string): string {
    return new Date(date).toLocaleDateString();
  }

  getComparableVehicleDescription(comparison: MarketComparison): string {
    return `${comparison.comparableYear} ${comparison.comparableMake} ${comparison.comparableModel}`;
  }
}
