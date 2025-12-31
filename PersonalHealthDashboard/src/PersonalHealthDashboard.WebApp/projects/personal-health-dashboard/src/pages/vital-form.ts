import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { VitalService } from '../services';
import { VitalType, VitalTypeLabels, CreateVital, UpdateVital } from '../models';

@Component({
  selector: 'app-vital-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatIconModule
  ],
  template: `
    <div class="vital-form">
      <mat-card class="vital-form__card">
        <mat-card-header class="vital-form__header">
          <mat-card-title class="vital-form__title">
            <mat-icon class="vital-form__title-icon">favorite</mat-icon>
            <span>{{ isEditMode ? 'Edit Vital' : 'Add New Vital' }}</span>
          </mat-card-title>
        </mat-card-header>

        <mat-card-content>
          <form [formGroup]="vitalForm" (ngSubmit)="onSubmit()" class="vital-form__form">
            <mat-form-field appearance="outline" class="vital-form__field">
              <mat-label>Vital Type</mat-label>
              <mat-select formControlName="vitalType" required>
                <mat-option *ngFor="let type of vitalTypes" [value]="type.value">
                  {{ type.label }}
                </mat-option>
              </mat-select>
              <mat-error *ngIf="vitalForm.get('vitalType')?.hasError('required')">
                Vital type is required
              </mat-error>
            </mat-form-field>

            <div class="vital-form__row">
              <mat-form-field appearance="outline" class="vital-form__field">
                <mat-label>Value</mat-label>
                <input matInput type="number" formControlName="value" required step="0.01">
                <mat-error *ngIf="vitalForm.get('value')?.hasError('required')">
                  Value is required
                </mat-error>
              </mat-form-field>

              <mat-form-field appearance="outline" class="vital-form__field">
                <mat-label>Unit</mat-label>
                <input matInput formControlName="unit" required>
                <mat-error *ngIf="vitalForm.get('unit')?.hasError('required')">
                  Unit is required
                </mat-error>
              </mat-form-field>
            </div>

            <mat-form-field appearance="outline" class="vital-form__field">
              <mat-label>Measured At</mat-label>
              <input matInput [matDatepicker]="picker" formControlName="measuredAt" required>
              <mat-datepicker-toggle matIconSuffix [for]="picker"></mat-datepicker-toggle>
              <mat-datepicker #picker></mat-datepicker>
              <mat-error *ngIf="vitalForm.get('measuredAt')?.hasError('required')">
                Measured date is required
              </mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="vital-form__field">
              <mat-label>Source (Optional)</mat-label>
              <input matInput formControlName="source" placeholder="e.g., Home monitor, Doctor's office">
            </mat-form-field>

            <mat-form-field appearance="outline" class="vital-form__field">
              <mat-label>Notes (Optional)</mat-label>
              <textarea matInput formControlName="notes" rows="4" placeholder="Any additional notes..."></textarea>
            </mat-form-field>

            <div class="vital-form__actions">
              <button mat-button type="button" (click)="onCancel()">Cancel</button>
              <button mat-raised-button color="primary" type="submit" [disabled]="!vitalForm.valid || isSubmitting">
                {{ isEditMode ? 'Update' : 'Create' }}
              </button>
            </div>
          </form>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .vital-form {
      padding: 2rem;
      max-width: 800px;
      margin: 0 auto;
    }

    .vital-form__card {
      width: 100%;
    }

    .vital-form__header {
      margin-bottom: 1.5rem;
    }

    .vital-form__title {
      display: flex;
      align-items: center;
      gap: 0.5rem;
      font-size: 1.5rem;
      margin: 0;
    }

    .vital-form__title-icon {
      font-size: 1.75rem;
      width: 1.75rem;
      height: 1.75rem;
    }

    .vital-form__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
    }

    .vital-form__field {
      width: 100%;
    }

    .vital-form__row {
      display: grid;
      grid-template-columns: 1fr 1fr;
      gap: 1rem;
    }

    .vital-form__actions {
      display: flex;
      justify-content: flex-end;
      gap: 1rem;
      margin-top: 1rem;
    }

    @media (max-width: 768px) {
      .vital-form {
        padding: 1rem;
      }

      .vital-form__row {
        grid-template-columns: 1fr;
      }

      .vital-form__actions {
        flex-direction: column-reverse;
      }

      .vital-form__actions button {
        width: 100%;
      }
    }
  `]
})
export class VitalForm implements OnInit {
  private fb = inject(FormBuilder);
  private vitalService = inject(VitalService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  vitalForm!: FormGroup;
  isEditMode = false;
  isSubmitting = false;
  vitalId?: string;

  vitalTypes = Object.entries(VitalTypeLabels).map(([value, label]) => ({
    value: Number(value),
    label
  }));

  ngOnInit(): void {
    this.initForm();

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditMode = true;
      this.vitalId = id;
      this.loadVital(id);
    }
  }

  private initForm(): void {
    this.vitalForm = this.fb.group({
      vitalType: [VitalType.BloodPressure, Validators.required],
      value: [null, Validators.required],
      unit: ['', Validators.required],
      measuredAt: [new Date(), Validators.required],
      source: [''],
      notes: ['']
    });
  }

  private loadVital(id: string): void {
    this.vitalService.getById(id).subscribe(vital => {
      this.vitalForm.patchValue({
        vitalType: vital.vitalType,
        value: vital.value,
        unit: vital.unit,
        measuredAt: new Date(vital.measuredAt),
        source: vital.source,
        notes: vital.notes
      });
    });
  }

  onSubmit(): void {
    if (this.vitalForm.valid && !this.isSubmitting) {
      this.isSubmitting = true;
      const formValue = this.vitalForm.value;

      const vitalData = {
        vitalType: formValue.vitalType,
        value: formValue.value,
        unit: formValue.unit,
        measuredAt: formValue.measuredAt.toISOString(),
        source: formValue.source || null,
        notes: formValue.notes || null
      };

      if (this.isEditMode && this.vitalId) {
        const updateData: UpdateVital = {
          vitalId: this.vitalId,
          ...vitalData,
          userId: '00000000-0000-0000-0000-000000000000'
        };
        this.vitalService.update(updateData).subscribe({
          next: () => this.router.navigate(['/vitals']),
          error: () => this.isSubmitting = false
        });
      } else {
        const createData: CreateVital = {
          ...vitalData,
          userId: '00000000-0000-0000-0000-000000000000'
        };
        this.vitalService.create(createData).subscribe({
          next: () => this.router.navigate(['/vitals']),
          error: () => this.isSubmitting = false
        });
      }
    }
  }

  onCancel(): void {
    this.router.navigate(['/vitals']);
  }
}
