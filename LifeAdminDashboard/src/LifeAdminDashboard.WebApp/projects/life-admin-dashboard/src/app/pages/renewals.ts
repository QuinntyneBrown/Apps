import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule } from '@angular/material/chips';
import { provideNativeDateAdapter } from '@angular/material/core';
import { RenewalService } from '../services';
import { Renewal, CreateRenewal, UpdateRenewal } from '../models';

@Component({
  selector: 'app-renewals',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatCheckboxModule,
    MatChipsModule
  ],
  providers: [provideNativeDateAdapter()],
  template: `
    <div class="renewals">
      <div class="renewals__header">
        <h1 class="renewals__title">Renewals</h1>
        <button mat-raised-button color="primary" (click)="showForm = !showForm" class="renewals__add-btn">
          <mat-icon>{{ showForm ? 'close' : 'add' }}</mat-icon>
          {{ showForm ? 'Cancel' : 'Add Renewal' }}
        </button>
      </div>

      <mat-card *ngIf="showForm" class="renewals__form-card">
        <mat-card-header>
          <mat-card-title>{{ editingRenewal ? 'Edit' : 'Create' }} Renewal</mat-card-title>
        </mat-card-header>
        <mat-card-content>
          <form [formGroup]="renewalForm" class="renewals__form">
            <mat-form-field class="renewals__form-field">
              <mat-label>Name</mat-label>
              <input matInput formControlName="name" required>
            </mat-form-field>

            <mat-form-field class="renewals__form-field">
              <mat-label>Renewal Type</mat-label>
              <input matInput formControlName="renewalType" required>
            </mat-form-field>

            <mat-form-field class="renewals__form-field">
              <mat-label>Provider</mat-label>
              <input matInput formControlName="provider">
            </mat-form-field>

            <mat-form-field class="renewals__form-field">
              <mat-label>Renewal Date</mat-label>
              <input matInput [matDatepicker]="picker" formControlName="renewalDate" required>
              <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
              <mat-datepicker #picker></mat-datepicker>
            </mat-form-field>

            <mat-form-field class="renewals__form-field">
              <mat-label>Cost</mat-label>
              <input matInput type="number" formControlName="cost" step="0.01" min="0">
            </mat-form-field>

            <mat-form-field class="renewals__form-field">
              <mat-label>Frequency</mat-label>
              <input matInput formControlName="frequency" required placeholder="e.g., Annual, Monthly">
            </mat-form-field>

            <div class="renewals__form-checkbox">
              <mat-checkbox formControlName="isAutoRenewal">Auto Renewal</mat-checkbox>
            </div>

            <div class="renewals__form-checkbox" *ngIf="editingRenewal">
              <mat-checkbox formControlName="isActive">Is Active</mat-checkbox>
            </div>

            <mat-form-field class="renewals__form-field">
              <mat-label>Notes</mat-label>
              <textarea matInput formControlName="notes" rows="3"></textarea>
            </mat-form-field>
          </form>
        </mat-card-content>
        <mat-card-actions>
          <button mat-raised-button color="primary" (click)="saveRenewal()" [disabled]="renewalForm.invalid">
            {{ editingRenewal ? 'Update' : 'Create' }}
          </button>
          <button mat-button (click)="cancelEdit()">Cancel</button>
        </mat-card-actions>
      </mat-card>

      <mat-card class="renewals__table-card">
        <table mat-table [dataSource]="renewals$ | async" class="renewals__table">
          <ng-container matColumnDef="name">
            <th mat-header-cell *matHeaderCellDef>Name</th>
            <td mat-cell *matCellDef="let renewal">{{ renewal.name }}</td>
          </ng-container>

          <ng-container matColumnDef="type">
            <th mat-header-cell *matHeaderCellDef>Type</th>
            <td mat-cell *matCellDef="let renewal">{{ renewal.renewalType }}</td>
          </ng-container>

          <ng-container matColumnDef="provider">
            <th mat-header-cell *matHeaderCellDef>Provider</th>
            <td mat-cell *matCellDef="let renewal">{{ renewal.provider || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="renewalDate">
            <th mat-header-cell *matHeaderCellDef>Renewal Date</th>
            <td mat-cell *matCellDef="let renewal">{{ renewal.renewalDate | date: 'short' }}</td>
          </ng-container>

          <ng-container matColumnDef="cost">
            <th mat-header-cell *matHeaderCellDef>Cost</th>
            <td mat-cell *matCellDef="let renewal">
              {{ renewal.cost != null ? (renewal.cost | currency) : '-' }}
            </td>
          </ng-container>

          <ng-container matColumnDef="frequency">
            <th mat-header-cell *matHeaderCellDef>Frequency</th>
            <td mat-cell *matCellDef="let renewal">{{ renewal.frequency }}</td>
          </ng-container>

          <ng-container matColumnDef="autoRenewal">
            <th mat-header-cell *matHeaderCellDef>Auto Renewal</th>
            <td mat-cell *matCellDef="let renewal">
              <mat-chip [class]="renewal.isAutoRenewal ? 'auto-renewal-yes' : 'auto-renewal-no'">
                {{ renewal.isAutoRenewal ? 'Yes' : 'No' }}
              </mat-chip>
            </td>
          </ng-container>

          <ng-container matColumnDef="status">
            <th mat-header-cell *matHeaderCellDef>Status</th>
            <td mat-cell *matCellDef="let renewal">
              <mat-chip [class]="renewal.isActive ? 'status-active' : 'status-inactive'">
                {{ renewal.isActive ? 'Active' : 'Inactive' }}
              </mat-chip>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let renewal">
              <button mat-icon-button (click)="editRenewal(renewal)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button (click)="deleteRenewal(renewal)" color="warn">
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
    .renewals {
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
        font-weight: 500;
      }

      &__add-btn {
        display: flex;
        align-items: center;
        gap: 0.5rem;
      }

      &__form-card {
        margin-bottom: 2rem;
      }

      &__form {
        display: flex;
        flex-direction: column;
        gap: 1rem;
      }

      &__form-field {
        width: 100%;
      }

      &__form-checkbox {
        margin: 0.5rem 0;
      }

      &__table-card {
        overflow-x: auto;
      }

      &__table {
        width: 100%;
      }
    }

    .auto-renewal-yes { background-color: #e8f5e9; color: #388e3c; }
    .auto-renewal-no { background-color: #fff3e0; color: #f57c00; }
    .status-active { background-color: #e3f2fd; color: #1976d2; }
    .status-inactive { background-color: #f5f5f5; color: #757575; }
  `]
})
export class Renewals implements OnInit {
  private renewalService = inject(RenewalService);
  private fb = inject(FormBuilder);

  renewals$ = this.renewalService.renewals$;
  showForm = false;
  editingRenewal: Renewal | null = null;

  displayedColumns = ['name', 'type', 'provider', 'renewalDate', 'cost', 'frequency', 'autoRenewal', 'status', 'actions'];

  renewalForm: FormGroup = this.fb.group({
    name: ['', Validators.required],
    renewalType: ['', Validators.required],
    provider: [''],
    renewalDate: ['', Validators.required],
    cost: [''],
    frequency: ['', Validators.required],
    isAutoRenewal: [false],
    isActive: [true],
    notes: ['']
  });

  ngOnInit(): void {
    this.renewalService.getAll().subscribe();
  }

  saveRenewal(): void {
    if (this.renewalForm.invalid) return;

    const formValue = this.renewalForm.value;
    const renewalData = {
      ...formValue,
      renewalDate: new Date(formValue.renewalDate).toISOString(),
      cost: formValue.cost ? parseFloat(formValue.cost) : undefined,
      userId: '00000000-0000-0000-0000-000000000000' // Placeholder user ID
    };

    if (this.editingRenewal) {
      const updateRenewal: UpdateRenewal = {
        ...renewalData,
        renewalId: this.editingRenewal.renewalId
      };
      this.renewalService.update(updateRenewal).subscribe(() => {
        this.cancelEdit();
      });
    } else {
      const createRenewal: CreateRenewal = renewalData;
      this.renewalService.create(createRenewal).subscribe(() => {
        this.cancelEdit();
      });
    }
  }

  editRenewal(renewal: Renewal): void {
    this.editingRenewal = renewal;
    this.showForm = true;
    this.renewalForm.patchValue({
      name: renewal.name,
      renewalType: renewal.renewalType,
      provider: renewal.provider,
      renewalDate: new Date(renewal.renewalDate),
      cost: renewal.cost,
      frequency: renewal.frequency,
      isAutoRenewal: renewal.isAutoRenewal,
      isActive: renewal.isActive,
      notes: renewal.notes
    });
  }

  deleteRenewal(renewal: Renewal): void {
    if (confirm(`Are you sure you want to delete "${renewal.name}"?`)) {
      this.renewalService.delete(renewal.renewalId).subscribe();
    }
  }

  cancelEdit(): void {
    this.editingRenewal = null;
    this.showForm = false;
    this.renewalForm.reset({
      isAutoRenewal: false,
      isActive: true
    });
  }
}
