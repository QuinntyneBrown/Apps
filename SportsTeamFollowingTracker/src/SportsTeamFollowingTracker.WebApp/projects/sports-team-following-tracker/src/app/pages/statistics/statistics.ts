import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { StatisticService } from '../../services';
import { StatisticDialog } from '../../components';
import { CreateStatisticRequest, UpdateStatisticRequest } from '../../models';

@Component({
  selector: 'app-statistics',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatDialogModule
  ],
  templateUrl: './statistics.html',
  styleUrl: './statistics.scss'
})
export class Statistics {
  private _statisticService = inject(StatisticService);
  private _dialog = inject(MatDialog);

  statistics$ = this._statisticService.statistics$;
  displayedColumns = ['statName', 'value', 'recordedDate', 'actions'];

  constructor() {
    this._statisticService.getStatistics().subscribe();
  }

  openAddDialog(): void {
    const dialogRef = this._dialog.open(StatisticDialog, {
      width: '500px',
      data: {}
    });

    dialogRef.afterClosed().subscribe((result: CreateStatisticRequest) => {
      if (result) {
        this._statisticService.createStatistic(result).subscribe();
      }
    });
  }

  openEditDialog(statistic: any): void {
    const dialogRef = this._dialog.open(StatisticDialog, {
      width: '500px',
      data: { statistic }
    });

    dialogRef.afterClosed().subscribe((result: UpdateStatisticRequest) => {
      if (result) {
        this._statisticService.updateStatistic(result).subscribe();
      }
    });
  }

  deleteStatistic(id: string): void {
    if (confirm('Are you sure you want to delete this statistic?')) {
      this._statisticService.deleteStatistic(id).subscribe();
    }
  }
}
