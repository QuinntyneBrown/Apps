import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { GroupService, MeetingService, TopicService, ResourceService } from '../services';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatButtonModule,
    MatIconModule
  ],
  template: `
    <div class="dashboard">
      <h1 class="dashboard__title">Dashboard</h1>

      <div class="dashboard__cards">
        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon class="dashboard__card-icon dashboard__card-icon--groups">groups</mat-icon>
            <mat-card-title>Groups</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-count">{{ (groups$ | async)?.length || 0 }}</div>
            <p class="dashboard__card-description">Active men's groups</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/groups">View All</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon class="dashboard__card-icon dashboard__card-icon--meetings">event</mat-icon>
            <mat-card-title>Meetings</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-count">{{ (meetings$ | async)?.length || 0 }}</div>
            <p class="dashboard__card-description">Scheduled meetings</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/meetings">View All</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon class="dashboard__card-icon dashboard__card-icon--topics">topic</mat-icon>
            <mat-card-title>Topics</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-count">{{ (topics$ | async)?.length || 0 }}</div>
            <p class="dashboard__card-description">Discussion topics</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/topics">View All</a>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-icon class="dashboard__card-icon dashboard__card-icon--resources">library_books</mat-icon>
            <mat-card-title>Resources</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-count">{{ (resources$ | async)?.length || 0 }}</div>
            <p class="dashboard__card-description">Shared resources</p>
          </mat-card-content>
          <mat-card-actions>
            <a mat-button color="primary" routerLink="/resources">View All</a>
          </mat-card-actions>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .dashboard {
      padding: 2rem;

      &__title {
        margin: 0 0 2rem 0;
        font-size: 2rem;
        font-weight: 400;
      }

      &__cards {
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
        gap: 1.5rem;
      }

      &__card {
        mat-card-header {
          display: flex;
          align-items: center;
          gap: 1rem;
        }

        mat-card-content {
          margin-top: 1rem;
        }

        mat-card-actions {
          padding: 0 1rem 1rem;
        }
      }

      &__card-icon {
        font-size: 2.5rem;
        width: 2.5rem;
        height: 2.5rem;

        &--groups {
          color: #3f51b5;
        }

        &--meetings {
          color: #009688;
        }

        &--topics {
          color: #ff9800;
        }

        &--resources {
          color: #9c27b0;
        }
      }

      &__card-count {
        font-size: 3rem;
        font-weight: 300;
        line-height: 1;
      }

      &__card-description {
        margin: 0.5rem 0 0 0;
        color: rgba(0, 0, 0, 0.6);
      }
    }
  `]
})
export class Dashboard implements OnInit {
  private readonly groupService = inject(GroupService);
  private readonly meetingService = inject(MeetingService);
  private readonly topicService = inject(TopicService);
  private readonly resourceService = inject(ResourceService);

  groups$ = this.groupService.groups$;
  meetings$ = this.meetingService.meetings$;
  topics$ = this.topicService.topics$;
  resources$ = this.resourceService.resources$;

  ngOnInit(): void {
    this.groupService.getAll().subscribe();
    this.meetingService.getAll().subscribe();
    this.topicService.getAll().subscribe();
    this.resourceService.getAll().subscribe();
  }
}
