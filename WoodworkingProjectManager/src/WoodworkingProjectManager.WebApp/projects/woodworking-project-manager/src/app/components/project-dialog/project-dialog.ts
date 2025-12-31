import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { Project, ProjectStatus, WoodType } from '../../models';

@Component({
  selector: 'app-project-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  templateUrl: './project-dialog.html',
  styleUrl: './project-dialog.scss'
})
export class ProjectDialog implements OnInit {
  private _fb = inject(FormBuilder);
  private _dialogRef = inject(MatDialogRef<ProjectDialog>);
  data = inject(MAT_DIALOG_DATA);

  projectForm!: FormGroup;
  projectStatuses = Object.values(ProjectStatus);
  woodTypes = Object.values(WoodType);

  ngOnInit(): void {
    this.projectForm = this._fb.group({
      name: [this.data?.name || '', Validators.required],
      description: [this.data?.description || '', Validators.required],
      status: [this.data?.status || ProjectStatus.Planned, Validators.required],
      woodType: [this.data?.woodType || WoodType.Oak, Validators.required],
      startDate: [this.data?.startDate || null],
      completionDate: [this.data?.completionDate || null],
      estimatedCost: [this.data?.estimatedCost || null],
      actualCost: [this.data?.actualCost || null],
      notes: [this.data?.notes || '']
    });
  }

  onSubmit(): void {
    if (this.projectForm.valid) {
      this._dialogRef.close(this.projectForm.value);
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}
