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
import { MatSliderModule } from '@angular/material/slider';
import { TastingNoteService, BatchService } from '../services';

@Component({
  selector: 'app-tasting-note-form',
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
    MatNativeDateModule,
    MatSliderModule
  ],
  template: `
    <div class="tasting-note-form">
      <div class="tasting-note-form__header">
        <button mat-icon-button (click)="goBack()">
          <mat-icon>arrow_back</mat-icon>
        </button>
        <h1 class="tasting-note-form__title">{{ isEdit ? 'Edit Tasting Note' : 'New Tasting Note' }}</h1>
      </div>

      <mat-card class="tasting-note-form__card">
        <form [formGroup]="form" (ngSubmit)="onSubmit()">
          <div class="tasting-note-form__row">
            <mat-form-field appearance="outline" class="tasting-note-form__field" *ngIf="!isEdit">
              <mat-label>Batch</mat-label>
              <mat-select formControlName="batchId" required>
                <mat-option *ngFor="let batch of batches$ | async" [value]="batch.batchId">
                  Batch #{{ batch.batchNumber }}
                </mat-option>
              </mat-select>
              <mat-error *ngIf="form.get('batchId')?.hasError('required')">Batch is required</mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="tasting-note-form__field">
              <mat-label>Tasting Date</mat-label>
              <input matInput [matDatepicker]="tastingDatePicker" formControlName="tastingDate" required>
              <mat-datepicker-toggle matSuffix [for]="tastingDatePicker"></mat-datepicker-toggle>
              <mat-datepicker #tastingDatePicker></mat-datepicker>
              <mat-error *ngIf="form.get('tastingDate')?.hasError('required')">Tasting date is required</mat-error>
            </mat-form-field>
          </div>

          <div class="tasting-note-form__rating">
            <label class="tasting-note-form__label">Rating (1-5 stars)</label>
            <div class="tasting-note-form__stars">
              <mat-icon
                *ngFor="let star of [1,2,3,4,5]"
                class="tasting-note-form__star"
                [class.tasting-note-form__star--active]="star <= form.get('rating')?.value"
                (click)="setRating(star)">
                {{ star <= form.get('rating')?.value ? 'star' : 'star_border' }}
              </mat-icon>
            </div>
          </div>

          <mat-form-field appearance="outline" class="tasting-note-form__field tasting-note-form__field--full">
            <mat-label>Appearance</mat-label>
            <textarea matInput formControlName="appearance" rows="2" placeholder="Describe the color, clarity, head retention..."></textarea>
          </mat-form-field>

          <mat-form-field appearance="outline" class="tasting-note-form__field tasting-note-form__field--full">
            <mat-label>Aroma</mat-label>
            <textarea matInput formControlName="aroma" rows="2" placeholder="Describe the aroma, hops, malt character..."></textarea>
          </mat-form-field>

          <mat-form-field appearance="outline" class="tasting-note-form__field tasting-note-form__field--full">
            <mat-label>Flavor</mat-label>
            <textarea matInput formControlName="flavor" rows="3" placeholder="Describe the taste, balance, finish..."></textarea>
          </mat-form-field>

          <mat-form-field appearance="outline" class="tasting-note-form__field tasting-note-form__field--full">
            <mat-label>Mouthfeel</mat-label>
            <textarea matInput formControlName="mouthfeel" rows="2" placeholder="Describe the body, carbonation, warmth..."></textarea>
          </mat-form-field>

          <mat-form-field appearance="outline" class="tasting-note-form__field tasting-note-form__field--full">
            <mat-label>Overall Impression</mat-label>
            <textarea matInput formControlName="overallImpression" rows="3" placeholder="Your overall thoughts and impressions..."></textarea>
          </mat-form-field>

          <div class="tasting-note-form__actions">
            <button mat-button type="button" (click)="goBack()">Cancel</button>
            <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid">
              <mat-icon>save</mat-icon>
              {{ isEdit ? 'Update' : 'Create' }} Tasting Note
            </button>
          </div>
        </form>
      </mat-card>
    </div>
  `,
  styles: [`
    .tasting-note-form {
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

      &__rating {
        margin-bottom: 2rem;
      }

      &__label {
        display: block;
        font-size: 0.875rem;
        color: rgba(0, 0, 0, 0.6);
        margin-bottom: 0.5rem;
      }

      &__stars {
        display: flex;
        gap: 0.25rem;
      }

      &__star {
        font-size: 2.5rem;
        height: 2.5rem;
        width: 2.5rem;
        cursor: pointer;
        color: #ccc;
        transition: color 0.2s;

        &:hover {
          color: #ffd700;
        }

        &--active {
          color: #ffd700;
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
export class TastingNoteForm implements OnInit {
  form: FormGroup;
  isEdit = false;
  tastingNoteId?: string;
  batches$ = this.batchService.batches$;

  constructor(
    private fb: FormBuilder,
    private tastingNoteService: TastingNoteService,
    private batchService: BatchService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.form = this.fb.group({
      batchId: ['', Validators.required],
      tastingDate: [new Date(), Validators.required],
      rating: [3, [Validators.required, Validators.min(1), Validators.max(5)]],
      appearance: [''],
      aroma: [''],
      flavor: [''],
      mouthfeel: [''],
      overallImpression: ['']
    });
  }

  ngOnInit() {
    this.tastingNoteId = this.route.snapshot.paramMap.get('id') || undefined;
    this.isEdit = !!this.tastingNoteId && this.route.snapshot.url[this.route.snapshot.url.length - 1].path === 'edit';

    this.batchService.getBatches().subscribe();

    if (this.tastingNoteId && this.isEdit) {
      this.loadTastingNote();
    }

    if (this.isEdit) {
      this.form.get('batchId')?.disable();
    }
  }

  loadTastingNote() {
    if (!this.tastingNoteId) return;

    this.tastingNoteService.getTastingNote(this.tastingNoteId).subscribe(note => {
      this.form.patchValue({
        batchId: note.batchId,
        tastingDate: new Date(note.tastingDate),
        rating: note.rating,
        appearance: note.appearance,
        aroma: note.aroma,
        flavor: note.flavor,
        mouthfeel: note.mouthfeel,
        overallImpression: note.overallImpression
      });
    });
  }

  setRating(rating: number) {
    this.form.patchValue({ rating });
  }

  onSubmit() {
    if (this.form.invalid) return;

    const formValue = this.form.getRawValue();

    if (this.isEdit && this.tastingNoteId) {
      this.tastingNoteService.updateTastingNote(this.tastingNoteId, {
        tastingNoteId: this.tastingNoteId,
        tastingDate: formValue.tastingDate,
        rating: formValue.rating,
        appearance: formValue.appearance,
        aroma: formValue.aroma,
        flavor: formValue.flavor,
        mouthfeel: formValue.mouthfeel,
        overallImpression: formValue.overallImpression
      }).subscribe(() => {
        this.router.navigate(['/tasting-notes']);
      });
    } else {
      this.tastingNoteService.createTastingNote({
        userId: '00000000-0000-0000-0000-000000000000', // TODO: Replace with actual user ID
        batchId: formValue.batchId,
        tastingDate: formValue.tastingDate,
        rating: formValue.rating,
        appearance: formValue.appearance,
        aroma: formValue.aroma,
        flavor: formValue.flavor,
        mouthfeel: formValue.mouthfeel,
        overallImpression: formValue.overallImpression
      }).subscribe(() => {
        this.router.navigate(['/tasting-notes']);
      });
    }
  }

  goBack() {
    this.router.navigate(['/tasting-notes']);
  }
}
