import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { Interaction } from '../../models';

export interface InteractionDialogData {
  interaction?: Interaction;
  userId: string;
  contactId: string;
}

@Component({
  selector: 'app-interaction-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  templateUrl: './interaction-dialog.html',
  styleUrl: './interaction-dialog.scss'
})
export class InteractionDialog implements OnInit {
  form!: FormGroup;
  isEditMode = false;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<InteractionDialog>,
    @Inject(MAT_DIALOG_DATA) public data: InteractionDialogData
  ) {}

  ngOnInit(): void {
    this.isEditMode = !!this.data.interaction;
    this.initForm();
  }

  initForm(): void {
    this.form = this.fb.group({
      interactionType: [this.data.interaction?.interactionType || '', Validators.required],
      interactionDate: [
        this.data.interaction?.interactionDate ? new Date(this.data.interaction.interactionDate) : new Date(),
        Validators.required
      ],
      subject: [this.data.interaction?.subject || ''],
      notes: [this.data.interaction?.notes || ''],
      outcome: [this.data.interaction?.outcome || ''],
      durationMinutes: [this.data.interaction?.durationMinutes || null]
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        interactionDate: formValue.interactionDate.toISOString(),
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
