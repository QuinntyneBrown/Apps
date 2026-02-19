import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { NeighborService, EventService, MessageService, RecommendationService } from '../services';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  template: `
    <div class="dashboard">
      <h1 class="dashboard__title">Dashboard</h1>
      <div class="dashboard__cards">
        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">people</mat-icon>
              Neighbors
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__card-count">{{ (neighbors$ | async)?.length || 0 }}</p>
            <p class="dashboard__card-description">Total neighbors in the network</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/neighbors">View All</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">event</mat-icon>
              Events
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__card-count">{{ (events$ | async)?.length || 0 }}</p>
            <p class="dashboard__card-description">Upcoming neighborhood events</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/events">View All</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">message</mat-icon>
              Messages
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__card-count">{{ (messages$ | async)?.length || 0 }}</p>
            <p class="dashboard__card-description">Total messages exchanged</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/messages">View All</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">recommend</mat-icon>
              Recommendations
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="dashboard__card-count">{{ (recommendations$ | async)?.length || 0 }}</p>
            <p class="dashboard__card-description">Local business recommendations</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/recommendations">View All</a>
          </mat-card-actions>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .dashboard {
      padding: 24px;

      &__title {
        margin-bottom: 24px;
        font-size: 2rem;
        font-weight: 400;
      }

      &__cards {
        display: grid;
        grid-template-columns: repeat(auto-fill, minmax(280px, 1fr));
        gap: 24px;
      }

      &__card {
        &-title {
          display: flex;
          align-items: center;
          gap: 8px;
        }

        &-icon {
          color: #3f51b5;
        }

        &-count {
          font-size: 3rem;
          font-weight: 300;
          margin: 16px 0 8px 0;
          color: #3f51b5;
        }

        &-description {
          color: rgba(0, 0, 0, 0.6);
          margin: 0;
        }
      }
    }
  `]
})
export class Dashboard implements OnInit {
  private neighborService = inject(NeighborService);
  private eventService = inject(EventService);
  private messageService = inject(MessageService);
  private recommendationService = inject(RecommendationService);

  neighbors$ = this.neighborService.neighbors$;
  events$ = this.eventService.events$;
  messages$ = this.messageService.messages$;
  recommendations$ = this.recommendationService.recommendations$;

  ngOnInit() {
    this.neighborService.getAll().subscribe();
    this.eventService.getAll().subscribe();
    this.messageService.getAll().subscribe();
    this.recommendationService.getAll().subscribe();
  }
}
