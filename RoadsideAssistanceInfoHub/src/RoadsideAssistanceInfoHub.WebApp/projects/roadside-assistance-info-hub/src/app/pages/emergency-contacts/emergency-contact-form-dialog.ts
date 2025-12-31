import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { EmergencyContact } from '../../models';

@Component({
  selector: 'app-emergency-contact-form-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCheckboxModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit Emergency Contact' : 'Add Emergency Contact' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="contact-form">
        <mat-form-field class="contact-form__field">
          <mat-label>Name</mat-label>
          <input matInput formControlName="name" required>
        </mat-form-field>

        <mat-form-field class="contact-form__field">
          <mat-label>Phone Number</mat-label>
          <input matInput formControlName="phoneNumber" required>
        </mat-form-field>

        <mat-form-field class="contact-form__field">
          <mat-label>Relationship</mat-label>
          <input matInput formControlName="relationship">
        </mat-form-field>

        <mat-form-field class="contact-form__field">
          <mat-label>Alternate Phone</mat-label>
          <input matInput formControlName="alternatePhone">
        </mat-form-field>

        <mat-form-field class="contact-form__field contact-form__field--full">
          <mat-label>Email</mat-label>
          <input matInput type="email" formControlName="email">
        </mat-form-field>

        <mat-form-field class="contact-form__field contact-form__field--full">
          <mat-label>Address</mat-label>
          <textarea matInput formControlName="address" rows="2"></textarea>
        </mat-form-field>

        <mat-form-field class="contact-form__field">
          <mat-label>Contact Type</mat-label>
          <input matInput formControlName="contactType">
        </mat-form-field>

        <mat-form-field class="contact-form__field">
          <mat-label>Service Area</mat-label>
          <input matInput formControlName="serviceArea">
        </mat-form-field>

        <mat-form-field class="contact-form__field contact-form__field--full">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="3"></textarea>
        </mat-form-field>

        <div class="contact-form__checkboxes">
          <mat-checkbox formControlName="isPrimaryContact">Primary Contact</mat-checkbox>
          <mat-checkbox formControlName="isActive">Active</mat-checkbox>
        </div>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button (click)="dialogRef.close()">Cancel</button>
      <button mat-raised-button color="primary" (click)="onSubmit()" [disabled]="!form.valid">
        {{ data ? 'Update' : 'Create' }}
      </button>
    </mat-dialog-actions>
  `,
  styles: [`
    .contact-form {
      display: grid;
      grid-template-columns: 1fr 1fr;
      gap: 1rem;
      padding: 1rem 0;

      &__field {
        width: 100%;

        &--full {
          grid-column: 1 / -1;
        }
      }

      &__checkboxes {
        grid-column: 1 / -1;
        display: flex;
        gap: 1rem;
      }
    }
  `]
})
export class EmergencyContactFormDialog {
  private _fb = inject(FormBuilder);
  public dialogRef = inject(MatDialogRef<EmergencyContactFormDialog>);
  public data: EmergencyContact | null = inject(MAT_DIALOG_DATA);

  form: FormGroup;

  constructor() {
    this.form = this._fb.group({
      name: [this.data?.name || '', Validators.required],
      phoneNumber: [this.data?.phoneNumber || '', Validators.required],
      relationship: [this.data?.relationship || ''],
      alternatePhone: [this.data?.alternatePhone || ''],
      email: [this.data?.email || ''],
      address: [this.data?.address || ''],
      isPrimaryContact: [this.data?.isPrimaryContact ?? false],
      contactType: [this.data?.contactType || ''],
      serviceArea: [this.data?.serviceArea || ''],
      notes: [this.data?.notes || ''],
      isActive: [this.data?.isActive ?? true]
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      this.dialogRef.close(this.form.value);
    }
  }
}
