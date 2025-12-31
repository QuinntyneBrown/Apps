import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute, RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { SkillService } from '../services';

@Component({
  selector: 'app-skill-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterLink,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  template: `
    <div class="skill-form">
      <div class="skill-form__header">
        <h1 class="skill-form__title">{{ isEditMode ? 'Edit Skill' : 'New Skill' }}</h1>
        <p class="skill-form__subtitle">{{ isEditMode ? 'Update skill details' : 'Add a new skill to your profile' }}</p>
      </div>

      <mat-card class="skill-form__card">
        <mat-card-content>
          <form [formGroup]="form" (ngSubmit)="onSubmit()" class="skill-form__form">
            <div class="skill-form__row">
              <mat-form-field class="skill-form__field skill-form__field--full">
                <mat-label>Skill Name</mat-label>
                <input matInput formControlName="name" placeholder="e.g., JavaScript, Project Management" required>
                @if (form.get('name')?.hasError('required') && form.get('name')?.touched) {
                  <mat-error>Name is required</mat-error>
                }
              </mat-form-field>
            </div>

            <div class="skill-form__row skill-form__row--two">
              <mat-form-field class="skill-form__field">
                <mat-label>Category</mat-label>
                <input matInput formControlName="category" placeholder="e.g., Programming, Soft Skills" required>
                @if (form.get('category')?.hasError('required') && form.get('category')?.touched) {
                  <mat-error>Category is required</mat-error>
                }
              </mat-form-field>

              <mat-form-field class="skill-form__field">
                <mat-label>Proficiency Level</mat-label>
                <input matInput formControlName="proficiencyLevel" placeholder="e.g., Expert, Intermediate" required>
                @if (form.get('proficiencyLevel')?.hasError('required') && form.get('proficiencyLevel')?.touched) {
                  <mat-error>Proficiency level is required</mat-error>
                }
              </mat-form-field>
            </div>

            <div class="skill-form__row skill-form__row--two">
              <mat-form-field class="skill-form__field">
                <mat-label>Years of Experience</mat-label>
                <input matInput formControlName="yearsOfExperience" type="number" step="0.5" min="0" placeholder="e.g., 5">
              </mat-form-field>

              <mat-form-field class="skill-form__field">
                <mat-label>Last Used Date</mat-label>
                <input matInput [matDatepicker]="picker" formControlName="lastUsedDate">
                <mat-datepicker-toggle matIconSuffix [for]="picker"></mat-datepicker-toggle>
                <mat-datepicker #picker></mat-datepicker>
              </mat-form-field>
            </div>

            <div class="skill-form__row">
              <mat-form-field class="skill-form__field skill-form__field--full">
                <mat-label>Notes</mat-label>
                <textarea
                  matInput
                  formControlName="notes"
                  placeholder="Additional notes about this skill"
                  rows="4"></textarea>
              </mat-form-field>
            </div>

            <div class="skill-form__actions">
              <a mat-button routerLink="/skills" type="button">Cancel</a>
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
    .skill-form {
      padding: 2rem;
      max-width: 900px;
      margin: 0 auto;
    }

    .skill-form__header {
      margin-bottom: 2rem;
    }

    .skill-form__title {
      font-size: 2rem;
      font-weight: 500;
      margin: 0 0 0.5rem 0;
      color: rgba(0, 0, 0, 0.87);
    }

    .skill-form__subtitle {
      font-size: 1rem;
      margin: 0;
      color: rgba(0, 0, 0, 0.6);
    }

    .skill-form__card {
      margin-bottom: 2rem;
    }

    .skill-form__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
    }

    .skill-form__row {
      display: flex;
      gap: 1rem;
    }

    .skill-form__row--two {
      display: grid;
      grid-template-columns: 1fr 1fr;
    }

    .skill-form__field {
      flex: 1;
    }

    .skill-form__field--full {
      width: 100%;
    }

    .skill-form__actions {
      display: flex;
      justify-content: flex-end;
      gap: 1rem;
      margin-top: 1rem;
      padding-top: 1rem;
      border-top: 1px solid rgba(0, 0, 0, 0.12);
    }

    @media (max-width: 768px) {
      .skill-form {
        padding: 1rem;
      }

      .skill-form__row--two {
        grid-template-columns: 1fr;
      }
    }
  `]
})
export class SkillForm implements OnInit {
  private fb = inject(FormBuilder);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private skillService = inject(SkillService);

  form!: FormGroup;
  isEditMode = false;
  saving = false;
  skillId?: string;

  ngOnInit(): void {
    this.form = this.fb.group({
      name: ['', Validators.required],
      category: ['', Validators.required],
      proficiencyLevel: ['', Validators.required],
      yearsOfExperience: [null],
      lastUsedDate: [''],
      notes: ['']
    });

    this.skillId = this.route.snapshot.paramMap.get('id') || undefined;
    this.isEditMode = !!this.skillId && this.skillId !== 'new';

    if (this.isEditMode && this.skillId) {
      this.loadSkill(this.skillId);
    }
  }

  loadSkill(id: string): void {
    this.skillService.getSkillById(id).subscribe({
      next: (skill) => {
        this.form.patchValue({
          name: skill.name,
          category: skill.category,
          proficiencyLevel: skill.proficiencyLevel,
          yearsOfExperience: skill.yearsOfExperience,
          lastUsedDate: skill.lastUsedDate ? new Date(skill.lastUsedDate) : null,
          notes: skill.notes
        });
      },
      error: () => {
        alert('Failed to load skill');
        this.router.navigate(['/skills']);
      }
    });
  }

  onSubmit(): void {
    if (this.form.invalid) {
      return;
    }

    this.saving = true;
    const formValue = this.form.value;
    const skillData = {
      ...formValue,
      lastUsedDate: formValue.lastUsedDate ? new Date(formValue.lastUsedDate).toISOString() : null,
      userId: '00000000-0000-0000-0000-000000000000' // This should be replaced with actual user ID
    };

    const request = this.isEditMode && this.skillId
      ? this.skillService.updateSkill({ ...skillData, skillId: this.skillId })
      : this.skillService.createSkill(skillData);

    request.subscribe({
      next: () => {
        this.router.navigate(['/skills']);
      },
      error: () => {
        this.saving = false;
        alert('Failed to save skill');
      }
    });
  }
}
