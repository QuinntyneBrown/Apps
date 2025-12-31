import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatChipsModule } from '@angular/material/chips';
import { GamesService } from '../../services';
import { GameFormDialog } from '../../components/game-form-dialog/game-form-dialog';
import { Game } from '../../models';

@Component({
  selector: 'app-games',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatDialogModule,
    MatChipsModule
  ],
  templateUrl: './games.html',
  styleUrl: './games.scss'
})
export class Games {
  private _gamesService = inject(GamesService);
  private _dialog = inject(MatDialog);

  games$ = this._gamesService.games$;
  displayedColumns: string[] = ['title', 'platform', 'genre', 'status', 'rating', 'actions'];

  ngOnInit() {
    this._gamesService.getAll().subscribe();
  }

  openAddDialog(): void {
    const dialogRef = this._dialog.open(GameFormDialog, {
      width: '600px',
      data: {}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._gamesService.create(result).subscribe();
      }
    });
  }

  openEditDialog(game: Game): void {
    const dialogRef = this._dialog.open(GameFormDialog, {
      width: '600px',
      data: { game }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._gamesService.update(game.gameId, result).subscribe();
      }
    });
  }

  deleteGame(id: string): void {
    if (confirm('Are you sure you want to delete this game?')) {
      this._gamesService.delete(id).subscribe();
    }
  }
}
