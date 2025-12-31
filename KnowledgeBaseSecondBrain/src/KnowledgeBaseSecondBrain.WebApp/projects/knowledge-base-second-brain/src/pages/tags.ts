import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { TagService } from '../services';
import { Tag } from '../models';

@Component({
  selector: 'app-tag-dialog',
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
    <h2 mat-dialog-title>{{ data ? 'Edit Tag' : 'Create Tag' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="tag-dialog__form">
        <mat-form-field class="tag-dialog__field">
          <mat-label>Name</mat-label>
          <input matInput formControlName="name" required>
        </mat-form-field>

        <mat-form-field class="tag-dialog__field">
          <mat-label>Color (Optional)</mat-label>
          <input matInput type="color" formControlName="color">
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
    .tag-dialog {
      &__form {
        display: flex;
        flex-direction: column;
        min-width: 400px;
        padding: 1rem 0;
      }

      &__field {
        width: 100%;
        margin-bottom: 1rem;
      }
    }
  `]
})
export class TagDialog {
  private readonly fb = inject(FormBuilder);
  private readonly dialogRef = inject(MatDialogModule);

  data: Tag | null = null;
  form: FormGroup;

  constructor() {
    this.form = this.fb.group({
      name: ['', Validators.required],
      color: [null]
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
        color: formValue.color || null
      };

      if (this.data) {
        (this.dialogRef as any).close({ ...result, tagId: this.data.tagId });
      } else {
        (this.dialogRef as any).close({ ...result, userId: '00000000-0000-0000-0000-000000000000' });
      }
    }
  }
}

@Component({
  selector: 'app-tags',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule
  ],
  template: `
    <div class="tags">
      <div class="tags__header">
        <h1 class="tags__title">Tags</h1>
        <button mat-raised-button color="primary" (click)="openDialog()">
          <mat-icon>add</mat-icon>
          Create Tag
        </button>
      </div>

      <div class="tags__table-container">
        <table mat-table [dataSource]="tags$ | async" class="tags__table">
          <ng-container matColumnDef="name">
            <th mat-header-cell *matHeaderCellDef>Name</th>
            <td mat-cell *matCellDef="let tag">{{ tag.name }}</td>
          </ng-container>

          <ng-container matColumnDef="color">
            <th mat-header-cell *matHeaderCellDef>Color</th>
            <td mat-cell *matCellDef="let tag">
              @if (tag.color) {
                <div class="tags__color-indicator" [style.background-color]="tag.color"></div>
              }
              <span>{{ tag.color || 'None' }}</span>
            </td>
          </ng-container>

          <ng-container matColumnDef="createdAt">
            <th mat-header-cell *matHeaderCellDef>Created</th>
            <td mat-cell *matCellDef="let tag">{{ tag.createdAt | date:'short' }}</td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let tag">
              <button mat-icon-button color="primary" (click)="openDialog(tag)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="deleteTag(tag.tagId)">
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
    .tags {
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

      &__color-indicator {
        display: inline-block;
        width: 20px;
        height: 20px;
        border-radius: 4px;
        margin-right: 0.5rem;
        vertical-align: middle;
        border: 1px solid #ddd;
      }
    }
  `]
})
export class Tags implements OnInit {
  private readonly tagService = inject(TagService);
  private readonly dialog = inject(MatDialog);

  tags$ = this.tagService.tags$;
  displayedColumns = ['name', 'color', 'createdAt', 'actions'];

  ngOnInit() {
    this.tagService.getTags().subscribe();
  }

  openDialog(tag?: Tag) {
    const dialogRef = this.dialog.open(TagDialog, {
      width: '500px',
      data: tag || null
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (tag) {
          this.tagService.updateTag(result).subscribe();
        } else {
          this.tagService.createTag(result).subscribe();
        }
      }
    });
  }

  deleteTag(id: string) {
    if (confirm('Are you sure you want to delete this tag?')) {
      this.tagService.deleteTag(id).subscribe();
    }
  }
}
