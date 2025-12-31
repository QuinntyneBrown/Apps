import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { SeasonService } from '../../services';
import { SeasonDialog } from '../../components';
import { CreateSeasonRequest, UpdateSeasonRequest } from '../../models';

@Component({
  selector: 'app-seasons',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatDialogModule
  ],
  templateUrl: './seasons.html',
  styleUrl: './seasons.scss'
})
export class Seasons {
  private _seasonService = inject(SeasonService);
  private _dialog = inject(MatDialog);

  seasons$ = this._seasonService.seasons$;
  displayedColumns = ['seasonName', 'year', 'wins', 'losses', 'totalGames', 'winPercentage', 'actions'];

  constructor() {
    this._seasonService.getSeasons().subscribe();
  }

  openAddDialog(): void {
    const dialogRef = this._dialog.open(SeasonDialog, {
      width: '500px',
      data: {}
    });

    dialogRef.afterClosed().subscribe((result: CreateSeasonRequest) => {
      if (result) {
        this._seasonService.createSeason(result).subscribe();
      }
    });
  }

  openEditDialog(season: any): void {
    const dialogRef = this._dialog.open(SeasonDialog, {
      width: '500px',
      data: { season }
    });

    dialogRef.afterClosed().subscribe((result: UpdateSeasonRequest) => {
      if (result) {
        this._seasonService.updateSeason(result).subscribe();
      }
    });
  }

  deleteSeason(id: string): void {
    if (confirm('Are you sure you want to delete this season?')) {
      this._seasonService.deleteSeason(id).subscribe();
    }
  }
}
