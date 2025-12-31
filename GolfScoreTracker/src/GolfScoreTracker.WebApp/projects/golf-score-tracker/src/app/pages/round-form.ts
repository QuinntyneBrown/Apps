import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RoundsService, CoursesService } from '../services';
import { CreateRoundCommand, UpdateRoundCommand } from '../models';

@Component({
  selector: 'app-round-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterLink,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonModule,
    MatIconModule
  ],
  template: `
    <div class="round-form">
      <div class="round-form__header">
        <button mat-button routerLink="/rounds">
          <mat-icon>arrow_back</mat-icon>
          Back to Rounds
        </button>
      </div>

      <mat-card class="round-form__card">
        <mat-card-header>
          <mat-card-title>{{ isEditMode ? 'Edit Round' : 'New Round' }}</mat-card-title>
        </mat-card-header>

        <mat-card-content>
          <form [formGroup]="roundForm" (ngSubmit)="onSubmit()" class="round-form__form">
            <mat-form-field appearance="outline" class="round-form__field">
              <mat-label>Course</mat-label>
              <mat-select formControlName="courseId" required>
                <mat-option *ngFor="let course of (courses$ | async)" [value]="course.courseId">
                  {{ course.name }}
                </mat-option>
              </mat-select>
              <mat-error *ngIf="roundForm.get('courseId')?.hasError('required')">
                Course is required
              </mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="round-form__field">
              <mat-label>Played Date</mat-label>
              <input matInput [matDatepicker]="picker" formControlName="playedDate" required>
              <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
              <mat-datepicker #picker></mat-datepicker>
              <mat-error *ngIf="roundForm.get('playedDate')?.hasError('required')">
                Date is required
              </mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="round-form__field">
              <mat-label>User ID</mat-label>
              <input matInput formControlName="userId" required>
              <mat-error *ngIf="roundForm.get('userId')?.hasError('required')">
                User ID is required
              </mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="round-form__field">
              <mat-label>Total Score</mat-label>
              <input matInput type="number" formControlName="totalScore" required>
              <mat-error *ngIf="roundForm.get('totalScore')?.hasError('required')">
                Total score is required
              </mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="round-form__field">
              <mat-label>Total Par</mat-label>
              <input matInput type="number" formControlName="totalPar" required>
              <mat-error *ngIf="roundForm.get('totalPar')?.hasError('required')">
                Total par is required
              </mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="round-form__field">
              <mat-label>Weather</mat-label>
              <input matInput formControlName="weather">
            </mat-form-field>

            <mat-form-field appearance="outline" class="round-form__field">
              <mat-label>Notes</mat-label>
              <textarea matInput formControlName="notes" rows="3"></textarea>
            </mat-form-field>

            <div class="round-form__actions">
              <button mat-raised-button type="button" routerLink="/rounds">Cancel</button>
              <button mat-raised-button color="primary" type="submit" [disabled]="!roundForm.valid">
                {{ isEditMode ? 'Update' : 'Create' }}
              </button>
            </div>
          </form>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .round-form {
      padding: 2rem;
      max-width: 800px;
      margin: 0 auto;
    }

    .round-form__header {
      margin-bottom: 1rem;
    }

    .round-form__card {
      margin-top: 1rem;
    }

    .round-form__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
      margin-top: 1rem;
    }

    .round-form__field {
      width: 100%;
    }

    .round-form__actions {
      display: flex;
      gap: 1rem;
      justify-content: flex-end;
      margin-top: 1rem;
    }
  `]
})
export class RoundForm implements OnInit {
  roundForm: FormGroup;
  courses$ = this.coursesService.courses$;
  isEditMode = false;
  private roundId: string | null = null;

  constructor(
    private fb: FormBuilder,
    private roundsService: RoundsService,
    private coursesService: CoursesService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    const defaultUserId = '00000000-0000-0000-0000-000000000000';
    this.roundForm = this.fb.group({
      userId: [defaultUserId, Validators.required],
      courseId: ['', Validators.required],
      playedDate: [new Date(), Validators.required],
      totalScore: [72, Validators.required],
      totalPar: [72, Validators.required],
      weather: [''],
      notes: ['']
    });
  }

  ngOnInit(): void {
    this.coursesService.getCourses().subscribe();

    this.roundId = this.route.snapshot.paramMap.get('id');
    this.isEditMode = !!this.roundId && this.route.snapshot.url.some(segment => segment.path === 'edit');

    if (this.isEditMode && this.roundId) {
      this.roundsService.getRoundById(this.roundId).subscribe(round => {
        this.roundForm.patchValue({
          userId: round.userId,
          courseId: round.courseId,
          playedDate: new Date(round.playedDate),
          totalScore: round.totalScore,
          totalPar: round.totalPar,
          weather: round.weather,
          notes: round.notes
        });
      });
    }
  }

  onSubmit(): void {
    if (this.roundForm.valid) {
      if (this.isEditMode && this.roundId) {
        const command: UpdateRoundCommand = {
          roundId: this.roundId,
          ...this.roundForm.value
        };
        this.roundsService.updateRound(this.roundId, command).subscribe(() => {
          this.router.navigate(['/rounds', this.roundId]);
        });
      } else {
        const command: CreateRoundCommand = this.roundForm.value;
        this.roundsService.createRound(command).subscribe(round => {
          this.router.navigate(['/rounds', round.roundId]);
        });
      }
    }
  }
}
