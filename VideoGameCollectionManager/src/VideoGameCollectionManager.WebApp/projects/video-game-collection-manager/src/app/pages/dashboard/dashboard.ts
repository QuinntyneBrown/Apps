import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { GamesService, PlaySessionsService, WishlistsService } from '../../services';
import { map } from 'rxjs/operators';
import { combineLatest } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard {
  private _gamesService = inject(GamesService);
  private _playSessionsService = inject(PlaySessionsService);
  private _wishlistsService = inject(WishlistsService);

  stats$ = combineLatest([
    this._gamesService.games$,
    this._playSessionsService.playSessions$,
    this._wishlistsService.wishlists$
  ]).pipe(
    map(([games, sessions, wishlists]) => ({
      totalGames: games.length,
      completedGames: games.filter(g => g.status === 'Completed').length,
      totalPlaySessions: sessions.length,
      totalWishlistItems: wishlists.filter(w => !w.isAcquired).length
    }))
  );

  ngOnInit() {
    this._gamesService.getAll().subscribe();
    this._playSessionsService.getAll().subscribe();
    this._wishlistsService.getAll().subscribe();
  }
}
