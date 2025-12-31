import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { BucketListItemService } from '../../services';
import { BucketListItem, Category, ItemStatus } from '../../models';
import { BucketListItemCard } from '../../components/bucket-list-item-card/bucket-list-item-card';
import { BucketListItemDialog } from '../../components/bucket-list-item-dialog/bucket-list-item-dialog';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
    MatSelectModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    BucketListItemCard
  ],
  template: `
    <div class="dashboard">
      <div class="dashboard__header">
        <h1 class="dashboard__title">My Bucket List</h1>
        <button mat-raised-button color="primary" (click)="openCreateDialog()">
          <mat-icon>add</mat-icon>
          Add Item
        </button>
      </div>

      <div class="dashboard__filters">
        <mat-form-field class="dashboard__filter">
          <mat-label>Filter by Category</mat-label>
          <mat-select [formControl]="categoryFilter" (selectionChange)="onFilterChange()">
            <mat-option [value]="null">All Categories</mat-option>
            <mat-option *ngFor="let cat of categories" [value]="cat.value">
              {{ cat.label }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="dashboard__filter">
          <mat-label>Filter by Status</mat-label>
          <mat-select [formControl]="statusFilter" (selectionChange)="onFilterChange()">
            <mat-option [value]="null">All Statuses</mat-option>
            <mat-option *ngFor="let stat of statuses" [value]="stat.value">
              {{ stat.label }}
            </mat-option>
          </mat-select>
        </mat-form-field>
      </div>

      <div class="dashboard__content">
        <mat-spinner *ngIf="loading" class="dashboard__spinner"></mat-spinner>

        <div *ngIf="!loading && (bucketListItems$ | async) as items" class="dashboard__items">
          <div *ngIf="items.length === 0" class="dashboard__empty">
            <mat-icon class="dashboard__empty-icon">list_alt</mat-icon>
            <p>No bucket list items found. Create your first item to get started!</p>
            <button mat-raised-button color="primary" (click)="openCreateDialog()">
              <mat-icon>add</mat-icon>
              Create First Item
            </button>
          </div>

          <app-bucket-list-item-card
            *ngFor="let item of items"
            [item]="item"
            (edit)="openEditDialog($event)"
            (delete)="deleteItem($event)">
          </app-bucket-list-item-card>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .dashboard {
      padding: 24px;
      max-width: 1200px;
      margin: 0 auto;

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 24px;
      }

      &__title {
        margin: 0;
        font-size: 2rem;
        font-weight: 500;
      }

      &__filters {
        display: flex;
        gap: 16px;
        margin-bottom: 24px;
      }

      &__filter {
        min-width: 200px;
      }

      &__content {
        position: relative;
        min-height: 200px;
      }

      &__spinner {
        margin: 48px auto;
      }

      &__items {
        display: flex;
        flex-direction: column;
        gap: 16px;
      }

      &__empty {
        text-align: center;
        padding: 48px 24px;
        color: rgba(0, 0, 0, 0.6);
      }

      &__empty-icon {
        font-size: 64px;
        width: 64px;
        height: 64px;
        margin: 0 auto 16px;
        color: rgba(0, 0, 0, 0.3);
      }
    }
  `]
})
export class Dashboard implements OnInit {
  bucketListItems$ = this.bucketListItemService.bucketListItems$;
  loading = false;
  categoryFilter = new FormControl<Category | null>(null);
  statusFilter = new FormControl<ItemStatus | null>(null);

  categories = [
    { value: Category.Travel, label: 'Travel' },
    { value: Category.Adventure, label: 'Adventure' },
    { value: Category.Career, label: 'Career' },
    { value: Category.Learning, label: 'Learning' },
    { value: Category.Health, label: 'Health' },
    { value: Category.Relationships, label: 'Relationships' },
    { value: Category.Creative, label: 'Creative' },
    { value: Category.Other, label: 'Other' }
  ];

  statuses = [
    { value: ItemStatus.NotStarted, label: 'Not Started' },
    { value: ItemStatus.InProgress, label: 'In Progress' },
    { value: ItemStatus.Completed, label: 'Completed' },
    { value: ItemStatus.OnHold, label: 'On Hold' },
    { value: ItemStatus.Cancelled, label: 'Cancelled' }
  ];

  constructor(
    private bucketListItemService: BucketListItemService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.loadItems();
  }

  loadItems(): void {
    this.loading = true;
    const category = this.categoryFilter.value !== null ? this.categoryFilter.value : undefined;
    const status = this.statusFilter.value !== null ? this.statusFilter.value : undefined;

    this.bucketListItemService.getBucketListItems(undefined, category, status).subscribe({
      next: () => {
        this.loading = false;
      },
      error: (error) => {
        this.loading = false;
        this.snackBar.open('Error loading bucket list items', 'Close', { duration: 3000 });
        console.error('Error loading items:', error);
      }
    });
  }

  onFilterChange(): void {
    this.loadItems();
  }

  openCreateDialog(): void {
    const dialogRef = this.dialog.open(BucketListItemDialog, {
      width: '500px',
      data: { userId: '00000000-0000-0000-0000-000000000001' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.bucketListItemService.createBucketListItem(result).subscribe({
          next: () => {
            this.snackBar.open('Bucket list item created successfully', 'Close', { duration: 3000 });
          },
          error: (error) => {
            this.snackBar.open('Error creating bucket list item', 'Close', { duration: 3000 });
            console.error('Error creating item:', error);
          }
        });
      }
    });
  }

  openEditDialog(item: BucketListItem): void {
    const dialogRef = this.dialog.open(BucketListItemDialog, {
      width: '500px',
      data: { item, userId: item.userId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.bucketListItemService.updateBucketListItem(item.bucketListItemId, result).subscribe({
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

  deleteItem(bucketListItemId: string): void {
    if (confirm('Are you sure you want to delete this bucket list item?')) {
      this.bucketListItemService.deleteBucketListItem(bucketListItemId).subscribe({
        next: () => {
          this.snackBar.open('Bucket list item deleted successfully', 'Close', { duration: 3000 });
        },
        error: (error) => {
          this.snackBar.open('Error deleting bucket list item', 'Close', { duration: 3000 });
          console.error('Error deleting item:', error);
        }
      });
    }
  }
}
