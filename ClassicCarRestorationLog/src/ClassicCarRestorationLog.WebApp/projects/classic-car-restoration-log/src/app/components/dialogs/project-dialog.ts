import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { Project, ProjectPhase, CreateProjectCommand, UpdateProjectCommand } from '../../models';

export interface ProjectDialogData {
  project?: Project;
  userId: string;
}

@Component({
  selector: 'app-project-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  templateUrl: './project-dialog.html',
  styleUrl: './project-dialog.scss'
})
export class ProjectDialog {
  form: FormGroup;
  isEdit: boolean;
  phases = Object.keys(ProjectPhase)
    .filter(key => !isNaN(Number(key)))
    .map(key => ({ value: Number(key), label: ProjectPhase[Number(key)] }));

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<ProjectDialog>,
    @Inject(MAT_DIALOG_DATA) public data: ProjectDialogData
  ) {
    this.isEdit = !!data.project;
    this.form = this.fb.group({
      carMake: [data.project?.carMake || '', Validators.required],
      carModel: [data.project?.carModel || '', Validators.required],
      year: [data.project?.year || null],
      phase: [data.project?.phase ?? ProjectPhase.Planning, Validators.required],
      startDate: [data.project?.startDate ? new Date(data.project.startDate) : new Date()],
      completionDate: [data.project?.completionDate ? new Date(data.project.completionDate) : null],
      estimatedBudget: [data.project?.estimatedBudget || null],
      actualCost: [data.project?.actualCost || null],
      notes: [data.project?.notes || '']
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;

      if (this.isEdit && this.data.project) {
        const command: UpdateProjectCommand = {
          projectId: this.data.project.projectId,
          userId: this.data.userId,
          carMake: formValue.carMake,
          carModel: formValue.carModel,
          year: formValue.year,
          phase: formValue.phase,
          startDate: formValue.startDate instanceof Date ? formValue.startDate.toISOString() : formValue.startDate,
          completionDate: formValue.completionDate instanceof Date ? formValue.completionDate.toISOString() : formValue.completionDate,
          estimatedBudget: formValue.estimatedBudget,
          actualCost: formValue.actualCost,
          notes: formValue.notes
        };
        this.dialogRef.close(command);
      } else {
        const command: CreateProjectCommand = {
          userId: this.data.userId,
          carMake: formValue.carMake,
          carModel: formValue.carModel,
          year: formValue.year,
          phase: formValue.phase,
          startDate: formValue.startDate instanceof Date ? formValue.startDate.toISOString() : formValue.startDate,
          estimatedBudget: formValue.estimatedBudget,
          notes: formValue.notes
        };
        this.dialogRef.close(command);
      }
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
