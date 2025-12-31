import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { PhotoService } from '../services';

@Component({
  selector: 'app-photos',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule, MatCardModule],
  template: `
    <div class="photos">
      <div class="photos__header">
        <h1 class="photos__title">Photos</h1>
        <a mat-raised-button color="primary" routerLink="/photos/new" class="photos__add-btn">
          <mat-icon>add</mat-icon>
          New Photo
        </a>
      </div>

      <mat-card class="photos__card">
        <mat-card-content>
          <table mat-table [dataSource]="(photos$ | async) || []" class="photos__table">
            <ng-container matColumnDef="fileName">
              <th mat-header-cell *matHeaderCellDef>File Name</th>
              <td mat-cell *matCellDef="let photo">{{ photo.fileName }}</td>
            </ng-container>

            <ng-container matColumnDef="filePath">
              <th mat-header-cell *matHeaderCellDef>File Path</th>
              <td mat-cell *matCellDef="let photo">{{ photo.filePath || '-' }}</td>
            </ng-container>

            <ng-container matColumnDef="cameraSettings">
              <th mat-header-cell *matHeaderCellDef>Camera Settings</th>
              <td mat-cell *matCellDef="let photo">{{ photo.cameraSettings || '-' }}</td>
            </ng-container>

            <ng-container matColumnDef="rating">
              <th mat-header-cell *matHeaderCellDef>Rating</th>
              <td mat-cell *matCellDef="let photo">
                <span *ngIf="photo.rating">{{ photo.rating }} / 5</span>
                <span *ngIf="!photo.rating">-</span>
              </td>
            </ng-container>

            <ng-container matColumnDef="tags">
              <th mat-header-cell *matHeaderCellDef>Tags</th>
              <td mat-cell *matCellDef="let photo">{{ photo.tags || '-' }}</td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let photo">
                <button mat-icon-button color="primary" [routerLink]="['/photos', photo.photoId]">
                  <mat-icon>edit</mat-icon>
                </button>
                <button mat-icon-button color="warn" (click)="deletePhoto(photo.photoId)">
                  <mat-icon>delete</mat-icon>
                </button>
              </td>
            </ng-container>

            <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
            <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
          </table>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .photos {
      padding: 2rem;
    }

    .photos__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .photos__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 500;
    }

    .photos__add-btn {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .photos__card {
      width: 100%;
    }

    .photos__table {
      width: 100%;
    }
  `]
})
export class Photos implements OnInit {
  private readonly photoService = inject(PhotoService);

  photos$ = this.photoService.photos$;
  displayedColumns = ['fileName', 'filePath', 'cameraSettings', 'rating', 'tags', 'actions'];

  ngOnInit(): void {
    this.photoService.getAll().subscribe();
  }

  deletePhoto(id: string): void {
    if (confirm('Are you sure you want to delete this photo?')) {
      this.photoService.delete(id).subscribe();
    }
  }
}
