import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { FollowUp } from '../../models';

export interface FollowUpDialogData {
  followUp?: FollowUp;
  userId: string;
  contactId: string;
}

@Component({
  selector: 'app-follow-up-dialog',
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
  templateUrl: './follow-up-dialog.html',
  styleUrl: './follow-up-dialog.scss'
})
export class FollowUpDialog implements OnInit {
  form!: FormGroup;
  isEditMode = false;
  priorities = ['Low', 'Medium', 'High'];

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<FollowUpDialog>,
    @Inject(MAT_DIALOG_DATA) public data: FollowUpDialogData
  ) {}

  ngOnInit(): void {
    this.isEditMode = !!this.data.followUp;
    this.initForm();
  }

  initForm(): void {
    this.form = this.fb.group({
      description: [this.data.followUp?.description || '', Validators.required],
      dueDate: [
        this.data.followUp?.dueDate ? new Date(this.data.followUp.dueDate) : new Date(),
        Validators.required
      ],
      priority: [this.data.followUp?.priority || 'Medium', Validators.required],
      notes: [this.data.followUp?.notes || '']
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        dueDate: formValue.dueDate.toISOString(),
        userId: this.data.userId,
        contactId: this.data.contactId
      };
      this.dialogRef.close(result);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
