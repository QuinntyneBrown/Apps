import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { PhotoService, SessionService } from '../services';

@Component({
  selector: 'app-photo-form',
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
    <div class="photo-form">
      <mat-card class="photo-form__card">
        <mat-card-header>
          <mat-card-title>{{ isEditMode ? 'Edit Photo' : 'New Photo' }}</mat-card-title>
        </mat-card-header>

        <mat-card-content>
          <form [formGroup]="form" class="photo-form__form">
            <mat-form-field class="photo-form__field">
              <mat-label>Session</mat-label>
              <mat-select formControlName="sessionId" required>
                <mat-option *ngFor="let session of (sessions$ | async) || []" [value]="session.sessionId">
                  {{ session.title }}
                </mat-option>
              </mat-select>
            </mat-form-field>

            <mat-form-field class="photo-form__field">
              <mat-label>File Name</mat-label>
              <input matInput formControlName="fileName" required>
            </mat-form-field>

            <mat-form-field class="photo-form__field">
              <mat-label>File Path</mat-label>
              <input matInput formControlName="filePath">
            </mat-form-field>

            <mat-form-field class="photo-form__field">
              <mat-label>Camera Settings</mat-label>
              <input matInput formControlName="cameraSettings">
            </mat-form-field>

            <mat-form-field class="photo-form__field">
              <mat-label>Rating (1-5)</mat-label>
              <input matInput type="number" formControlName="rating" min="1" max="5">
            </mat-form-field>

            <mat-form-field class="photo-form__field">
              <mat-label>Tags</mat-label>
              <input matInput formControlName="tags">
            </mat-form-field>
          </form>
        </mat-card-content>

        <mat-card-actions class="photo-form__actions">
          <button mat-button (click)="cancel()">Cancel</button>
          <button mat-raised-button color="primary" (click)="save()" [disabled]="!form.valid">Save</button>
        </mat-card-actions>
      </mat-card>
    </div>
  `,
  styles: [`
    .photo-form {
      padding: 2rem;
      max-width: 800px;
      margin: 0 auto;
    }

    .photo-form__card {
      width: 100%;
    }

    .photo-form__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
      margin-top: 1rem;
    }

    .photo-form__field {
      width: 100%;
    }

    .photo-form__actions {
      display: flex;
      justify-content: flex-end;
      gap: 0.5rem;
      padding: 1rem;
    }
  `]
})
export class PhotoForm implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly photoService = inject(PhotoService);
  private readonly sessionService = inject(SessionService);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);

  form: FormGroup;
  isEditMode = false;
  photoId?: string;

  sessions$ = this.sessionService.sessions$;

  constructor() {
    this.form = this.fb.group({
      sessionId: ['', Validators.required],
      fileName: ['', Validators.required],
      filePath: [''],
      cameraSettings: [''],
      rating: [null],
      tags: ['']
    });
  }

  ngOnInit(): void {
    this.sessionService.getAll().subscribe();

    this.photoId = this.route.snapshot.paramMap.get('id') || undefined;
    this.isEditMode = !!this.photoId;

    if (this.isEditMode && this.photoId) {
      this.photoService.getById(this.photoId).subscribe(photo => {
        this.form.patchValue({
          sessionId: photo.sessionId,
          fileName: photo.fileName,
          filePath: photo.filePath,
          cameraSettings: photo.cameraSettings,
          rating: photo.rating,
          tags: photo.tags
        });
      });
    }
  }

  save(): void {
    if (this.form.valid) {
      const formValue = this.form.value;

      if (this.isEditMode && this.photoId) {
        this.photoService.update({
          photoId: this.photoId,
          ...formValue
        }).subscribe(() => {
          this.router.navigate(['/photos']);
        });
      } else {
        this.photoService.create({
          userId: '00000000-0000-0000-0000-000000000000', // TODO: Get from auth
          ...formValue
        }).subscribe(() => {
          this.router.navigate(['/photos']);
        });
      }
    }
  }

  cancel(): void {
    this.router.navigate(['/photos']);
  }
}
