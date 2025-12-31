import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { TeamService, GameService, SeasonService, StatisticService } from '../../services';
import { map } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard {
  private _teamService = inject(TeamService);
  private _gameService = inject(GameService);
  private _seasonService = inject(SeasonService);
  private _statisticService = inject(StatisticService);

  teams$ = this._teamService.teams$;
  games$ = this._gameService.games$;
  seasons$ = this._seasonService.seasons$;
  statistics$ = this._statisticService.statistics$;

  teamCount$ = this.teams$.pipe(map(teams => teams.length));
  gameCount$ = this.games$.pipe(map(games => games.length));
  seasonCount$ = this.seasons$.pipe(map(seasons => seasons.length));
  statisticCount$ = this.statistics$.pipe(map(stats => stats.length));

  favoriteTeams$ = this.teams$.pipe(
    map(teams => teams.filter(t => t.isFavorite))
  );

  upcomingGames$ = this.games$.pipe(
    map(games => {
      const now = new Date();
      return games
        .filter(g => new Date(g.gameDate) >= now && !g.isCompleted)
        .sort((a, b) => new Date(a.gameDate).getTime() - new Date(b.gameDate).getTime())
        .slice(0, 5);
    })
  );

  recentGames$ = this.games$.pipe(
    map(games => {
      return games
        .filter(g => g.isCompleted)
        .sort((a, b) => new Date(b.gameDate).getTime() - new Date(a.gameDate).getTime())
        .slice(0, 5);
    })
  );

  constructor() {
    this._teamService.getTeams().subscribe();
    this._gameService.getGames().subscribe();
    this._seasonService.getSeasons().subscribe();
    this._statisticService.getStatistics().subscribe();
  }
}
