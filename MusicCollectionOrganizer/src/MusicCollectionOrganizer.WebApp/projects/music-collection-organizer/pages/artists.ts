import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ArtistService } from '../services/artist.service';
import { Artist, CreateArtist, UpdateArtist } from '../models';

@Component({
  selector: 'app-artist-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit Artist' : 'Add Artist' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="artist-dialog__form">
        <mat-form-field class="artist-dialog__field">
          <mat-label>Name</mat-label>
          <input matInput formControlName="name" required />
        </mat-form-field>

        <mat-form-field class="artist-dialog__field">
          <mat-label>Biography</mat-label>
          <textarea matInput formControlName="biography" rows="4"></textarea>
        </mat-form-field>

        <mat-form-field class="artist-dialog__field">
          <mat-label>Country</mat-label>
          <input matInput formControlName="country" />
        </mat-form-field>

        <mat-form-field class="artist-dialog__field">
          <mat-label>Formed Year</mat-label>
          <input matInput type="number" formControlName="formedYear" />
        </mat-form-field>

        <mat-form-field class="artist-dialog__field">
          <mat-label>Website</mat-label>
          <input matInput formControlName="website" type="url" />
        </mat-form-field>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" (click)="onSave()" [disabled]="form.invalid">
        Save
      </button>
    </mat-dialog-actions>
  `,
  styles: [`
    .artist-dialog__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
      min-width: 500px;
      padding: 1rem 0;
    }

    .artist-dialog__field {
      width: 100%;
    }
  `]
})
export class ArtistDialog implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly dialogRef = inject(MatDialog);

  data: Artist | null = null;

  form = this.fb.group({
    name: ['', Validators.required],
    biography: [''],
    country: [''],
    formedYear: [null as number | null],
    website: ['']
  });

  ngOnInit(): void {
    if (this.data) {
      this.form.patchValue({
        name: this.data.name,
        biography: this.data.biography || '',
        country: this.data.country || '',
        formedYear: this.data.formedYear,
        website: this.data.website || ''
      });
    }
  }

  onSave(): void {
    if (this.form.valid) {
      this.dialogRef.closeAll();
    }
  }
}

@Component({
  selector: 'app-artists',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule
  ],
  template: `
    <div class="artists">
      <div class="artists__header">
        <h1 class="artists__title">Artists</h1>
        <button mat-raised-button color="primary" (click)="onAdd()" class="artists__add-button">
          <mat-icon>add</mat-icon>
          Add Artist
        </button>
      </div>

      <div class="artists__table-container">
        <table mat-table [dataSource]="artists$ | async" class="artists__table">
          <ng-container matColumnDef="name">
            <th mat-header-cell *matHeaderCellDef>Name</th>
            <td mat-cell *matCellDef="let artist">{{ artist.name }}</td>
          </ng-container>

          <ng-container matColumnDef="country">
            <th mat-header-cell *matHeaderCellDef>Country</th>
            <td mat-cell *matCellDef="let artist">{{ artist.country || 'N/A' }}</td>
          </ng-container>

          <ng-container matColumnDef="formedYear">
            <th mat-header-cell *matHeaderCellDef>Formed Year</th>
            <td mat-cell *matCellDef="let artist">{{ artist.formedYear || 'N/A' }}</td>
          </ng-container>

          <ng-container matColumnDef="website">
            <th mat-header-cell *matHeaderCellDef>Website</th>
            <td mat-cell *matCellDef="let artist">
              <a *ngIf="artist.website" [href]="artist.website" target="_blank" class="artists__website-link">
                {{ artist.website }}
              </a>
              <span *ngIf="!artist.website">N/A</span>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let artist">
              <button mat-icon-button (click)="onEdit(artist)" class="artists__action-button">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="onDelete(artist)" class="artists__action-button">
                <mat-icon>delete</mat-icon>
              </button>
            </td>
          </ng-container>

          <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
          <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
        </table>
      </div>
    </div>
  `,
  styles: [`
    .artists {
      padding: 2rem;
    }

    .artists__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .artists__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 500;
      color: #333;
    }

    .artists__add-button {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .artists__table-container {
      overflow-x: auto;
    }

    .artists__table {
      width: 100%;
      background: white;
      box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    }

    .artists__website-link {
      color: #3f51b5;
      text-decoration: none;
    }

    .artists__website-link:hover {
      text-decoration: underline;
    }

    .artists__action-button {
      margin-right: 0.5rem;
    }
  `]
})
export class Artists implements OnInit {
  private readonly artistService = inject(ArtistService);
  private readonly dialog = inject(MatDialog);

  artists$ = this.artistService.artists$;
  displayedColumns = ['name', 'country', 'formedYear', 'website', 'actions'];

  ngOnInit(): void {
    this.artistService.getArtists().subscribe();
  }

  onAdd(): void {
    const dialogRef = this.dialog.open(ArtistDialog, {
      width: '600px'
    });

    dialogRef.componentInstance.data = null;

    dialogRef.afterClosed().subscribe(() => {
      const form = dialogRef.componentInstance.form;
      if (form.valid && form.dirty) {
        const artist: CreateArtist = {
          userId: '00000000-0000-0000-0000-000000000000', // Default user ID
          ...form.value
        } as CreateArtist;
        this.artistService.createArtist(artist).subscribe();
      }
    });
  }

  onEdit(artist: Artist): void {
    const dialogRef = this.dialog.open(ArtistDialog, {
      width: '600px'
    });

    dialogRef.componentInstance.data = artist;

    dialogRef.afterClosed().subscribe(() => {
      const form = dialogRef.componentInstance.form;
      if (form.valid && form.dirty) {
        const updatedArtist: UpdateArtist = {
          artistId: artist.artistId,
          ...form.value
        } as UpdateArtist;
        this.artistService.updateArtist(updatedArtist).subscribe();
      }
    });
  }

  onDelete(artist: Artist): void {
    if (confirm(`Are you sure you want to delete "${artist.name}"?`)) {
      this.artistService.deleteArtist(artist.artistId).subscribe();
    }
  }
}
