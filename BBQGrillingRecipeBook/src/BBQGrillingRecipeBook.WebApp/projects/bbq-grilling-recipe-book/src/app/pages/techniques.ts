import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { TechniqueService } from '../services';
import { Technique, CreateTechnique, UpdateTechnique } from '../models';

@Component({
  selector: 'app-technique-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit Technique' : 'New Technique' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="technique-dialog__form">
        <mat-form-field class="technique-dialog__field">
          <mat-label>Name</mat-label>
          <input matInput formControlName="name" required>
        </mat-form-field>

        <mat-form-field class="technique-dialog__field">
          <mat-label>Description</mat-label>
          <textarea matInput formControlName="description" rows="3" required></textarea>
        </mat-form-field>

        <mat-form-field class="technique-dialog__field">
          <mat-label>Category</mat-label>
          <input matInput formControlName="category" required>
        </mat-form-field>

        <mat-form-field class="technique-dialog__field">
          <mat-label>Difficulty Level (1-5)</mat-label>
          <input matInput type="number" formControlName="difficultyLevel" min="1" max="5" required>
        </mat-form-field>

        <mat-form-field class="technique-dialog__field">
          <mat-label>Instructions</mat-label>
          <textarea matInput formControlName="instructions" rows="4" required></textarea>
        </mat-form-field>

        <mat-form-field class="technique-dialog__field">
          <mat-label>Tips</mat-label>
          <textarea matInput formControlName="tips" rows="3"></textarea>
        </mat-form-field>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" [disabled]="!form.valid" (click)="save()">Save</button>
    </mat-dialog-actions>
  `,
  styles: [`
    .technique-dialog__form {
      display: flex;
      flex-direction: column;
      min-width: 500px;
      padding: 1rem 0;
    }

    .technique-dialog__field {
      width: 100%;
    }
  `]
})
export class TechniqueDialog {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialog);

  data: Technique | null = null;
  form: FormGroup;

  constructor() {
    this.form = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      category: ['', Validators.required],
      difficultyLevel: [1, [Validators.required, Validators.min(1), Validators.max(5)]],
      instructions: ['', Validators.required],
      tips: ['']
    });

    if (this.data) {
      this.form.patchValue(this.data);
    }
  }

  save(): void {
    if (this.form.valid) {
      this.dialogRef.closeAll();
    }
  }
}

@Component({
  selector: 'app-techniques',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './techniques.html',
  styleUrl: './techniques.scss'
})
export class Techniques implements OnInit {
  private techniqueService = inject(TechniqueService);
  private dialog = inject(MatDialog);

  techniques$ = this.techniqueService.techniques$;
  displayedColumns = ['name', 'category', 'difficultyLevel', 'favorite', 'actions'];

  ngOnInit(): void {
    this.techniqueService.getTechniques().subscribe();
  }

  openDialog(technique?: Technique): void {
    const dialogRef = this.dialog.open(TechniqueDialog, {
      width: '600px',
      data: technique || null
    });

    const dialogComponent = dialogRef.componentInstance;
    dialogComponent.data = technique || null;

    if (technique) {
      dialogComponent.form.patchValue(technique);
    }

    dialogRef.afterClosed().subscribe(result => {
      if (dialogComponent.form.valid) {
        const formValue = dialogComponent.form.value;

        if (technique) {
          const updateData: UpdateTechnique = {
            techniqueId: technique.techniqueId,
            ...formValue
          };
          this.techniqueService.updateTechnique(updateData).subscribe();
        } else {
          const createData: CreateTechnique = {
            userId: '00000000-0000-0000-0000-000000000000',
            ...formValue
          };
          this.techniqueService.createTechnique(createData).subscribe();
        }
      }
    });
  }

  deleteTechnique(id: string): void {
    if (confirm('Are you sure you want to delete this technique?')) {
      this.techniqueService.deleteTechnique(id).subscribe();
    }
  }
}
