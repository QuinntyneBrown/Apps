import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { TagService } from '../services';
import { Tag, CreateTagCommand } from '../models';

@Component({
  selector: 'app-tags',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule,
    MatFormFieldModule,
    MatInputModule
  ],
  template: `
    <div class="tags">
      <div class="tags__header">
        <h1 class="tags__title">Tags</h1>
      </div>

      <mat-card class="tags__create-card">
        <mat-card-header>
          <mat-card-title>Create New Tag</mat-card-title>
        </mat-card-header>
        <mat-card-content class="tags__create-content">
          <mat-form-field class="tags__create-field">
            <mat-label>Tag Name</mat-label>
            <input matInput [(ngModel)]="newTagName" placeholder="Enter tag name" (keyup.enter)="createTag()">
          </mat-form-field>
          <button mat-raised-button color="primary" (click)="createTag()" [disabled]="!newTagName">
            <mat-icon>add</mat-icon>
            Create Tag
          </button>
        </mat-card-content>
      </mat-card>

      <div class="tags__list">
        @for (tag of tags$ | async; track tag.tagId) {
          <mat-card class="tags__card">
            <mat-card-content class="tags__card-content">
              <div class="tags__card-info">
                <mat-icon class="tags__card-icon">label</mat-icon>
                <div class="tags__card-details">
                  <h3 class="tags__card-name">{{ tag.name }}</h3>
                  <p class="tags__card-count">{{ tag.photoCount }} photos</p>
                </div>
              </div>
              <button mat-icon-button color="warn" (click)="deleteTag(tag.tagId)">
                <mat-icon>delete</mat-icon>
              </button>
            </mat-card-content>
          </mat-card>
        }
      </div>

      @if ((tags$ | async)?.length === 0) {
        <div class="tags__empty">
          <mat-icon class="tags__empty-icon">label</mat-icon>
          <p class="tags__empty-text">No tags yet. Create your first tag!</p>
        </div>
      }
    </div>
  `,
  styles: [`
    .tags {
      padding: 2rem;

      &__header {
        margin-bottom: 2rem;
      }

      &__title {
        margin: 0;
        font-size: 2rem;
      }

      &__create-card {
        margin-bottom: 2rem;
      }

      &__create-content {
        display: flex;
        gap: 1rem;
        align-items: flex-start;
      }

      &__create-field {
        flex: 1;
      }

      &__list {
        display: grid;
        gap: 1rem;
      }

      &__card {
        &-content {
          display: flex;
          justify-content: space-between;
          align-items: center;
          padding: 1rem !important;
        }

        &-info {
          display: flex;
          align-items: center;
          gap: 1rem;
        }

        &-icon {
          color: #673ab7;
          font-size: 2rem;
          width: 2rem;
          height: 2rem;
        }

        &-details {
          display: flex;
          flex-direction: column;
        }

        &-name {
          margin: 0;
          font-size: 1.25rem;
          font-weight: 500;
        }

        &-count {
          margin: 0;
          color: #666;
          font-size: 0.875rem;
        }
      }

      &__empty {
        text-align: center;
        padding: 4rem;
        color: #999;

        &-icon {
          font-size: 96px;
          width: 96px;
          height: 96px;
          margin-bottom: 1rem;
        }

        &-text {
          font-size: 1.25rem;
        }
      }
    }
  `]
})
export class Tags implements OnInit {
  tags$ = this.tagService.tags$;
  newTagName = '';

  constructor(private tagService: TagService) {}

  ngOnInit(): void {
    this.tagService.loadTags();
  }

  createTag(): void {
    if (!this.newTagName.trim()) return;

    const command: CreateTagCommand = {
      userId: '00000000-0000-0000-0000-000000000000', // TODO: Get from auth service
      name: this.newTagName.trim()
    };

    this.tagService.createTag(command).subscribe(() => {
      this.newTagName = '';
      this.tagService.loadTags();
    });
  }

  deleteTag(id: string): void {
    if (confirm('Are you sure you want to delete this tag?')) {
      this.tagService.deleteTag(id).subscribe(() => {
        this.tagService.loadTags();
      });
    }
  }
}
