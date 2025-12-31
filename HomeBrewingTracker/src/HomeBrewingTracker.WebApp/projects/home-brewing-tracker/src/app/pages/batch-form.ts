import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { BatchService, RecipeService } from '../services';
import { BatchStatus, BatchStatusLabels, Recipe } from '../models';

@Component({
  selector: 'app-batch-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatIconModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  template: `
    <div class="batch-form">
      <div class="batch-form__header">
        <button mat-icon-button (click)="goBack()">
          <mat-icon>arrow_back</mat-icon>
        </button>
        <h1 class="batch-form__title">{{ isEdit ? 'Edit Batch' : 'New Batch' }}</h1>
      </div>

      <mat-card class="batch-form__card">
        <form [formGroup]="form" (ngSubmit)="onSubmit()">
          <div class="batch-form__row">
            <mat-form-field appearance="outline" class="batch-form__field">
              <mat-label>Batch Number</mat-label>
              <input matInput formControlName="batchNumber" required>
              <mat-error *ngIf="form.get('batchNumber')?.hasError('required')">Batch number is required</mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="batch-form__field" *ngIf="!isEdit">
              <mat-label>Recipe</mat-label>
              <mat-select formControlName="recipeId" required>
                <mat-option *ngFor="let recipe of recipes$ | async" [value]="recipe.recipeId">
                  {{ recipe.name }}
                </mat-option>
              </mat-select>
              <mat-error *ngIf="form.get('recipeId')?.hasError('required')">Recipe is required</mat-error>
            </mat-form-field>
          </div>

          <div class="batch-form__row" *ngIf="isEdit">
            <mat-form-field appearance="outline" class="batch-form__field">
              <mat-label>Status</mat-label>
              <mat-select formControlName="status" required>
                <mat-option *ngFor="let status of batchStatuses" [value]="status.value">
                  {{ status.label }}
                </mat-option>
              </mat-select>
              <mat-error *ngIf="form.get('status')?.hasError('required')">Status is required</mat-error>
            </mat-form-field>
          </div>

          <div class="batch-form__row">
            <mat-form-field appearance="outline" class="batch-form__field">
              <mat-label>Brew Date</mat-label>
              <input matInput [matDatepicker]="brewDatePicker" formControlName="brewDate" required>
              <mat-datepicker-toggle matSuffix [for]="brewDatePicker"></mat-datepicker-toggle>
              <mat-datepicker #brewDatePicker></mat-datepicker>
              <mat-error *ngIf="form.get('brewDate')?.hasError('required')">Brew date is required</mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="batch-form__field">
              <mat-label>Bottling Date</mat-label>
              <input matInput [matDatepicker]="bottlingDatePicker" formControlName="bottlingDate">
              <mat-datepicker-toggle matSuffix [for]="bottlingDatePicker"></mat-datepicker-toggle>
              <mat-datepicker #bottlingDatePicker></mat-datepicker>
            </mat-form-field>
          </div>

          <div class="batch-form__row">
            <mat-form-field appearance="outline" class="batch-form__field">
              <mat-label>Original Gravity</mat-label>
              <input matInput type="number" step="0.001" formControlName="actualOriginalGravity">
            </mat-form-field>

            <mat-form-field appearance="outline" class="batch-form__field">
              <mat-label>Final Gravity</mat-label>
              <input matInput type="number" step="0.001" formControlName="actualFinalGravity">
            </mat-form-field>

            <mat-form-field appearance="outline" class="batch-form__field">
              <mat-label>ABV (%)</mat-label>
              <input matInput type="number" step="0.1" formControlName="actualABV">
            </mat-form-field>
          </div>

          <mat-form-field appearance="outline" class="batch-form__field batch-form__field--full">
            <mat-label>Notes</mat-label>
            <textarea matInput formControlName="notes" rows="4"></textarea>
          </mat-form-field>

          <div class="batch-form__actions">
            <button mat-button type="button" (click)="goBack()">Cancel</button>
            <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid">
              <mat-icon>save</mat-icon>
              {{ isEdit ? 'Update' : 'Create' }} Batch
            </button>
          </div>
        </form>
      </mat-card>
    </div>
  `,
  styles: [`
    .batch-form {
      padding: 2rem;
      max-width: 900px;
      margin: 0 auto;

      &__header {
        display: flex;
        align-items: center;
        margin-bottom: 2rem;
        gap: 1rem;
      }

      &__title {
        font-size: 2rem;
        margin: 0;
      }

      &__card {
        padding: 2rem;
      }

      &__row {
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
        gap: 1rem;
        margin-bottom: 1rem;
      }

      &__field {
        width: 100%;

        &--full {
          margin-bottom: 1rem;
        }
      }

      &__actions {
        display: flex;
        justify-content: flex-end;
        gap: 1rem;
        margin-top: 2rem;
      }
    }
  `]
})
export class BatchForm implements OnInit {
  form: FormGroup;
  isEdit = false;
  batchId?: string;
  recipes$ = this.recipeService.recipes$;
  batchStatuses = Object.keys(BatchStatus)
    .filter(key => !isNaN(Number(key)))
    .map(key => ({
      value: Number(key),
      label: BatchStatusLabels[Number(key)]
    }));

  constructor(
    private fb: FormBuilder,
    private batchService: BatchService,
    private recipeService: RecipeService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.form = this.fb.group({
      batchNumber: ['', Validators.required],
      recipeId: ['', Validators.required],
      status: [BatchStatus.Planned],
      brewDate: [new Date(), Validators.required],
      bottlingDate: [null],
      actualOriginalGravity: [null],
      actualFinalGravity: [null],
      actualABV: [null],
      notes: ['']
    });
  }

  ngOnInit() {
    this.batchId = this.route.snapshot.paramMap.get('id') || undefined;
    this.isEdit = !!this.batchId && this.route.snapshot.url[this.route.snapshot.url.length - 1].path === 'edit';

    this.recipeService.getRecipes().subscribe();

    if (this.batchId && this.isEdit) {
      this.loadBatch();
    }

    if (this.isEdit) {
      this.form.get('recipeId')?.disable();
    }
  }

  loadBatch() {
    if (!this.batchId) return;

    this.batchService.getBatch(this.batchId).subscribe(batch => {
      this.form.patchValue({
        batchNumber: batch.batchNumber,
        recipeId: batch.recipeId,
        status: batch.status,
        brewDate: new Date(batch.brewDate),
        bottlingDate: batch.bottlingDate ? new Date(batch.bottlingDate) : null,
        actualOriginalGravity: batch.actualOriginalGravity,
        actualFinalGravity: batch.actualFinalGravity,
        actualABV: batch.actualABV,
        notes: batch.notes
      });
    });
  }

  onSubmit() {
    if (this.form.invalid) return;

    const formValue = this.form.getRawValue();

    if (this.isEdit && this.batchId) {
      this.batchService.updateBatch(this.batchId, {
        batchId: this.batchId,
        batchNumber: formValue.batchNumber,
        status: formValue.status,
        brewDate: formValue.brewDate,
        bottlingDate: formValue.bottlingDate,
        actualOriginalGravity: formValue.actualOriginalGravity,
        actualFinalGravity: formValue.actualFinalGravity,
        actualABV: formValue.actualABV,
        notes: formValue.notes
      }).subscribe(() => {
        this.router.navigate(['/batches']);
      });
    } else {
      this.batchService.createBatch({
        userId: '00000000-0000-0000-0000-000000000000', // TODO: Replace with actual user ID
        recipeId: formValue.recipeId,
        batchNumber: formValue.batchNumber,
        brewDate: formValue.brewDate,
        notes: formValue.notes
      }).subscribe(() => {
        this.router.navigate(['/batches']);
      });
    }
  }

  goBack() {
    this.router.navigate(['/batches']);
  }
}
