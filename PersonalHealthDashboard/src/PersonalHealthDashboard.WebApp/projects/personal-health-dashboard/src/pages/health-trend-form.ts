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
import { HealthTrendService } from '../services';
import { CreateHealthTrend, UpdateHealthTrend } from '../models';

@Component({
  selector: 'app-health-trend-form',
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
    <div class="health-trend-form">
      <mat-card class="health-trend-form__card">
        <mat-card-header class="health-trend-form__header">
          <mat-card-title class="health-trend-form__title">
            <mat-icon class="health-trend-form__title-icon">trending_up</mat-icon>
            <span>{{ isEditMode ? 'Edit Health Trend' : 'Add New Health Trend' }}</span>
          </mat-card-title>
        </mat-card-header>

        <mat-card-content>
          <form [formGroup]="healthTrendForm" (ngSubmit)="onSubmit()" class="health-trend-form__form">
            <mat-form-field appearance="outline" class="health-trend-form__field">
              <mat-label>Metric Name</mat-label>
              <input matInput formControlName="metricName" required placeholder="e.g., Blood Pressure, Weight">
              <mat-error *ngIf="healthTrendForm.get('metricName')?.hasError('required')">
                Metric name is required
              </mat-error>
            </mat-form-field>

            <div class="health-trend-form__row">
              <mat-form-field appearance="outline" class="health-trend-form__field">
                <mat-label>Start Date</mat-label>
                <input matInput [matDatepicker]="startPicker" formControlName="startDate" required>
                <mat-datepicker-toggle matIconSuffix [for]="startPicker"></mat-datepicker-toggle>
                <mat-datepicker #startPicker></mat-datepicker>
                <mat-error *ngIf="healthTrendForm.get('startDate')?.hasError('required')">
                  Start date is required
                </mat-error>
              </mat-form-field>

              <mat-form-field appearance="outline" class="health-trend-form__field">
                <mat-label>End Date</mat-label>
                <input matInput [matDatepicker]="endPicker" formControlName="endDate" required>
                <mat-datepicker-toggle matIconSuffix [for]="endPicker"></mat-datepicker-toggle>
                <mat-datepicker #endPicker></mat-datepicker>
                <mat-error *ngIf="healthTrendForm.get('endDate')?.hasError('required')">
                  End date is required
                </mat-error>
              </mat-form-field>
            </div>

            <div class="health-trend-form__row health-trend-form__row--three">
              <mat-form-field appearance="outline" class="health-trend-form__field">
                <mat-label>Average Value</mat-label>
                <input matInput type="number" formControlName="averageValue" required step="0.01">
                <mat-error *ngIf="healthTrendForm.get('averageValue')?.hasError('required')">
                  Average value is required
                </mat-error>
              </mat-form-field>

              <mat-form-field appearance="outline" class="health-trend-form__field">
                <mat-label>Min Value</mat-label>
                <input matInput type="number" formControlName="minValue" required step="0.01">
                <mat-error *ngIf="healthTrendForm.get('minValue')?.hasError('required')">
                  Min value is required
                </mat-error>
              </mat-form-field>

              <mat-form-field appearance="outline" class="health-trend-form__field">
                <mat-label>Max Value</mat-label>
                <input matInput type="number" formControlName="maxValue" required step="0.01">
                <mat-error *ngIf="healthTrendForm.get('maxValue')?.hasError('required')">
                  Max value is required
                </mat-error>
              </mat-form-field>
            </div>

            <div class="health-trend-form__row">
              <mat-form-field appearance="outline" class="health-trend-form__field">
                <mat-label>Trend Direction</mat-label>
                <input matInput formControlName="trendDirection" required placeholder="e.g., Increasing, Decreasing, Stable">
                <mat-error *ngIf="healthTrendForm.get('trendDirection')?.hasError('required')">
                  Trend direction is required
                </mat-error>
              </mat-form-field>

              <mat-form-field appearance="outline" class="health-trend-form__field">
                <mat-label>Percentage Change</mat-label>
                <input matInput type="number" formControlName="percentageChange" required step="0.01">
                <mat-icon matIconSuffix>percent</mat-icon>
                <mat-error *ngIf="healthTrendForm.get('percentageChange')?.hasError('required')">
                  Percentage change is required
                </mat-error>
              </mat-form-field>
            </div>

            <mat-form-field appearance="outline" class="health-trend-form__field">
              <mat-label>Insights (Optional)</mat-label>
              <textarea matInput formControlName="insights" rows="4" placeholder="Any insights or observations..."></textarea>
            </mat-form-field>

            <div class="health-trend-form__actions">
              <button mat-button type="button" (click)="onCancel()">Cancel</button>
              <button mat-raised-button color="primary" type="submit" [disabled]="!healthTrendForm.valid || isSubmitting">
                {{ isEditMode ? 'Update' : 'Create' }}
              </button>
            </div>
          </form>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .health-trend-form {
      padding: 2rem;
      max-width: 900px;
      margin: 0 auto;
    }

    .health-trend-form__card {
      width: 100%;
    }

    .health-trend-form__header {
      margin-bottom: 1.5rem;
    }

    .health-trend-form__title {
      display: flex;
      align-items: center;
      gap: 0.5rem;
      font-size: 1.5rem;
      margin: 0;
    }

    .health-trend-form__title-icon {
      font-size: 1.75rem;
      width: 1.75rem;
      height: 1.75rem;
    }

    .health-trend-form__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
    }

    .health-trend-form__field {
      width: 100%;
    }

    .health-trend-form__row {
      display: grid;
      grid-template-columns: 1fr 1fr;
      gap: 1rem;
    }

    .health-trend-form__row--three {
      grid-template-columns: 1fr 1fr 1fr;
    }

    .health-trend-form__actions {
      display: flex;
      justify-content: flex-end;
      gap: 1rem;
      margin-top: 1rem;
    }

    @media (max-width: 768px) {
      .health-trend-form {
        padding: 1rem;
      }

      .health-trend-form__row,
      .health-trend-form__row--three {
        grid-template-columns: 1fr;
      }

      .health-trend-form__actions {
        flex-direction: column-reverse;
      }

      .health-trend-form__actions button {
        width: 100%;
      }
    }
  `]
})
export class HealthTrendForm implements OnInit {
  private fb = inject(FormBuilder);
  private healthTrendService = inject(HealthTrendService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  healthTrendForm!: FormGroup;
  isEditMode = false;
  isSubmitting = false;
  healthTrendId?: string;

  ngOnInit(): void {
    this.initForm();

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditMode = true;
      this.healthTrendId = id;
      this.loadHealthTrend(id);
    }
  }

  private initForm(): void {
    this.healthTrendForm = this.fb.group({
      metricName: ['', Validators.required],
      startDate: [new Date(), Validators.required],
      endDate: [new Date(), Validators.required],
      averageValue: [null, Validators.required],
      minValue: [null, Validators.required],
      maxValue: [null, Validators.required],
      trendDirection: ['', Validators.required],
      percentageChange: [null, Validators.required],
      insights: ['']
    });
  }

  private loadHealthTrend(id: string): void {
    this.healthTrendService.getById(id).subscribe(trend => {
      this.healthTrendForm.patchValue({
        metricName: trend.metricName,
        startDate: new Date(trend.startDate),
        endDate: new Date(trend.endDate),
        averageValue: trend.averageValue,
        minValue: trend.minValue,
        maxValue: trend.maxValue,
        trendDirection: trend.trendDirection,
        percentageChange: trend.percentageChange,
        insights: trend.insights
      });
    });
  }

  onSubmit(): void {
    if (this.healthTrendForm.valid && !this.isSubmitting) {
      this.isSubmitting = true;
      const formValue = this.healthTrendForm.value;

      const healthTrendData = {
        metricName: formValue.metricName,
        startDate: formValue.startDate.toISOString(),
        endDate: formValue.endDate.toISOString(),
        averageValue: formValue.averageValue,
        minValue: formValue.minValue,
        maxValue: formValue.maxValue,
        trendDirection: formValue.trendDirection,
        percentageChange: formValue.percentageChange,
        insights: formValue.insights || null
      };

      if (this.isEditMode && this.healthTrendId) {
        const updateData: UpdateHealthTrend = {
          healthTrendId: this.healthTrendId,
          ...healthTrendData
        };
        this.healthTrendService.update(updateData).subscribe({
          next: () => this.router.navigate(['/health-trends']),
          error: () => this.isSubmitting = false
        });
      } else {
        const createData: CreateHealthTrend = {
          ...healthTrendData,
          userId: '00000000-0000-0000-0000-000000000000'
        };
        this.healthTrendService.create(createData).subscribe({
          next: () => this.router.navigate(['/health-trends']),
          error: () => this.isSubmitting = false
        });
      }
    }
  }

  onCancel(): void {
    this.router.navigate(['/health-trends']);
  }
}
