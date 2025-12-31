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
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { TrainingPlanService, RaceService } from '../../services';
import { TrainingPlan, CreateTrainingPlanRequest, UpdateTrainingPlanRequest } from '../../models';

@Component({
  selector: 'app-training-plans',
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
    MatSelectModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  templateUrl: './training-plans.html',
  styleUrl: './training-plans.scss'
})
export class TrainingPlans implements OnInit {
  private readonly _trainingPlanService = inject(TrainingPlanService);
  private readonly _raceService = inject(RaceService);
  private readonly _dialog = inject(MatDialog);
  private readonly _fb = inject(FormBuilder);

  trainingPlans$ = this._trainingPlanService.trainingPlans$;
  races$ = this._raceService.races$;
  displayedColumns = ['name', 'startDate', 'endDate', 'weeklyMileageGoal', 'isActive', 'actions'];

  trainingPlanForm = this._fb.group({
    trainingPlanId: [''],
    userId: ['00000000-0000-0000-0000-000000000001'],
    name: ['', Validators.required],
    raceId: [null as string | null],
    startDate: [new Date(), Validators.required],
    endDate: [new Date(), Validators.required],
    weeklyMileageGoal: [null as number | null],
    planDetails: [''],
    isActive: [false],
    notes: ['']
  });

  isEditing = false;

  ngOnInit() {
    this._trainingPlanService.getTrainingPlans().subscribe();
    this._raceService.getRaces().subscribe();
  }

  openDialog(templateRef: any, plan?: TrainingPlan) {
    if (plan) {
      this.isEditing = true;
      this.trainingPlanForm.patchValue({
        ...plan,
        startDate: new Date(plan.startDate),
        endDate: new Date(plan.endDate)
      });
    } else {
      this.isEditing = false;
      this.trainingPlanForm.reset({
        userId: '00000000-0000-0000-0000-000000000001',
        name: '',
        raceId: null,
        startDate: new Date(),
        endDate: new Date(),
        weeklyMileageGoal: null,
        planDetails: '',
        isActive: false,
        notes: ''
      });
    }

    this._dialog.open(templateRef, {
      width: '600px'
    });
  }

  saveTrainingPlan() {
    if (this.trainingPlanForm.valid) {
      const formValue = this.trainingPlanForm.value;

      if (this.isEditing && formValue.trainingPlanId) {
        const request: UpdateTrainingPlanRequest = {
          name: formValue.name!,
          raceId: formValue.raceId || undefined,
          startDate: formValue.startDate!.toISOString(),
          endDate: formValue.endDate!.toISOString(),
          weeklyMileageGoal: formValue.weeklyMileageGoal ?? undefined,
          planDetails: formValue.planDetails || undefined,
          isActive: formValue.isActive!,
          notes: formValue.notes || undefined
        };

        this._trainingPlanService.updateTrainingPlan(formValue.trainingPlanId, request).subscribe(() => {
          this._dialog.closeAll();
        });
      } else {
        const request: CreateTrainingPlanRequest = {
          userId: formValue.userId!,
          name: formValue.name!,
          raceId: formValue.raceId || undefined,
          startDate: formValue.startDate!.toISOString(),
          endDate: formValue.endDate!.toISOString(),
          weeklyMileageGoal: formValue.weeklyMileageGoal ?? undefined,
          planDetails: formValue.planDetails || undefined,
          isActive: formValue.isActive!,
          notes: formValue.notes || undefined
        };

        this._trainingPlanService.createTrainingPlan(request).subscribe(() => {
          this._dialog.closeAll();
        });
      }
    }
  }

  deleteTrainingPlan(plan: TrainingPlan) {
    if (confirm(`Are you sure you want to delete "${plan.name}"?`)) {
      this._trainingPlanService.deleteTrainingPlan(plan.trainingPlanId).subscribe();
    }
  }

  closeDialog() {
    this._dialog.closeAll();
  }
}
