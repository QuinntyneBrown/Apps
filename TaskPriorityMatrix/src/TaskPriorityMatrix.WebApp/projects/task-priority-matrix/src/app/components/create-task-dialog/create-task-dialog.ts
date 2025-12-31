import { Component, Inject, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { PriorityTask, Urgency, Importance, Category } from '../../models';

@Component({
  selector: 'app-create-task-dialog',
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
  templateUrl: './create-task-dialog.html',
  styleUrl: './create-task-dialog.scss'
})
export class CreateTaskDialog implements OnInit {
  private _fb = inject(FormBuilder);
  private _dialogRef = inject(MatDialogRef<CreateTaskDialog>);

  form!: FormGroup;
  Urgency = Urgency;
  Importance = Importance;
  categories: Category[] = [];

  constructor(@Inject(MAT_DIALOG_DATA) public data: { task?: PriorityTask; categories: Category[] }) {
    this.categories = data.categories;
  }

  ngOnInit(): void {
    this.form = this._fb.group({
      title: [this.data.task?.title || '', Validators.required],
      description: [this.data.task?.description || ''],
      urgency: [this.data.task?.urgency ?? Urgency.NotUrgent, Validators.required],
      importance: [this.data.task?.importance ?? Importance.NotImportant, Validators.required],
      dueDate: [this.data.task?.dueDate || null],
      categoryId: [this.data.task?.categoryId || null]
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      this._dialogRef.close(this.form.value);
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}
