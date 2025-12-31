import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { SavingsTipService } from '../services';

@Component({
  selector: 'app-savings-tips-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  template: `
    <div class="savings-tips-form">
      <mat-card class="savings-tips-form__card">
        <mat-card-header>
          <mat-card-title class="savings-tips-form__title">
            {{ isEditMode ? 'Edit' : 'Create' }} Savings Tip
          </mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <form [formGroup]="form" (ngSubmit)="onSubmit()" class="savings-tips-form__form">
            <mat-form-field class="savings-tips-form__field">
              <mat-label>Title</mat-label>
              <input matInput formControlName="title" placeholder="Enter tip title">
            </mat-form-field>

            <mat-form-field class="savings-tips-form__field">
              <mat-label>Description</mat-label>
              <textarea matInput formControlName="description" rows="5" placeholder="Enter tip description"></textarea>
            </mat-form-field>

            <div class="savings-tips-form__actions">
              <button mat-raised-button type="button" (click)="cancel()">
                Cancel
              </button>
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
    .savings-tips-form {
      padding: 24px;

      &__card {
        max-width: 600px;
        margin: 0 auto;
      }

      &__title {
        font-size: 24px;
        margin-bottom: 16px;
      }

      &__form {
        display: flex;
        flex-direction: column;
        gap: 16px;
      }

      &__field {
        width: 100%;
      }

      &__actions {
        display: flex;
        gap: 16px;
        justify-content: flex-end;
        margin-top: 16px;
      }
    }
  `]
})
export class SavingsTipsForm implements OnInit {
  form: FormGroup;
  isEditMode = false;
  tipId: string | null = null;

  constructor(
    private fb: FormBuilder,
    private savingsTipService: SavingsTipService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.form = this.fb.group({
      title: ['', Validators.required],
      description: ['']
    });
  }

  ngOnInit(): void {
    this.tipId = this.route.snapshot.paramMap.get('id');
    if (this.tipId) {
      this.isEditMode = true;
      this.loadTip();
    }
  }

  loadTip(): void {
    if (this.tipId) {
      this.savingsTipService.getById(this.tipId).subscribe(tip => {
        this.form.patchValue({
          title: tip.title,
          description: tip.description
        });
      });
    }
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;

      if (this.isEditMode && this.tipId) {
        this.savingsTipService.update(this.tipId, {
          savingsTipId: this.tipId,
          ...formValue
        }).subscribe(() => {
          this.router.navigate(['/savings-tips']);
        });
      } else {
        this.savingsTipService.create(formValue).subscribe(() => {
          this.router.navigate(['/savings-tips']);
        });
      }
    }
  }

  cancel(): void {
    this.router.navigate(['/savings-tips']);
  }
}
