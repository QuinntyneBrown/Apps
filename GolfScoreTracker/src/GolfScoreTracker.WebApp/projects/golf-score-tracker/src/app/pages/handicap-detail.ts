import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { HandicapsService } from '../services';

@Component({
  selector: 'app-handicap-detail',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  template: `
    <div class="handicap-detail">
      <div class="handicap-detail__header">
        <button mat-button routerLink="/handicaps">
          <mat-icon>arrow_back</mat-icon>
          Back to Handicaps
        </button>
      </div>

      <mat-card *ngIf="(handicap$ | async) as handicap" class="handicap-detail__card">
        <mat-card-header>
          <mat-card-title>Handicap Details</mat-card-title>
          <mat-card-subtitle>Index: {{ handicap.handicapIndex | number:'1.1-1' }}</mat-card-subtitle>
        </mat-card-header>

        <mat-card-content>
          <div class="handicap-detail__info">
            <div class="handicap-detail__info-item">
              <strong>Handicap Index:</strong> {{ handicap.handicapIndex | number:'1.1-1' }}
            </div>
            <div class="handicap-detail__info-item">
              <strong>Calculated Date:</strong> {{ handicap.calculatedDate | date:'fullDate' }}
            </div>
            <div class="handicap-detail__info-item">
              <strong>Rounds Used:</strong> {{ handicap.roundsUsed }}
            </div>
            <div class="handicap-detail__info-item" *ngIf="handicap.notes">
              <strong>Notes:</strong> {{ handicap.notes }}
            </div>
            <div class="handicap-detail__info-item">
              <strong>Created:</strong> {{ handicap.createdAt | date:'medium' }}
            </div>
          </div>
        </mat-card-content>

        <mat-card-actions>
          <a mat-raised-button color="primary" [routerLink]="['/handicaps', handicap.handicapId, 'edit']">
            <mat-icon>edit</mat-icon>
            Edit
          </a>
          <button mat-raised-button color="warn" (click)="deleteHandicap(handicap.handicapId)">
            <mat-icon>delete</mat-icon>
            Delete
          </button>
        </mat-card-actions>
      </mat-card>
    </div>
  `,
  styles: [`
    .handicap-detail {
      padding: 2rem;
      max-width: 800px;
      margin: 0 auto;
    }

    .handicap-detail__header {
      margin-bottom: 1rem;
    }

    .handicap-detail__card {
      margin-top: 1rem;
    }

    .handicap-detail__info {
      display: grid;
      gap: 1rem;
      margin-top: 1rem;
    }

    .handicap-detail__info-item {
      padding: 0.5rem 0;
      border-bottom: 1px solid #eee;
    }

    .handicap-detail__info-item:last-child {
      border-bottom: none;
    }
  `]
})
export class HandicapDetail implements OnInit {
  handicap$ = this.handicapsService.currentHandicap$;
  private handicapId: string = '';

  constructor(
    private handicapsService: HandicapsService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.handicapId = this.route.snapshot.paramMap.get('id')!;
    this.handicapsService.getHandicapById(this.handicapId).subscribe();
  }

  deleteHandicap(id: string): void {
    if (confirm('Are you sure you want to delete this handicap?')) {
      this.handicapsService.deleteHandicap(id).subscribe(() => {
        this.router.navigate(['/handicaps']);
      });
    }
  }
}
