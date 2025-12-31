import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule } from '@angular/material/chips';
import { NoteService } from '../services';
import { Note, NoteType, NoteTypeLabels } from '../models';

@Component({
  selector: 'app-note-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatCheckboxModule,
    MatButtonModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit Note' : 'Create Note' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="note-dialog__form">
        <mat-form-field class="note-dialog__field">
          <mat-label>Title</mat-label>
          <input matInput formControlName="title" required>
        </mat-form-field>

        <mat-form-field class="note-dialog__field">
          <mat-label>Content</mat-label>
          <textarea matInput formControlName="content" rows="6" required></textarea>
        </mat-form-field>

        <mat-form-field class="note-dialog__field">
          <mat-label>Note Type</mat-label>
          <mat-select formControlName="noteType" required>
            @for (type of noteTypes; track type.value) {
              <mat-option [value]="type.value">{{ type.label }}</mat-option>
            }
          </mat-select>
        </mat-form-field>

        <mat-form-field class="note-dialog__field">
          <mat-label>Parent Note ID (Optional)</mat-label>
          <input matInput formControlName="parentNoteId">
        </mat-form-field>

        @if (data) {
          <div class="note-dialog__checkboxes">
            <mat-checkbox formControlName="isFavorite">Favorite</mat-checkbox>
            <mat-checkbox formControlName="isArchived">Archived</mat-checkbox>
          </div>
        }
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" (click)="save()" [disabled]="form.invalid">
        {{ data ? 'Update' : 'Create' }}
      </button>
    </mat-dialog-actions>
  `,
  styles: [`
    .note-dialog {
      &__form {
        display: flex;
        flex-direction: column;
        min-width: 500px;
        padding: 1rem 0;
      }

      &__field {
        width: 100%;
        margin-bottom: 1rem;
      }

      &__checkboxes {
        display: flex;
        gap: 1rem;
        margin-top: 1rem;
      }
    }
  `]
})
export class NoteDialog {
  private readonly fb = inject(FormBuilder);
  private readonly dialogRef = inject(MatDialogModule);

  data: Note | null = null;
  form: FormGroup;
  noteTypes = Object.keys(NoteType)
    .filter(key => !isNaN(Number(NoteType[key as keyof typeof NoteType])))
    .map(key => ({
      value: NoteType[key as keyof typeof NoteType] as NoteType,
      label: NoteTypeLabels[NoteType[key as keyof typeof NoteType] as NoteType]
    }));

  constructor() {
    this.form = this.fb.group({
      title: ['', Validators.required],
      content: ['', Validators.required],
      noteType: [NoteType.Text, Validators.required],
      parentNoteId: [null],
      isFavorite: [false],
      isArchived: [false]
    });

    if (this.data) {
      this.form.patchValue(this.data);
    }
  }

  save() {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        parentNoteId: formValue.parentNoteId || null
      };

      if (this.data) {
        (this.dialogRef as any).close({ ...result, noteId: this.data.noteId });
      } else {
        (this.dialogRef as any).close({ ...result, userId: '00000000-0000-0000-0000-000000000000' });
      }
    }
  }
}

@Component({
  selector: 'app-notes',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule
  ],
  template: `
    <div class="notes">
      <div class="notes__header">
        <h1 class="notes__title">Notes</h1>
        <button mat-raised-button color="primary" (click)="openDialog()">
          <mat-icon>add</mat-icon>
          Create Note
        </button>
      </div>

      <div class="notes__table-container">
        <table mat-table [dataSource]="notes$ | async" class="notes__table">
          <ng-container matColumnDef="title">
            <th mat-header-cell *matHeaderCellDef>Title</th>
            <td mat-cell *matCellDef="let note">{{ note.title }}</td>
          </ng-container>

          <ng-container matColumnDef="noteType">
            <th mat-header-cell *matHeaderCellDef>Type</th>
            <td mat-cell *matCellDef="let note">
              <mat-chip>{{ getNoteTypeLabel(note.noteType) }}</mat-chip>
            </td>
          </ng-container>

          <ng-container matColumnDef="isFavorite">
            <th mat-header-cell *matHeaderCellDef>Favorite</th>
            <td mat-cell *matCellDef="let note">
              <mat-icon [class.notes__favorite]="note.isFavorite">
                {{ note.isFavorite ? 'star' : 'star_border' }}
              </mat-icon>
            </td>
          </ng-container>

          <ng-container matColumnDef="isArchived">
            <th mat-header-cell *matHeaderCellDef>Archived</th>
            <td mat-cell *matCellDef="let note">
              <mat-icon>{{ note.isArchived ? 'check' : 'close' }}</mat-icon>
            </td>
          </ng-container>

          <ng-container matColumnDef="createdAt">
            <th mat-header-cell *matHeaderCellDef>Created</th>
            <td mat-cell *matCellDef="let note">{{ note.createdAt | date:'short' }}</td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let note">
              <button mat-icon-button color="primary" (click)="openDialog(note)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="deleteNote(note.noteId)">
                <mat-icon>delete</mat-icon>
              </button>
            </td>
          </ng-container>

          <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
          <tr mat-row *matRowDef="let row; columns: displayedColumns"></tr>
        </table>
      </div>
    </div>
  `,
  styles: [`
    .notes {
      padding: 2rem;

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 2rem;
      }

      &__title {
        margin: 0;
        color: #333;
      }

      &__table-container {
        overflow-x: auto;
      }

      &__table {
        width: 100%;
        background: white;
      }

      &__favorite {
        color: #ffc107;
      }
    }
  `]
})
export class Notes implements OnInit {
  private readonly noteService = inject(NoteService);
  private readonly dialog = inject(MatDialog);

  notes$ = this.noteService.notes$;
  displayedColumns = ['title', 'noteType', 'isFavorite', 'isArchived', 'createdAt', 'actions'];

  ngOnInit() {
    this.noteService.getNotes().subscribe();
  }

  getNoteTypeLabel(type: NoteType): string {
    return NoteTypeLabels[type];
  }

  openDialog(note?: Note) {
    const dialogRef = this.dialog.open(NoteDialog, {
      width: '600px',
      data: note || null
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (note) {
          this.noteService.updateNote(result).subscribe();
        } else {
          this.noteService.createNote(result).subscribe();
        }
      }
    });
  }

  deleteNote(id: string) {
    if (confirm('Are you sure you want to delete this note?')) {
      this.noteService.deleteNote(id).subscribe();
    }
  }
}
