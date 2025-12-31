import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { ResourceService, GroupService } from '../services';

@Component({
  selector: 'app-resource-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule
  ],
  template: `
    <div class="resource-form">
      <h1 class="resource-form__title">{{ isEditMode ? 'Edit Resource' : 'New Resource' }}</h1>

      <mat-card class="resource-form__card">
        <form [formGroup]="form" (ngSubmit)="onSubmit()" class="resource-form__form">
          <mat-form-field appearance="outline" class="resource-form__field" *ngIf="!isEditMode">
            <mat-label>Group</mat-label>
            <mat-select formControlName="groupId" required>
              <mat-option *ngFor="let group of groups$ | async" [value]="group.groupId">
                {{ group.name }}
              </mat-option>
            </mat-select>
            <mat-error *ngIf="form.get('groupId')?.hasError('required')">Group is required</mat-error>
          </mat-form-field>

          <mat-form-field appearance="outline" class="resource-form__field">
            <mat-label>Title</mat-label>
            <input matInput formControlName="title" required>
            <mat-error *ngIf="form.get('title')?.hasError('required')">Title is required</mat-error>
          </mat-form-field>

          <mat-form-field appearance="outline" class="resource-form__field">
            <mat-label>Description</mat-label>
            <textarea matInput formControlName="description" rows="3"></textarea>
          </mat-form-field>

          <mat-form-field appearance="outline" class="resource-form__field">
            <mat-label>URL</mat-label>
            <input matInput formControlName="url" type="url">
          </mat-form-field>

          <mat-form-field appearance="outline" class="resource-form__field">
            <mat-label>Resource Type</mat-label>
            <input matInput formControlName="resourceType" placeholder="e.g., Article, Book, Video">
          </mat-form-field>

          <div class="resource-form__actions">
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
    .resource-form {
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

      &__actions {
        display: flex;
        justify-content: flex-end;
        gap: 1rem;
        margin-top: 1rem;
      }
    }
  `]
})
export class ResourceForm implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly resourceService = inject(ResourceService);
  private readonly groupService = inject(GroupService);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);

  form: FormGroup;
  isEditMode = false;
  resourceId: string | null = null;
  groups$ = this.groupService.groups$;

  constructor() {
    this.form = this.fb.group({
      groupId: ['', Validators.required],
      title: ['', Validators.required],
      description: [''],
      url: [''],
      resourceType: ['']
    });
  }

  ngOnInit(): void {
    this.groupService.getAll().subscribe();

    this.resourceId = this.route.snapshot.paramMap.get('id');
    this.isEditMode = !!this.resourceId && this.resourceId !== 'new';

    if (this.isEditMode && this.resourceId) {
      this.resourceService.getById(this.resourceId).subscribe(resource => {
        this.form.patchValue({
          groupId: resource.groupId,
          title: resource.title,
          description: resource.description,
          url: resource.url,
          resourceType: resource.resourceType
        });
      });
    }
  }

  onSubmit(): void {
    if (this.form.invalid) return;

    const formValue = this.form.value;

    if (this.isEditMode && this.resourceId) {
      this.resourceService.update({
        resourceId: this.resourceId,
        title: formValue.title,
        description: formValue.description,
        url: formValue.url,
        resourceType: formValue.resourceType
      }).subscribe(() => {
        this.router.navigate(['/resources']);
      });
    } else {
      this.resourceService.create({
        groupId: formValue.groupId,
        sharedByUserId: '00000000-0000-0000-0000-000000000000', // TODO: Replace with actual user ID
        title: formValue.title,
        description: formValue.description,
        url: formValue.url,
        resourceType: formValue.resourceType
      }).subscribe(() => {
        this.router.navigate(['/resources']);
      });
    }
  }

  cancel(): void {
    this.router.navigate(['/resources']);
  }
}
