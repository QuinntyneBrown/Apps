import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { AlbumService } from '../services/album.service';
import { ArtistService } from '../services/artist.service';
import { ListeningLogService } from '../services/listening-log.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  template: `
    <div class="dashboard">
      <h1 class="dashboard__title">Music Collection Dashboard</h1>

      <div class="dashboard__cards">
        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">album</mat-icon>
              Albums
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-count">{{ (albums$ | async)?.length || 0 }}</div>
            <p class="dashboard__card-description">Total albums in your collection</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-raised-button color="primary" routerLink="/albums" class="dashboard__card-button">
              View Albums
            </a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">person</mat-icon>
              Artists
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-count">{{ (artists$ | async)?.length || 0 }}</div>
            <p class="dashboard__card-description">Total artists in your collection</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-raised-button color="primary" routerLink="/artists" class="dashboard__card-button">
              View Artists
            </a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">headphones</mat-icon>
              Listening Logs
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-count">{{ (listeningLogs$ | async)?.length || 0 }}</div>
            <p class="dashboard__card-description">Total listening sessions logged</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-raised-button color="primary" routerLink="/listening-logs" class="dashboard__card-button">
              View Logs
            </a>
          </mat-card-actions>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .dashboard {
      padding: 2rem;
    }

    .dashboard__title {
      margin: 0 0 2rem 0;
      font-size: 2rem;
      font-weight: 500;
      color: #333;
    }

    .dashboard__cards {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
      gap: 2rem;
    }

    .dashboard__card {
      display: flex;
      flex-direction: column;
    }

    .dashboard__card-title {
      display: flex;
      align-items: center;
      gap: 0.5rem;
      font-size: 1.25rem;
    }

    .dashboard__card-icon {
      font-size: 1.5rem;
      width: 1.5rem;
      height: 1.5rem;
    }

    .dashboard__card-count {
      font-size: 3rem;
      font-weight: 700;
      color: #3f51b5;
      margin: 1rem 0;
    }

    .dashboard__card-description {
      color: #666;
      margin: 0;
    }

    .dashboard__card-button {
      width: 100%;
    }
  `]
})
export class Dashboard implements OnInit {
  private readonly albumService = inject(AlbumService);
  private readonly artistService = inject(ArtistService);
  private readonly listeningLogService = inject(ListeningLogService);

  albums$ = this.albumService.albums$;
  artists$ = this.artistService.artists$;
  listeningLogs$ = this.listeningLogService.listeningLogs$;

  ngOnInit(): void {
    this.albumService.getAlbums().subscribe();
    this.artistService.getArtists().subscribe();
    this.listeningLogService.getListeningLogs().subscribe();
  }
}
