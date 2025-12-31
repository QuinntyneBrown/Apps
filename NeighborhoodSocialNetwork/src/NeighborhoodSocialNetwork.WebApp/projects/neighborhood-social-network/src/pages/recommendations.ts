import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCardModule } from '@angular/material/card';
import { RecommendationService } from '../services';
import { Recommendation, CreateRecommendation, UpdateRecommendation, RecommendationType, RecommendationTypeLabels } from '../models';

@Component({
  selector: 'app-recommendation-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit' : 'Add' }} Recommendation</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="recommendation-form">
        <mat-form-field class="recommendation-form__field">
          <mat-label>Neighbor ID</mat-label>
          <input matInput formControlName="neighborId" required>
        </mat-form-field>

        <mat-form-field class="recommendation-form__field">
          <mat-label>Title</mat-label>
          <input matInput formControlName="title" required>
        </mat-form-field>

        <mat-form-field class="recommendation-form__field">
          <mat-label>Description</mat-label>
          <textarea matInput formControlName="description" rows="3" required></textarea>
        </mat-form-field>

        <mat-form-field class="recommendation-form__field">
          <mat-label>Recommendation Type</mat-label>
          <mat-select formControlName="recommendationType" required>
            <mat-option *ngFor="let type of recommendationTypes" [value]="type.value">
              {{ type.label }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="recommendation-form__field">
          <mat-label>Business Name</mat-label>
          <input matInput formControlName="businessName">
        </mat-form-field>

        <mat-form-field class="recommendation-form__field">
          <mat-label>Location</mat-label>
          <input matInput formControlName="location">
        </mat-form-field>

        <mat-form-field class="recommendation-form__field">
          <mat-label>Rating (1-5)</mat-label>
          <input matInput type="number" formControlName="rating" min="1" max="5">
        </mat-form-field>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" [disabled]="form.invalid" (click)="save()">
        Save
      </button>
    </mat-dialog-actions>
  `,
  styles: [`
    .recommendation-form {
      display: flex;
      flex-direction: column;
      gap: 16px;
      min-width: 400px;

      &__field {
        width: 100%;
      }
    }
  `]
})
export class RecommendationDialog {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialog);

  data?: Recommendation;
  form: FormGroup;
  recommendationTypes = Object.keys(RecommendationType)
    .filter(key => !isNaN(Number(key)))
    .map(key => ({
      value: Number(key),
      label: RecommendationTypeLabels[Number(key) as RecommendationType]
    }));

  constructor() {
    this.form = this.fb.group({
      neighborId: ['', Validators.required],
      title: ['', Validators.required],
      description: ['', Validators.required],
      recommendationType: [RecommendationType.Other, Validators.required],
      businessName: [''],
      location: [''],
      rating: [null, [Validators.min(1), Validators.max(5)]]
    });

    if (this.data) {
      this.form.patchValue(this.data);
    }
  }

  save() {
    if (this.form.valid) {
      const result = this.data
        ? { ...this.form.value, recommendationId: this.data.recommendationId }
        : this.form.value;
      this.dialogRef.closeAll();
    }
  }
}

@Component({
  selector: 'app-recommendations',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule
  ],
  template: `
    <div class="recommendations">
      <div class="recommendations__header">
        <h1 class="recommendations__title">Recommendations</h1>
        <button mat-raised-button color="primary" (click)="openDialog()">
          <mat-icon>add</mat-icon>
          Add Recommendation
        </button>
      </div>

      <mat-card class="recommendations__card">
        <table mat-table [dataSource]="recommendations$ | async" class="recommendations__table">
          <ng-container matColumnDef="title">
            <th mat-header-cell *matHeaderCellDef>Title</th>
            <td mat-cell *matCellDef="let recommendation">{{ recommendation.title }}</td>
          </ng-container>

          <ng-container matColumnDef="description">
            <th mat-header-cell *matHeaderCellDef>Description</th>
            <td mat-cell *matCellDef="let recommendation">
              {{ recommendation.description.length > 50 ? recommendation.description.substring(0, 50) + '...' : recommendation.description }}
            </td>
          </ng-container>

          <ng-container matColumnDef="recommendationType">
            <th mat-header-cell *matHeaderCellDef>Type</th>
            <td mat-cell *matCellDef="let recommendation">
              {{ getRecommendationTypeLabel(recommendation.recommendationType) }}
            </td>
          </ng-container>

          <ng-container matColumnDef="businessName">
            <th mat-header-cell *matHeaderCellDef>Business</th>
            <td mat-cell *matCellDef="let recommendation">{{ recommendation.businessName || 'N/A' }}</td>
          </ng-container>

          <ng-container matColumnDef="location">
            <th mat-header-cell *matHeaderCellDef>Location</th>
            <td mat-cell *matCellDef="let recommendation">{{ recommendation.location || 'N/A' }}</td>
          </ng-container>

          <ng-container matColumnDef="rating">
            <th mat-header-cell *matHeaderCellDef>Rating</th>
            <td mat-cell *matCellDef="let recommendation">
              <span class="recommendations__rating">
                {{ recommendation.rating ? recommendation.rating + '/5' : 'N/A' }}
                <mat-icon *ngIf="recommendation.rating">star</mat-icon>
              </span>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let recommendation">
              <button mat-icon-button color="primary" (click)="openDialog(recommendation)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="delete(recommendation.recommendationId)">
                <mat-icon>delete</mat-icon>
              </button>
            </td>
          </ng-container>

          <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
          <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
        </table>
      </mat-card>
    </div>
  `,
  styles: [`
    .recommendations {
      padding: 24px;

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 24px;
      }

      &__title {
        margin: 0;
        font-size: 2rem;
        font-weight: 400;
      }

      &__card {
        overflow-x: auto;
      }

      &__table {
        width: 100%;
      }

      &__rating {
        display: flex;
        align-items: center;
        gap: 4px;

        mat-icon {
          color: #ffc107;
          font-size: 18px;
          width: 18px;
          height: 18px;
        }
      }
    }
  `]
})
export class Recommendations implements OnInit {
  private recommendationService = inject(RecommendationService);
  private dialog = inject(MatDialog);

  recommendations$ = this.recommendationService.recommendations$;
  displayedColumns = ['title', 'description', 'recommendationType', 'businessName', 'location', 'rating', 'actions'];

  ngOnInit() {
    this.recommendationService.getAll().subscribe();
  }

  getRecommendationTypeLabel(type: RecommendationType): string {
    return RecommendationTypeLabels[type];
  }

  openDialog(recommendation?: Recommendation) {
    const dialogRef = this.dialog.open(RecommendationDialog, {
      width: '500px',
      data: recommendation
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (recommendation) {
          this.recommendationService.update(result as UpdateRecommendation).subscribe();
        } else {
          this.recommendationService.create(result as CreateRecommendation).subscribe();
        }
      }
    });
  }

  delete(id: string) {
    if (confirm('Are you sure you want to delete this recommendation?')) {
      this.recommendationService.delete(id).subscribe();
    }
  }
}
