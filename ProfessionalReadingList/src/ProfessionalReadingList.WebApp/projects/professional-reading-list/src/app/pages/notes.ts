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
import { NoteService, ResourceService } from '../services';
import { Note, Resource } from '../models';
import { Observable } from 'rxjs';

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
    MatButtonModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit Note' : 'Add Note' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="note-dialog__form">
        <mat-form-field appearance="outline" class="note-dialog__field">
          <mat-label>Resource</mat-label>
          <mat-select formControlName="resourceId" required>
            <mat-option *ngFor="let resource of resources$ | async" [value]="resource.resourceId">
              {{ resource.title }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field appearance="outline" class="note-dialog__field note-dialog__field--full">
          <mat-label>Content</mat-label>
          <textarea matInput formControlName="content" rows="5" required></textarea>
        </mat-form-field>

        <mat-form-field appearance="outline" class="note-dialog__field">
          <mat-label>Page Reference</mat-label>
          <input matInput formControlName="pageReference">
        </mat-form-field>

        <mat-form-field appearance="outline" class="note-dialog__field note-dialog__field--full">
          <mat-label>Quote</mat-label>
          <textarea matInput formControlName="quote" rows="3"></textarea>
        </mat-form-field>

        <mat-form-field appearance="outline" class="note-dialog__field note-dialog__field--full">
          <mat-label>Tags (comma-separated)</mat-label>
          <input matInput formControlName="tagsInput">
        </mat-form-field>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" (click)="save()" [disabled]="!form.valid">
        Save
      </button>
    </mat-dialog-actions>
  `,
  styles: [`
    .note-dialog__form {
      display: grid;
      grid-template-columns: 1fr 1fr;
      gap: 16px;
      min-width: 600px;
      padding: 16px 0;
    }

    .note-dialog__field {
      width: 100%;
    }

    .note-dialog__field--full {
      grid-column: 1 / -1;
    }
  `]
})
export class NoteDialog implements OnInit {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialog);
  private resourceService = inject(ResourceService);

  data: Note | null = null;
  resources$: Observable<Resource[]> = this.resourceService.resources$;

  form = this.fb.group({
    resourceId: ['', Validators.required],
    content: ['', Validators.required],
    pageReference: [''],
    quote: [''],
    tagsInput: ['']
  });

  ngOnInit() {
    if (this.data) {
      this.form.patchValue({
        resourceId: this.data.resourceId,
        content: this.data.content,
        pageReference: this.data.pageReference || '',
        quote: this.data.quote || '',
        tagsInput: this.data.tags.join(', ')
      });
    }
  }

  save() {
    if (this.form.valid) {
      const tagsInput = this.form.value.tagsInput || '';
      const tags = tagsInput.split(',').map(t => t.trim()).filter(t => t);

      const result = {
        ...this.form.value,
        tags
      };
      this.dialogRef.closeAll();
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
    MatIconModule
  ],
  template: `
    <div class="notes">
      <div class="notes__header">
        <h1 class="notes__title">Notes</h1>
        <button mat-raised-button color="primary" (click)="openDialog()">
          <mat-icon>add</mat-icon>
          Add Note
        </button>
      </div>

      <div class="notes__table-container">
        <table mat-table [dataSource]="notes$ | async" class="notes__table">
          <ng-container matColumnDef="content">
            <th mat-header-cell *matHeaderCellDef>Content</th>
            <td mat-cell *matCellDef="let note">{{ note.content | slice:0:100 }}{{ note.content.length > 100 ? '...' : '' }}</td>
          </ng-container>

          <ng-container matColumnDef="pageReference">
            <th mat-header-cell *matHeaderCellDef>Page</th>
            <td mat-cell *matCellDef="let note">{{ note.pageReference || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="tags">
            <th mat-header-cell *matHeaderCellDef>Tags</th>
            <td mat-cell *matCellDef="let note">{{ note.tags.join(', ') || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="createdAt">
            <th mat-header-cell *matHeaderCellDef>Created</th>
            <td mat-cell *matCellDef="let note">{{ note.createdAt | date }}</td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let note">
              <button mat-icon-button (click)="openDialog(note)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="delete(note.noteId)">
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
    .notes {
      padding: 24px;
    }

    .notes__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 24px;
    }

    .notes__title {
      margin: 0;
      font-size: 32px;
      font-weight: 400;
    }

    .notes__table-container {
      overflow-x: auto;
    }

    .notes__table {
      width: 100%;
    }
  `]
})
export class Notes implements OnInit {
  private noteService = inject(NoteService);
  private dialog = inject(MatDialog);

  notes$: Observable<Note[]> = this.noteService.notes$;
  displayedColumns = ['content', 'pageReference', 'tags', 'createdAt', 'actions'];

  ngOnInit() {
    this.noteService.getAll().subscribe();
  }

  openDialog(note?: Note) {
    const dialogRef = this.dialog.open(NoteDialog, {
      width: '700px',
      data: note
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (note) {
          this.noteService.update(note.noteId, {
            noteId: note.noteId,
            ...result
          }).subscribe();
        } else {
          this.noteService.create({
            userId: '00000000-0000-0000-0000-000000000000',
            ...result
          }).subscribe();
        }
      }
    });
  }

  delete(id: string) {
    if (confirm('Are you sure you want to delete this note?')) {
      this.noteService.delete(id).subscribe();
    }
  }
}
