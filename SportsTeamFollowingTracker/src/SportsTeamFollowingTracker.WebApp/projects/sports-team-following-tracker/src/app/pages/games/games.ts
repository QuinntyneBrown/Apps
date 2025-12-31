import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatChipsModule } from '@angular/material/chips';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { GameService } from '../../services';
import { GameDialog } from '../../components';
import { CreateGameRequest, UpdateGameRequest } from '../../models';

@Component({
  selector: 'app-games',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatChipsModule,
    MatDialogModule
  ],
  templateUrl: './games.html',
  styleUrl: './games.scss'
})
export class Games {
  private _gameService = inject(GameService);
  private _dialog = inject(MatDialog);

  games$ = this._gameService.games$;
  displayedColumns = ['teamName', 'opponent', 'gameDate', 'score', 'result', 'actions'];

  constructor() {
    this._gameService.getGames().subscribe();
  }

  openAddDialog(): void {
    const dialogRef = this._dialog.open(GameDialog, {
      width: '500px',
      data: {}
    });

    dialogRef.afterClosed().subscribe((result: CreateGameRequest) => {
      if (result) {
        this._gameService.createGame(result).subscribe();
      }
    });
  }

  openEditDialog(game: any): void {
    const dialogRef = this._dialog.open(GameDialog, {
      width: '500px',
      data: { game }
    });

    dialogRef.afterClosed().subscribe((result: UpdateGameRequest) => {
      if (result) {
        this._gameService.updateGame(result).subscribe();
      }
    });
  }

  deleteGame(id: string): void {
    if (confirm('Are you sure you want to delete this game?')) {
      this._gameService.deleteGame(id).subscribe();
    }
  }
}
