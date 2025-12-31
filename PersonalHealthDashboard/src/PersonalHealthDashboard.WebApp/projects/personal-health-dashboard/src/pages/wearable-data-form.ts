import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { WearableDataService } from '../services';
import { CreateWearableData, UpdateWearableData } from '../models';

@Component({
  selector: 'app-wearable-data-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatIconModule
  ],
  template: `
    <div class="wearable-data-form">
      <mat-card class="wearable-data-form__card">
        <mat-card-header class="wearable-data-form__header">
          <mat-card-title class="wearable-data-form__title">
            <mat-icon class="wearable-data-form__title-icon">watch</mat-icon>
            <span>{{ isEditMode ? 'Edit Wearable Data' : 'Add New Wearable Data' }}</span>
          </mat-card-title>
        </mat-card-header>

        <mat-card-content>
          <form [formGroup]="wearableDataForm" (ngSubmit)="onSubmit()" class="wearable-data-form__form">
            <mat-form-field appearance="outline" class="wearable-data-form__field">
              <mat-label>Device Name</mat-label>
              <input matInput formControlName="deviceName" required placeholder="e.g., Apple Watch, Fitbit">
              <mat-icon matIconSuffix>watch</mat-icon>
              <mat-error *ngIf="wearableDataForm.get('deviceName')?.hasError('required')">
                Device name is required
              </mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="wearable-data-form__field">
              <mat-label>Data Type</mat-label>
              <input matInput formControlName="dataType" required placeholder="e.g., Steps, Heart Rate, Sleep">
              <mat-error *ngIf="wearableDataForm.get('dataType')?.hasError('required')">
                Data type is required
              </mat-error>
            </mat-form-field>

            <div class="wearable-data-form__row">
              <mat-form-field appearance="outline" class="wearable-data-form__field">
                <mat-label>Value</mat-label>
                <input matInput type="number" formControlName="value" required step="0.01">
                <mat-error *ngIf="wearableDataForm.get('value')?.hasError('required')">
                  Value is required
                </mat-error>
              </mat-form-field>

              <mat-form-field appearance="outline" class="wearable-data-form__field">
                <mat-label>Unit</mat-label>
                <input matInput formControlName="unit" required placeholder="e.g., steps, bpm, hours">
                <mat-error *ngIf="wearableDataForm.get('unit')?.hasError('required')">
                  Unit is required
                </mat-error>
              </mat-form-field>
            </div>

            <div class="wearable-data-form__row">
              <mat-form-field appearance="outline" class="wearable-data-form__field">
                <mat-label>Recorded At</mat-label>
                <input matInput [matDatepicker]="recordedPicker" formControlName="recordedAt" required>
                <mat-datepicker-toggle matIconSuffix [for]="recordedPicker"></mat-datepicker-toggle>
                <mat-datepicker #recordedPicker></mat-datepicker>
                <mat-error *ngIf="wearableDataForm.get('recordedAt')?.hasError('required')">
                  Recorded date is required
                </mat-error>
              </mat-form-field>

              <mat-form-field appearance="outline" class="wearable-data-form__field">
                <mat-label>Synced At</mat-label>
                <input matInput [matDatepicker]="syncedPicker" formControlName="syncedAt" required>
                <mat-datepicker-toggle matIconSuffix [for]="syncedPicker"></mat-datepicker-toggle>
                <mat-datepicker #syncedPicker></mat-datepicker>
                <mat-error *ngIf="wearableDataForm.get('syncedAt')?.hasError('required')">
                  Synced date is required
                </mat-error>
              </mat-form-field>
            </div>

            <mat-form-field appearance="outline" class="wearable-data-form__field">
              <mat-label>Metadata (Optional)</mat-label>
              <textarea matInput formControlName="metadata" rows="4" placeholder="Additional metadata in JSON format..."></textarea>
            </mat-form-field>

            <div class="wearable-data-form__actions">
              <button mat-button type="button" (click)="onCancel()">Cancel</button>
              <button mat-raised-button color="primary" type="submit" [disabled]="!wearableDataForm.valid || isSubmitting">
                {{ isEditMode ? 'Update' : 'Create' }}
              </button>
            </div>
          </form>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .wearable-data-form {
      padding: 2rem;
      max-width: 800px;
      margin: 0 auto;
    }

    .wearable-data-form__card {
      width: 100%;
    }

    .wearable-data-form__header {
      margin-bottom: 1.5rem;
    }

    .wearable-data-form__title {
      display: flex;
      align-items: center;
      gap: 0.5rem;
      font-size: 1.5rem;
      margin: 0;
    }

    .wearable-data-form__title-icon {
      font-size: 1.75rem;
      width: 1.75rem;
      height: 1.75rem;
    }

    .wearable-data-form__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
    }

    .wearable-data-form__field {
      width: 100%;
    }

    .wearable-data-form__row {
      display: grid;
      grid-template-columns: 1fr 1fr;
      gap: 1rem;
    }

    .wearable-data-form__actions {
      display: flex;
      justify-content: flex-end;
      gap: 1rem;
      margin-top: 1rem;
    }

    @media (max-width: 768px) {
      .wearable-data-form {
        padding: 1rem;
      }

      .wearable-data-form__row {
        grid-template-columns: 1fr;
      }

      .wearable-data-form__actions {
        flex-direction: column-reverse;
      }

      .wearable-data-form__actions button {
        width: 100%;
      }
    }
  `]
})
export class WearableDataForm implements OnInit {
  private fb = inject(FormBuilder);
  private wearableDataService = inject(WearableDataService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  wearableDataForm!: FormGroup;
  isEditMode = false;
  isSubmitting = false;
  wearableDataId?: string;

  ngOnInit(): void {
    this.initForm();

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditMode = true;
      this.wearableDataId = id;
      this.loadWearableData(id);
    }
  }

  private initForm(): void {
    this.wearableDataForm = this.fb.group({
      deviceName: ['', Validators.required],
      dataType: ['', Validators.required],
      value: [null, Validators.required],
      unit: ['', Validators.required],
      recordedAt: [new Date(), Validators.required],
      syncedAt: [new Date(), Validators.required],
      metadata: ['']
    });
  }

  private loadWearableData(id: string): void {
    this.wearableDataService.getById(id).subscribe(data => {
      this.wearableDataForm.patchValue({
        deviceName: data.deviceName,
        dataType: data.dataType,
        value: data.value,
        unit: data.unit,
        recordedAt: new Date(data.recordedAt),
        syncedAt: new Date(data.syncedAt),
        metadata: data.metadata
      });
    });
  }

  onSubmit(): void {
    if (this.wearableDataForm.valid && !this.isSubmitting) {
      this.isSubmitting = true;
      const formValue = this.wearableDataForm.value;

      const wearableData = {
        deviceName: formValue.deviceName,
        dataType: formValue.dataType,
        value: formValue.value,
        unit: formValue.unit,
        recordedAt: formValue.recordedAt.toISOString(),
        syncedAt: formValue.syncedAt.toISOString(),
        metadata: formValue.metadata || null
      };

      if (this.isEditMode && this.wearableDataId) {
        const updateData: UpdateWearableData = {
          wearableDataId: this.wearableDataId,
          ...wearableData
        };
        this.wearableDataService.update(updateData).subscribe({
          next: () => this.router.navigate(['/wearable-data']),
          error: () => this.isSubmitting = false
        });
      } else {
        const createData: CreateWearableData = {
          ...wearableData,
          userId: '00000000-0000-0000-0000-000000000000'
        };
        this.wearableDataService.create(createData).subscribe({
          next: () => this.router.navigate(['/wearable-data']),
          error: () => this.isSubmitting = false
        });
      }
    }
  }

  onCancel(): void {
    this.router.navigate(['/wearable-data']);
  }
}
