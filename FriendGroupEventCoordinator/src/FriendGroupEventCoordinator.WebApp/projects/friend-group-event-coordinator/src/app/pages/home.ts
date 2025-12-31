import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterLink, MatButtonModule, MatCardModule, MatIconModule],
  template: `
    <div class="home">
      <div class="home__hero">
        <h1 class="home__title">Friend Group Event Coordinator</h1>
        <p class="home__subtitle">Organize events with your friends and manage group activities effortlessly</p>
      </div>

      <div class="home__cards">
        <mat-card class="home__card">
          <mat-card-header>
            <mat-icon class="home__card-icon">groups</mat-icon>
            <mat-card-title>Manage Groups</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p>Create and organize friend groups. Add members, assign roles, and keep everyone connected.</p>
          </mat-card-content>
          <mat-card-actions>
            <button mat-raised-button color="primary" routerLink="/groups">
              View Groups
            </button>
          </mat-card-actions>
        </mat-card>

        <mat-card class="home__card">
          <mat-card-header>
            <mat-icon class="home__card-icon">event</mat-icon>
            <mat-card-title>Plan Events</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p>Schedule events, set locations, and manage attendees. Keep track of who's coming and when.</p>
          </mat-card-content>
          <mat-card-actions>
            <button mat-raised-button color="primary" routerLink="/events">
              View Events
            </button>
          </mat-card-actions>
        </mat-card>

        <mat-card class="home__card">
          <mat-card-header>
            <mat-icon class="home__card-icon">how_to_reg</mat-icon>
            <mat-card-title>Track RSVPs</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p>Collect responses from members, track attendance, and manage guest lists for your events.</p>
          </mat-card-content>
          <mat-card-actions>
            <button mat-raised-button color="primary" routerLink="/events">
              Get Started
            </button>
          </mat-card-actions>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .home {
      padding: 2rem;
    }

    .home__hero {
      text-align: center;
      padding: 3rem 0;
      margin-bottom: 3rem;
    }

    .home__title {
      font-size: 2.5rem;
      margin-bottom: 1rem;
      color: #3f51b5;
    }

    .home__subtitle {
      font-size: 1.25rem;
      color: rgba(0, 0, 0, 0.6);
      max-width: 600px;
      margin: 0 auto;
    }

    .home__cards {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
      gap: 2rem;
      max-width: 1200px;
      margin: 0 auto;
    }

    .home__card {
      text-align: center;
    }

    .home__card-icon {
      font-size: 3rem;
      width: 3rem;
      height: 3rem;
      color: #3f51b5;
      margin: 1rem auto;
    }

    mat-card-header {
      display: flex;
      flex-direction: column;
      align-items: center;
    }

    mat-card-actions {
      display: flex;
      justify-content: center;
      padding: 1rem;
    }
  `]
})
export class Home {}
