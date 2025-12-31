import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { PhotoService } from '../services';
import { Photo } from '../models';

@Component({
  selector: 'app-photos',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule,
    MatCheckboxModule
  ],
  template: `
    <div class="photos">
      <div class="photos__header">
        <h1 class="photos__title">Photos</h1>
        <div class="photos__filters">
          <mat-checkbox [(ngModel)]="favoritesOnly" (change)="onFilterChange()" class="photos__filter">
            Favorites Only
          </mat-checkbox>
          <button mat-raised-button color="primary" (click)="uploadPhoto()" class="photos__upload-btn">
            <mat-icon>add_photo_alternate</mat-icon>
            Upload Photo
          </button>
        </div>
      </div>

      <div class="photos__grid">
        @for (photo of photos$ | async; track photo.photoId) {
          <mat-card class="photos__card">
            @if (photo.thumbnailUrl || photo.fileUrl) {
              <img mat-card-image [src]="photo.thumbnailUrl || photo.fileUrl" [alt]="photo.fileName" class="photos__card-image">
            }
            @if (photo.isFavorite) {
              <mat-icon class="photos__card-favorite">favorite</mat-icon>
            }
            <mat-card-content class="photos__card-content">
              <p class="photos__card-caption">{{ photo.caption || photo.fileName }}</p>
              @if (photo.location) {
                <div class="photos__card-location">
                  <mat-icon>location_on</mat-icon>
                  <span>{{ photo.location }}</span>
                </div>
              }
              @if (photo.dateTaken) {
                <p class="photos__card-date">{{ photo.dateTaken | date:'mediumDate' }}</p>
              }
            </mat-card-content>
            <mat-card-actions>
              <button mat-button color="primary">Edit</button>
              <button mat-icon-button [color]="photo.isFavorite ? 'warn' : ''" (click)="toggleFavorite(photo)">
                <mat-icon>{{ photo.isFavorite ? 'favorite' : 'favorite_border' }}</mat-icon>
              </button>
              <button mat-button color="warn" (click)="deletePhoto(photo.photoId)">Delete</button>
            </mat-card-actions>
          </mat-card>
        }
      </div>

      @if ((photos$ | async)?.length === 0) {
        <div class="photos__empty">
          <mat-icon class="photos__empty-icon">photo</mat-icon>
          <p class="photos__empty-text">No photos yet. Upload your first photo!</p>
        </div>
      }
    </div>
  `,
  styles: [`
    .photos {
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
      }

      &__filters {
        display: flex;
        align-items: center;
        gap: 1rem;
      }

      &__upload-btn {
        display: flex;
        align-items: center;
        gap: 0.5rem;
      }

      &__grid {
        display: grid;
        grid-template-columns: repeat(auto-fill, minmax(280px, 1fr));
        gap: 1.5rem;
      }

      &__card {
        position: relative;

        &-image {
          height: 250px;
          object-fit: cover;
        }

        &-favorite {
          position: absolute;
          top: 1rem;
          right: 1rem;
          color: #f44336;
          background: white;
          border-radius: 50%;
          padding: 0.25rem;
        }

        &-content {
          padding: 1rem;
        }

        &-caption {
          margin: 0 0 0.5rem 0;
          font-weight: 500;
        }

        &-location {
          display: flex;
          align-items: center;
          gap: 0.25rem;
          color: #666;
          font-size: 0.875rem;
          margin-bottom: 0.5rem;

          mat-icon {
            font-size: 1rem;
            width: 1rem;
            height: 1rem;
          }
        }

        &-date {
          margin: 0;
          font-size: 0.875rem;
          color: #999;
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
export class Photos implements OnInit {
  photos$ = this.photoService.photos$;
  favoritesOnly = false;

  constructor(private photoService: PhotoService) {}

  ngOnInit(): void {
    this.photoService.loadPhotos();
  }

  onFilterChange(): void {
    this.photoService.loadPhotos(undefined, undefined, this.favoritesOnly);
  }

  uploadPhoto(): void {
    // TODO: Implement upload photo dialog
    console.log('Upload photo dialog');
  }

  toggleFavorite(photo: Photo): void {
    const command = {
      photoId: photo.photoId,
      albumId: photo.albumId,
      caption: photo.caption,
      dateTaken: photo.dateTaken,
      location: photo.location,
      isFavorite: !photo.isFavorite
    };
    this.photoService.updatePhoto(photo.photoId, command).subscribe(() => {
      this.photoService.loadPhotos(undefined, undefined, this.favoritesOnly);
    });
  }

  deletePhoto(id: string): void {
    if (confirm('Are you sure you want to delete this photo?')) {
      this.photoService.deletePhoto(id).subscribe(() => {
        this.photoService.loadPhotos(undefined, undefined, this.favoritesOnly);
      });
    }
  }
}
