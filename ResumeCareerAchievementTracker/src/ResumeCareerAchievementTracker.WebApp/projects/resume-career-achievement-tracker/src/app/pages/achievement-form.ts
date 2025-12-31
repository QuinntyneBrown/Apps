import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormArray } from '@angular/forms';
import { Router, ActivatedRoute, RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { AchievementService } from '../services';
import { AchievementType, AchievementTypeLabels } from '../models';

@Component({
  selector: 'app-achievement-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterLink,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatChipsModule,
    MatIconModule
  ],
  template: `
    <div class="achievement-form">
      <div class="achievement-form__header">
        <h1 class="achievement-form__title">{{ isEditMode ? 'Edit Achievement' : 'New Achievement' }}</h1>
        <p class="achievement-form__subtitle">{{ isEditMode ? 'Update achievement details' : 'Add a new career achievement' }}</p>
      </div>

      <mat-card class="achievement-form__card">
        <mat-card-content>
          <form [formGroup]="form" (ngSubmit)="onSubmit()" class="achievement-form__form">
            <div class="achievement-form__row">
              <mat-form-field class="achievement-form__field achievement-form__field--full">
                <mat-label>Title</mat-label>
                <input matInput formControlName="title" placeholder="Enter achievement title" required>
                @if (form.get('title')?.hasError('required') && form.get('title')?.touched) {
                  <mat-error>Title is required</mat-error>
                }
              </mat-form-field>
            </div>

            <div class="achievement-form__row">
              <mat-form-field class="achievement-form__field achievement-form__field--full">
                <mat-label>Description</mat-label>
                <textarea
                  matInput
                  formControlName="description"
                  placeholder="Describe the achievement"
                  rows="4"
                  required></textarea>
                @if (form.get('description')?.hasError('required') && form.get('description')?.touched) {
                  <mat-error>Description is required</mat-error>
                }
              </mat-form-field>
            </div>

            <div class="achievement-form__row achievement-form__row--two">
              <mat-form-field class="achievement-form__field">
                <mat-label>Type</mat-label>
                <mat-select formControlName="achievementType" required>
                  @for (type of achievementTypes; track type.value) {
                    <mat-option [value]="type.value">{{ type.label }}</mat-option>
                  }
                </mat-select>
                @if (form.get('achievementType')?.hasError('required') && form.get('achievementType')?.touched) {
                  <mat-error>Type is required</mat-error>
                }
              </mat-form-field>

              <mat-form-field class="achievement-form__field">
                <mat-label>Achievement Date</mat-label>
                <input matInput [matDatepicker]="picker" formControlName="achievedDate" required>
                <mat-datepicker-toggle matIconSuffix [for]="picker"></mat-datepicker-toggle>
                <mat-datepicker #picker></mat-datepicker>
                @if (form.get('achievedDate')?.hasError('required') && form.get('achievedDate')?.touched) {
                  <mat-error>Date is required</mat-error>
                }
              </mat-form-field>
            </div>

            <div class="achievement-form__row">
              <mat-form-field class="achievement-form__field achievement-form__field--full">
                <mat-label>Organization</mat-label>
                <input matInput formControlName="organization" placeholder="Company or organization">
              </mat-form-field>
            </div>

            <div class="achievement-form__row">
              <mat-form-field class="achievement-form__field achievement-form__field--full">
                <mat-label>Impact</mat-label>
                <textarea
                  matInput
                  formControlName="impact"
                  placeholder="Describe the impact of this achievement"
                  rows="3"></textarea>
              </mat-form-field>
            </div>

            <div class="achievement-form__row">
              <div class="achievement-form__field achievement-form__field--full">
                <mat-label class="achievement-form__label">Tags</mat-label>
                <div class="achievement-form__tags">
                  @for (tag of tags.controls; track $index) {
                    <mat-chip class="achievement-form__tag">
                      {{ tag.value }}
                      <button matChipRemove (click)="removeTag($index)" type="button">
                        <mat-icon>cancel</mat-icon>
                      </button>
                    </mat-chip>
                  }
                </div>
                <div class="achievement-form__tag-input">
                  <mat-form-field class="achievement-form__field">
                    <mat-label>Add Tag</mat-label>
                    <input
                      matInput
                      #tagInput
                      placeholder="Enter tag and press Enter"
                      (keyup.enter)="addTag(tagInput.value); tagInput.value = ''">
                  </mat-form-field>
                </div>
              </div>
            </div>

            <div class="achievement-form__actions">
              <a mat-button routerLink="/achievements" type="button">Cancel</a>
              <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid || saving">
                {{ saving ? 'Saving...' : (isEditMode ? 'Update' : 'Create') }}
              </button>
            </div>
          </form>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .achievement-form {
      padding: 2rem;
      max-width: 900px;
      margin: 0 auto;
    }

    .achievement-form__header {
      margin-bottom: 2rem;
    }

    .achievement-form__title {
      font-size: 2rem;
      font-weight: 500;
      margin: 0 0 0.5rem 0;
      color: rgba(0, 0, 0, 0.87);
    }

    .achievement-form__subtitle {
      font-size: 1rem;
      margin: 0;
      color: rgba(0, 0, 0, 0.6);
    }

    .achievement-form__card {
      margin-bottom: 2rem;
    }

    .achievement-form__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
    }

    .achievement-form__row {
      display: flex;
      gap: 1rem;
    }

    .achievement-form__row--two {
      display: grid;
      grid-template-columns: 1fr 1fr;
    }

    .achievement-form__field {
      flex: 1;
    }

    .achievement-form__field--full {
      width: 100%;
    }

    .achievement-form__label {
      display: block;
      font-size: 0.875rem;
      color: rgba(0, 0, 0, 0.6);
      margin-bottom: 0.5rem;
    }

    .achievement-form__tags {
      display: flex;
      flex-wrap: wrap;
      gap: 0.5rem;
      margin-bottom: 1rem;
    }

    .achievement-form__tag {
      display: flex;
      align-items: center;
      gap: 0.25rem;
    }

    .achievement-form__tag-input {
      max-width: 300px;
    }

    .achievement-form__actions {
      display: flex;
      justify-content: flex-end;
      gap: 1rem;
      margin-top: 1rem;
      padding-top: 1rem;
      border-top: 1px solid rgba(0, 0, 0, 0.12);
    }

    @media (max-width: 768px) {
      .achievement-form {
        padding: 1rem;
      }

      .achievement-form__row--two {
        grid-template-columns: 1fr;
      }
    }
  `]
})
export class AchievementForm implements OnInit {
  private fb = inject(FormBuilder);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private achievementService = inject(AchievementService);

  form!: FormGroup;
  isEditMode = false;
  saving = false;
  achievementId?: string;

  achievementTypes = Object.entries(AchievementTypeLabels).map(([value, label]) => ({
    value: Number(value),
    label
  }));

  get tags(): FormArray {
    return this.form.get('tags') as FormArray;
  }

  ngOnInit(): void {
    this.form = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      achievementType: [AchievementType.Award, Validators.required],
      achievedDate: ['', Validators.required],
      organization: [''],
      impact: [''],
      tags: this.fb.array([])
    });

    this.achievementId = this.route.snapshot.paramMap.get('id') || undefined;
    this.isEditMode = !!this.achievementId && this.achievementId !== 'new';

    if (this.isEditMode && this.achievementId) {
      this.loadAchievement(this.achievementId);
    }
  }

  loadAchievement(id: string): void {
    this.achievementService.getAchievementById(id).subscribe({
      next: (achievement) => {
        this.form.patchValue({
          title: achievement.title,
          description: achievement.description,
          achievementType: achievement.achievementType,
          achievedDate: new Date(achievement.achievedDate),
          organization: achievement.organization,
          impact: achievement.impact
        });

        achievement.tags.forEach(tag => {
          this.tags.push(this.fb.control(tag));
        });
      },
      error: () => {
        alert('Failed to load achievement');
        this.router.navigate(['/achievements']);
      }
    });
  }

  addTag(value: string): void {
    const trimmedValue = value.trim();
    if (trimmedValue && !this.tags.value.includes(trimmedValue)) {
      this.tags.push(this.fb.control(trimmedValue));
    }
  }

  removeTag(index: number): void {
    this.tags.removeAt(index);
  }

  onSubmit(): void {
    if (this.form.invalid) {
      return;
    }

    this.saving = true;
    const formValue = this.form.value;
    const achievementData = {
      ...formValue,
      achievedDate: new Date(formValue.achievedDate).toISOString(),
      userId: '00000000-0000-0000-0000-000000000000' // This should be replaced with actual user ID
    };

    const request = this.isEditMode && this.achievementId
      ? this.achievementService.updateAchievement({ ...achievementData, achievementId: this.achievementId })
      : this.achievementService.createAchievement(achievementData);

    request.subscribe({
      next: () => {
        this.router.navigate(['/achievements']);
      },
      error: () => {
        this.saving = false;
        alert('Failed to save achievement');
      }
    });
  }
}
