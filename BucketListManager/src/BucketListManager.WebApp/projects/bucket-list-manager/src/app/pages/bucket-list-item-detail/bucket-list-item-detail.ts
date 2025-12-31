import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTabsModule } from '@angular/material/tabs';
import { MatListModule } from '@angular/material/list';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { BucketListItemService, MemoryService, MilestoneService } from '../../services';
import { BucketListItem, Memory, Milestone, Category, Priority, ItemStatus } from '../../models';
import { BucketListItemDialog } from '../../components/bucket-list-item-dialog/bucket-list-item-dialog';

@Component({
  selector: 'app-bucket-list-item-detail',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatChipsModule,
    MatProgressSpinnerModule,
    MatTabsModule,
    MatListModule,
    MatSnackBarModule
  ],
  template: `
    <div class="detail" *ngIf="!loading && item">
      <div class="detail__header">
        <button mat-icon-button (click)="goBack()">
          <mat-icon>arrow_back</mat-icon>
        </button>
        <h1 class="detail__title">{{ item.title }}</h1>
        <button mat-raised-button color="primary" (click)="openEditDialog()">
          <mat-icon>edit</mat-icon>
          Edit
        </button>
      </div>

      <mat-card class="detail__card">
        <mat-card-content>
          <div class="detail__section">
            <h3>Details</h3>
            <p class="detail__description">{{ item.description }}</p>

            <div class="detail__chips">
              <mat-chip-set>
                <mat-chip [class]="'detail__chip--' + getCategoryClass()">
                  {{ getCategoryLabel() }}
                </mat-chip>
                <mat-chip [class]="'detail__chip--' + getPriorityClass()">
                  {{ getPriorityLabel() }}
                </mat-chip>
                <mat-chip [class]="'detail__chip--' + getStatusClass()">
                  {{ getStatusLabel() }}
                </mat-chip>
              </mat-chip-set>
            </div>

            <div class="detail__metadata">
              <div class="detail__metadata-item" *ngIf="item.targetDate">
                <mat-icon>event</mat-icon>
                <span>Target Date: {{ item.targetDate | date }}</span>
              </div>
              <div class="detail__metadata-item" *ngIf="item.completedDate">
                <mat-icon>check_circle</mat-icon>
                <span>Completed: {{ item.completedDate | date }}</span>
              </div>
              <div class="detail__metadata-item">
                <mat-icon>calendar_today</mat-icon>
                <span>Created: {{ item.createdAt | date }}</span>
              </div>
            </div>

            <div class="detail__notes" *ngIf="item.notes">
              <h4>Notes</h4>
              <p>{{ item.notes }}</p>
            </div>
          </div>
        </mat-card-content>
      </mat-card>

      <mat-tab-group class="detail__tabs">
        <mat-tab label="Milestones">
          <div class="detail__tab-content">
            <div class="detail__tab-header">
              <h3>Milestones</h3>
              <button mat-raised-button color="primary">
                <mat-icon>add</mat-icon>
                Add Milestone
              </button>
            </div>

            <mat-list *ngIf="milestones.length > 0">
              <mat-list-item *ngFor="let milestone of milestones">
                <mat-icon matListItemIcon [class.detail__milestone--completed]="milestone.isCompleted">
                  {{ milestone.isCompleted ? 'check_circle' : 'radio_button_unchecked' }}
                </mat-icon>
                <div matListItemTitle>{{ milestone.title }}</div>
                <div matListItemLine *ngIf="milestone.description">{{ milestone.description }}</div>
                <div matListItemLine *ngIf="milestone.completedDate">
                  Completed: {{ milestone.completedDate | date }}
                </div>
              </mat-list-item>
            </mat-list>

            <div *ngIf="milestones.length === 0" class="detail__empty">
              <p>No milestones yet. Add your first milestone!</p>
            </div>
          </div>
        </mat-tab>

        <mat-tab label="Memories">
          <div class="detail__tab-content">
            <div class="detail__tab-header">
              <h3>Memories</h3>
              <button mat-raised-button color="primary">
                <mat-icon>add</mat-icon>
                Add Memory
              </button>
            </div>

            <div class="detail__memories" *ngIf="memories.length > 0">
              <mat-card *ngFor="let memory of memories" class="detail__memory-card">
                <mat-card-header>
                  <mat-card-title>{{ memory.title }}</mat-card-title>
                  <mat-card-subtitle>{{ memory.memoryDate | date }}</mat-card-subtitle>
                </mat-card-header>
                <mat-card-content>
                  <p *ngIf="memory.description">{{ memory.description }}</p>
                  <img *ngIf="memory.photoUrl" [src]="memory.photoUrl" class="detail__memory-photo" alt="{{ memory.title }}">
                </mat-card-content>
              </mat-card>
            </div>

            <div *ngIf="memories.length === 0" class="detail__empty">
              <p>No memories yet. Add your first memory!</p>
            </div>
          </div>
        </mat-tab>
      </mat-tab-group>
    </div>

    <div *ngIf="loading" class="detail__loading">
      <mat-spinner></mat-spinner>
    </div>
  `,
  styles: [`
    .detail {
      padding: 24px;
      max-width: 1200px;
      margin: 0 auto;

      &__header {
        display: flex;
        align-items: center;
        gap: 16px;
        margin-bottom: 24px;
      }

      &__title {
        flex: 1;
        margin: 0;
        font-size: 2rem;
        font-weight: 500;
      }

      &__card {
        margin-bottom: 24px;
      }

      &__section {
        padding: 16px 0;
      }

      &__description {
        font-size: 1.1rem;
        margin: 16px 0;
        color: rgba(0, 0, 0, 0.7);
      }

      &__chips {
        margin: 16px 0;
      }

      &__chip {
        &--travel { background-color: #2196F3; color: white; }
        &--adventure { background-color: #FF9800; color: white; }
        &--career { background-color: #4CAF50; color: white; }
        &--learning { background-color: #9C27B0; color: white; }
        &--health { background-color: #F44336; color: white; }
        &--relationships { background-color: #E91E63; color: white; }
        &--creative { background-color: #00BCD4; color: white; }
        &--other { background-color: #607D8B; color: white; }

        &--low { background-color: #8BC34A; color: white; }
        &--medium { background-color: #FFC107; color: white; }
        &--high { background-color: #FF5722; color: white; }
        &--critical { background-color: #D32F2F; color: white; }

        &--not-started { background-color: #9E9E9E; color: white; }
        &--in-progress { background-color: #2196F3; color: white; }
        &--completed { background-color: #4CAF50; color: white; }
        &--on-hold { background-color: #FF9800; color: white; }
        &--cancelled { background-color: #F44336; color: white; }
      }

      &__metadata {
        margin: 24px 0;
      }

      &__metadata-item {
        display: flex;
        align-items: center;
        gap: 8px;
        margin: 8px 0;
        color: rgba(0, 0, 0, 0.6);

        mat-icon {
          font-size: 20px;
          width: 20px;
          height: 20px;
        }
      }

      &__notes {
        margin-top: 24px;
        padding: 16px;
        background-color: #f5f5f5;
        border-radius: 4px;

        h4 {
          margin: 0 0 8px 0;
        }

        p {
          margin: 0;
          white-space: pre-wrap;
        }
      }

      &__tabs {
        margin-top: 24px;
      }

      &__tab-content {
        padding: 24px;
      }

      &__tab-header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 16px;

        h3 {
          margin: 0;
        }
      }

      &__milestone--completed {
        color: #4CAF50;
      }

      &__empty {
        text-align: center;
        padding: 48px 24px;
        color: rgba(0, 0, 0, 0.6);
      }

      &__memories {
        display: grid;
        grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
        gap: 16px;
      }

      &__memory-card {
        height: 100%;
      }

      &__memory-photo {
        width: 100%;
        max-height: 200px;
        object-fit: cover;
        border-radius: 4px;
        margin-top: 8px;
      }

      &__loading {
        display: flex;
        justify-content: center;
        align-items: center;
        min-height: 400px;
      }
    }
  `]
})
export class BucketListItemDetail implements OnInit {
  item: BucketListItem | null = null;
  memories: Memory[] = [];
  milestones: Milestone[] = [];
  loading = true;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private bucketListItemService: BucketListItemService,
    private memoryService: MemoryService,
    private milestoneService: MilestoneService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadItem(id);
      this.loadMemories(id);
      this.loadMilestones(id);
    }
  }

  loadItem(id: string): void {
    this.bucketListItemService.getBucketListItemById(id).subscribe({
      next: (item) => {
        this.item = item;
        this.loading = false;
      },
      error: (error) => {
        this.loading = false;
        this.snackBar.open('Error loading bucket list item', 'Close', { duration: 3000 });
        console.error('Error loading item:', error);
        this.router.navigate(['/']);
      }
    });
  }

  loadMemories(bucketListItemId: string): void {
    this.memoryService.getMemories(undefined, bucketListItemId).subscribe({
      next: (memories) => {
        this.memories = memories;
      },
      error: (error) => {
        console.error('Error loading memories:', error);
      }
    });
  }

  loadMilestones(bucketListItemId: string): void {
    this.milestoneService.getMilestones(undefined, bucketListItemId).subscribe({
      next: (milestones) => {
        this.milestones = milestones;
      },
      error: (error) => {
        console.error('Error loading milestones:', error);
      }
    });
  }

  openEditDialog(): void {
    if (!this.item) return;

    const dialogRef = this.dialog.open(BucketListItemDialog, {
      width: '500px',
      data: { item: this.item, userId: this.item.userId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result && this.item) {
        this.bucketListItemService.updateBucketListItem(this.item.bucketListItemId, result).subscribe({
          next: () => {
            this.snackBar.open('Bucket list item updated successfully', 'Close', { duration: 3000 });
          },
          error: (error) => {
            this.snackBar.open('Error updating bucket list item', 'Close', { duration: 3000 });
            console.error('Error updating item:', error);
          }
        });
      }
    });
  }

  goBack(): void {
    this.router.navigate(['/']);
  }

  getCategoryLabel(): string {
    return this.item ? Category[this.item.category] : '';
  }

  getCategoryClass(): string {
    return this.item ? Category[this.item.category].toLowerCase() : '';
  }

  getPriorityLabel(): string {
    return this.item ? Priority[this.item.priority] : '';
  }

  getPriorityClass(): string {
    return this.item ? Priority[this.item.priority].toLowerCase() : '';
  }

  getStatusLabel(): string {
    if (!this.item) return '';
    const status = ItemStatus[this.item.status];
    return status.replace(/([A-Z])/g, ' $1').trim();
  }

  getStatusClass(): string {
    return this.item ? ItemStatus[this.item.status].replace(/([A-Z])/g, '-$1').toLowerCase().substring(1) : '';
  }
}
