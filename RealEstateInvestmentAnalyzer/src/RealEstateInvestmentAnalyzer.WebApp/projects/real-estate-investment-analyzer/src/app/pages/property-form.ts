import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { PropertyService } from '../services';
import { PropertyType, PropertyTypeLabels } from '../models';

@Component({
  selector: 'app-property-form',
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
    MatNativeDateModule
  ],
  template: `
    <div class="property-form">
      <div class="property-form__header">
        <h1 class="property-form__title">{{ isEditMode ? 'Edit Property' : 'Add Property' }}</h1>
      </div>

      <mat-card class="property-form__card">
        <mat-card-content>
          <form [formGroup]="form" (ngSubmit)="onSubmit()" class="property-form__form">
            <mat-form-field appearance="outline" class="property-form__field">
              <mat-label>Address</mat-label>
              <input matInput formControlName="address" required>
              <mat-error *ngIf="form.get('address')?.hasError('required')">Address is required</mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="property-form__field">
              <mat-label>Property Type</mat-label>
              <mat-select formControlName="propertyType" required>
                <mat-option *ngFor="let type of propertyTypes" [value]="type.value">
                  {{ type.label }}
                </mat-option>
              </mat-select>
              <mat-error *ngIf="form.get('propertyType')?.hasError('required')">Property type is required</mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="property-form__field">
              <mat-label>Purchase Price</mat-label>
              <input matInput type="number" formControlName="purchasePrice" required>
              <span matTextPrefix>$&nbsp;</span>
              <mat-error *ngIf="form.get('purchasePrice')?.hasError('required')">Purchase price is required</mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="property-form__field">
              <mat-label>Purchase Date</mat-label>
              <input matInput [matDatepicker]="purchasePicker" formControlName="purchaseDate" required>
              <mat-datepicker-toggle matIconSuffix [for]="purchasePicker"></mat-datepicker-toggle>
              <mat-datepicker #purchasePicker></mat-datepicker>
              <mat-error *ngIf="form.get('purchaseDate')?.hasError('required')">Purchase date is required</mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="property-form__field">
              <mat-label>Current Value</mat-label>
              <input matInput type="number" formControlName="currentValue" required>
              <span matTextPrefix>$&nbsp;</span>
              <mat-error *ngIf="form.get('currentValue')?.hasError('required')">Current value is required</mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="property-form__field">
              <mat-label>Square Feet</mat-label>
              <input matInput type="number" formControlName="squareFeet" required>
              <mat-error *ngIf="form.get('squareFeet')?.hasError('required')">Square feet is required</mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="property-form__field">
              <mat-label>Bedrooms</mat-label>
              <input matInput type="number" formControlName="bedrooms" required>
              <mat-error *ngIf="form.get('bedrooms')?.hasError('required')">Bedrooms is required</mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="property-form__field">
              <mat-label>Bathrooms</mat-label>
              <input matInput type="number" formControlName="bathrooms" required>
              <mat-error *ngIf="form.get('bathrooms')?.hasError('required')">Bathrooms is required</mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="property-form__field property-form__field--full">
              <mat-label>Notes</mat-label>
              <textarea matInput formControlName="notes" rows="3"></textarea>
            </mat-form-field>

            <div class="property-form__actions">
              <button mat-raised-button type="button" (click)="onCancel()">Cancel</button>
              <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid">
                {{ isEditMode ? 'Update' : 'Create' }}
              </button>
            </div>
          </form>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .property-form {
      padding: 2rem;
    }

    .property-form__header {
      margin-bottom: 2rem;
    }

    .property-form__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 500;
    }

    .property-form__form {
      display: grid;
      grid-template-columns: repeat(2, 1fr);
      gap: 1rem;
    }

    .property-form__field {
      width: 100%;
    }

    .property-form__field--full {
      grid-column: 1 / -1;
    }

    .property-form__actions {
      grid-column: 1 / -1;
      display: flex;
      justify-content: flex-end;
      gap: 1rem;
      margin-top: 1rem;
    }
  `]
})
export class PropertyForm implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  private readonly propertyService = inject(PropertyService);

  form!: FormGroup;
  isEditMode = false;
  propertyId?: string;

  propertyTypes = Object.entries(PropertyTypeLabels).map(([value, label]) => ({
    value: Number(value),
    label
  }));

  ngOnInit(): void {
    this.form = this.fb.group({
      address: ['', Validators.required],
      propertyType: [PropertyType.SingleFamily, Validators.required],
      purchasePrice: [0, Validators.required],
      purchaseDate: [new Date(), Validators.required],
      currentValue: [0, Validators.required],
      squareFeet: [0, Validators.required],
      bedrooms: [0, Validators.required],
      bathrooms: [0, Validators.required],
      notes: ['']
    });

    this.propertyId = this.route.snapshot.paramMap.get('id') || undefined;
    if (this.propertyId) {
      this.isEditMode = true;
      this.loadProperty(this.propertyId);
    }
  }

  loadProperty(id: string): void {
    this.propertyService.getProperty(id).subscribe(property => {
      this.form.patchValue({
        address: property.address,
        propertyType: property.propertyType,
        purchasePrice: property.purchasePrice,
        purchaseDate: new Date(property.purchaseDate),
        currentValue: property.currentValue,
        squareFeet: property.squareFeet,
        bedrooms: property.bedrooms,
        bathrooms: property.bathrooms,
        notes: property.notes
      });
    });
  }

  onSubmit(): void {
    if (this.form.invalid) {
      return;
    }

    const formValue = this.form.value;
    const propertyData = {
      ...formValue,
      purchaseDate: formValue.purchaseDate.toISOString()
    };

    if (this.isEditMode && this.propertyId) {
      this.propertyService.updateProperty({ propertyId: this.propertyId, ...propertyData }).subscribe(() => {
        this.router.navigate(['/properties']);
      });
    } else {
      this.propertyService.createProperty(propertyData).subscribe(() => {
        this.router.navigate(['/properties']);
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/properties']);
  }
}
