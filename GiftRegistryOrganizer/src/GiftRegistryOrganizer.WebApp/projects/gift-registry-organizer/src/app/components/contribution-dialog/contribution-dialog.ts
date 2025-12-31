import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { RegistryItem } from '../../models';

export interface ContributionDialogData {
  item: RegistryItem;
}

@Component({
  selector: 'app-contribution-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './contribution-dialog.html',
  styleUrl: './contribution-dialog.scss'
})
export class ContributionDialog implements OnInit {
  form!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<ContributionDialog>,
    @Inject(MAT_DIALOG_DATA) public data: ContributionDialogData
  ) {}

  ngOnInit(): void {
    this.initForm();
  }

  initForm(): void {
    const remainingQuantity = this.data.item.quantityDesired - this.data.item.quantityReceived;
    this.form = this.fb.group({
      contributorName: ['', Validators.required],
      contributorEmail: ['', Validators.email],
      quantity: [Math.min(1, remainingQuantity), [Validators.required, Validators.min(1), Validators.max(remainingQuantity)]]
    });
  }

  getRemainingQuantity(): number {
    return this.data.item.quantityDesired - this.data.item.quantityReceived;
  }

  onSubmit(): void {
    if (this.form.valid) {
      const result = {
        ...this.form.value,
        registryItemId: this.data.item.registryItemId
      };
      this.dialogRef.close(result);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
