import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { SessionService } from '../services';
import { SessionType, SessionTypeLabels } from '../models';

@Component({
  selector: 'app-session-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  template: `
    <div class="session-form">
      <mat-card class="session-form__card">
        <mat-card-header>
          <mat-card-title>{{ isEditMode ? 'Edit Session' : 'New Session' }}</mat-card-title>
        </mat-card-header>

        <mat-card-content>
          <form [formGroup]="form" class="session-form__form">
            <mat-form-field class="session-form__field">
              <mat-label>Title</mat-label>
              <input matInput formControlName="title" required>
            </mat-form-field>

            <mat-form-field class="session-form__field">
              <mat-label>Session Type</mat-label>
              <mat-select formControlName="sessionType" required>
                <mat-option *ngFor="let type of sessionTypes" [value]="type.value">
                  {{ type.label }}
                </mat-option>
              </mat-select>
            </mat-form-field>

            <mat-form-field class="session-form__field">
              <mat-label>Session Date</mat-label>
              <input matInput [matDatepicker]="picker" formControlName="sessionDate" required>
              <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
              <mat-datepicker #picker></mat-datepicker>
            </mat-form-field>

            <mat-form-field class="session-form__field">
              <mat-label>Location</mat-label>
              <input matInput formControlName="location">
            </mat-form-field>

            <mat-form-field class="session-form__field">
              <mat-label>Client</mat-label>
              <input matInput formControlName="client">
            </mat-form-field>

            <mat-form-field class="session-form__field">
              <mat-label>Notes</mat-label>
              <textarea matInput formControlName="notes" rows="4"></textarea>
            </mat-form-field>
          </form>
        </mat-card-content>

        <mat-card-actions class="session-form__actions">
          <button mat-button (click)="cancel()">Cancel</button>
          <button mat-raised-button color="primary" (click)="save()" [disabled]="!form.valid">Save</button>
        </mat-card-actions>
      </mat-card>
    </div>
  `,
  styles: [`
    .session-form {
      padding: 2rem;
      max-width: 800px;
      margin: 0 auto;
    }

    .session-form__card {
      width: 100%;
    }

    .session-form__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
      margin-top: 1rem;
    }

    .session-form__field {
      width: 100%;
    }

    .session-form__actions {
      display: flex;
      justify-content: flex-end;
      gap: 0.5rem;
      padding: 1rem;
    }
  `]
})
export class SessionForm implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly sessionService = inject(SessionService);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);

  form: FormGroup;
  isEditMode = false;
  sessionId?: string;

  sessionTypes = Object.keys(SessionType)
    .filter(key => !isNaN(Number(key)))
    .map(key => ({
      value: Number(key),
      label: SessionTypeLabels[Number(key)]
    }));

  constructor() {
    this.form = this.fb.group({
      title: ['', Validators.required],
      sessionType: [SessionType.Portrait, Validators.required],
      sessionDate: [new Date(), Validators.required],
      location: [''],
      client: [''],
      notes: ['']
    });
  }

  ngOnInit(): void {
    this.sessionId = this.route.snapshot.paramMap.get('id') || undefined;
    this.isEditMode = !!this.sessionId;

    if (this.isEditMode && this.sessionId) {
      this.sessionService.getById(this.sessionId).subscribe(session => {
        this.form.patchValue({
          title: session.title,
          sessionType: session.sessionType,
          sessionDate: new Date(session.sessionDate),
          location: session.location,
          client: session.client,
          notes: session.notes
        });
      });
    }
  }

  save(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const sessionDate = new Date(formValue.sessionDate).toISOString();

      if (this.isEditMode && this.sessionId) {
        this.sessionService.update({
          sessionId: this.sessionId,
          ...formValue,
          sessionDate
        }).subscribe(() => {
          this.router.navigate(['/sessions']);
        });
      } else {
        this.sessionService.create({
          userId: '00000000-0000-0000-0000-000000000000', // TODO: Get from auth
          ...formValue,
          sessionDate
        }).subscribe(() => {
          this.router.navigate(['/sessions']);
        });
      }
    }
  }

  cancel(): void {
    this.router.navigate(['/sessions']);
  }
}
