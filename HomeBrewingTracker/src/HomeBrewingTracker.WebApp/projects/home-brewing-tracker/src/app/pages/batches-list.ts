import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { BatchService } from '../services';
import { Batch, BatchStatusLabels } from '../models';

@Component({
  selector: 'app-batches-list',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule],
  template: `
    <div class="batches-list">
      <div class="batches-list__header">
        <h1 class="batches-list__title">Batches</h1>
        <button mat-raised-button color="primary" (click)="createBatch()">
          <mat-icon>add</mat-icon>
          New Batch
        </button>
      </div>

      <div class="batches-list__grid" *ngIf="(batchService.batches$ | async) as batches">
        <mat-card class="batches-list__card" *ngFor="let batch of batches" (click)="viewBatch(batch.batchId)">
          <mat-card-header>
            <mat-card-title>Batch #{{ batch.batchNumber }}</mat-card-title>
            <mat-card-subtitle>Brew Date: {{ batch.brewDate | date }}</mat-card-subtitle>
          </mat-card-header>
          <mat-card-content>
            <mat-chip-set>
              <mat-chip [class]="'batches-list__status batches-list__status--' + batch.status">
                {{ getStatusLabel(batch.status) }}
              </mat-chip>
            </mat-chip-set>
            <div class="batches-list__stats">
              <span *ngIf="batch.actualABV"><strong>ABV:</strong> {{ batch.actualABV }}%</span>
              <span *ngIf="batch.actualOriginalGravity"><strong>OG:</strong> {{ batch.actualOriginalGravity }}</span>
              <span *ngIf="batch.actualFinalGravity"><strong>FG:</strong> {{ batch.actualFinalGravity }}</span>
            </div>
            <p class="batches-list__notes" *ngIf="batch.notes">{{ batch.notes }}</p>
          </mat-card-content>
          <mat-card-actions>
            <button mat-button color="primary" (click)="viewBatch(batch.batchId); $event.stopPropagation()">
              <mat-icon>visibility</mat-icon>
              View
            </button>
            <button mat-button color="accent" (click)="editBatch(batch.batchId); $event.stopPropagation()">
              <mat-icon>edit</mat-icon>
              Edit
            </button>
            <button mat-button color="warn" (click)="deleteBatch(batch.batchId); $event.stopPropagation()">
              <mat-icon>delete</mat-icon>
              Delete
            </button>
          </mat-card-actions>
        </mat-card>

        <div *ngIf="batches.length === 0" class="batches-list__empty">
          <mat-icon class="batches-list__empty-icon">science</mat-icon>
          <h2>No batches yet</h2>
          <p>Start tracking your first brewing batch!</p>
          <button mat-raised-button color="primary" (click)="createBatch()">
            <mat-icon>add</mat-icon>
            Create Batch
          </button>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .batches-list {
      padding: 2rem;
      max-width: 1400px;
      margin: 0 auto;

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 2rem;
      }

      &__title {
        font-size: 2rem;
        margin: 0;
      }

      &__grid {
        display: grid;
        grid-template-columns: repeat(auto-fill, minmax(350px, 1fr));
        gap: 1.5rem;
      }

      &__card {
        cursor: pointer;
        transition: transform 0.2s, box-shadow 0.2s;

        &:hover {
          transform: translateY(-4px);
          box-shadow: 0 4px 20px rgba(0, 0, 0, 0.15);
        }
      }

      &__status {
        &--0 { background-color: #9e9e9e !important; } // Planned
        &--1 { background-color: #ff9800 !important; } // Fermenting
        &--2 { background-color: #2196f3 !important; } // Bottled
        &--3 { background-color: #9c27b0 !important; } // Conditioning
        &--4 { background-color: #4caf50 !important; } // Completed
        &--5 { background-color: #f44336 !important; } // Failed
      }

      &__stats {
        display: flex;
        gap: 1rem;
        margin: 1rem 0;
        flex-wrap: wrap;

        span {
          font-size: 0.875rem;
        }
      }

      &__notes {
        margin-top: 1rem;
        color: #666;
        font-style: italic;
        overflow: hidden;
        text-overflow: ellipsis;
        display: -webkit-box;
        -webkit-line-clamp: 2;
        -webkit-box-orient: vertical;
      }

      &__empty {
        grid-column: 1 / -1;
        text-align: center;
        padding: 4rem 2rem;
        color: #666;

        &-icon {
          font-size: 4rem;
          height: 4rem;
          width: 4rem;
          color: #ccc;
        }

        h2 {
          margin: 1rem 0;
        }

        p {
          margin-bottom: 2rem;
        }
      }
    }
  `]
})
export class BatchesList implements OnInit {
  constructor(
    public batchService: BatchService,
    private router: Router
  ) {}

  ngOnInit() {
    this.batchService.getBatches().subscribe();
  }

  getStatusLabel(status: number): string {
    return BatchStatusLabels[status] || 'Unknown';
  }

  createBatch() {
    this.router.navigate(['/batches/new']);
  }

  viewBatch(id: string) {
    this.router.navigate(['/batches', id]);
  }

  editBatch(id: string) {
    this.router.navigate(['/batches', id, 'edit']);
  }

  deleteBatch(id: string) {
    if (confirm('Are you sure you want to delete this batch?')) {
      this.batchService.deleteBatch(id).subscribe();
    }
  }
}
