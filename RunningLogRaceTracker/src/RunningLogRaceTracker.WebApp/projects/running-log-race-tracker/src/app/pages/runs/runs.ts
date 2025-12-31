import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { RunService } from '../../services';
import { Run, CreateRunRequest, UpdateRunRequest } from '../../models';

@Component({
  selector: 'app-runs',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  templateUrl: './runs.html',
  styleUrl: './runs.scss'
})
export class Runs implements OnInit {
  private readonly _runService = inject(RunService);
  private readonly _dialog = inject(MatDialog);
  private readonly _fb = inject(FormBuilder);

  runs$ = this._runService.runs$;
  displayedColumns = ['completedAt', 'distance', 'durationMinutes', 'averagePace', 'actions'];

  runForm = this._fb.group({
    runId: [''],
    userId: ['00000000-0000-0000-0000-000000000001'],
    distance: [0, [Validators.required, Validators.min(0)]],
    durationMinutes: [0, [Validators.required, Validators.min(0)]],
    completedAt: [new Date(), Validators.required],
    averagePace: [null as number | null],
    averageHeartRate: [null as number | null],
    elevationGain: [null as number | null],
    caloriesBurned: [null as number | null],
    route: [''],
    weather: [''],
    notes: [''],
    effortRating: [null as number | null]
  });

  isEditing = false;

  ngOnInit() {
    this._runService.getRuns().subscribe();
  }

  openDialog(templateRef: any, run?: Run) {
    if (run) {
      this.isEditing = true;
      this.runForm.patchValue({
        ...run,
        completedAt: new Date(run.completedAt)
      });
    } else {
      this.isEditing = false;
      this.runForm.reset({
        userId: '00000000-0000-0000-0000-000000000001',
        distance: 0,
        durationMinutes: 0,
        completedAt: new Date()
      });
    }

    this._dialog.open(templateRef, {
      width: '600px'
    });
  }

  saveRun() {
    if (this.runForm.valid) {
      const formValue = this.runForm.value;

      if (this.isEditing && formValue.runId) {
        const request: UpdateRunRequest = {
          distance: formValue.distance!,
          durationMinutes: formValue.durationMinutes!,
          completedAt: formValue.completedAt!.toISOString(),
          averagePace: formValue.averagePace ?? undefined,
          averageHeartRate: formValue.averageHeartRate ?? undefined,
          elevationGain: formValue.elevationGain ?? undefined,
          caloriesBurned: formValue.caloriesBurned ?? undefined,
          route: formValue.route || undefined,
          weather: formValue.weather || undefined,
          notes: formValue.notes || undefined,
          effortRating: formValue.effortRating ?? undefined
        };

        this._runService.updateRun(formValue.runId, request).subscribe(() => {
          this._dialog.closeAll();
        });
      } else {
        const request: CreateRunRequest = {
          userId: formValue.userId!,
          distance: formValue.distance!,
          durationMinutes: formValue.durationMinutes!,
          completedAt: formValue.completedAt!.toISOString(),
          averagePace: formValue.averagePace ?? undefined,
          averageHeartRate: formValue.averageHeartRate ?? undefined,
          elevationGain: formValue.elevationGain ?? undefined,
          caloriesBurned: formValue.caloriesBurned ?? undefined,
          route: formValue.route || undefined,
          weather: formValue.weather || undefined,
          notes: formValue.notes || undefined,
          effortRating: formValue.effortRating ?? undefined
        };

        this._runService.createRun(request).subscribe(() => {
          this._dialog.closeAll();
        });
      }
    }
  }

  deleteRun(run: Run) {
    if (confirm(`Are you sure you want to delete this run?`)) {
      this._runService.deleteRun(run.runId).subscribe();
    }
  }

  closeDialog() {
    this._dialog.closeAll();
  }
}
