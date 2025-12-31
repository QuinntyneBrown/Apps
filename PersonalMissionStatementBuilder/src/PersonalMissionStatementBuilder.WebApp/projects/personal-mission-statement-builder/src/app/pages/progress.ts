import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { ProgressService, GoalService } from '../services';
import { Progress, CreateProgress, UpdateProgress } from '../models';

@Component({
  selector: 'app-progress-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit' : 'Add' }} Progress Entry</h2>
    <form [formGroup]="form" (ngSubmit)="onSubmit()">
      <mat-dialog-content>
        <mat-form-field class="dialog__field">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="4" required></textarea>
        </mat-form-field>

        <mat-form-field class="dialog__field">
          <mat-label>Completion Percentage</mat-label>
          <input matInput type="number" formControlName="completionPercentage" min="0" max="100" required>
        </mat-form-field>

        <mat-form-field class="dialog__field">
          <mat-label>Progress Date</mat-label>
          <input matInput [matDatepicker]="picker" formControlName="progressDate">
          <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
          <mat-datepicker #picker></mat-datepicker>
        </mat-form-field>
      </mat-dialog-content>
      <mat-dialog-actions align="end">
        <button mat-button type="button" mat-dialog-close>Cancel</button>
        <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid">
          {{ data ? 'Update' : 'Create' }}
        </button>
      </mat-dialog-actions>
    </form>
  `,
  styles: [`
    .dialog__field {
      width: 100%;
      margin-bottom: 1rem;
    }
  `]
})
export class ProgressDialog {
  private fb = inject(FormBuilder);
  data: Progress | null = inject(MatDialog).openDialogs[0]?.componentInstance?.data || null;

  form = this.fb.group({
    notes: [this.data?.notes || '', Validators.required],
    completionPercentage: [this.data?.completionPercentage || 0, [Validators.required, Validators.min(0), Validators.max(100)]],
    progressDate: [this.data?.progressDate || new Date()]
  });

  onSubmit() {
    if (this.form.valid) {
      inject(MatDialog).closeAll();
    }
  }
}

@Component({
  selector: 'app-progress',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatProgressBarModule
  ],
  templateUrl: './progress.html',
  styleUrl: './progress.scss'
})
export class ProgressPage implements OnInit {
  private progressService = inject(ProgressService);
  private goalService = inject(GoalService);
  private dialog = inject(MatDialog);

  progresses$ = this.progressService.progresses$;
  goals$ = this.goalService.goals$;
  displayedColumns = ['progressDate', 'notes', 'completionPercentage', 'createdAt', 'actions'];

  ngOnInit() {
    this.progressService.getAll().subscribe();
    this.goalService.getAll().subscribe();
  }

  openDialog(progress?: Progress) {
    const dialogRef = this.dialog.open(ProgressDialog, {
      width: '500px',
      data: progress
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        if (progress) {
          this.updateProgress(progress.progressId, result);
        } else {
          this.createProgress(result);
        }
      }
    });
  }

  createProgress(data: any) {
    const create: CreateProgress = {
      goalId: '00000000-0000-0000-0000-000000000000', // Replace with actual goal ID
      userId: '00000000-0000-0000-0000-000000000000', // Replace with actual user ID
      notes: data.notes,
      completionPercentage: data.completionPercentage,
      progressDate: data.progressDate
    };
    this.progressService.create(create).subscribe();
  }

  updateProgress(id: string, data: any) {
    const update: UpdateProgress = {
      progressId: id,
      notes: data.notes,
      completionPercentage: data.completionPercentage,
      progressDate: data.progressDate
    };
    this.progressService.update(update).subscribe();
  }

  deleteProgress(id: string) {
    if (confirm('Are you sure you want to delete this progress entry?')) {
      this.progressService.delete(id).subscribe();
    }
  }

  getGoalTitle(goalId: string, goals: any[]): string {
    const goal = goals.find(g => g.goalId === goalId);
    return goal ? goal.title : 'Unknown Goal';
  }
}
