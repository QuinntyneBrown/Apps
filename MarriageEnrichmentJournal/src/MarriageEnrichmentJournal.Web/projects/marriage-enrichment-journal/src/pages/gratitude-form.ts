import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, ActivatedRoute, RouterLink } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { GratitudeService } from '../services';

@Component({
  selector: 'app-gratitude-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterLink,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  template: `
    <div class="gratitude-form">
      <h1 class="gratitude-form__title">{{ isEditMode ? 'Edit' : 'Create' }} Gratitude</h1>

      <form [formGroup]="form" (ngSubmit)="onSubmit()" class="gratitude-form__form">
        <mat-form-field class="gratitude-form__field">
          <mat-label>Text</mat-label>
          <textarea matInput formControlName="text" rows="6" required></textarea>
          <mat-error *ngIf="form.get('text')?.hasError('required')">Text is required</mat-error>
        </mat-form-field>

        <mat-form-field class="gratitude-form__field">
          <mat-label>Gratitude Date</mat-label>
          <input matInput [matDatepicker]="picker" formControlName="gratitudeDate" required>
          <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
          <mat-datepicker #picker></mat-datepicker>
          <mat-error *ngIf="form.get('gratitudeDate')?.hasError('required')">Gratitude date is required</mat-error>
        </mat-form-field>

        <div class="gratitude-form__actions">
          <button mat-raised-button type="button" routerLink="/gratitudes">Cancel</button>
          <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid">
            {{ isEditMode ? 'Update' : 'Create' }}
          </button>
        </div>
      </form>
    </div>
  `,
  styles: [`
    .gratitude-form {
      padding: 2rem;
      max-width: 800px;
      margin: 0 auto;
    }

    .gratitude-form__title {
      margin-bottom: 2rem;
      color: #333;
    }

    .gratitude-form__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
    }

    .gratitude-form__field {
      width: 100%;
    }

    .gratitude-form__actions {
      display: flex;
      gap: 1rem;
      justify-content: flex-end;
      margin-top: 1rem;
    }
  `]
})
export class GratitudeForm implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  private readonly gratitudeService = inject(GratitudeService);

  form!: FormGroup;
  isEditMode = false;
  gratitudeId?: string;

  ngOnInit(): void {
    this.gratitudeId = this.route.snapshot.paramMap.get('id') || undefined;
    this.isEditMode = !!this.gratitudeId;

    this.form = this.fb.group({
      text: ['', Validators.required],
      gratitudeDate: [new Date(), Validators.required],
      userId: ['00000000-0000-0000-0000-000000000001']
    });

    if (this.isEditMode && this.gratitudeId) {
      this.gratitudeService.getById(this.gratitudeId).subscribe(gratitude => {
        this.form.patchValue({
          text: gratitude.text,
          gratitudeDate: new Date(gratitude.gratitudeDate),
          userId: gratitude.userId
        });
      });
    }
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const gratitude = {
        ...formValue,
        gratitudeDate: formValue.gratitudeDate.toISOString()
      };

      if (this.isEditMode && this.gratitudeId) {
        this.gratitudeService.update({ ...gratitude, gratitudeId: this.gratitudeId }).subscribe(() => {
          this.router.navigate(['/gratitudes']);
        });
      } else {
        this.gratitudeService.create(gratitude).subscribe(() => {
          this.router.navigate(['/gratitudes']);
        });
      }
    }
  }
}
