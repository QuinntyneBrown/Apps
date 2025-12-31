import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { NoteLinkService } from '../services';
import { NoteLink } from '../models';

@Component({
  selector: 'app-note-link-dialog',
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
    <h2 mat-dialog-title>{{ data ? 'Edit Note Link' : 'Create Note Link' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="note-link-dialog__form">
        @if (!data) {
          <mat-form-field class="note-link-dialog__field">
            <mat-label>Source Note ID</mat-label>
            <input matInput formControlName="sourceNoteId" required>
          </mat-form-field>

          <mat-form-field class="note-link-dialog__field">
            <mat-label>Target Note ID</mat-label>
            <input matInput formControlName="targetNoteId" required>
          </mat-form-field>
        }

        <mat-form-field class="note-link-dialog__field">
          <mat-label>Description (Optional)</mat-label>
          <textarea matInput formControlName="description" rows="3"></textarea>
        </mat-form-field>

        <mat-form-field class="note-link-dialog__field">
          <mat-label>Link Type (Optional)</mat-label>
          <input matInput formControlName="linkType">
        </mat-form-field>
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
    .note-link-dialog {
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
    }
  `]
})
export class NoteLinkDialog {
  private readonly fb = inject(FormBuilder);
  private readonly dialogRef = inject(MatDialogModule);

  data: NoteLink | null = null;
  form: FormGroup;

  constructor() {
    this.form = this.fb.group({
      sourceNoteId: ['', Validators.required],
      targetNoteId: ['', Validators.required],
      description: [null],
      linkType: [null]
    });

    if (this.data) {
      this.form.patchValue(this.data);
      this.form.get('sourceNoteId')?.disable();
      this.form.get('targetNoteId')?.disable();
    }
  }

  save() {
    if (this.form.valid) {
      const formValue = this.form.getRawValue();
      const result = {
        ...formValue,
        description: formValue.description || null,
        linkType: formValue.linkType || null
      };

      if (this.data) {
        (this.dialogRef as any).close({
          noteLinkId: this.data.noteLinkId,
          description: result.description,
          linkType: result.linkType
        });
      } else {
        (this.dialogRef as any).close(result);
      }
    }
  }
}

@Component({
  selector: 'app-note-links',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule
  ],
  template: `
    <div class="note-links">
      <div class="note-links__header">
        <h1 class="note-links__title">Note Links</h1>
        <button mat-raised-button color="primary" (click)="openDialog()">
          <mat-icon>add</mat-icon>
          Create Note Link
        </button>
      </div>

      <div class="note-links__table-container">
        <table mat-table [dataSource]="noteLinks$ | async" class="note-links__table">
          <ng-container matColumnDef="sourceNoteId">
            <th mat-header-cell *matHeaderCellDef>Source Note</th>
            <td mat-cell *matCellDef="let link">{{ link.sourceNoteId }}</td>
          </ng-container>

          <ng-container matColumnDef="targetNoteId">
            <th mat-header-cell *matHeaderCellDef>Target Note</th>
            <td mat-cell *matCellDef="let link">{{ link.targetNoteId }}</td>
          </ng-container>

          <ng-container matColumnDef="linkType">
            <th mat-header-cell *matHeaderCellDef>Link Type</th>
            <td mat-cell *matCellDef="let link">{{ link.linkType || 'None' }}</td>
          </ng-container>

          <ng-container matColumnDef="description">
            <th mat-header-cell *matHeaderCellDef>Description</th>
            <td mat-cell *matCellDef="let link">{{ link.description || 'None' }}</td>
          </ng-container>

          <ng-container matColumnDef="createdAt">
            <th mat-header-cell *matHeaderCellDef>Created</th>
            <td mat-cell *matCellDef="let link">{{ link.createdAt | date:'short' }}</td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let link">
              <button mat-icon-button color="primary" (click)="openDialog(link)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="deleteNoteLink(link.noteLinkId)">
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
    .note-links {
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
    }
  `]
})
export class NoteLinks implements OnInit {
  private readonly noteLinkService = inject(NoteLinkService);
  private readonly dialog = inject(MatDialog);

  noteLinks$ = this.noteLinkService.noteLinks$;
  displayedColumns = ['sourceNoteId', 'targetNoteId', 'linkType', 'description', 'createdAt', 'actions'];

  ngOnInit() {
    this.noteLinkService.getNoteLinks().subscribe();
  }

  openDialog(noteLink?: NoteLink) {
    const dialogRef = this.dialog.open(NoteLinkDialog, {
      width: '600px',
      data: noteLink || null
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (noteLink) {
          this.noteLinkService.updateNoteLink(result).subscribe();
        } else {
          this.noteLinkService.createNoteLink(result).subscribe();
        }
      }
    });
  }

  deleteNoteLink(id: string) {
    if (confirm('Are you sure you want to delete this note link?')) {
      this.noteLinkService.deleteNoteLink(id).subscribe();
    }
  }
}
