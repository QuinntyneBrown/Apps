import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink, Router } from '@angular/router';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { ModificationsService } from '../services';
import { Modification, ModCategory, MOD_CATEGORY_LABELS } from '../models';

@Component({
  selector: 'app-modification-detail',
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
    MatChipsModule,
    MatIconModule
  ],
  template: `
    <div class="modification-detail">
      <div class="modification-detail__header">
        <h1>{{ isEditMode ? (isNewModification ? 'New Modification' : 'Edit Modification') : 'Modification Details' }}</h1>
        <div>
          <a mat-button routerLink="/modifications">Back to List</a>
          <button *ngIf="!isEditMode && !isNewModification" mat-raised-button color="primary" (click)="toggleEditMode()">Edit</button>
        </div>
      </div>

      <mat-card *ngIf="!isEditMode && modification" class="modification-detail__view">
        <mat-card-content>
          <div class="modification-detail__field">
            <label>Name:</label>
            <span>{{ modification.name }}</span>
          </div>
          <div class="modification-detail__field">
            <label>Category:</label>
            <span>{{ getCategoryLabel(modification.category) }}</span>
          </div>
          <div class="modification-detail__field">
            <label>Description:</label>
            <span>{{ modification.description }}</span>
          </div>
          <div class="modification-detail__field" *ngIf="modification.manufacturer">
            <label>Manufacturer:</label>
            <span>{{ modification.manufacturer }}</span>
          </div>
          <div class="modification-detail__field" *ngIf="modification.estimatedCost">
            <label>Estimated Cost:</label>
            <span>{{ modification.estimatedCost | currency }}</span>
          </div>
          <div class="modification-detail__field" *ngIf="modification.difficultyLevel">
            <label>Difficulty Level:</label>
            <span>{{ modification.difficultyLevel }}/5</span>
          </div>
          <div class="modification-detail__field" *ngIf="modification.estimatedInstallationTime">
            <label>Est. Installation Time:</label>
            <span>{{ modification.estimatedInstallationTime }} hours</span>
          </div>
          <div class="modification-detail__field" *ngIf="modification.performanceGain">
            <label>Performance Gain:</label>
            <span>{{ modification.performanceGain }}</span>
          </div>
          <div class="modification-detail__field" *ngIf="modification.compatibleVehicles.length">
            <label>Compatible Vehicles:</label>
            <div class="modification-detail__chips">
              <mat-chip *ngFor="let vehicle of modification.compatibleVehicles">{{ vehicle }}</mat-chip>
            </div>
          </div>
          <div class="modification-detail__field" *ngIf="modification.requiredTools.length">
            <label>Required Tools:</label>
            <div class="modification-detail__chips">
              <mat-chip *ngFor="let tool of modification.requiredTools">{{ tool }}</mat-chip>
            </div>
          </div>
          <div class="modification-detail__field" *ngIf="modification.notes">
            <label>Notes:</label>
            <span>{{ modification.notes }}</span>
          </div>
        </mat-card-content>
      </mat-card>

      <mat-card *ngIf="isEditMode" class="modification-detail__form">
        <mat-card-content>
          <form [formGroup]="modificationForm" (ngSubmit)="onSubmit()">
            <mat-form-field appearance="outline">
              <mat-label>Name</mat-label>
              <input matInput formControlName="name" required>
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
              <mat-label>Manufacturer</mat-label>
              <input matInput formControlName="manufacturer">
            </mat-form-field>

            <mat-form-field appearance="outline">
              <mat-label>Estimated Cost</mat-label>
              <input matInput type="number" formControlName="estimatedCost">
            </mat-form-field>

            <mat-form-field appearance="outline">
              <mat-label>Difficulty Level (1-5)</mat-label>
              <input matInput type="number" formControlName="difficultyLevel" min="1" max="5">
            </mat-form-field>

            <mat-form-field appearance="outline">
              <mat-label>Estimated Installation Time (hours)</mat-label>
              <input matInput type="number" formControlName="estimatedInstallationTime">
            </mat-form-field>

            <mat-form-field appearance="outline">
              <mat-label>Performance Gain</mat-label>
              <textarea matInput formControlName="performanceGain" rows="2"></textarea>
            </mat-form-field>

            <mat-form-field appearance="outline">
              <mat-label>Notes</mat-label>
              <textarea matInput formControlName="notes" rows="3"></textarea>
            </mat-form-field>

            <div class="modification-detail__actions">
              <button mat-raised-button type="button" (click)="cancel()">Cancel</button>
              <button mat-raised-button color="primary" type="submit" [disabled]="!modificationForm.valid">Save</button>
            </div>
          </form>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .modification-detail {
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
export class ModificationDetail implements OnInit {
  modification: Modification | null = null;
  modificationForm: FormGroup;
  isEditMode = false;
  isNewModification = false;
  categories = Object.entries(MOD_CATEGORY_LABELS).map(([value, label]) => ({ value: +value, label }));

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private modificationsService: ModificationsService,
    private fb: FormBuilder
  ) {
    this.modificationForm = this.fb.group({
      name: ['', Validators.required],
      category: [ModCategory.Other, Validators.required],
      description: ['', Validators.required],
      manufacturer: [''],
      estimatedCost: [null],
      difficultyLevel: [null, [Validators.min(1), Validators.max(5)]],
      estimatedInstallationTime: [null],
      performanceGain: [''],
      notes: ['']
    });
  }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    const isEdit = this.route.snapshot.url.some(segment => segment.path === 'edit');

    if (id === 'new') {
      this.isNewModification = true;
      this.isEditMode = true;
    } else if (id) {
      this.modificationsService.getById(id).subscribe(modification => {
        this.modification = modification;
        if (isEdit) {
          this.isEditMode = true;
          this.modificationForm.patchValue(modification);
        }
      });
    }
  }

  toggleEditMode() {
    this.isEditMode = !this.isEditMode;
    if (this.isEditMode && this.modification) {
      this.modificationForm.patchValue(this.modification);
    }
  }

  getCategoryLabel(category: number): string {
    return MOD_CATEGORY_LABELS[category];
  }

  cancel() {
    if (this.isNewModification) {
      this.router.navigate(['/modifications']);
    } else {
      this.isEditMode = false;
    }
  }

  onSubmit() {
    if (this.modificationForm.valid) {
      const formValue = this.modificationForm.value;

      if (this.isNewModification) {
        this.modificationsService.create(formValue).subscribe(() => {
          this.router.navigate(['/modifications']);
        });
      } else if (this.modification) {
        this.modificationsService.update(this.modification.modificationId, {
          ...formValue,
          modificationId: this.modification.modificationId
        }).subscribe(updatedModification => {
          this.modification = updatedModification;
          this.isEditMode = false;
        });
      }
    }
  }
}
