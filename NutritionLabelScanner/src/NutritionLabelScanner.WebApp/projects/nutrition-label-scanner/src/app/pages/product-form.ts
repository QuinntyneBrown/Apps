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
import { ProductService } from '../services';
import { Product } from '../models';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  template: `
    <div class="product-form">
      <h1 class="product-form__title">{{ isEditMode ? 'Edit Product' : 'New Product' }}</h1>

      <mat-card class="product-form__card">
        <form [formGroup]="form" (ngSubmit)="onSubmit()" class="product-form__form">
          <mat-form-field class="product-form__field">
            <mat-label>Name</mat-label>
            <input matInput formControlName="name" required>
            <mat-error *ngIf="form.get('name')?.hasError('required')">Name is required</mat-error>
          </mat-form-field>

          <mat-form-field class="product-form__field">
            <mat-label>Brand</mat-label>
            <input matInput formControlName="brand">
          </mat-form-field>

          <mat-form-field class="product-form__field">
            <mat-label>Barcode</mat-label>
            <input matInput formControlName="barcode">
          </mat-form-field>

          <mat-form-field class="product-form__field">
            <mat-label>Category</mat-label>
            <input matInput formControlName="category">
          </mat-form-field>

          <mat-form-field class="product-form__field">
            <mat-label>Serving Size</mat-label>
            <input matInput formControlName="servingSize">
          </mat-form-field>

          <mat-form-field class="product-form__field">
            <mat-label>Scanned At</mat-label>
            <input matInput [matDatepicker]="picker" formControlName="scannedAt" required>
            <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
            <mat-datepicker #picker></mat-datepicker>
            <mat-error *ngIf="form.get('scannedAt')?.hasError('required')">Scanned date is required</mat-error>
          </mat-form-field>

          <mat-form-field class="product-form__field">
            <mat-label>Notes</mat-label>
            <textarea matInput formControlName="notes" rows="3"></textarea>
          </mat-form-field>

          <div class="product-form__actions">
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
    .product-form {
      padding: 2rem;
      max-width: 800px;
      margin: 0 auto;
    }

    .product-form__title {
      margin: 0 0 2rem 0;
      font-size: 2rem;
      font-weight: 500;
    }

    .product-form__card {
      padding: 2rem;
    }

    .product-form__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
    }

    .product-form__field {
      width: 100%;
    }

    .product-form__actions {
      display: flex;
      justify-content: flex-end;
      gap: 1rem;
      margin-top: 1rem;
    }
  `]
})
export class ProductForm implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  private readonly productService = inject(ProductService);

  form: FormGroup;
  isEditMode = false;
  productId?: string;

  constructor() {
    this.form = this.fb.group({
      name: ['', Validators.required],
      brand: [''],
      barcode: [''],
      category: [''],
      servingSize: [''],
      scannedAt: [new Date(), Validators.required],
      notes: ['']
    });
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id && id !== 'new') {
      this.isEditMode = true;
      this.productId = id;
      this.loadProduct(id);
    }
  }

  loadProduct(id: string): void {
    this.productService.getById(id).subscribe(product => {
      this.form.patchValue({
        name: product.name,
        brand: product.brand,
        barcode: product.barcode,
        category: product.category,
        servingSize: product.servingSize,
        scannedAt: new Date(product.scannedAt),
        notes: product.notes
      });
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const userId = '00000000-0000-0000-0000-000000000000'; // Replace with actual user ID from auth

      if (this.isEditMode && this.productId) {
        this.productService.update({
          productId: this.productId,
          userId,
          name: formValue.name,
          brand: formValue.brand,
          barcode: formValue.barcode,
          category: formValue.category,
          servingSize: formValue.servingSize,
          scannedAt: formValue.scannedAt.toISOString(),
          notes: formValue.notes
        }).subscribe(() => {
          this.router.navigate(['/products']);
        });
      } else {
        this.productService.create({
          userId,
          name: formValue.name,
          brand: formValue.brand,
          barcode: formValue.barcode,
          category: formValue.category,
          servingSize: formValue.servingSize,
          scannedAt: formValue.scannedAt.toISOString(),
          notes: formValue.notes
        }).subscribe(() => {
          this.router.navigate(['/products']);
        });
      }
    }
  }

  onCancel(): void {
    this.router.navigate(['/products']);
  }
}
