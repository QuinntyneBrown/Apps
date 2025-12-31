import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { NutritionInfoService, ProductService } from '../services';

@Component({
  selector: 'app-nutrition-info-form',
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
    <div class="nutrition-info-form">
      <h1 class="nutrition-info-form__title">{{ isEditMode ? 'Edit Nutrition Info' : 'New Nutrition Info' }}</h1>

      <mat-card class="nutrition-info-form__card">
        <form [formGroup]="form" (ngSubmit)="onSubmit()" class="nutrition-info-form__form">
          <mat-form-field class="nutrition-info-form__field">
            <mat-label>Product</mat-label>
            <mat-select formControlName="productId" required>
              <mat-option *ngFor="let product of (productService.products$ | async)" [value]="product.productId">
                {{ product.name }} {{ product.brand ? '(' + product.brand + ')' : '' }}
              </mat-option>
            </mat-select>
            <mat-error *ngIf="form.get('productId')?.hasError('required')">Product is required</mat-error>
          </mat-form-field>

          <mat-form-field class="nutrition-info-form__field">
            <mat-label>Calories</mat-label>
            <input matInput type="number" formControlName="calories" required>
            <mat-error *ngIf="form.get('calories')?.hasError('required')">Calories is required</mat-error>
          </mat-form-field>

          <mat-form-field class="nutrition-info-form__field">
            <mat-label>Total Fat (g)</mat-label>
            <input matInput type="number" step="0.01" formControlName="totalFat" required>
            <mat-error *ngIf="form.get('totalFat')?.hasError('required')">Total Fat is required</mat-error>
          </mat-form-field>

          <mat-form-field class="nutrition-info-form__field">
            <mat-label>Saturated Fat (g)</mat-label>
            <input matInput type="number" step="0.01" formControlName="saturatedFat">
          </mat-form-field>

          <mat-form-field class="nutrition-info-form__field">
            <mat-label>Trans Fat (g)</mat-label>
            <input matInput type="number" step="0.01" formControlName="transFat">
          </mat-form-field>

          <mat-form-field class="nutrition-info-form__field">
            <mat-label>Cholesterol (mg)</mat-label>
            <input matInput type="number" step="0.01" formControlName="cholesterol">
          </mat-form-field>

          <mat-form-field class="nutrition-info-form__field">
            <mat-label>Sodium (mg)</mat-label>
            <input matInput type="number" step="0.01" formControlName="sodium" required>
            <mat-error *ngIf="form.get('sodium')?.hasError('required')">Sodium is required</mat-error>
          </mat-form-field>

          <mat-form-field class="nutrition-info-form__field">
            <mat-label>Total Carbohydrates (g)</mat-label>
            <input matInput type="number" step="0.01" formControlName="totalCarbohydrates" required>
            <mat-error *ngIf="form.get('totalCarbohydrates')?.hasError('required')">Total Carbohydrates is required</mat-error>
          </mat-form-field>

          <mat-form-field class="nutrition-info-form__field">
            <mat-label>Dietary Fiber (g)</mat-label>
            <input matInput type="number" step="0.01" formControlName="dietaryFiber">
          </mat-form-field>

          <mat-form-field class="nutrition-info-form__field">
            <mat-label>Total Sugars (g)</mat-label>
            <input matInput type="number" step="0.01" formControlName="totalSugars">
          </mat-form-field>

          <mat-form-field class="nutrition-info-form__field">
            <mat-label>Protein (g)</mat-label>
            <input matInput type="number" step="0.01" formControlName="protein" required>
            <mat-error *ngIf="form.get('protein')?.hasError('required')">Protein is required</mat-error>
          </mat-form-field>

          <mat-form-field class="nutrition-info-form__field">
            <mat-label>Additional Nutrients</mat-label>
            <textarea matInput formControlName="additionalNutrients" rows="3"></textarea>
          </mat-form-field>

          <div class="nutrition-info-form__actions">
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
    .nutrition-info-form {
      padding: 2rem;
      max-width: 800px;
      margin: 0 auto;
    }

    .nutrition-info-form__title {
      margin: 0 0 2rem 0;
      font-size: 2rem;
      font-weight: 500;
    }

    .nutrition-info-form__card {
      padding: 2rem;
    }

    .nutrition-info-form__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
    }

    .nutrition-info-form__field {
      width: 100%;
    }

    .nutrition-info-form__actions {
      display: flex;
      justify-content: flex-end;
      gap: 1rem;
      margin-top: 1rem;
    }
  `]
})
export class NutritionInfoForm implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  private readonly nutritionInfoService = inject(NutritionInfoService);
  public readonly productService = inject(ProductService);

  form: FormGroup;
  isEditMode = false;
  nutritionInfoId?: string;

  constructor() {
    this.form = this.fb.group({
      productId: ['', Validators.required],
      calories: [0, [Validators.required, Validators.min(0)]],
      totalFat: [0, [Validators.required, Validators.min(0)]],
      saturatedFat: [null],
      transFat: [null],
      cholesterol: [null],
      sodium: [0, [Validators.required, Validators.min(0)]],
      totalCarbohydrates: [0, [Validators.required, Validators.min(0)]],
      dietaryFiber: [null],
      totalSugars: [null],
      protein: [0, [Validators.required, Validators.min(0)]],
      additionalNutrients: ['']
    });
  }

  ngOnInit(): void {
    this.productService.getAll().subscribe();

    const id = this.route.snapshot.paramMap.get('id');
    if (id && id !== 'new') {
      this.isEditMode = true;
      this.nutritionInfoId = id;
      this.loadNutritionInfo(id);
    }
  }

  loadNutritionInfo(id: string): void {
    this.nutritionInfoService.getById(id).subscribe(info => {
      this.form.patchValue({
        productId: info.productId,
        calories: info.calories,
        totalFat: info.totalFat,
        saturatedFat: info.saturatedFat,
        transFat: info.transFat,
        cholesterol: info.cholesterol,
        sodium: info.sodium,
        totalCarbohydrates: info.totalCarbohydrates,
        dietaryFiber: info.dietaryFiber,
        totalSugars: info.totalSugars,
        protein: info.protein,
        additionalNutrients: info.additionalNutrients
      });
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;

      if (this.isEditMode && this.nutritionInfoId) {
        this.nutritionInfoService.update({
          nutritionInfoId: this.nutritionInfoId,
          productId: formValue.productId,
          calories: formValue.calories,
          totalFat: formValue.totalFat,
          saturatedFat: formValue.saturatedFat,
          transFat: formValue.transFat,
          cholesterol: formValue.cholesterol,
          sodium: formValue.sodium,
          totalCarbohydrates: formValue.totalCarbohydrates,
          dietaryFiber: formValue.dietaryFiber,
          totalSugars: formValue.totalSugars,
          protein: formValue.protein,
          additionalNutrients: formValue.additionalNutrients
        }).subscribe(() => {
          this.router.navigate(['/nutrition-infos']);
        });
      } else {
        this.nutritionInfoService.create({
          productId: formValue.productId,
          calories: formValue.calories,
          totalFat: formValue.totalFat,
          saturatedFat: formValue.saturatedFat,
          transFat: formValue.transFat,
          cholesterol: formValue.cholesterol,
          sodium: formValue.sodium,
          totalCarbohydrates: formValue.totalCarbohydrates,
          dietaryFiber: formValue.dietaryFiber,
          totalSugars: formValue.totalSugars,
          protein: formValue.protein,
          additionalNutrients: formValue.additionalNutrients
        }).subscribe(() => {
          this.router.navigate(['/nutrition-infos']);
        });
      }
    }
  }

  onCancel(): void {
    this.router.navigate(['/nutrition-infos']);
  }
}
