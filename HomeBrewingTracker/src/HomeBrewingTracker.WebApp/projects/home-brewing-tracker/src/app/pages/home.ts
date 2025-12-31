import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  template: `
    <div class="home">
      <div class="home__header">
        <h1 class="home__title">Welcome to Home Brewing Tracker</h1>
        <p class="home__subtitle">Track your brewing journey from recipe to tasting</p>
      </div>

      <div class="home__cards">
        <mat-card class="home__card">
          <mat-card-header>
            <mat-icon class="home__card-icon">book</mat-icon>
            <mat-card-title>Recipes</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p>Create and manage your brewing recipes. Track ingredients, instructions, and brewing parameters.</p>
          </mat-card-content>
          <mat-card-actions>
            <button mat-raised-button color="primary" routerLink="/recipes">View Recipes</button>
          </mat-card-actions>
        </mat-card>

        <mat-card class="home__card">
          <mat-card-header>
            <mat-icon class="home__card-icon">science</mat-icon>
            <mat-card-title>Batches</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p>Monitor your brewing batches. Track status, gravity readings, and bottling dates.</p>
          </mat-card-content>
          <mat-card-actions>
            <button mat-raised-button color="primary" routerLink="/batches">View Batches</button>
          </mat-card-actions>
        </mat-card>

        <mat-card class="home__card">
          <mat-card-header>
            <mat-icon class="home__card-icon">rate_review</mat-icon>
            <mat-card-title>Tasting Notes</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p>Record your tasting experiences. Rate and review your finished brews.</p>
          </mat-card-content>
          <mat-card-actions>
            <button mat-raised-button color="primary" routerLink="/tasting-notes">View Tasting Notes</button>
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

      &__header {
        text-align: center;
        margin-bottom: 3rem;
      }

      &__title {
        font-size: 2.5rem;
        margin-bottom: 1rem;
        color: #3f51b5;
      }

      &__subtitle {
        font-size: 1.25rem;
        color: #666;
      }

      &__cards {
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
        gap: 2rem;
      }

      &__card {
        mat-card-header {
          display: flex;
          align-items: center;
          margin-bottom: 1rem;
        }

        &-icon {
          font-size: 2rem;
          height: 2rem;
          width: 2rem;
          margin-right: 1rem;
          color: #3f51b5;
        }

        mat-card-content {
          min-height: 80px;
        }

        mat-card-actions {
          padding: 0 1rem 1rem;
        }
      }
    }
  `]
})
export class Home {}
