import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { AlbumService } from '../services/album.service';
import { ArtistService } from '../services/artist.service';
import { Album, CreateAlbum, UpdateAlbum, Format, FormatLabels, Genre, GenreLabels } from '../models';

@Component({
  selector: 'app-album-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit Album' : 'Add Album' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="album-dialog__form">
        <mat-form-field class="album-dialog__field">
          <mat-label>Title</mat-label>
          <input matInput formControlName="title" required />
        </mat-form-field>

        <mat-form-field class="album-dialog__field">
          <mat-label>Artist</mat-label>
          <mat-select formControlName="artistId">
            <mat-option [value]="null">None</mat-option>
            <mat-option *ngFor="let artist of artists$ | async" [value]="artist.artistId">
              {{ artist.name }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="album-dialog__field">
          <mat-label>Format</mat-label>
          <mat-select formControlName="format" required>
            <mat-option *ngFor="let format of formats" [value]="format.value">
              {{ format.label }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="album-dialog__field">
          <mat-label>Genre</mat-label>
          <mat-select formControlName="genre" required>
            <mat-option *ngFor="let genre of genres" [value]="genre.value">
              {{ genre.label }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="album-dialog__field">
          <mat-label>Release Year</mat-label>
          <input matInput type="number" formControlName="releaseYear" />
        </mat-form-field>

        <mat-form-field class="album-dialog__field">
          <mat-label>Label</mat-label>
          <input matInput formControlName="label" />
        </mat-form-field>

        <mat-form-field class="album-dialog__field">
          <mat-label>Purchase Price</mat-label>
          <input matInput type="number" formControlName="purchasePrice" />
        </mat-form-field>

        <mat-form-field class="album-dialog__field">
          <mat-label>Purchase Date</mat-label>
          <input matInput [matDatepicker]="picker" formControlName="purchaseDate" />
          <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
          <mat-datepicker #picker></mat-datepicker>
        </mat-form-field>

        <mat-form-field class="album-dialog__field">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="3"></textarea>
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
    .album-dialog__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
      min-width: 500px;
      padding: 1rem 0;
    }

    .album-dialog__field {
      width: 100%;
    }
  `]
})
export class AlbumDialog implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly dialogRef = inject(MatDialog);
  private readonly artistService = inject(ArtistService);

  data: Album | null = null;
  artists$ = this.artistService.artists$;

  formats = Object.keys(Format)
    .filter(key => !isNaN(Number(key)))
    .map(key => ({ value: Number(key), label: FormatLabels[Number(key) as Format] }));

  genres = Object.keys(Genre)
    .filter(key => !isNaN(Number(key)))
    .map(key => ({ value: Number(key), label: GenreLabels[Number(key) as Genre] }));

  form = this.fb.group({
    title: ['', Validators.required],
    artistId: [null as string | null],
    format: [Format.CD, Validators.required],
    genre: [Genre.Rock, Validators.required],
    releaseYear: [null as number | null],
    label: [''],
    purchasePrice: [null as number | null],
    purchaseDate: [null as string | null],
    notes: ['']
  });

  ngOnInit(): void {
    this.artistService.getArtists().subscribe();

    if (this.data) {
      this.form.patchValue({
        title: this.data.title,
        artistId: this.data.artistId,
        format: this.data.format,
        genre: this.data.genre,
        releaseYear: this.data.releaseYear,
        label: this.data.label || '',
        purchasePrice: this.data.purchasePrice,
        purchaseDate: this.data.purchaseDate,
        notes: this.data.notes || ''
      });
    }
  }

  onSave(): void {
    if (this.form.valid) {
      this.dialogRef.closeAll();
      // The parent component will handle the actual save
    }
  }
}

@Component({
  selector: 'app-albums',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule
  ],
  template: `
    <div class="albums">
      <div class="albums__header">
        <h1 class="albums__title">Albums</h1>
        <button mat-raised-button color="primary" (click)="onAdd()" class="albums__add-button">
          <mat-icon>add</mat-icon>
          Add Album
        </button>
      </div>

      <div class="albums__table-container">
        <table mat-table [dataSource]="albums$ | async" class="albums__table">
          <ng-container matColumnDef="title">
            <th mat-header-cell *matHeaderCellDef>Title</th>
            <td mat-cell *matCellDef="let album">{{ album.title }}</td>
          </ng-container>

          <ng-container matColumnDef="artistName">
            <th mat-header-cell *matHeaderCellDef>Artist</th>
            <td mat-cell *matCellDef="let album">{{ album.artistName || 'N/A' }}</td>
          </ng-container>

          <ng-container matColumnDef="format">
            <th mat-header-cell *matHeaderCellDef>Format</th>
            <td mat-cell *matCellDef="let album">{{ getFormatLabel(album.format) }}</td>
          </ng-container>

          <ng-container matColumnDef="genre">
            <th mat-header-cell *matHeaderCellDef>Genre</th>
            <td mat-cell *matCellDef="let album">{{ getGenreLabel(album.genre) }}</td>
          </ng-container>

          <ng-container matColumnDef="releaseYear">
            <th mat-header-cell *matHeaderCellDef>Year</th>
            <td mat-cell *matCellDef="let album">{{ album.releaseYear || 'N/A' }}</td>
          </ng-container>

          <ng-container matColumnDef="purchasePrice">
            <th mat-header-cell *matHeaderCellDef>Price</th>
            <td mat-cell *matCellDef="let album">{{ album.purchasePrice ? '$' + album.purchasePrice : 'N/A' }}</td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let album">
              <button mat-icon-button (click)="onEdit(album)" class="albums__action-button">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="onDelete(album)" class="albums__action-button">
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
    .albums {
      padding: 2rem;
    }

    .albums__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .albums__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 500;
      color: #333;
    }

    .albums__add-button {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .albums__table-container {
      overflow-x: auto;
    }

    .albums__table {
      width: 100%;
      background: white;
      box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    }

    .albums__action-button {
      margin-right: 0.5rem;
    }
  `]
})
export class Albums implements OnInit {
  private readonly albumService = inject(AlbumService);
  private readonly dialog = inject(MatDialog);

  albums$ = this.albumService.albums$;
  displayedColumns = ['title', 'artistName', 'format', 'genre', 'releaseYear', 'purchasePrice', 'actions'];

  ngOnInit(): void {
    this.albumService.getAlbums().subscribe();
  }

  getFormatLabel(format: Format): string {
    return FormatLabels[format];
  }

  getGenreLabel(genre: Genre): string {
    return GenreLabels[genre];
  }

  onAdd(): void {
    const dialogRef = this.dialog.open(AlbumDialog, {
      width: '600px',
      data: null
    });

    dialogRef.componentInstance.data = null;

    dialogRef.afterClosed().subscribe(() => {
      const form = dialogRef.componentInstance.form;
      if (form.valid && form.dirty) {
        const album: CreateAlbum = {
          userId: '00000000-0000-0000-0000-000000000000', // Default user ID
          ...form.value
        } as CreateAlbum;
        this.albumService.createAlbum(album).subscribe();
      }
    });
  }

  onEdit(album: Album): void {
    const dialogRef = this.dialog.open(AlbumDialog, {
      width: '600px'
    });

    dialogRef.componentInstance.data = album;

    dialogRef.afterClosed().subscribe(() => {
      const form = dialogRef.componentInstance.form;
      if (form.valid && form.dirty) {
        const updatedAlbum: UpdateAlbum = {
          albumId: album.albumId,
          ...form.value
        } as UpdateAlbum;
        this.albumService.updateAlbum(updatedAlbum).subscribe();
      }
    });
  }

  onDelete(album: Album): void {
    if (confirm(`Are you sure you want to delete "${album.title}"?`)) {
      this.albumService.deleteAlbum(album.albumId).subscribe();
    }
  }
}
