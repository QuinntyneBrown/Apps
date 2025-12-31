import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { PlaySessionsService, GamesService } from '../../services';
import { PlaySessionFormDialog } from '../../components/play-session-form-dialog/play-session-form-dialog';
import { PlaySession } from '../../models';
import { map } from 'rxjs/operators';
import { combineLatest } from 'rxjs';

@Component({
  selector: 'app-play-sessions',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatDialogModule
  ],
  templateUrl: './play-sessions.html',
  styleUrl: './play-sessions.scss'
})
export class PlaySessions {
  private _playSessionsService = inject(PlaySessionsService);
  private _gamesService = inject(GamesService);
  private _dialog = inject(MatDialog);

  displayedColumns: string[] = ['game', 'startTime', 'endTime', 'duration', 'actions'];

  sessionsWithGames$ = combineLatest([
    this._playSessionsService.playSessions$,
    this._gamesService.games$
  ]).pipe(
    map(([sessions, games]) =>
      sessions.map(session => ({
        ...session,
        gameName: games.find(g => g.gameId === session.gameId)?.title || 'Unknown Game'
      }))
    )
  );

  ngOnInit() {
    this._playSessionsService.getAll().subscribe();
    this._gamesService.getAll().subscribe();
  }

  openAddDialog(): void {
    const dialogRef = this._dialog.open(PlaySessionFormDialog, {
      width: '600px',
      data: {}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._playSessionsService.create(result).subscribe();
      }
    });
  }

  openEditDialog(session: PlaySession): void {
    const dialogRef = this._dialog.open(PlaySessionFormDialog, {
      width: '600px',
      data: { session }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._playSessionsService.update(session.playSessionId, result).subscribe();
      }
    });
  }

  deleteSession(id: string): void {
    if (confirm('Are you sure you want to delete this play session?')) {
      this._playSessionsService.delete(id).subscribe();
    }
  }

  formatDateTime(dateString: string | undefined): string {
    if (!dateString) return '-';
    return new Date(dateString).toLocaleString();
  }
}
