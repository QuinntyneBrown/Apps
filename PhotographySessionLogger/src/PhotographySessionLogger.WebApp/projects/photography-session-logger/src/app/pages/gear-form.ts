import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { GearService } from '../services';

@Component({
  selector: 'app-gear-form',
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
    <div class="gear-form">
      <mat-card class="gear-form__card">
        <mat-card-header>
          <mat-card-title>{{ isEditMode ? 'Edit Gear' : 'New Gear' }}</mat-card-title>
        </mat-card-header>

        <mat-card-content>
          <form [formGroup]="form" class="gear-form__form">
            <mat-form-field class="gear-form__field">
              <mat-label>Name</mat-label>
              <input matInput formControlName="name" required>
            </mat-form-field>

            <mat-form-field class="gear-form__field">
              <mat-label>Gear Type</mat-label>
              <input matInput formControlName="gearType" required>
            </mat-form-field>

            <mat-form-field class="gear-form__field">
              <mat-label>Brand</mat-label>
              <input matInput formControlName="brand">
            </mat-form-field>

            <mat-form-field class="gear-form__field">
              <mat-label>Model</mat-label>
              <input matInput formControlName="model">
            </mat-form-field>

            <mat-form-field class="gear-form__field">
              <mat-label>Purchase Date</mat-label>
              <input matInput [matDatepicker]="picker" formControlName="purchaseDate">
              <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
              <mat-datepicker #picker></mat-datepicker>
            </mat-form-field>

            <mat-form-field class="gear-form__field">
              <mat-label>Purchase Price</mat-label>
              <input matInput type="number" formControlName="purchasePrice" min="0" step="0.01">
            </mat-form-field>

            <mat-form-field class="gear-form__field">
              <mat-label>Notes</mat-label>
              <textarea matInput formControlName="notes" rows="4"></textarea>
            </mat-form-field>
          </form>
        </mat-card-content>

        <mat-card-actions class="gear-form__actions">
          <button mat-button (click)="cancel()">Cancel</button>
          <button mat-raised-button color="primary" (click)="save()" [disabled]="!form.valid">Save</button>
        </mat-card-actions>
      </mat-card>
    </div>
  `,
  styles: [`
    .gear-form {
      padding: 2rem;
      max-width: 800px;
      margin: 0 auto;
    }

    .gear-form__card {
      width: 100%;
    }

    .gear-form__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
      margin-top: 1rem;
    }

    .gear-form__field {
      width: 100%;
    }

    .gear-form__actions {
      display: flex;
      justify-content: flex-end;
      gap: 0.5rem;
      padding: 1rem;
    }
  `]
})
export class GearForm implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly gearService = inject(GearService);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);

  form: FormGroup;
  isEditMode = false;
  gearId?: string;

  constructor() {
    this.form = this.fb.group({
      name: ['', Validators.required],
      gearType: ['', Validators.required],
      brand: [''],
      model: [''],
      purchaseDate: [null],
      purchasePrice: [null],
      notes: ['']
    });
  }

  ngOnInit(): void {
    this.gearId = this.route.snapshot.paramMap.get('id') || undefined;
    this.isEditMode = !!this.gearId;

    if (this.isEditMode && this.gearId) {
      this.gearService.getById(this.gearId).subscribe(gear => {
        this.form.patchValue({
          name: gear.name,
          gearType: gear.gearType,
          brand: gear.brand,
          model: gear.model,
          purchaseDate: gear.purchaseDate ? new Date(gear.purchaseDate) : null,
          purchasePrice: gear.purchasePrice,
          notes: gear.notes
        });
      });
    }
  }

  save(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const purchaseDate = formValue.purchaseDate ? new Date(formValue.purchaseDate).toISOString() : undefined;

      if (this.isEditMode && this.gearId) {
        this.gearService.update({
          gearId: this.gearId,
          ...formValue,
          purchaseDate
        }).subscribe(() => {
          this.router.navigate(['/gears']);
        });
      } else {
        this.gearService.create({
          userId: '00000000-0000-0000-0000-000000000000', // TODO: Get from auth
          ...formValue,
          purchaseDate
        }).subscribe(() => {
          this.router.navigate(['/gears']);
        });
      }
    }
  }

  cancel(): void {
    this.router.navigate(['/gears']);
  }
}
