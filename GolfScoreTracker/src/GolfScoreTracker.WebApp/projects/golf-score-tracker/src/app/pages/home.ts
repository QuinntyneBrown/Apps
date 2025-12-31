import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RoundsService, HandicapsService } from '../services';
import { Round, Handicap } from '../models';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  template: `
    <div class="home">
      <div class="home__header">
        <h1 class="home__title">Golf Score Tracker</h1>
        <p class="home__subtitle">Track your golf rounds, scores, and improve your game</p>
      </div>

      <div class="home__cards">
        <mat-card class="home__card">
          <mat-card-header>
            <mat-icon mat-card-avatar>golf_course</mat-icon>
            <mat-card-title>Recent Rounds</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div *ngIf="(rounds$ | async) as rounds">
              <p *ngIf="rounds.length === 0">No rounds recorded yet</p>
              <div *ngIf="rounds.length > 0" class="home__stats">
                <p>Total Rounds: {{ rounds.length }}</p>
                <p *ngIf="rounds[0]">Last Played: {{ rounds[0].playedDate | date }}</p>
              </div>
            </div>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/rounds">View All Rounds</a>
            <a mat-button color="accent" routerLink="/rounds/new">New Round</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="home__card">
          <mat-card-header>
            <mat-icon mat-card-avatar>terrain</mat-icon>
            <mat-card-title>Courses</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p>Manage your favorite golf courses</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/courses">View Courses</a>
            <a mat-button color="accent" routerLink="/courses/new">Add Course</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="home__card">
          <mat-card-header>
            <mat-icon mat-card-avatar>trending_up</mat-icon>
            <mat-card-title>Handicap</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div *ngIf="(handicaps$ | async) as handicaps">
              <p *ngIf="handicaps.length === 0">No handicap calculated yet</p>
              <div *ngIf="handicaps.length > 0 && handicaps[0]" class="home__stats">
                <p>Current Handicap: {{ handicaps[0].handicapIndex | number:'1.1-1' }}</p>
                <p>Based on {{ handicaps[0].roundsUsed }} rounds</p>
              </div>
            </div>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/handicaps">View Handicaps</a>
          </mat-card-actions>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .home {
      padding: 2rem;
      max-width: 1200px;
      margin: 0 auto;
    }

    .home__header {
      text-align: center;
      margin-bottom: 3rem;
    }

    .home__title {
      font-size: 2.5rem;
      margin-bottom: 0.5rem;
      color: #1976d2;
    }

    .home__subtitle {
      font-size: 1.2rem;
      color: #666;
    }

    .home__cards {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
      gap: 2rem;
    }

    .home__card {
      display: flex;
      flex-direction: column;
    }

    .home__stats {
      margin-top: 1rem;
    }

    .home__stats p {
      margin: 0.5rem 0;
    }
  `]
})
export class Home implements OnInit {
  rounds$ = this.roundsService.rounds$;
  handicaps$ = this.handicapsService.handicaps$;

  constructor(
    private roundsService: RoundsService,
    private handicapsService: HandicapsService
  ) {}

  ngOnInit(): void {
    this.roundsService.getRounds().subscribe();
    this.handicapsService.getHandicaps().subscribe();
  }
}
