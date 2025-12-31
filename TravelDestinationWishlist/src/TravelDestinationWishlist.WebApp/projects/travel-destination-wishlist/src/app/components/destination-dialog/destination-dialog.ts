import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { Destination, DestinationType } from '../../models';

@Component({
  selector: 'app-destination-dialog',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule
  ],
  templateUrl: './destination-dialog.html',
  styleUrl: './destination-dialog.scss'
})
export class DestinationDialog {
  form: FormGroup;
  destinationTypes = Object.values(DestinationType);

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<DestinationDialog>,
    @Inject(MAT_DIALOG_DATA) public data: { destination?: Destination; userId: string }
  ) {
    this.form = this.fb.group({
      name: [data.destination?.name || '', Validators.required],
      country: [data.destination?.country || '', Validators.required],
      destinationType: [data.destination?.destinationType || DestinationType.City, Validators.required],
      description: [data.destination?.description || ''],
      priority: [data.destination?.priority || 3, [Validators.required, Validators.min(1), Validators.max(5)]],
      notes: [data.destination?.notes || '']
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        userId: this.data.userId,
        ...(this.data.destination && { destinationId: this.data.destination.destinationId })
      };
      this.dialogRef.close(result);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
