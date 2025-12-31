import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { Material, Project } from '../../models';
import { ProjectService } from '../../services';

@Component({
  selector: 'app-material-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule
  ],
  templateUrl: './material-dialog.html',
  styleUrl: './material-dialog.scss'
})
export class MaterialDialog implements OnInit {
  private _fb = inject(FormBuilder);
  private _dialogRef = inject(MatDialogRef<MaterialDialog>);
  private _projectService = inject(ProjectService);
  data = inject(MAT_DIALOG_DATA);

  materialForm!: FormGroup;
  projects$ = this._projectService.projects$;

  ngOnInit(): void {
    this._projectService.getProjects().subscribe();

    this.materialForm = this._fb.group({
      name: [this.data?.name || '', Validators.required],
      description: [this.data?.description || ''],
      quantity: [this.data?.quantity || 0, [Validators.required, Validators.min(0)]],
      unit: [this.data?.unit || '', Validators.required],
      cost: [this.data?.cost || null],
      supplier: [this.data?.supplier || ''],
      projectId: [this.data?.projectId || null]
    });
  }

  onSubmit(): void {
    if (this.materialForm.valid) {
      this._dialogRef.close(this.materialForm.value);
    }
  }

  onCancel(): void {
    this._dialogRef.close();
  }
}
