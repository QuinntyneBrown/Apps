import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { RoutineService } from '../services';
import { Routine, CreateRoutineRequest, UpdateRoutineRequest } from '../models';

@Component({
  selector: 'app-routine-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatCheckboxModule,
    MatButtonModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit Routine' : 'New Routine' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="routine-form">
        <mat-form-field class="routine-form__field">
          <mat-label>Name</mat-label>
          <input matInput formControlName="name" required>
        </mat-form-field>

        <mat-form-field class="routine-form__field">
          <mat-label>Description</mat-label>
          <textarea matInput formControlName="description" rows="3"></textarea>
        </mat-form-field>

        <mat-form-field class="routine-form__field">
          <mat-label>Target Start Time (HH:mm)</mat-label>
          <input matInput formControlName="targetStartTime" type="time" required>
        </mat-form-field>

        <mat-form-field class="routine-form__field">
          <mat-label>Estimated Duration (minutes)</mat-label>
          <input matInput formControlName="estimatedDurationMinutes" type="number" required>
        </mat-form-field>

        <mat-checkbox formControlName="isActive" *ngIf="data">Active</mat-checkbox>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" (click)="save()" [disabled]="!form.valid">Save</button>
    </mat-dialog-actions>
  `,
  styles: [`
    .routine-form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
      min-width: 400px;
      padding: 1rem 0;

      &__field {
        width: 100%;
      }
    }
  `]
})
export class RoutineDialog {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialog);
  data?: Routine;

  form: FormGroup;

  constructor() {
    this.form = this.fb.group({
      name: ['', Validators.required],
      description: [''],
      targetStartTime: ['', Validators.required],
      estimatedDurationMinutes: [0, [Validators.required, Validators.min(1)]],
      isActive: [true]
    });

    if (this.data) {
      this.form.patchValue({
        name: this.data.name,
        description: this.data.description,
        targetStartTime: this.data.targetStartTime,
        estimatedDurationMinutes: this.data.estimatedDurationMinutes,
        isActive: this.data.isActive
      });
    }
  }

  save() {
    if (this.form.valid) {
      this.dialogRef.closeAll();
    }
  }
}

@Component({
  selector: 'app-routines',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule
  ],
  template: `
    <div class="routines">
      <div class="routines__header">
        <h1 class="routines__title">Routines</h1>
        <button mat-raised-button color="primary" (click)="openDialog()">
          <mat-icon>add</mat-icon>
          New Routine
        </button>
      </div>

      <div class="routines__table">
        <table mat-table [dataSource]="routines$ | async" class="mat-elevation-z2">
          <ng-container matColumnDef="name">
            <th mat-header-cell *matHeaderCellDef>Name</th>
            <td mat-cell *matCellDef="let routine">{{ routine.name }}</td>
          </ng-container>

          <ng-container matColumnDef="description">
            <th mat-header-cell *matHeaderCellDef>Description</th>
            <td mat-cell *matCellDef="let routine">{{ routine.description || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="targetStartTime">
            <th mat-header-cell *matHeaderCellDef>Start Time</th>
            <td mat-cell *matCellDef="let routine">{{ routine.targetStartTime }}</td>
          </ng-container>

          <ng-container matColumnDef="estimatedDurationMinutes">
            <th mat-header-cell *matHeaderCellDef>Duration (min)</th>
            <td mat-cell *matCellDef="let routine">{{ routine.estimatedDurationMinutes }}</td>
          </ng-container>

          <ng-container matColumnDef="isActive">
            <th mat-header-cell *matHeaderCellDef>Active</th>
            <td mat-cell *matCellDef="let routine">
              <mat-icon [class.active]="routine.isActive">
                {{ routine.isActive ? 'check_circle' : 'cancel' }}
              </mat-icon>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let routine">
              <button mat-icon-button color="primary" (click)="openDialog(routine)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="delete(routine.routineId)">
                <mat-icon>delete</mat-icon>
              </button>
            </td>
          </ng-container>

          <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
          <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
        </table>
      </div>
    </div>
  `,
  styles: [`
    .routines {
      padding: 2rem;

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 2rem;
      }

      &__title {
        margin: 0;
        font-size: 2rem;
        font-weight: 400;
      }

      &__table {
        width: 100%;

        table {
          width: 100%;
        }

        .active {
          color: #4caf50;
        }

        mat-icon:not(.active) {
          color: #f44336;
        }
      }
    }
  `]
})
export class Routines implements OnInit {
  private routineService = inject(RoutineService);
  private dialog = inject(MatDialog);

  routines$ = this.routineService.routines$;
  displayedColumns = ['name', 'description', 'targetStartTime', 'estimatedDurationMinutes', 'isActive', 'actions'];

  ngOnInit() {
    this.routineService.getAll().subscribe();
  }

  openDialog(routine?: Routine) {
    const dialogRef = this.dialog.open(RoutineDialog, {
      width: '500px',
      data: routine
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (routine) {
          this.update(routine.routineId, result);
        } else {
          this.create(result);
        }
      }
    });
  }

  create(request: CreateRoutineRequest) {
    this.routineService.create(request).subscribe();
  }

  update(id: string, request: UpdateRoutineRequest) {
    this.routineService.update(id, request).subscribe();
  }

  delete(id: string) {
    if (confirm('Are you sure you want to delete this routine?')) {
      this.routineService.delete(id).subscribe();
    }
  }
}
