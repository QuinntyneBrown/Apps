import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { AlbumService } from '../services';
import { Album } from '../models';

@Component({
  selector: 'app-albums',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule
  ],
  template: `
    <div class="albums">
      <div class="albums__header">
        <h1 class="albums__title">Albums</h1>
        <button mat-raised-button color="primary" (click)="openCreateDialog()" class="albums__add-btn">
          <mat-icon>add</mat-icon>
          Create Album
        </button>
      </div>

      <div class="albums__grid">
        @for (album of albums$ | async; track album.albumId) {
          <mat-card class="albums__card">
            @if (album.coverPhotoUrl) {
              <img mat-card-image [src]="album.coverPhotoUrl" [alt]="album.name" class="albums__card-image">
            } @else {
              <div class="albums__card-placeholder">
                <mat-icon>photo_library</mat-icon>
              </div>
            }
            <mat-card-header>
              <mat-card-title class="albums__card-title">{{ album.name }}</mat-card-title>
              <mat-card-subtitle class="albums__card-subtitle">{{ album.photoCount }} photos</mat-card-subtitle>
            </mat-card-header>
            <mat-card-content>
              <p class="albums__card-description">{{ album.description || 'No description' }}</p>
              <p class="albums__card-date">Created: {{ album.createdDate | date:'short' }}</p>
            </mat-card-content>
            <mat-card-actions>
              <button mat-button color="primary">View</button>
              <button mat-button color="warn" (click)="deleteAlbum(album.albumId)">Delete</button>
            </mat-card-actions>
          </mat-card>
        }
      </div>

      @if ((albums$ | async)?.length === 0) {
        <div class="albums__empty">
          <mat-icon class="albums__empty-icon">photo_library</mat-icon>
          <p class="albums__empty-text">No albums yet. Create your first album!</p>
        </div>
      }
    </div>
  `,
  styles: [`
    .albums {
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

      &__add-btn {
        display: flex;
        align-items: center;
        gap: 0.5rem;
      }

      &__grid {
        display: grid;
        grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
        gap: 1.5rem;
      }

      &__card {
        &-image {
          height: 200px;
          object-fit: cover;
        }

        &-placeholder {
          height: 200px;
          display: flex;
          align-items: center;
          justify-content: center;
          background-color: #f5f5f5;

          mat-icon {
            font-size: 64px;
            width: 64px;
            height: 64px;
            color: #ccc;
          }
        }

        &-title {
          font-size: 1.25rem;
        }

        &-subtitle {
          color: #666;
        }

        &-description {
          margin: 0.5rem 0;
          color: #666;
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
export class Albums implements OnInit {
  albums$ = this.albumService.albums$;

  constructor(
    private albumService: AlbumService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.albumService.loadAlbums();
  }

  openCreateDialog(): void {
    // TODO: Implement create album dialog
    console.log('Create album dialog');
  }

  deleteAlbum(id: string): void {
    if (confirm('Are you sure you want to delete this album?')) {
      this.albumService.deleteAlbum(id).subscribe(() => {
        this.albumService.loadAlbums();
      });
    }
  }
}
