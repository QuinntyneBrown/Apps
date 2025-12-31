import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { TopicService } from '../services';
import { Topic, TopicCategoryLabels } from '../models';

@Component({
  selector: 'app-topics-list',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatChipsModule
  ],
  template: `
    <div class="topics-list">
      <div class="topics-list__header">
        <h1 class="topics-list__title">Topics</h1>
        <button mat-raised-button color="primary" (click)="createTopic()">
          <mat-icon>add</mat-icon>
          New Topic
        </button>
      </div>

      <mat-card class="topics-list__card">
        <table mat-table [dataSource]="topics$ | async" class="topics-list__table">
          <ng-container matColumnDef="title">
            <th mat-header-cell *matHeaderCellDef>Title</th>
            <td mat-cell *matCellDef="let topic">{{ topic.title }}</td>
          </ng-container>

          <ng-container matColumnDef="category">
            <th mat-header-cell *matHeaderCellDef>Category</th>
            <td mat-cell *matCellDef="let topic">
              <mat-chip>{{ getCategoryLabel(topic.category) }}</mat-chip>
            </td>
          </ng-container>

          <ng-container matColumnDef="description">
            <th mat-header-cell *matHeaderCellDef>Description</th>
            <td mat-cell *matCellDef="let topic">{{ topic.description || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="discussionNotes">
            <th mat-header-cell *matHeaderCellDef>Discussion Notes</th>
            <td mat-cell *matCellDef="let topic">
              {{ topic.discussionNotes ? (topic.discussionNotes.length > 50 ? topic.discussionNotes.substring(0, 50) + '...' : topic.discussionNotes) : '-' }}
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let topic">
              <button mat-icon-button color="primary" (click)="editTopic(topic.topicId)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="deleteTopic(topic.topicId)">
                <mat-icon>delete</mat-icon>
              </button>
            </td>
          </ng-container>

          <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
          <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
        </table>
      </mat-card>
    </div>
  `,
  styles: [`
    .topics-list {
      padding: 2rem;

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 2rem;
      }

      &__title {
        margin: 0;
        font-size: 2rem;
        font-weight: 400;
      }

      &__card {
        overflow: auto;
      }

      &__table {
        width: 100%;
      }
    }
  `]
})
export class TopicsList implements OnInit {
  private readonly topicService = inject(TopicService);
  private readonly router = inject(Router);

  topics$ = this.topicService.topics$;
  displayedColumns = ['title', 'category', 'description', 'discussionNotes', 'actions'];

  ngOnInit(): void {
    this.topicService.getAll().subscribe();
  }

  createTopic(): void {
    this.router.navigate(['/topics/new']);
  }

  editTopic(id: string): void {
    this.router.navigate(['/topics', id]);
  }

  deleteTopic(id: string): void {
    if (confirm('Are you sure you want to delete this topic?')) {
      this.topicService.delete(id).subscribe();
    }
  }

  getCategoryLabel(category: number): string {
    return TopicCategoryLabels[category] || 'Unknown';
  }
}
