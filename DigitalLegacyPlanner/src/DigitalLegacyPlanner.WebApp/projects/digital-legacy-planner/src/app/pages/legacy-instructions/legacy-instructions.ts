import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { LegacyInstructionsService } from '../../services';

@Component({
  selector: 'app-legacy-instructions',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule],
  template: `
    <div class="page">
      <div class="page__header">
        <h1 class="page__title">Legacy Instructions</h1>
        <p class="page__subtitle">Create instructions for your loved ones</p>
      </div>
      <mat-card class="page__card">
        <mat-card-content>
          <p>Legacy Instructions management interface coming soon...</p>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .page {
      padding: 2rem;
      max-width: 1400px;
      margin: 0 auto;

      &__header {
        margin-bottom: 2rem;
      }

      &__title {
        font-size: 2rem;
        font-weight: 500;
        margin: 0 0 0.5rem 0;
        color: #333;
      }

      &__subtitle {
        font-size: 1rem;
        color: #666;
        margin: 0;
      }

      &__card {
        text-align: center;
        padding: 2rem;
      }
    }
  `]
})
export class LegacyInstructions implements OnInit {
  instructions$ = this.instructionsService.instructions$;

  constructor(private instructionsService: LegacyInstructionsService) {}

  ngOnInit(): void {
    this.instructionsService.getAll().subscribe();
  }
}
