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
      <div class="home__hero">
        <h1 class="home__title">Family Photo Album Organizer</h1>
        <p class="home__subtitle">Organize, manage, and cherish your family memories</p>
      </div>

      <div class="home__features">
        <mat-card class="home__card">
          <mat-card-header>
            <mat-icon class="home__card-icon" color="primary">photo_library</mat-icon>
            <mat-card-title>Albums</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p>Create and organize photo albums by events, dates, or themes</p>
          </mat-card-content>
          <mat-card-actions>
            <button mat-raised-button color="primary" routerLink="/albums">View Albums</button>
          </mat-card-actions>
        </mat-card>

        <mat-card class="home__card">
          <mat-card-header>
            <mat-icon class="home__card-icon" color="accent">photo</mat-icon>
            <mat-card-title>Photos</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p>Upload and manage your photos with captions, locations, and dates</p>
          </mat-card-content>
          <mat-card-actions>
            <button mat-raised-button color="primary" routerLink="/photos">View Photos</button>
          </mat-card-actions>
        </mat-card>

        <mat-card class="home__card">
          <mat-card-header>
            <mat-icon class="home__card-icon" color="primary">label</mat-icon>
            <mat-card-title>Tags</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p>Organize photos with custom tags for easy searching and filtering</p>
          </mat-card-content>
          <mat-card-actions>
            <button mat-raised-button color="primary" routerLink="/tags">View Tags</button>
          </mat-card-actions>
        </mat-card>

        <mat-card class="home__card">
          <mat-card-header>
            <mat-icon class="home__card-icon" color="accent">person</mat-icon>
            <mat-card-title>People</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p>Tag people in your photos and track family member appearances</p>
          </mat-card-content>
          <mat-card-actions>
            <button mat-raised-button color="primary" routerLink="/people">View People</button>
          </mat-card-actions>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .home {
      padding: 2rem;

      &__hero {
        text-align: center;
        padding: 3rem 0;
        background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
        color: white;
        border-radius: 8px;
        margin-bottom: 3rem;
      }

      &__title {
        margin: 0 0 1rem 0;
        font-size: 2.5rem;
        font-weight: 600;
      }

      &__subtitle {
        margin: 0;
        font-size: 1.25rem;
        opacity: 0.9;
      }

      &__features {
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
        gap: 2rem;
      }

      &__card {
        mat-card-header {
          display: flex;
          align-items: center;
          gap: 1rem;
          margin-bottom: 1rem;
        }

        &-icon {
          font-size: 3rem;
          width: 3rem;
          height: 3rem;
        }

        mat-card-content {
          p {
            color: #666;
            margin: 0;
          }
        }

        mat-card-actions {
          padding: 1rem;
        }
      }
    }
  `]
})
export class Home {}
