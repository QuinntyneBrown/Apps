import { Component, OnInit, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatCardModule } from '@angular/material/card';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { ClientService } from '../services';
import { Client } from '../models';

@Component({
  selector: 'app-clients',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatCheckboxModule,
    MatCardModule,
    MatSnackBarModule
  ],
  template: `
    <div class="clients">
      <div class="clients__header">
        <h1 class="clients__title">Clients</h1>
        <button mat-raised-button color="primary" (click)="openDialog()" class="clients__add-btn">
          <mat-icon>add</mat-icon>
          Add Client
        </button>
      </div>

      <mat-card class="clients__card">
        <table mat-table [dataSource]="clients$ | async" class="clients__table">
          <ng-container matColumnDef="name">
            <th mat-header-cell *matHeaderCellDef>Name</th>
            <td mat-cell *matCellDef="let client">{{ client.name }}</td>
          </ng-container>

          <ng-container matColumnDef="companyName">
            <th mat-header-cell *matHeaderCellDef>Company</th>
            <td mat-cell *matCellDef="let client">{{ client.companyName || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="email">
            <th mat-header-cell *matHeaderCellDef>Email</th>
            <td mat-cell *matCellDef="let client">{{ client.email || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="phone">
            <th mat-header-cell *matHeaderCellDef>Phone</th>
            <td mat-cell *matCellDef="let client">{{ client.phone || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="isActive">
            <th mat-header-cell *matHeaderCellDef>Status</th>
            <td mat-cell *matCellDef="let client">
              <span [class.clients__status--active]="client.isActive"
                    [class.clients__status--inactive]="!client.isActive">
                {{ client.isActive ? 'Active' : 'Inactive' }}
              </span>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let client">
              <button mat-icon-button color="primary" (click)="openDialog(client)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="deleteClient(client)">
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
    .clients {
      padding: 2rem;
    }

    .clients__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .clients__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 500;
    }

    .clients__add-btn {
      display: flex;
      gap: 0.5rem;
    }

    .clients__card {
      padding: 0;
    }

    .clients__table {
      width: 100%;
    }

    .clients__status--active {
      color: #4caf50;
      font-weight: 500;
    }

    .clients__status--inactive {
      color: #f44336;
      font-weight: 500;
    }
  `]
})
export class Clients implements OnInit {
  displayedColumns: string[] = ['name', 'companyName', 'email', 'phone', 'isActive', 'actions'];
  clients$ = this.clientService.clients$;
  private userId = '00000000-0000-0000-0000-000000000000'; // TODO: Get from auth service

  constructor(
    private clientService: ClientService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.loadClients();
  }

  loadClients(): void {
    this.clientService.getClients(this.userId).subscribe();
  }

  openDialog(client?: Client): void {
    const dialogRef = this.dialog.open(ClientDialog, {
      width: '600px',
      data: client
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (client) {
          this.updateClient(client.clientId, result);
        } else {
          this.createClient(result);
        }
      }
    });
  }

  createClient(data: any): void {
    this.clientService.createClient({ ...data, userId: this.userId }).subscribe({
      next: () => {
        this.snackBar.open('Client created successfully', 'Close', { duration: 3000 });
      },
      error: () => {
        this.snackBar.open('Error creating client', 'Close', { duration: 3000 });
      }
    });
  }

  updateClient(id: string, data: any): void {
    this.clientService.updateClient(id, { ...data, clientId: id, userId: this.userId }).subscribe({
      next: () => {
        this.snackBar.open('Client updated successfully', 'Close', { duration: 3000 });
      },
      error: () => {
        this.snackBar.open('Error updating client', 'Close', { duration: 3000 });
      }
    });
  }

  deleteClient(client: Client): void {
    if (confirm(`Are you sure you want to delete ${client.name}?`)) {
      this.clientService.deleteClient(client.clientId, this.userId).subscribe({
        next: () => {
          this.snackBar.open('Client deleted successfully', 'Close', { duration: 3000 });
        },
        error: () => {
          this.snackBar.open('Error deleting client', 'Close', { duration: 3000 });
        }
      });
    }
  }
}

@Component({
  selector: 'app-client-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCheckboxModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit Client' : 'Add Client' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="client-dialog__form">
        <mat-form-field appearance="outline" class="client-dialog__field">
          <mat-label>Name</mat-label>
          <input matInput formControlName="name" required>
        </mat-form-field>

        <mat-form-field appearance="outline" class="client-dialog__field">
          <mat-label>Company Name</mat-label>
          <input matInput formControlName="companyName">
        </mat-form-field>

        <mat-form-field appearance="outline" class="client-dialog__field">
          <mat-label>Email</mat-label>
          <input matInput type="email" formControlName="email">
        </mat-form-field>

        <mat-form-field appearance="outline" class="client-dialog__field">
          <mat-label>Phone</mat-label>
          <input matInput formControlName="phone">
        </mat-form-field>

        <mat-form-field appearance="outline" class="client-dialog__field">
          <mat-label>Address</mat-label>
          <textarea matInput formControlName="address" rows="3"></textarea>
        </mat-form-field>

        <mat-form-field appearance="outline" class="client-dialog__field">
          <mat-label>Website</mat-label>
          <input matInput formControlName="website">
        </mat-form-field>

        <mat-form-field appearance="outline" class="client-dialog__field">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="3"></textarea>
        </mat-form-field>

        <mat-checkbox formControlName="isActive" *ngIf="data">Active</mat-checkbox>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" [disabled]="!form.valid" (click)="save()">Save</button>
    </mat-dialog-actions>
  `,
  styles: [`
    .client-dialog__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
      min-width: 500px;
    }

    .client-dialog__field {
      width: 100%;
    }
  `]
})
export class ClientDialog {
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<ClientDialog>,
    @Inject(MAT_DIALOG_DATA) public data: Client
  ) {
    this.form = this.fb.group({
      name: [data?.name || '', Validators.required],
      companyName: [data?.companyName || ''],
      email: [data?.email || '', Validators.email],
      phone: [data?.phone || ''],
      address: [data?.address || ''],
      website: [data?.website || ''],
      notes: [data?.notes || ''],
      isActive: [data?.isActive ?? true]
    });
  }

  save(): void {
    if (this.form.valid) {
      this.dialogRef.close(this.form.value);
    }
  }
}
