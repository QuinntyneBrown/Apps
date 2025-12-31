import { Component, OnInit, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatCardModule } from '@angular/material/card';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { InvoiceService, ClientService, ProjectService } from '../services';
import { Invoice, Client, Project } from '../models';

@Component({
  selector: 'app-invoices',
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
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatCardModule,
    MatSnackBarModule
  ],
  template: `
    <div class="invoices">
      <div class="invoices__header">
        <h1 class="invoices__title">Invoices</h1>
        <button mat-raised-button color="primary" (click)="openDialog()" class="invoices__add-btn">
          <mat-icon>add</mat-icon>
          Add Invoice
        </button>
      </div>

      <mat-card class="invoices__card">
        <table mat-table [dataSource]="invoices$ | async" class="invoices__table">
          <ng-container matColumnDef="invoiceNumber">
            <th mat-header-cell *matHeaderCellDef>Invoice #</th>
            <td mat-cell *matCellDef="let invoice">{{ invoice.invoiceNumber }}</td>
          </ng-container>

          <ng-container matColumnDef="invoiceDate">
            <th mat-header-cell *matHeaderCellDef>Invoice Date</th>
            <td mat-cell *matCellDef="let invoice">{{ invoice.invoiceDate | date:'shortDate' }}</td>
          </ng-container>

          <ng-container matColumnDef="dueDate">
            <th mat-header-cell *matHeaderCellDef>Due Date</th>
            <td mat-cell *matCellDef="let invoice">{{ invoice.dueDate | date:'shortDate' }}</td>
          </ng-container>

          <ng-container matColumnDef="totalAmount">
            <th mat-header-cell *matHeaderCellDef>Amount</th>
            <td mat-cell *matCellDef="let invoice">{{ invoice.currency }} {{ invoice.totalAmount | number:'1.2-2' }}</td>
          </ng-container>

          <ng-container matColumnDef="status">
            <th mat-header-cell *matHeaderCellDef>Status</th>
            <td mat-cell *matCellDef="let invoice">
              <span [class]="'invoices__status invoices__status--' + invoice.status.toLowerCase()">
                {{ invoice.status }}
              </span>
            </td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let invoice">
              <button mat-icon-button color="primary" (click)="openDialog(invoice)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="deleteInvoice(invoice)">
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
    .invoices {
      padding: 2rem;
    }

    .invoices__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .invoices__title {
      margin: 0;
      font-size: 2rem;
      font-weight: 500;
    }

    .invoices__add-btn {
      display: flex;
      gap: 0.5rem;
    }

    .invoices__card {
      padding: 0;
    }

    .invoices__table {
      width: 100%;
    }

    .invoices__status {
      padding: 0.25rem 0.5rem;
      border-radius: 4px;
      font-weight: 500;
    }

    .invoices__status--draft {
      background-color: #e0e0e0;
      color: #424242;
    }

    .invoices__status--sent {
      background-color: #e3f2fd;
      color: #1976d2;
    }

    .invoices__status--paid {
      background-color: #e8f5e9;
      color: #388e3c;
    }

    .invoices__status--overdue {
      background-color: #ffebee;
      color: #d32f2f;
    }

    .invoices__status--cancelled {
      background-color: #fafafa;
      color: #757575;
    }
  `]
})
export class Invoices implements OnInit {
  displayedColumns: string[] = ['invoiceNumber', 'invoiceDate', 'dueDate', 'totalAmount', 'status', 'actions'];
  invoices$ = this.invoiceService.invoices$;
  private userId = '00000000-0000-0000-0000-000000000000';

  constructor(
    private invoiceService: InvoiceService,
    private clientService: ClientService,
    private projectService: ProjectService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.loadInvoices();
    this.loadClients();
    this.loadProjects();
  }

  loadInvoices(): void {
    this.invoiceService.getInvoices(this.userId).subscribe();
  }

  loadClients(): void {
    this.clientService.getClients(this.userId).subscribe();
  }

  loadProjects(): void {
    this.projectService.getProjects(this.userId).subscribe();
  }

  openDialog(invoice?: Invoice): void {
    const dialogRef = this.dialog.open(InvoiceDialog, {
      width: '700px',
      data: invoice
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (invoice) {
          this.updateInvoice(invoice.invoiceId, result);
        } else {
          this.createInvoice(result);
        }
      }
    });
  }

  createInvoice(data: any): void {
    this.invoiceService.createInvoice({ ...data, userId: this.userId }).subscribe({
      next: () => {
        this.snackBar.open('Invoice created successfully', 'Close', { duration: 3000 });
      },
      error: () => {
        this.snackBar.open('Error creating invoice', 'Close', { duration: 3000 });
      }
    });
  }

  updateInvoice(id: string, data: any): void {
    this.invoiceService.updateInvoice(id, { ...data, invoiceId: id, userId: this.userId }).subscribe({
      next: () => {
        this.snackBar.open('Invoice updated successfully', 'Close', { duration: 3000 });
      },
      error: () => {
        this.snackBar.open('Error updating invoice', 'Close', { duration: 3000 });
      }
    });
  }

  deleteInvoice(invoice: Invoice): void {
    if (confirm(`Are you sure you want to delete invoice ${invoice.invoiceNumber}?`)) {
      this.invoiceService.deleteInvoice(invoice.invoiceId, this.userId).subscribe({
        next: () => {
          this.snackBar.open('Invoice deleted successfully', 'Close', { duration: 3000 });
        },
        error: () => {
          this.snackBar.open('Error deleting invoice', 'Close', { duration: 3000 });
        }
      });
    }
  }
}

@Component({
  selector: 'app-invoice-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit Invoice' : 'Add Invoice' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="invoice-dialog__form">
        <mat-form-field appearance="outline" class="invoice-dialog__field">
          <mat-label>Client</mat-label>
          <mat-select formControlName="clientId" required>
            <mat-option *ngFor="let client of clients$ | async" [value]="client.clientId">
              {{ client.name }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field appearance="outline" class="invoice-dialog__field">
          <mat-label>Project (Optional)</mat-label>
          <mat-select formControlName="projectId">
            <mat-option [value]="null">None</mat-option>
            <mat-option *ngFor="let project of projects$ | async" [value]="project.projectId">
              {{ project.name }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field appearance="outline" class="invoice-dialog__field">
          <mat-label>Invoice Number</mat-label>
          <input matInput formControlName="invoiceNumber" required>
        </mat-form-field>

        <mat-form-field appearance="outline" class="invoice-dialog__field">
          <mat-label>Invoice Date</mat-label>
          <input matInput [matDatepicker]="invoicePicker" formControlName="invoiceDate" required>
          <mat-datepicker-toggle matSuffix [for]="invoicePicker"></mat-datepicker-toggle>
          <mat-datepicker #invoicePicker></mat-datepicker>
        </mat-form-field>

        <mat-form-field appearance="outline" class="invoice-dialog__field">
          <mat-label>Due Date</mat-label>
          <input matInput [matDatepicker]="duePicker" formControlName="dueDate" required>
          <mat-datepicker-toggle matSuffix [for]="duePicker"></mat-datepicker-toggle>
          <mat-datepicker #duePicker></mat-datepicker>
        </mat-form-field>

        <mat-form-field appearance="outline" class="invoice-dialog__field">
          <mat-label>Total Amount</mat-label>
          <input matInput type="number" formControlName="totalAmount" required>
        </mat-form-field>

        <mat-form-field appearance="outline" class="invoice-dialog__field">
          <mat-label>Currency</mat-label>
          <input matInput formControlName="currency">
        </mat-form-field>

        <mat-form-field appearance="outline" class="invoice-dialog__field">
          <mat-label>Status</mat-label>
          <mat-select formControlName="status" required>
            <mat-option value="Draft">Draft</mat-option>
            <mat-option value="Sent">Sent</mat-option>
            <mat-option value="Paid">Paid</mat-option>
            <mat-option value="Overdue">Overdue</mat-option>
            <mat-option value="Cancelled">Cancelled</mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field appearance="outline" class="invoice-dialog__field" *ngIf="data">
          <mat-label>Paid Date</mat-label>
          <input matInput [matDatepicker]="paidPicker" formControlName="paidDate">
          <mat-datepicker-toggle matSuffix [for]="paidPicker"></mat-datepicker-toggle>
          <mat-datepicker #paidPicker></mat-datepicker>
        </mat-form-field>

        <mat-form-field appearance="outline" class="invoice-dialog__field">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="3"></textarea>
        </mat-form-field>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" [disabled]="!form.valid" (click)="save()">Save</button>
    </mat-dialog-actions>
  `,
  styles: [`
    .invoice-dialog__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
      min-width: 600px;
    }

    .invoice-dialog__field {
      width: 100%;
    }
  `]
})
export class InvoiceDialog {
  form: FormGroup;
  clients$ = this.clientService.clients$;
  projects$ = this.projectService.projects$;

  constructor(
    private fb: FormBuilder,
    private clientService: ClientService,
    private projectService: ProjectService,
    public dialogRef: MatDialogRef<InvoiceDialog>,
    @Inject(MAT_DIALOG_DATA) public data: Invoice
  ) {
    this.form = this.fb.group({
      clientId: [data?.clientId || '', Validators.required],
      projectId: [data?.projectId || null],
      invoiceNumber: [data?.invoiceNumber || '', Validators.required],
      invoiceDate: [data?.invoiceDate || new Date(), Validators.required],
      dueDate: [data?.dueDate || new Date(), Validators.required],
      totalAmount: [data?.totalAmount || 0, [Validators.required, Validators.min(0)]],
      currency: [data?.currency || 'USD'],
      status: [data?.status || 'Draft', Validators.required],
      paidDate: [data?.paidDate || null],
      notes: [data?.notes || '']
    });
  }

  save(): void {
    if (this.form.valid) {
      this.dialogRef.close(this.form.value);
    }
  }
}
