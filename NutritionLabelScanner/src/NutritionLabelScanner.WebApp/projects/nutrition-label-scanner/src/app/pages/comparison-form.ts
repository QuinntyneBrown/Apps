import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { ComparisonService, ProductService } from '../services';

@Component({
  selector: 'app-comparison-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule
  ],
  template: `
    <div class="comparison-form">
      <h1 class="comparison-form__title">{{ isEditMode ? 'Edit Comparison' : 'New Comparison' }}</h1>

      <mat-card class="comparison-form__card">
        <form [formGroup]="form" (ngSubmit)="onSubmit()" class="comparison-form__form">
          <mat-form-field class="comparison-form__field">
            <mat-label>Name</mat-label>
            <input matInput formControlName="name" required>
            <mat-error *ngIf="form.get('name')?.hasError('required')">Name is required</mat-error>
          </mat-form-field>

          <mat-form-field class="comparison-form__field">
            <mat-label>Product IDs (comma-separated)</mat-label>
            <input matInput formControlName="productIds" required>
            <mat-hint>Enter product IDs separated by commas</mat-hint>
            <mat-error *ngIf="form.get('productIds')?.hasError('required')">Product IDs are required</mat-error>
          </mat-form-field>

          <mat-form-field class="comparison-form__field">
            <mat-label>Results</mat-label>
            <textarea matInput formControlName="results" rows="4"></textarea>
          </mat-form-field>

          <mat-form-field class="comparison-form__field">
            <mat-label>Winner Product</mat-label>
            <mat-select formControlName="winnerProductId">
              <mat-option [value]="null">None</mat-option>
              <mat-option *ngFor="let product of (productService.products$ | async)" [value]="product.productId">
                {{ product.name }} {{ product.brand ? '(' + product.brand + ')' : '' }}
              </mat-option>
            </mat-select>
          </mat-form-field>

          <div class="comparison-form__actions">
            <button mat-raised-button type="button" (click)="onCancel()">Cancel</button>
            <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid">
              {{ isEditMode ? 'Update' : 'Create' }}
            </button>
          </div>
        </form>
      </mat-card>
    </div>
  `,
  styles: [`
    .comparison-form {
      padding: 2rem;
      max-width: 800px;
      margin: 0 auto;
    }

    .comparison-form__title {
      margin: 0 0 2rem 0;
      font-size: 2rem;
      font-weight: 500;
    }

    .comparison-form__card {
      padding: 2rem;
    }

    .comparison-form__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
    }

    .comparison-form__field {
      width: 100%;
    }

    .comparison-form__actions {
      display: flex;
      justify-content: flex-end;
      gap: 1rem;
      margin-top: 1rem;
    }
  `]
})
export class ComparisonForm implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  private readonly comparisonService = inject(ComparisonService);
  public readonly productService = inject(ProductService);

  form: FormGroup;
  isEditMode = false;
  comparisonId?: string;

  constructor() {
    this.form = this.fb.group({
      name: ['', Validators.required],
      productIds: ['', Validators.required],
      results: [''],
      winnerProductId: [null]
    });
  }

  ngOnInit(): void {
    this.productService.getAll().subscribe();

    const id = this.route.snapshot.paramMap.get('id');
    if (id && id !== 'new') {
      this.isEditMode = true;
      this.comparisonId = id;
      this.loadComparison(id);
    }
  }

  loadComparison(id: string): void {
    this.comparisonService.getById(id).subscribe(comparison => {
      this.form.patchValue({
        name: comparison.name,
        productIds: comparison.productIds,
        results: comparison.results,
        winnerProductId: comparison.winnerProductId
      });
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const userId = '00000000-0000-0000-0000-000000000000'; // Replace with actual user ID from auth

      if (this.isEditMode && this.comparisonId) {
        this.comparisonService.update({
          comparisonId: this.comparisonId,
          userId,
          name: formValue.name,
          productIds: formValue.productIds,
          results: formValue.results,
          winnerProductId: formValue.winnerProductId
        }).subscribe(() => {
          this.router.navigate(['/comparisons']);
        });
      } else {
        this.comparisonService.create({
          userId,
          name: formValue.name,
          productIds: formValue.productIds,
          results: formValue.results,
          winnerProductId: formValue.winnerProductId
        }).subscribe(() => {
          this.router.navigate(['/comparisons']);
        });
      }
    }
  }

  onCancel(): void {
    this.router.navigate(['/comparisons']);
  }
}
