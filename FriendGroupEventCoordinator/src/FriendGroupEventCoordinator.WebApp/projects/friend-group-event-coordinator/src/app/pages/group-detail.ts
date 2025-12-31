import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTabsModule } from '@angular/material/tabs';
import { GroupsService, MembersService, EventsService } from '../services';
import { Group, Member, Event } from '../models';
import { Observable, switchMap } from 'rxjs';

@Component({
  selector: 'app-group-detail',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatCardModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatTabsModule
  ],
  template: `
    <div class="group-detail" *ngIf="group$ | async as group; else loading">
      <div class="group-detail__header">
        <button mat-icon-button (click)="goBack()" class="group-detail__back-btn">
          <mat-icon>arrow_back</mat-icon>
        </button>
        <h1 class="group-detail__title">{{ group.name }}</h1>
      </div>

      <mat-card class="group-detail__info">
        <mat-card-content>
          <p class="group-detail__description">{{ group.description || 'No description provided.' }}</p>
          <div class="group-detail__meta">
            <p><strong>Created:</strong> {{ group.createdAt | date: 'medium' }}</p>
            <p><strong>Status:</strong> {{ group.isActive ? 'Active' : 'Inactive' }}</p>
            <p><strong>Members:</strong> {{ group.activeMemberCount }}</p>
          </div>
        </mat-card-content>
      </mat-card>

      <mat-tab-group class="group-detail__tabs">
        <mat-tab label="Members">
          <div class="group-detail__tab-content">
            <div class="group-detail__tab-header">
              <h2>Members</h2>
              <button mat-raised-button color="primary">
                <mat-icon>person_add</mat-icon>
                Add Member
              </button>
            </div>
            <div *ngIf="members$ | async as members">
              <p *ngIf="members.length === 0">No members found.</p>
              <mat-card *ngFor="let member of members" class="group-detail__member-card">
                <mat-card-content>
                  <div class="group-detail__member-info">
                    <div>
                      <h3>{{ member.name }}</h3>
                      <p *ngIf="member.email">{{ member.email }}</p>
                    </div>
                    <div class="group-detail__member-badges">
                      <span *ngIf="member.isAdmin" class="group-detail__badge group-detail__badge--admin">Admin</span>
                      <span *ngIf="!member.isActive" class="group-detail__badge group-detail__badge--inactive">Inactive</span>
                    </div>
                  </div>
                </mat-card-content>
              </mat-card>
            </div>
          </div>
        </mat-tab>

        <mat-tab label="Events">
          <div class="group-detail__tab-content">
            <div class="group-detail__tab-header">
              <h2>Events</h2>
              <button mat-raised-button color="primary">
                <mat-icon>add</mat-icon>
                Create Event
              </button>
            </div>
            <div *ngIf="events$ | async as events">
              <p *ngIf="events.length === 0">No events found.</p>
              <mat-card *ngFor="let event of events" class="group-detail__event-card">
                <mat-card-content>
                  <h3>{{ event.title }}</h3>
                  <p>{{ event.description }}</p>
                  <p><strong>Date:</strong> {{ event.startDateTime | date: 'medium' }}</p>
                  <p *ngIf="event.location"><strong>Location:</strong> {{ event.location }}</p>
                </mat-card-content>
              </mat-card>
            </div>
          </div>
        </mat-tab>
      </mat-tab-group>
    </div>

    <ng-template #loading>
      <div class="group-detail__loading">
        <mat-spinner></mat-spinner>
      </div>
    </ng-template>
  `,
  styles: [`
    .group-detail {
      padding: 2rem;
    }

    .group-detail__header {
      display: flex;
      align-items: center;
      gap: 1rem;
      margin-bottom: 2rem;
    }

    .group-detail__back-btn {
      margin-right: 0.5rem;
    }

    .group-detail__title {
      margin: 0;
      font-size: 2rem;
    }

    .group-detail__info {
      margin-bottom: 2rem;
    }

    .group-detail__description {
      font-size: 1.1rem;
      margin-bottom: 1rem;
    }

    .group-detail__meta p {
      margin: 0.5rem 0;
    }

    .group-detail__tabs {
      margin-top: 2rem;
    }

    .group-detail__tab-content {
      padding: 2rem 0;
    }

    .group-detail__tab-header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 1.5rem;
    }

    .group-detail__member-card,
    .group-detail__event-card {
      margin-bottom: 1rem;
    }

    .group-detail__member-info {
      display: flex;
      justify-content: space-between;
      align-items: center;
    }

    .group-detail__member-badges {
      display: flex;
      gap: 0.5rem;
    }

    .group-detail__badge {
      padding: 0.25rem 0.75rem;
      border-radius: 1rem;
      font-size: 0.875rem;
      font-weight: 500;
    }

    .group-detail__badge--admin {
      background-color: #4caf50;
      color: white;
    }

    .group-detail__badge--inactive {
      background-color: #9e9e9e;
      color: white;
    }

    .group-detail__loading {
      display: flex;
      justify-content: center;
      align-items: center;
      padding: 3rem;
    }
  `]
})
export class GroupDetail implements OnInit {
  group$!: Observable<Group>;
  members$!: Observable<Member[]>;
  events$!: Observable<Event[]>;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private groupsService: GroupsService,
    private membersService: MembersService,
    private eventsService: EventsService
  ) {}

  ngOnInit(): void {
    const groupId = this.route.snapshot.paramMap.get('id');
    if (groupId) {
      this.group$ = this.groupsService.getGroup(groupId);
      this.members$ = this.membersService.getMembersByGroup(groupId);
      this.events$ = this.eventsService.getEventsByGroup(groupId);
    }
  }

  goBack(): void {
    this.router.navigate(['/groups']);
  }
}
