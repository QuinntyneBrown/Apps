import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { SourceService } from '../services';
import { Source, CreateSource, UpdateSource } from '../models';

@Component({
  selector: 'app-source-dialog',
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
    <h2 mat-dialog-title>{{ data ? 'Edit Source' : 'New Source' }}</h2>
    <form [formGroup]="form" (ngSubmit)="onSubmit()">
      <mat-dialog-content class="source-dialog">
        <mat-form-field class="source-dialog__field">
          <mat-label>Title</mat-label>
          <input matInput formControlName="title" required>
        </mat-form-field>

        <mat-form-field class="source-dialog__field">
          <mat-label>Author</mat-label>
          <input matInput formControlName="author">
        </mat-form-field>

        <mat-form-field class="source-dialog__field">
          <mat-label>Source Type</mat-label>
          <input matInput formControlName="sourceType" required placeholder="e.g., Book, Article, Video, Podcast">
        </mat-form-field>

        <mat-form-field class="source-dialog__field">
          <mat-label>URL</mat-label>
          <input matInput formControlName="url" type="url">
        </mat-form-field>

        <mat-form-field class="source-dialog__field">
          <mat-label>Date Consumed</mat-label>
          <input matInput type="date" formControlName="dateConsumed">
        </mat-form-field>

        <mat-form-field class="source-dialog__field">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="4"></textarea>
        </mat-form-field>
      </mat-dialog-content>
      <mat-dialog-actions align="end">
        <button mat-button type="button" mat-dialog-close>Cancel</button>
        <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid">Save</button>
      </mat-dialog-actions>
    </form>
  `,
  styles: [`
    .source-dialog {
      display: flex;
      flex-direction: column;
      min-width: 400px;
      padding: 1rem 0;

      &__field {
        width: 100%;
        margin-bottom: 1rem;
      }
    }
  `]
})
export class SourceDialog {
  private fb = inject(FormBuilder);
  public dialog = inject(MatDialog);

  data?: Source;
  form: FormGroup;

  constructor() {
    this.form = this.fb.group({
      title: ['', Validators.required],
      author: [''],
      sourceType: ['', Validators.required],
      url: [''],
      dateConsumed: [''],
      notes: ['']
    });

    if (this.data) {
      this.form.patchValue({
        ...this.data,
        dateConsumed: this.data.dateConsumed ? this.data.dateConsumed.split('T')[0] : ''
      });
    }
  }

  onSubmit(): void {
    if (this.form.valid) {
      this.dialog.getDialogById('source-dialog')?.close(this.form.value);
    }
  }
}

@Component({
  selector: 'app-sources',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule
  ],
  templateUrl: './sources.html',
  styleUrl: './sources.scss'
})
export class Sources implements OnInit {
  private sourceService = inject(SourceService);
  private dialog = inject(MatDialog);

  sources$ = this.sourceService.sources$;
  loading$ = this.sourceService.loading$;
  displayedColumns = ['title', 'author', 'sourceType', 'dateConsumed', 'actions'];

  ngOnInit(): void {
    this.sourceService.getSources().subscribe();
  }

  openDialog(source?: Source): void {
    const dialogRef = this.dialog.open(SourceDialog, {
      id: 'source-dialog',
      width: '600px',
      data: source
    });

    if (source) {
      dialogRef.componentInstance.data = source;
    }

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (source) {
          const updateData: UpdateSource = {
            sourceId: source.sourceId,
            ...result
          };
          this.sourceService.updateSource(updateData).subscribe();
        } else {
          const createData: CreateSource = {
            userId: '00000000-0000-0000-0000-000000000000',
            ...result
          };
          this.sourceService.createSource(createData).subscribe();
        }
      }
    });
  }

  deleteSource(source: Source): void {
    if (confirm(`Are you sure you want to delete "${source.title}"?`)) {
      this.sourceService.deleteSource(source.sourceId).subscribe();
    }
  }
}
