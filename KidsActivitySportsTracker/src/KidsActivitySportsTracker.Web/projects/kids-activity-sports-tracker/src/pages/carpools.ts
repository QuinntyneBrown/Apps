import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { CarpoolService } from '../services';
import { Carpool } from '../models';

@Component({
  selector: 'app-carpool-dialog',
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
    <h2 mat-dialog-title>{{ data ? 'Edit Carpool' : 'New Carpool' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="carpool-form">
        <mat-form-field class="carpool-form__field carpool-form__field--full">
          <mat-label>Carpool Name</mat-label>
          <input matInput formControlName="name" required>
        </mat-form-field>

        <mat-form-field class="carpool-form__field">
          <mat-label>Driver Name</mat-label>
          <input matInput formControlName="driverName">
        </mat-form-field>

        <mat-form-field class="carpool-form__field">
          <mat-label>Driver Contact</mat-label>
          <input matInput formControlName="driverContact">
        </mat-form-field>

        <mat-form-field class="carpool-form__field">
          <mat-label>Pickup Time</mat-label>
          <input matInput type="datetime-local" formControlName="pickupTime">
        </mat-form-field>

        <mat-form-field class="carpool-form__field">
          <mat-label>Pickup Location</mat-label>
          <input matInput formControlName="pickupLocation">
        </mat-form-field>

        <mat-form-field class="carpool-form__field">
          <mat-label>Dropoff Time</mat-label>
          <input matInput type="datetime-local" formControlName="dropoffTime">
        </mat-form-field>

        <mat-form-field class="carpool-form__field">
          <mat-label>Dropoff Location</mat-label>
          <input matInput formControlName="dropoffLocation">
        </mat-form-field>

        <mat-form-field class="carpool-form__field carpool-form__field--full">
          <mat-label>Participants</mat-label>
          <input matInput formControlName="participants">
        </mat-form-field>

        <mat-form-field class="carpool-form__field carpool-form__field--full">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="3"></textarea>
        </mat-form-field>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" [disabled]="!form.valid" (click)="onSave()">
        Save
      </button>
    </mat-dialog-actions>
  `,
  styles: [`
    .carpool-form {
      display: grid;
      grid-template-columns: repeat(2, 1fr);
      gap: 16px;
      padding: 16px 0;
    }

    .carpool-form__field {
      width: 100%;
    }

    .carpool-form__field--full {
      grid-column: 1 / -1;
    }
  `]
})
export class CarpoolDialog {
  private readonly fb = inject(FormBuilder);

  data?: Carpool;
  form: FormGroup;

  constructor() {
    this.form = this.fb.group({
      name: ['', Validators.required],
      driverName: [''],
      driverContact: [''],
      pickupTime: [''],
      pickupLocation: [''],
      dropoffTime: [''],
      dropoffLocation: [''],
      participants: [''],
      notes: ['']
    });

    if (this.data) {
      this.form.patchValue({
        name: this.data.name,
        driverName: this.data.driverName,
        driverContact: this.data.driverContact,
        pickupTime: this.data.pickupTime ? this.formatDateTimeLocal(this.data.pickupTime) : '',
        pickupLocation: this.data.pickupLocation,
        dropoffTime: this.data.dropoffTime ? this.formatDateTimeLocal(this.data.dropoffTime) : '',
        dropoffLocation: this.data.dropoffLocation,
        participants: this.data.participants,
        notes: this.data.notes
      });
    }
  }

  formatDateTimeLocal(dateString: string): string {
    const date = new Date(dateString);
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    const hours = String(date.getHours()).padStart(2, '0');
    const minutes = String(date.getMinutes()).padStart(2, '0');
    return `${year}-${month}-${day}T${hours}:${minutes}`;
  }

  onSave(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        pickupTime: formValue.pickupTime ? new Date(formValue.pickupTime).toISOString() : null,
        dropoffTime: formValue.dropoffTime ? new Date(formValue.dropoffTime).toISOString() : null
      };
    }
  }
}

@Component({
  selector: 'app-carpools',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule
  ],
  template: `
    <div class="carpools">
      <div class="carpools__header">
        <h1 class="carpools__title">Carpools</h1>
        <button mat-raised-button color="primary" (click)="openDialog()">
          <mat-icon>add</mat-icon>
          Add Carpool
        </button>
      </div>

      <div class="carpools__table-container">
        <table mat-table [dataSource]="(carpoolService.carpools$ | async) || []" class="carpools__table">
          <ng-container matColumnDef="name">
            <th mat-header-cell *matHeaderCellDef>Name</th>
            <td mat-cell *matCellDef="let carpool">{{ carpool.name }}</td>
          </ng-container>

          <ng-container matColumnDef="driverName">
            <th mat-header-cell *matHeaderCellDef>Driver</th>
            <td mat-cell *matCellDef="let carpool">{{ carpool.driverName || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="driverContact">
            <th mat-header-cell *matHeaderCellDef>Contact</th>
            <td mat-cell *matCellDef="let carpool">{{ carpool.driverContact || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="pickupTime">
            <th mat-header-cell *matHeaderCellDef>Pickup Time</th>
            <td mat-cell *matCellDef="let carpool">
              {{ carpool.pickupTime ? (carpool.pickupTime | date:'short') : '-' }}
            </td>
          </ng-container>

          <ng-container matColumnDef="pickupLocation">
            <th mat-header-cell *matHeaderCellDef>Pickup Location</th>
            <td mat-cell *matCellDef="let carpool">{{ carpool.pickupLocation || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="participants">
            <th mat-header-cell *matHeaderCellDef>Participants</th>
            <td mat-cell *matCellDef="let carpool">{{ carpool.participants || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let carpool">
              <button mat-icon-button (click)="openDialog(carpool)" class="carpools__action-btn">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="deleteCarpool(carpool.carpoolId)" class="carpools__action-btn">
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
    .carpools {
      padding: 24px;
    }

    .carpools__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 24px;
    }

    .carpools__title {
      margin: 0;
      font-size: 32px;
      font-weight: 500;
    }

    .carpools__table-container {
      overflow-x: auto;
    }

    .carpools__table {
      width: 100%;
    }

    .carpools__action-btn {
      margin-right: 8px;
    }
  `]
})
export class Carpools implements OnInit {
  readonly carpoolService = inject(CarpoolService);
  private readonly dialog = inject(MatDialog);

  displayedColumns = ['name', 'driverName', 'driverContact', 'pickupTime', 'pickupLocation', 'participants', 'actions'];

  ngOnInit(): void {
    this.carpoolService.loadCarpools().subscribe();
  }

  openDialog(carpool?: Carpool): void {
    console.log('Open dialog for carpool:', carpool);
  }

  deleteCarpool(id: string): void {
    if (confirm('Are you sure you want to delete this carpool?')) {
      this.carpoolService.deleteCarpool(id).subscribe();
    }
  }
}
