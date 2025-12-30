import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { Recipient, CreateRecipientRequest, UpdateRecipientRequest } from '../../models';

export interface RecipientDialogData {
  recipient?: Recipient;
}

export interface RecipientDialogResult {
  action: 'create' | 'update';
  data: CreateRecipientRequest | UpdateRecipientRequest;
}

@Component({
  selector: 'app-recipient-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './recipient-dialog.html',
  styleUrl: './recipient-dialog.scss'
})
export class RecipientDialog {
  form: FormGroup;
  isEditMode: boolean;

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<RecipientDialog>,
    @Inject(MAT_DIALOG_DATA) public data: RecipientDialogData
  ) {
    this.isEditMode = !!data.recipient;
    this.form = this.fb.group({
      name: [data.recipient?.name || '', Validators.required],
      relationship: [data.recipient?.relationship || '']
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;

      if (this.isEditMode && this.data.recipient) {
        const updateRequest: UpdateRecipientRequest = {
          recipientId: this.data.recipient.recipientId,
          name: formValue.name,
          relationship: formValue.relationship || undefined
        };
        this.dialogRef.close({ action: 'update', data: updateRequest } as RecipientDialogResult);
      } else {
        const createRequest: CreateRecipientRequest = {
          name: formValue.name,
          relationship: formValue.relationship || undefined
        };
        this.dialogRef.close({ action: 'create', data: createRequest } as RecipientDialogResult);
      }
    }
  }
}
