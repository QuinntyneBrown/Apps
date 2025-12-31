import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AchievementService } from '../services';
import { AchievementTypeLabels } from '../models';

@Component({
  selector: 'app-achievements-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule,
    MatTooltipModule,
    MatProgressSpinnerModule
  ],
  template: `
    <div class="achievements-list">
      <div class="achievements-list__header">
        <div>
          <h1 class="achievements-list__title">Achievements</h1>
          <p class="achievements-list__subtitle">Track your career accomplishments and milestones</p>
        </div>
        <a mat-raised-button color="primary" routerLink="/achievements/new" class="achievements-list__add-btn">
          <mat-icon>add</mat-icon>
          Add Achievement
        </a>
      </div>

      <div class="achievements-list__content">
        @if (loading$ | async) {
          <div class="achievements-list__loading">
            <mat-spinner></mat-spinner>
          </div>
        } @else if ((achievements$ | async)?.length === 0) {
          <div class="achievements-list__empty">
            <mat-icon class="achievements-list__empty-icon">emoji_events</mat-icon>
            <h2>No achievements yet</h2>
            <p>Start tracking your career accomplishments</p>
            <a mat-raised-button color="primary" routerLink="/achievements/new">
              Add Your First Achievement
            </a>
          </div>
        } @else {
          <table mat-table [dataSource]="achievements$ | async" class="achievements-list__table">
            <ng-container matColumnDef="title">
              <th mat-header-cell *matHeaderCellDef>Title</th>
              <td mat-cell *matCellDef="let achievement">
                <div class="achievements-list__cell-title">
                  {{ achievement.title }}
                  @if (achievement.isFeatured) {
                    <mat-icon class="achievements-list__featured-icon" matTooltip="Featured">star</mat-icon>
                  }
                </div>
              </td>
            </ng-container>

            <ng-container matColumnDef="type">
              <th mat-header-cell *matHeaderCellDef>Type</th>
              <td mat-cell *matCellDef="let achievement">
                <mat-chip class="achievements-list__chip">
                  {{ getAchievementTypeLabel(achievement.achievementType) }}
                </mat-chip>
              </td>
            </ng-container>

            <ng-container matColumnDef="achievedDate">
              <th mat-header-cell *matHeaderCellDef>Date</th>
              <td mat-cell *matCellDef="let achievement">
                {{ achievement.achievedDate | date:'MMM d, y' }}
              </td>
            </ng-container>

            <ng-container matColumnDef="organization">
              <th mat-header-cell *matHeaderCellDef>Organization</th>
              <td mat-cell *matCellDef="let achievement">
                {{ achievement.organization || '-' }}
              </td>
            </ng-container>

            <ng-container matColumnDef="tags">
              <th mat-header-cell *matHeaderCellDef>Tags</th>
              <td mat-cell *matCellDef="let achievement">
                @if (achievement.tags?.length > 0) {
                  <div class="achievements-list__tags">
                    @for (tag of achievement.tags.slice(0, 2); track tag) {
                      <mat-chip class="achievements-list__tag">{{ tag }}</mat-chip>
                    }
                    @if (achievement.tags.length > 2) {
                      <span class="achievements-list__tag-more">+{{ achievement.tags.length - 2 }}</span>
                    }
                  </div>
                } @else {
                  <span>-</span>
                }
              </td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let achievement">
                <div class="achievements-list__actions">
                  <button
                    mat-icon-button
                    [matTooltip]="achievement.isFeatured ? 'Unfeature' : 'Feature'"
                    (click)="toggleFeatured(achievement.achievementId)">
                    <mat-icon [class.achievements-list__action-featured]="achievement.isFeatured">
                      {{ achievement.isFeatured ? 'star' : 'star_border' }}
                    </mat-icon>
                  </button>
                  <a
                    mat-icon-button
                    [routerLink]="['/achievements', achievement.achievementId]"
                    matTooltip="Edit">
                    <mat-icon>edit</mat-icon>
                  </a>
                  <button
                    mat-icon-button
                    color="warn"
                    matTooltip="Delete"
                    (click)="deleteAchievement(achievement.achievementId)">
                    <mat-icon>delete</mat-icon>
                  </button>
                </div>
              </td>
            </ng-container>

            <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
            <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
          </table>
        }
      </div>
    </div>
  `,
  styles: [`
    .achievements-list {
      padding: 2rem;
      max-width: 1400px;
      margin: 0 auto;
    }

    .achievements-list__header {
      display: flex;
      justify-content: space-between;
      align-items: flex-start;
      margin-bottom: 2rem;
      gap: 1rem;
    }

    .achievements-list__title {
      font-size: 2rem;
      font-weight: 500;
      margin: 0 0 0.5rem 0;
      color: rgba(0, 0, 0, 0.87);
    }

    .achievements-list__subtitle {
      font-size: 1rem;
      margin: 0;
      color: rgba(0, 0, 0, 0.6);
    }

    .achievements-list__add-btn {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .achievements-list__content {
      background: white;
      border-radius: 4px;
      box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    }

    .achievements-list__loading {
      display: flex;
      justify-content: center;
      align-items: center;
      padding: 4rem;
    }

    .achievements-list__empty {
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      padding: 4rem 2rem;
      text-align: center;
    }

    .achievements-list__empty-icon {
      width: 80px;
      height: 80px;
      font-size: 80px;
      color: rgba(0, 0, 0, 0.26);
      margin-bottom: 1rem;
    }

    .achievements-list__table {
      width: 100%;
    }

    .achievements-list__cell-title {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .achievements-list__featured-icon {
      width: 20px;
      height: 20px;
      font-size: 20px;
      color: #ffd700;
    }

    .achievements-list__chip {
      font-size: 0.75rem;
    }

    .achievements-list__tags {
      display: flex;
      align-items: center;
      gap: 0.25rem;
      flex-wrap: wrap;
    }

    .achievements-list__tag {
      font-size: 0.75rem;
    }

    .achievements-list__tag-more {
      font-size: 0.75rem;
      color: rgba(0, 0, 0, 0.6);
    }

    .achievements-list__actions {
      display: flex;
      gap: 0.25rem;
    }

    .achievements-list__action-featured {
      color: #ffd700;
    }

    @media (max-width: 768px) {
      .achievements-list {
        padding: 1rem;
      }

      .achievements-list__header {
        flex-direction: column;
      }

      .achievements-list__add-btn {
        width: 100%;
      }
    }
  `]
})
export class AchievementsList implements OnInit {
  private achievementService = inject(AchievementService);

  achievements$ = this.achievementService.achievements$;
  loading$ = this.achievementService.loading$;

  displayedColumns = ['title', 'type', 'achievedDate', 'organization', 'tags', 'actions'];

  ngOnInit(): void {
    this.achievementService.getAchievements().subscribe();
  }

  getAchievementTypeLabel(type: number): string {
    return AchievementTypeLabels[type] || 'Unknown';
  }

  toggleFeatured(id: string): void {
    this.achievementService.toggleFeatured(id).subscribe();
  }

  deleteAchievement(id: string): void {
    if (confirm('Are you sure you want to delete this achievement?')) {
      this.achievementService.deleteAchievement(id).subscribe();
    }
  }
}
