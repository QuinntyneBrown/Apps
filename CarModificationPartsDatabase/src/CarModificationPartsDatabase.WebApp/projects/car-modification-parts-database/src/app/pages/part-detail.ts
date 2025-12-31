import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { Router } from '@angular/router';
import { PartsService } from '../services';
import { Part, ModCategory, MOD_CATEGORY_LABELS } from '../models';

@Component({
  selector: 'app-part-detail',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatCheckboxModule,
    MatChipsModule,
    MatIconModule
  ],
  template: `
    <div class="part-detail">
      <div class="part-detail__header">
        <h1>{{ isEditMode ? (isNewPart ? 'New Part' : 'Edit Part') : 'Part Details' }}</h1>
        <div>
          <a mat-button routerLink="/parts">Back to List</a>
          <button *ngIf="!isEditMode && !isNewPart" mat-raised-button color="primary" (click)="toggleEditMode()">Edit</button>
        </div>
      </div>

      <mat-card *ngIf="!isEditMode && part" class="part-detail__view">
        <mat-card-content>
          <div class="part-detail__field">
            <label>Name:</label>
            <span>{{ part.name }}</span>
          </div>
          <div class="part-detail__field">
            <label>Part Number:</label>
            <span>{{ part.partNumber || 'N/A' }}</span>
          </div>
          <div class="part-detail__field">
            <label>Manufacturer:</label>
            <span>{{ part.manufacturer }}</span>
          </div>
          <div class="part-detail__field">
            <label>Category:</label>
            <span>{{ getCategoryLabel(part.category) }}</span>
          </div>
          <div class="part-detail__field">
            <label>Description:</label>
            <span>{{ part.description }}</span>
          </div>
          <div class="part-detail__field">
            <label>Price:</label>
            <span>{{ part.price | currency }}</span>
          </div>
          <div class="part-detail__field">
            <label>In Stock:</label>
            <span>{{ part.inStock ? 'Yes' : 'No' }}</span>
          </div>
          <div class="part-detail__field" *ngIf="part.weight">
            <label>Weight:</label>
            <span>{{ part.weight }} lbs</span>
          </div>
          <div class="part-detail__field" *ngIf="part.dimensions">
            <label>Dimensions:</label>
            <span>{{ part.dimensions }}</span>
          </div>
          <div class="part-detail__field" *ngIf="part.warrantyInfo">
            <label>Warranty:</label>
            <span>{{ part.warrantyInfo }}</span>
          </div>
          <div class="part-detail__field" *ngIf="part.supplier">
            <label>Supplier:</label>
            <span>{{ part.supplier }}</span>
          </div>
          <div class="part-detail__field" *ngIf="part.compatibleVehicles.length">
            <label>Compatible Vehicles:</label>
            <div class="part-detail__chips">
              <mat-chip *ngFor="let vehicle of part.compatibleVehicles">{{ vehicle }}</mat-chip>
            </div>
          </div>
          <div class="part-detail__field" *ngIf="part.notes">
            <label>Notes:</label>
            <span>{{ part.notes }}</span>
          </div>
        </mat-card-content>
      </mat-card>

      <mat-card *ngIf="isEditMode" class="part-detail__form">
        <mat-card-content>
          <form [formGroup]="partForm" (ngSubmit)="onSubmit()">
            <mat-form-field appearance="outline">
              <mat-label>Name</mat-label>
              <input matInput formControlName="name" required>
            </mat-form-field>

            <mat-form-field appearance="outline">
              <mat-label>Part Number</mat-label>
              <input matInput formControlName="partNumber">
            </mat-form-field>

            <mat-form-field appearance="outline">
              <mat-label>Manufacturer</mat-label>
              <input matInput formControlName="manufacturer" required>
            </mat-form-field>

            <mat-form-field appearance="outline">
              <mat-label>Category</mat-label>
              <mat-select formControlName="category" required>
                <mat-option *ngFor="let cat of categories" [value]="cat.value">{{ cat.label }}</mat-option>
              </mat-select>
            </mat-form-field>

            <mat-form-field appearance="outline">
              <mat-label>Description</mat-label>
              <textarea matInput formControlName="description" rows="4" required></textarea>
            </mat-form-field>

            <mat-form-field appearance="outline">
              <mat-label>Price</mat-label>
              <input matInput type="number" formControlName="price" required>
            </mat-form-field>

            <mat-checkbox formControlName="inStock">In Stock</mat-checkbox>

            <mat-form-field appearance="outline">
              <mat-label>Weight (lbs)</mat-label>
              <input matInput type="number" formControlName="weight">
            </mat-form-field>

            <mat-form-field appearance="outline">
              <mat-label>Dimensions</mat-label>
              <input matInput formControlName="dimensions">
            </mat-form-field>

            <mat-form-field appearance="outline">
              <mat-label>Warranty Info</mat-label>
              <input matInput formControlName="warrantyInfo">
            </mat-form-field>

            <mat-form-field appearance="outline">
              <mat-label>Supplier</mat-label>
              <input matInput formControlName="supplier">
            </mat-form-field>

            <mat-form-field appearance="outline">
              <mat-label>Notes</mat-label>
              <textarea matInput formControlName="notes" rows="3"></textarea>
            </mat-form-field>

            <div class="part-detail__actions">
              <button mat-raised-button type="button" (click)="cancel()">Cancel</button>
              <button mat-raised-button color="primary" type="submit" [disabled]="!partForm.valid">Save</button>
            </div>
          </form>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .part-detail {
      padding: 2rem;

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 2rem;

        div {
          display: flex;
          gap: 1rem;
        }
      }

      &__view {
        max-width: 800px;
      }

      &__field {
        margin-bottom: 1rem;

        label {
          font-weight: 500;
          display: block;
          margin-bottom: 0.25rem;
        }
      }

      &__chips {
        display: flex;
        flex-wrap: wrap;
        gap: 0.5rem;
        margin-top: 0.5rem;
      }

      &__form {
        max-width: 800px;

        form {
          display: flex;
          flex-direction: column;
          gap: 1rem;
        }
      }

      &__actions {
        display: flex;
        gap: 1rem;
        justify-content: flex-end;
        margin-top: 1rem;
      }
    }
  `]
})
export class PartDetail implements OnInit {
  part: Part | null = null;
  partForm: FormGroup;
  isEditMode = false;
  isNewPart = false;
  categories = Object.entries(MOD_CATEGORY_LABELS).map(([value, label]) => ({ value: +value, label }));

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private partsService: PartsService,
    private fb: FormBuilder
  ) {
    this.partForm = this.fb.group({
      name: ['', Validators.required],
      partNumber: [''],
      manufacturer: ['', Validators.required],
      category: [ModCategory.Other, Validators.required],
      description: ['', Validators.required],
      price: [0, [Validators.required, Validators.min(0)]],
      inStock: [true],
      weight: [null],
      dimensions: [''],
      warrantyInfo: [''],
      supplier: [''],
      notes: ['']
    });
  }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    const isEdit = this.route.snapshot.url.some(segment => segment.path === 'edit');

    if (id === 'new') {
      this.isNewPart = true;
      this.isEditMode = true;
    } else if (id) {
      this.partsService.getById(id).subscribe(part => {
        this.part = part;
        if (isEdit) {
          this.isEditMode = true;
          this.partForm.patchValue(part);
        }
      });
    }
  }

  toggleEditMode() {
    this.isEditMode = !this.isEditMode;
    if (this.isEditMode && this.part) {
      this.partForm.patchValue(this.part);
    }
  }

  getCategoryLabel(category: number): string {
    return MOD_CATEGORY_LABELS[category];
  }

  cancel() {
    if (this.isNewPart) {
      this.router.navigate(['/parts']);
    } else {
      this.isEditMode = false;
    }
  }

  onSubmit() {
    if (this.partForm.valid) {
      const formValue = this.partForm.value;

      if (this.isNewPart) {
        this.partsService.create(formValue).subscribe(() => {
          this.router.navigate(['/parts']);
        });
      } else if (this.part) {
        this.partsService.update(this.part.partId, { ...formValue, partId: this.part.partId }).subscribe(updatedPart => {
          this.part = updatedPart;
          this.isEditMode = false;
        });
      }
    }
  }
}
