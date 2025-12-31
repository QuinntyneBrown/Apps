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
import { ListeningLogService } from '../services/listening-log.service';
import { AlbumService } from '../services/album.service';
import { ListeningLog, CreateListeningLog, UpdateListeningLog } from '../models';

@Component({
  selector: 'app-listening-log-dialog',
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
    <h2 mat-dialog-title>{{ data ? 'Edit Listening Log' : 'Add Listening Log' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="listening-log-dialog__form">
        <mat-form-field class="listening-log-dialog__field">
          <mat-label>Album</mat-label>
          <mat-select formControlName="albumId" required [disabled]="!!data">
            <mat-option *ngFor="let album of albums$ | async" [value]="album.albumId">
              {{ album.title }} {{ album.artistName ? '- ' + album.artistName : '' }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="listening-log-dialog__field">
          <mat-label>Listening Date</mat-label>
          <input matInput [matDatepicker]="picker" formControlName="listeningDate" required />
          <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
          <mat-datepicker #picker></mat-datepicker>
        </mat-form-field>

        <mat-form-field class="listening-log-dialog__field">
          <mat-label>Rating (1-10)</mat-label>
          <input matInput type="number" formControlName="rating" min="1" max="10" />
        </mat-form-field>

        <mat-form-field class="listening-log-dialog__field">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="4"></textarea>
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
    .listening-log-dialog__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
      min-width: 500px;
      padding: 1rem 0;
    }

    .listening-log-dialog__field {
      width: 100%;
    }
  `]
})
export class ListeningLogDialog implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly dialogRef = inject(MatDialog);
  private readonly albumService = inject(AlbumService);

  data: ListeningLog | null = null;
  albums$ = this.albumService.albums$;

  form = this.fb.group({
    albumId: ['', Validators.required],
    listeningDate: ['', Validators.required],
    rating: [null as number | null, [Validators.min(1), Validators.max(10)]],
    notes: ['']
  });

  ngOnInit(): void {
    this.albumService.getAlbums().subscribe();

    if (this.data) {
      this.form.patchValue({
        albumId: this.data.albumId,
        listeningDate: this.data.listeningDate,
        rating: this.data.rating,
        notes: this.data.notes || ''
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
  selector: 'app-listening-logs',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule
  ],
  template: `
    <div class="listening-logs">
      <div class="listening-logs__header">
        <h1 class="listening-logs__title">Listening Logs</h1>
        <button mat-raised-button color="primary" (click)="onAdd()" class="listening-logs__add-button">
          <mat-icon>add</mat-icon>
          Add Log
        </button>
      </div>

      <div class="listening-logs__table-container">
        <table mat-table [dataSource]="listeningLogs$ | async" class="listening-logs__table">
          <ng-container matColumnDef="albumTitle">
            <th mat-header-cell *matHeaderCellDef>Album</th>
            <td mat-cell *matCellDef="let log">{{ log.albumTitle || 'N/A' }}</td>
          </ng-container>

          <ng-container matColumnDef="listeningDate">
            <th mat-header-cell *matHeaderCellDef>Date</th>
            <td mat-cell *matCellDef="let log">{{ log.listeningDate | date:'mediumDate' }}</td>
          </ng-container>

          <ng-container matColumnDef="rating">
            <th mat-header-cell *matHeaderCellDef>Rating</th>
            <td mat-cell *matCellDef="let log">
              <span *ngIf="log.rating" class="listening-logs__rating">{{ log.rating }}/10</span>
              <span *ngIf="!log.rating">N/A</span>
            </td>
          </ng-container>

          <ng-container matColumnDef="notes">
            <th mat-header-cell *matHeaderCellDef>Notes</th>
            <td mat-cell *matCellDef="let log">
              <span class="listening-logs__notes">{{ log.notes || 'N/A' }}</span>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let log">
              <button mat-icon-button (click)="onEdit(log)" class="listening-logs__action-button">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="onDelete(log)" class="listening-logs__action-button">
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
    .listening-logs {
      padding: 2rem;
    }

    .listening-logs__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .listening-logs__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 500;
      color: #333;
    }

    .listening-logs__add-button {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .listening-logs__table-container {
      overflow-x: auto;
    }

    .listening-logs__table {
      width: 100%;
      background: white;
      box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    }

    .listening-logs__rating {
      color: #3f51b5;
      font-weight: 500;
    }

    .listening-logs__notes {
      display: -webkit-box;
      -webkit-line-clamp: 2;
      -webkit-box-orient: vertical;
      overflow: hidden;
      text-overflow: ellipsis;
      max-width: 300px;
    }

    .listening-logs__action-button {
      margin-right: 0.5rem;
    }
  `]
})
export class ListeningLogs implements OnInit {
  private readonly listeningLogService = inject(ListeningLogService);
  private readonly dialog = inject(MatDialog);

  listeningLogs$ = this.listeningLogService.listeningLogs$;
  displayedColumns = ['albumTitle', 'listeningDate', 'rating', 'notes', 'actions'];

  ngOnInit(): void {
    this.listeningLogService.getListeningLogs().subscribe();
  }

  onAdd(): void {
    const dialogRef = this.dialog.open(ListeningLogDialog, {
      width: '600px'
    });

    dialogRef.componentInstance.data = null;

    dialogRef.afterClosed().subscribe(() => {
      const form = dialogRef.componentInstance.form;
      if (form.valid && form.dirty) {
        const log: CreateListeningLog = {
          userId: '00000000-0000-0000-0000-000000000000', // Default user ID
          ...form.value
        } as CreateListeningLog;
        this.listeningLogService.createListeningLog(log).subscribe();
      }
    });
  }

  onEdit(log: ListeningLog): void {
    const dialogRef = this.dialog.open(ListeningLogDialog, {
      width: '600px'
    });

    dialogRef.componentInstance.data = log;

    dialogRef.afterClosed().subscribe(() => {
      const form = dialogRef.componentInstance.form;
      if (form.valid && form.dirty) {
        const updatedLog: UpdateListeningLog = {
          listeningLogId: log.listeningLogId,
          listeningDate: form.value.listeningDate!,
          rating: form.value.rating,
          notes: form.value.notes || null
        };
        this.listeningLogService.updateListeningLog(updatedLog).subscribe();
      }
    });
  }

  onDelete(log: ListeningLog): void {
    if (confirm(`Are you sure you want to delete this listening log?`)) {
      this.listeningLogService.deleteListeningLog(log.listeningLogId).subscribe();
    }
  }
}
