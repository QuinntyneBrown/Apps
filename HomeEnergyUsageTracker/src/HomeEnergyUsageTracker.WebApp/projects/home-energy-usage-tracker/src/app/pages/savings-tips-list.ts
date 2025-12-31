import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { SavingsTipService } from '../services';

@Component({
  selector: 'app-savings-tips-list',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule],
  template: `
    <div class="savings-tips-list">
      <div class="savings-tips-list__header">
        <h1 class="savings-tips-list__title">Energy Savings Tips</h1>
        <button mat-raised-button color="primary" (click)="navigateToCreate()" class="savings-tips-list__add-btn">
          <mat-icon>add</mat-icon>
          Add Tip
        </button>
      </div>
      <div class="savings-tips-list__grid">
        <mat-card *ngFor="let tip of savingsTips$ | async" class="savings-tips-list__card">
          <mat-card-header>
            <mat-card-title class="savings-tips-list__card-title">{{ tip.title }}</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <p class="savings-tips-list__description">{{ tip.description || 'No description provided.' }}</p>
            <small class="savings-tips-list__date">Created: {{ tip.createdAt | date: 'short' }}</small>
          </mat-card-content>
          <mat-card-actions class="savings-tips-list__card-actions">
            <button mat-button color="primary" (click)="navigateToEdit(tip.savingsTipId)">
              <mat-icon>edit</mat-icon>
              Edit
            </button>
            <button mat-button color="warn" (click)="delete(tip.savingsTipId)">
              <mat-icon>delete</mat-icon>
              Delete
            </button>
          </mat-card-actions>
        </mat-card>
      </div>
    </div>
  `,
  styles: [`
    .savings-tips-list {
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
        font-size: 32px;
        margin: 0;
      }

      &__add-btn {
        display: flex;
        align-items: center;
        gap: 8px;
      }

      &__grid {
        display: grid;
        grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
        gap: 24px;
      }

      &__card {
        display: flex;
        flex-direction: column;
      }

      &__card-title {
        font-size: 18px;
        font-weight: 500;
      }

      &__description {
        margin: 16px 0;
        color: rgba(0, 0, 0, 0.7);
      }

      &__date {
        display: block;
        color: rgba(0, 0, 0, 0.5);
        font-size: 12px;
      }

      &__card-actions {
        margin-top: auto;
        padding-top: 8px;
      }
    }
  `]
})
export class SavingsTipsList implements OnInit {
  savingsTips$ = this.savingsTipService.savingsTips$;

  constructor(
    private savingsTipService: SavingsTipService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.savingsTipService.getAll().subscribe();
  }

  navigateToCreate(): void {
    this.router.navigate(['/savings-tips/create']);
  }

  navigateToEdit(id: string): void {
    this.router.navigate(['/savings-tips/edit', id]);
  }

  delete(id: string): void {
    if (confirm('Are you sure you want to delete this savings tip?')) {
      this.savingsTipService.delete(id).subscribe();
    }
  }
}
