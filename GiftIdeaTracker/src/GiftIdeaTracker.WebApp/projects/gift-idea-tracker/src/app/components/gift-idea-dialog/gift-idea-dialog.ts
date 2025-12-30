import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { GiftIdea, Recipient, Occasion, CreateGiftIdeaRequest, UpdateGiftIdeaRequest } from '../../models';

export interface GiftIdeaDialogData {
  giftIdea?: GiftIdea;
  recipients: Recipient[];
}

export interface GiftIdeaDialogResult {
  action: 'create' | 'update';
  data: CreateGiftIdeaRequest | UpdateGiftIdeaRequest;
}

@Component({
  selector: 'app-gift-idea-dialog',
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
  templateUrl: './gift-idea-dialog.html',
  styleUrl: './gift-idea-dialog.scss'
})
export class GiftIdeaDialog {
  form: FormGroup;
  isEditMode: boolean;
  occasions = Object.values(Occasion);

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<GiftIdeaDialog>,
    @Inject(MAT_DIALOG_DATA) public data: GiftIdeaDialogData
  ) {
    this.isEditMode = !!data.giftIdea;
    this.form = this.fb.group({
      name: [data.giftIdea?.name || '', Validators.required],
      recipientId: [data.giftIdea?.recipientId || ''],
      occasion: [data.giftIdea?.occasion || Occasion.Other, Validators.required],
      estimatedPrice: [data.giftIdea?.estimatedPrice || null, [Validators.min(0)]]
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;

      if (this.isEditMode && this.data.giftIdea) {
        const updateRequest: UpdateGiftIdeaRequest = {
          giftIdeaId: this.data.giftIdea.giftIdeaId,
          name: formValue.name,
          recipientId: formValue.recipientId || undefined,
          occasion: formValue.occasion,
          estimatedPrice: formValue.estimatedPrice || undefined
        };
        this.dialogRef.close({ action: 'update', data: updateRequest } as GiftIdeaDialogResult);
      } else {
        const createRequest: CreateGiftIdeaRequest = {
          name: formValue.name,
          recipientId: formValue.recipientId || undefined,
          occasion: formValue.occasion,
          estimatedPrice: formValue.estimatedPrice || undefined
        };
        this.dialogRef.close({ action: 'create', data: createRequest } as GiftIdeaDialogResult);
      }
    }
  }
}
