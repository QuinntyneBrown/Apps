import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, ActivatedRoute, RouterLink } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { ReflectionService } from '../services';

@Component({
  selector: 'app-reflection-form',
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
    <div class="reflection-form">
      <h1 class="reflection-form__title">{{ isEditMode ? 'Edit' : 'Create' }} Reflection</h1>

      <form [formGroup]="form" (ngSubmit)="onSubmit()" class="reflection-form__form">
        <mat-form-field class="reflection-form__field">
          <mat-label>Topic</mat-label>
          <input matInput formControlName="topic">
        </mat-form-field>

        <mat-form-field class="reflection-form__field">
          <mat-label>Text</mat-label>
          <textarea matInput formControlName="text" rows="8" required></textarea>
          <mat-error *ngIf="form.get('text')?.hasError('required')">Text is required</mat-error>
        </mat-form-field>

        <mat-form-field class="reflection-form__field">
          <mat-label>Reflection Date</mat-label>
          <input matInput [matDatepicker]="picker" formControlName="reflectionDate" required>
          <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
          <mat-datepicker #picker></mat-datepicker>
          <mat-error *ngIf="form.get('reflectionDate')?.hasError('required')">Reflection date is required</mat-error>
        </mat-form-field>

        <div class="reflection-form__actions">
          <button mat-raised-button type="button" routerLink="/reflections">Cancel</button>
          <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid">
            {{ isEditMode ? 'Update' : 'Create' }}
          </button>
        </div>
      </form>
    </div>
  `,
  styles: [`
    .reflection-form {
      padding: 2rem;
      max-width: 800px;
      margin: 0 auto;
    }

    .reflection-form__title {
      margin-bottom: 2rem;
      color: #333;
    }

    .reflection-form__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
    }

    .reflection-form__field {
      width: 100%;
    }

    .reflection-form__actions {
      display: flex;
      gap: 1rem;
      justify-content: flex-end;
      margin-top: 1rem;
    }
  `]
})
export class ReflectionForm implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  private readonly reflectionService = inject(ReflectionService);

  form!: FormGroup;
  isEditMode = false;
  reflectionId?: string;

  ngOnInit(): void {
    this.reflectionId = this.route.snapshot.paramMap.get('id') || undefined;
    this.isEditMode = !!this.reflectionId;

    this.form = this.fb.group({
      topic: [''],
      text: ['', Validators.required],
      reflectionDate: [new Date(), Validators.required],
      userId: ['00000000-0000-0000-0000-000000000001']
    });

    if (this.isEditMode && this.reflectionId) {
      this.reflectionService.getById(this.reflectionId).subscribe(reflection => {
        this.form.patchValue({
          topic: reflection.topic,
          text: reflection.text,
          reflectionDate: new Date(reflection.reflectionDate),
          userId: reflection.userId
        });
      });
    }
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const reflection = {
        ...formValue,
        reflectionDate: formValue.reflectionDate.toISOString()
      };

      if (this.isEditMode && this.reflectionId) {
        this.reflectionService.update({ ...reflection, reflectionId: this.reflectionId }).subscribe(() => {
          this.router.navigate(['/reflections']);
        });
      } else {
        this.reflectionService.create(reflection).subscribe(() => {
          this.router.navigate(['/reflections']);
        });
      }
    }
  }
}
