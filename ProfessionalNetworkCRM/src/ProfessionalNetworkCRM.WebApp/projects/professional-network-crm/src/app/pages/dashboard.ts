import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ContactsService, FollowUpsService, InteractionsService } from '../services';

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
              <mat-icon class="dashboard__card-icon">contacts</mat-icon>
              Contacts
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-count">{{ (contactsService.contacts$ | async)?.length || 0 }}</div>
            <p class="dashboard__card-description">Total contacts in your network</p>
          </mat-card-content>
          <mat-card-actions>
            <button mat-button color="primary" routerLink="/contacts">View All</button>
            <button mat-raised-button color="primary" routerLink="/contacts/new">Add Contact</button>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">event</mat-icon>
              Follow-ups
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-count">{{ pendingFollowUps }}</div>
            <p class="dashboard__card-description">Pending follow-up tasks</p>
          </mat-card-content>
          <mat-card-actions>
            <button mat-button color="primary" routerLink="/follow-ups">View All</button>
            <button mat-raised-button color="primary" routerLink="/follow-ups/new">Add Follow-up</button>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">forum</mat-icon>
              Interactions
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-count">{{ (interactionsService.interactions$ | async)?.length || 0 }}</div>
            <p class="dashboard__card-description">Total logged interactions</p>
          </mat-card-content>
          <mat-card-actions>
            <button mat-button color="primary" routerLink="/interactions">View All</button>
            <button mat-raised-button color="primary" routerLink="/interactions/new">Log Interaction</button>
          </mat-card-actions>
        </mat-card>

        <mat-card class="dashboard__card">
          <mat-card-header>
            <mat-card-title class="dashboard__card-title">
              <mat-icon class="dashboard__card-icon">star</mat-icon>
              Priority Contacts
            </mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <div class="dashboard__card-count">{{ priorityContacts }}</div>
            <p class="dashboard__card-description">Marked as priority</p>
          </mat-card-content>
          <mat-card-actions>
            <button mat-button color="primary" routerLink="/contacts">View Contacts</button>
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
      margin-bottom: 2rem;
      color: #333;
    }

    .dashboard__cards {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
      gap: 1.5rem;
    }

    .dashboard__card {
      display: flex;
      flex-direction: column;
    }

    .dashboard__card-title {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .dashboard__card-icon {
      color: #1976d2;
    }

    .dashboard__card-count {
      font-size: 3rem;
      font-weight: bold;
      color: #1976d2;
      margin: 1rem 0;
    }

    .dashboard__card-description {
      color: #666;
      margin: 0;
    }
  `]
})
export class Dashboard implements OnInit {
  contactsService = inject(ContactsService);
  followUpsService = inject(FollowUpsService);
  interactionsService = inject(InteractionsService);

  pendingFollowUps = 0;
  priorityContacts = 0;

  ngOnInit(): void {
    this.contactsService.loadContacts().subscribe(contacts => {
      this.priorityContacts = contacts.filter(c => c.isPriority).length;
    });

    this.followUpsService.loadFollowUps().subscribe(followUps => {
      this.pendingFollowUps = followUps.filter(f => !f.isCompleted).length;
    });

    this.interactionsService.loadInteractions().subscribe();
  }
}
