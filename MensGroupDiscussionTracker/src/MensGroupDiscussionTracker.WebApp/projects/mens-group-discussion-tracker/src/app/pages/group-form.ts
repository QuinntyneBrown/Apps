import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { GroupService } from '../services';

@Component({
  selector: 'app-group-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCheckboxModule
  ],
  template: `
    <div class="group-form">
      <h1 class="group-form__title">{{ isEditMode ? 'Edit Group' : 'New Group' }}</h1>

      <mat-card class="group-form__card">
        <form [formGroup]="form" (ngSubmit)="onSubmit()" class="group-form__form">
          <mat-form-field appearance="outline" class="group-form__field">
            <mat-label>Name</mat-label>
            <input matInput formControlName="name" required>
            <mat-error *ngIf="form.get('name')?.hasError('required')">Name is required</mat-error>
          </mat-form-field>

          <mat-form-field appearance="outline" class="group-form__field">
            <mat-label>Description</mat-label>
            <textarea matInput formControlName="description" rows="3"></textarea>
          </mat-form-field>

          <mat-form-field appearance="outline" class="group-form__field">
            <mat-label>Meeting Schedule</mat-label>
            <input matInput formControlName="meetingSchedule">
          </mat-form-field>

          <mat-checkbox formControlName="isActive" class="group-form__checkbox" *ngIf="isEditMode">
            Active
          </mat-checkbox>

          <div class="group-form__actions">
            <button mat-raised-button type="button" (click)="cancel()">Cancel</button>
            <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid">
              {{ isEditMode ? 'Update' : 'Create' }}
            </button>
          </div>
        </form>
      </mat-card>
    </div>
  `,
  styles: [`
    .group-form {
      padding: 2rem;
      max-width: 800px;
      margin: 0 auto;

      &__title {
        margin: 0 0 2rem 0;
        font-size: 2rem;
        font-weight: 400;
      }

      &__card {
        padding: 2rem;
      }

      &__form {
        display: flex;
        flex-direction: column;
        gap: 1rem;
      }

      &__field {
        width: 100%;
      }

      &__checkbox {
        margin-bottom: 1rem;
      }

      &__actions {
        display: flex;
        justify-content: flex-end;
        gap: 1rem;
        margin-top: 1rem;
      }
    }
  `]
})
export class GroupForm implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly groupService = inject(GroupService);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);

  form: FormGroup;
  isEditMode = false;
  groupId: string | null = null;

  constructor() {
    this.form = this.fb.group({
      name: ['', Validators.required],
      description: [''],
      meetingSchedule: [''],
      isActive: [true]
    });
  }

  ngOnInit(): void {
    this.groupId = this.route.snapshot.paramMap.get('id');
    this.isEditMode = !!this.groupId && this.groupId !== 'new';

    if (this.isEditMode && this.groupId) {
      this.groupService.getById(this.groupId).subscribe(group => {
        this.form.patchValue({
          name: group.name,
          description: group.description,
          meetingSchedule: group.meetingSchedule,
          isActive: group.isActive
        });
      });
    }
  }

  onSubmit(): void {
    if (this.form.invalid) return;

    const formValue = this.form.value;

    if (this.isEditMode && this.groupId) {
      this.groupService.update({
        groupId: this.groupId,
        ...formValue
      }).subscribe(() => {
        this.router.navigate(['/groups']);
      });
    } else {
      this.groupService.create({
        createdByUserId: '00000000-0000-0000-0000-000000000000', // TODO: Replace with actual user ID
        name: formValue.name,
        description: formValue.description,
        meetingSchedule: formValue.meetingSchedule
      }).subscribe(() => {
        this.router.navigate(['/groups']);
      });
    }
  }

  cancel(): void {
    this.router.navigate(['/groups']);
  }
}
