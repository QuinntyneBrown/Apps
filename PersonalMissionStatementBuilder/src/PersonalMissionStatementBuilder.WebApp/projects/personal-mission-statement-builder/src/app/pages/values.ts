import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ValueService, MissionStatementService } from '../services';
import { Value, CreateValue, UpdateValue } from '../models';

@Component({
  selector: 'app-value-dialog',
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
    <h2 mat-dialog-title>{{ data ? 'Edit' : 'Add' }} Value</h2>
    <form [formGroup]="form" (ngSubmit)="onSubmit()">
      <mat-dialog-content>
        <mat-form-field class="dialog__field">
          <mat-label>Name</mat-label>
          <input matInput formControlName="name" required>
        </mat-form-field>

        <mat-form-field class="dialog__field">
          <mat-label>Description</mat-label>
          <textarea matInput formControlName="description" rows="4"></textarea>
        </mat-form-field>

        <mat-form-field class="dialog__field">
          <mat-label>Priority</mat-label>
          <input matInput type="number" formControlName="priority" required>
        </mat-form-field>
      </mat-dialog-content>
      <mat-dialog-actions align="end">
        <button mat-button type="button" mat-dialog-close>Cancel</button>
        <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid">
          {{ data ? 'Update' : 'Create' }}
        </button>
      </mat-dialog-actions>
    </form>
  `,
  styles: [`
    .dialog__field {
      width: 100%;
      margin-bottom: 1rem;
    }
  `]
})
export class ValueDialog {
  private fb = inject(FormBuilder);
  data: Value | null = inject(MatDialog).openDialogs[0]?.componentInstance?.data || null;

  form = this.fb.group({
    name: [this.data?.name || '', Validators.required],
    description: [this.data?.description || ''],
    priority: [this.data?.priority || 1, [Validators.required, Validators.min(1)]]
  });

  onSubmit() {
    if (this.form.valid) {
      inject(MatDialog).closeAll();
    }
  }
}

@Component({
  selector: 'app-values',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule
  ],
  templateUrl: './values.html',
  styleUrl: './values.scss'
})
export class Values implements OnInit {
  private valueService = inject(ValueService);
  private dialog = inject(MatDialog);

  values$ = this.valueService.values$;
  displayedColumns = ['name', 'description', 'priority', 'createdAt', 'actions'];

  ngOnInit() {
    this.valueService.getAll().subscribe();
  }

  openDialog(value?: Value) {
    const dialogRef = this.dialog.open(ValueDialog, {
      width: '500px',
      data: value
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        if (value) {
          this.updateValue(value.valueId, result);
        } else {
          this.createValue(result);
        }
      }
    });
  }

  createValue(data: any) {
    const create: CreateValue = {
      userId: '00000000-0000-0000-0000-000000000000', // Replace with actual user ID
      name: data.name,
      description: data.description,
      priority: data.priority
    };
    this.valueService.create(create).subscribe();
  }

  updateValue(id: string, data: any) {
    const update: UpdateValue = {
      valueId: id,
      name: data.name,
      description: data.description,
      priority: data.priority
    };
    this.valueService.update(update).subscribe();
  }

  deleteValue(id: string) {
    if (confirm('Are you sure you want to delete this value?')) {
      this.valueService.delete(id).subscribe();
    }
  }
}
