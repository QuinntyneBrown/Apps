import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { MarketComparisonService } from '../../services';
import { MarketComparison } from '../../models';
import { MarketComparisonFormDialog } from '../../components/market-comparison-form-dialog/market-comparison-form-dialog';

@Component({
  selector: 'app-market-comparisons',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatTableModule, MatDialogModule],
  templateUrl: './market-comparisons.html',
  styleUrl: './market-comparisons.scss'
})
export class MarketComparisons implements OnInit {
  private _marketComparisonService = inject(MarketComparisonService);
  private _dialog = inject(MatDialog);

  marketComparisons$!: Observable<MarketComparison[]>;
  displayedColumns: string[] = ['jobTitle', 'location', 'experienceLevel', 'minSalary', 'maxSalary', 'medianSalary', 'comparisonDate', 'actions'];

  ngOnInit(): void {
    this._marketComparisonService.getMarketComparisons().subscribe();
    this.marketComparisons$ = this._marketComparisonService.marketComparisons$;
  }

  openCreateDialog(): void {
    const dialogRef = this._dialog.open(MarketComparisonFormDialog, {
      width: '600px',
      data: null
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._marketComparisonService.createMarketComparison(result).subscribe();
      }
    });
  }

  openEditDialog(marketComparison: MarketComparison): void {
    const dialogRef = this._dialog.open(MarketComparisonFormDialog, {
      width: '600px',
      data: marketComparison
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._marketComparisonService.updateMarketComparison(marketComparison.marketComparisonId, result).subscribe();
      }
    });
  }

  deleteMarketComparison(id: string): void {
    if (confirm('Are you sure you want to delete this market comparison?')) {
      this._marketComparisonService.deleteMarketComparison(id).subscribe();
    }
  }
}
