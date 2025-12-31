import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { BucketListItem, Category, Priority, ItemStatus } from '../../models';

export interface BucketListItemDialogData {
  item?: BucketListItem;
  userId: string;
}

@Component({
  selector: 'app-bucket-list-item-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data.item ? 'Edit' : 'Create' }} Bucket List Item</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="bucket-list-item-dialog__form">
        <mat-form-field class="bucket-list-item-dialog__field">
          <mat-label>Title</mat-label>
          <input matInput formControlName="title" required>
          <mat-error *ngIf="form.get('title')?.hasError('required')">
            Title is required
          </mat-error>
        </mat-form-field>

        <mat-form-field class="bucket-list-item-dialog__field">
          <mat-label>Description</mat-label>
          <textarea matInput formControlName="description" rows="3" required></textarea>
          <mat-error *ngIf="form.get('description')?.hasError('required')">
            Description is required
          </mat-error>
        </mat-form-field>

        <mat-form-field class="bucket-list-item-dialog__field">
          <mat-label>Category</mat-label>
          <mat-select formControlName="category" required>
            <mat-option *ngFor="let cat of categories" [value]="cat.value">
              {{ cat.label }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="bucket-list-item-dialog__field">
          <mat-label>Priority</mat-label>
          <mat-select formControlName="priority" required>
            <mat-option *ngFor="let pri of priorities" [value]="pri.value">
              {{ pri.label }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="bucket-list-item-dialog__field" *ngIf="data.item">
          <mat-label>Status</mat-label>
          <mat-select formControlName="status" required>
            <mat-option *ngFor="let stat of statuses" [value]="stat.value">
              {{ stat.label }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="bucket-list-item-dialog__field">
          <mat-label>Target Date</mat-label>
          <input matInput [matDatepicker]="targetPicker" formControlName="targetDate">
          <mat-datepicker-toggle matSuffix [for]="targetPicker"></mat-datepicker-toggle>
          <mat-datepicker #targetPicker></mat-datepicker>
        </mat-form-field>

        <mat-form-field class="bucket-list-item-dialog__field" *ngIf="data.item">
          <mat-label>Completed Date</mat-label>
          <input matInput [matDatepicker]="completedPicker" formControlName="completedDate">
          <mat-datepicker-toggle matSuffix [for]="completedPicker"></mat-datepicker-toggle>
          <mat-datepicker #completedPicker></mat-datepicker>
        </mat-form-field>

        <mat-form-field class="bucket-list-item-dialog__field">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="3"></textarea>
        </mat-form-field>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button (click)="onCancel()">Cancel</button>
      <button mat-raised-button color="primary" (click)="onSave()" [disabled]="!form.valid">
        {{ data.item ? 'Update' : 'Create' }}
      </button>
    </mat-dialog-actions>
  `,
  styles: [`
    .bucket-list-item-dialog {
      &__form {
        display: flex;
        flex-direction: column;
        min-width: 400px;
      }

      &__field {
        width: 100%;
        margin-bottom: 16px;
      }
    }
  `]
})
export class BucketListItemDialog {
  form: FormGroup;
  categories = [
    { value: Category.Travel, label: 'Travel' },
    { value: Category.Adventure, label: 'Adventure' },
    { value: Category.Career, label: 'Career' },
    { value: Category.Learning, label: 'Learning' },
    { value: Category.Health, label: 'Health' },
    { value: Category.Relationships, label: 'Relationships' },
    { value: Category.Creative, label: 'Creative' },
    { value: Category.Other, label: 'Other' }
  ];
  priorities = [
    { value: Priority.Low, label: 'Low' },
    { value: Priority.Medium, label: 'Medium' },
    { value: Priority.High, label: 'High' },
    { value: Priority.Critical, label: 'Critical' }
  ];
  statuses = [
    { value: ItemStatus.NotStarted, label: 'Not Started' },
    { value: ItemStatus.InProgress, label: 'In Progress' },
    { value: ItemStatus.Completed, label: 'Completed' },
    { value: ItemStatus.OnHold, label: 'On Hold' },
    { value: ItemStatus.Cancelled, label: 'Cancelled' }
  ];

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<BucketListItemDialog>,
    @Inject(MAT_DIALOG_DATA) public data: BucketListItemDialogData
  ) {
    this.form = this.fb.group({
      title: [data.item?.title || '', Validators.required],
      description: [data.item?.description || '', Validators.required],
      category: [data.item?.category ?? Category.Other, Validators.required],
      priority: [data.item?.priority ?? Priority.Medium, Validators.required],
      status: [data.item?.status ?? ItemStatus.NotStarted],
      targetDate: [data.item?.targetDate ? new Date(data.item.targetDate) : null],
      completedDate: [data.item?.completedDate ? new Date(data.item.completedDate) : null],
      notes: [data.item?.notes || '']
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onSave(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        targetDate: formValue.targetDate?.toISOString(),
        completedDate: formValue.completedDate?.toISOString(),
        userId: this.data.userId,
        ...(this.data.item && { bucketListItemId: this.data.item.bucketListItemId })
      };
      this.dialogRef.close(result);
    }
  }
}
