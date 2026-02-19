import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatCardModule } from '@angular/material/card';
import { NeighborService } from '../services';
import { Neighbor, CreateNeighbor, UpdateNeighbor } from '../models';

@Component({
  selector: 'app-neighbor-dialog',
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
    <h2 mat-dialog-title>{{ data ? 'Edit' : 'Add' }} Neighbor</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="neighbor-form">
        <mat-form-field class="neighbor-form__field">
          <mat-label>User ID</mat-label>
          <input matInput formControlName="userId" required>
        </mat-form-field>

        <mat-form-field class="neighbor-form__field">
          <mat-label>Name</mat-label>
          <input matInput formControlName="name" required>
        </mat-form-field>

        <mat-form-field class="neighbor-form__field">
          <mat-label>Address</mat-label>
          <input matInput formControlName="address">
        </mat-form-field>

        <mat-form-field class="neighbor-form__field">
          <mat-label>Contact Info</mat-label>
          <input matInput formControlName="contactInfo">
        </mat-form-field>

        <mat-form-field class="neighbor-form__field">
          <mat-label>Bio</mat-label>
          <textarea matInput formControlName="bio" rows="3"></textarea>
        </mat-form-field>

        <mat-form-field class="neighbor-form__field">
          <mat-label>Interests</mat-label>
          <textarea matInput formControlName="interests" rows="2"></textarea>
        </mat-form-field>

        <mat-checkbox formControlName="isVerified" class="neighbor-form__checkbox">
          Verified
        </mat-checkbox>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" [disabled]="form.invalid" (click)="save()">
        Save
      </button>
    </mat-dialog-actions>
  `,
  styles: [`
    .neighbor-form {
      display: flex;
      flex-direction: column;
      gap: 16px;
      min-width: 400px;

      &__field {
        width: 100%;
      }

      &__checkbox {
        margin-top: 8px;
      }
    }
  `]
})
export class NeighborDialog {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialog);

  data?: Neighbor;
  form: FormGroup;

  constructor() {
    this.form = this.fb.group({
      userId: ['', Validators.required],
      name: ['', Validators.required],
      address: [''],
      contactInfo: [''],
      bio: [''],
      interests: [''],
      isVerified: [false]
    });

    if (this.data) {
      this.form.patchValue(this.data);
    }
  }

  save() {
    if (this.form.valid) {
      const result = this.data
        ? { ...this.form.value, neighborId: this.data.neighborId }
        : this.form.value;
      this.dialogRef.closeAll();
    }
  }
}

@Component({
  selector: 'app-neighbors',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule
  ],
  template: `
    <div class="neighbors">
      <div class="neighbors__header">
        <h1 class="neighbors__title">Neighbors</h1>
        <button mat-raised-button color="primary" (click)="openDialog()">
          <mat-icon>add</mat-icon>
          Add Neighbor
        </button>
      </div>

      <mat-card class="neighbors__card">
        <table mat-table [dataSource]="neighbors$ | async" class="neighbors__table">
          <ng-container matColumnDef="name">
            <th mat-header-cell *matHeaderCellDef>Name</th>
            <td mat-cell *matCellDef="let neighbor">{{ neighbor.name }}</td>
          </ng-container>

          <ng-container matColumnDef="address">
            <th mat-header-cell *matHeaderCellDef>Address</th>
            <td mat-cell *matCellDef="let neighbor">{{ neighbor.address || 'N/A' }}</td>
          </ng-container>

          <ng-container matColumnDef="contactInfo">
            <th mat-header-cell *matHeaderCellDef>Contact Info</th>
            <td mat-cell *matCellDef="let neighbor">{{ neighbor.contactInfo || 'N/A' }}</td>
          </ng-container>

          <ng-container matColumnDef="interests">
            <th mat-header-cell *matHeaderCellDef>Interests</th>
            <td mat-cell *matCellDef="let neighbor">{{ neighbor.interests || 'N/A' }}</td>
          </ng-container>

          <ng-container matColumnDef="isVerified">
            <th mat-header-cell *matHeaderCellDef>Verified</th>
            <td mat-cell *matCellDef="let neighbor">
              <mat-icon [class.verified]="neighbor.isVerified">
                {{ neighbor.isVerified ? 'check_circle' : 'cancel' }}
              </mat-icon>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let neighbor">
              <button mat-icon-button color="primary" (click)="openDialog(neighbor)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="delete(neighbor.neighborId)">
                <mat-icon>delete</mat-icon>
              </button>
            </td>
          </ng-container>

          <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
          <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
        </table>
      </mat-card>
    </div>
  `,
  styles: [`
    .neighbors {
      padding: 24px;

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 24px;
      }

      &__title {
        margin: 0;
        font-size: 2rem;
        font-weight: 400;
      }

      &__card {
        overflow-x: auto;
      }

      &__table {
        width: 100%;

        .verified {
          color: #4caf50;
        }

        mat-icon:not(.verified) {
          color: #f44336;
        }
      }
    }
  `]
})
export class Neighbors implements OnInit {
  private neighborService = inject(NeighborService);
  private dialog = inject(MatDialog);

  neighbors$ = this.neighborService.neighbors$;
  displayedColumns = ['name', 'address', 'contactInfo', 'interests', 'isVerified', 'actions'];

  ngOnInit() {
    this.neighborService.getAll().subscribe();
  }

  openDialog(neighbor?: Neighbor) {
    const dialogRef = this.dialog.open(NeighborDialog, {
      width: '500px',
      data: neighbor
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (neighbor) {
          this.neighborService.update(result as UpdateNeighbor).subscribe();
        } else {
          this.neighborService.create(result as CreateNeighbor).subscribe();
        }
      }
    });
  }

  delete(id: string) {
    if (confirm('Are you sure you want to delete this neighbor?')) {
      this.neighborService.delete(id).subscribe();
    }
  }
}
