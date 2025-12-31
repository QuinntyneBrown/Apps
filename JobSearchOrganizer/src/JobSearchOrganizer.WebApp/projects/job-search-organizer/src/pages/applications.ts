import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatCardModule } from '@angular/material/card';
import { MatChipModule } from '@angular/material/chip';
import { ApplicationService, CompanyService } from '../services';
import { Application, CreateApplication, UpdateApplication, ApplicationStatus, ApplicationStatusLabels } from '../models';

@Component({
  selector: 'app-application-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit Application' : 'Add Application' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="application-dialog__form">
        <mat-form-field class="application-dialog__field">
          <mat-label>Company</mat-label>
          <mat-select formControlName="companyId" required>
            <mat-option *ngFor="let company of (companyService.companies$ | async)" [value]="company.companyId">
              {{ company.name }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="application-dialog__field">
          <mat-label>Job Title</mat-label>
          <input matInput formControlName="jobTitle" required>
        </mat-form-field>

        <mat-form-field class="application-dialog__field">
          <mat-label>Job URL</mat-label>
          <input matInput formControlName="jobUrl" type="url">
        </mat-form-field>

        <mat-form-field class="application-dialog__field">
          <mat-label>Status</mat-label>
          <mat-select formControlName="status" required>
            <mat-option *ngFor="let status of statusOptions" [value]="status.value">
              {{ status.label }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="application-dialog__field">
          <mat-label>Applied Date</mat-label>
          <input matInput [matDatepicker]="picker" formControlName="appliedDate" required>
          <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
          <mat-datepicker #picker></mat-datepicker>
        </mat-form-field>

        <mat-form-field class="application-dialog__field">
          <mat-label>Salary Range</mat-label>
          <input matInput formControlName="salaryRange">
        </mat-form-field>

        <mat-form-field class="application-dialog__field">
          <mat-label>Location</mat-label>
          <input matInput formControlName="location">
        </mat-form-field>

        <mat-form-field class="application-dialog__field">
          <mat-label>Job Type</mat-label>
          <input matInput formControlName="jobType" placeholder="e.g., Full-time, Part-time, Contract">
        </mat-form-field>

        <mat-checkbox formControlName="isRemote">
          Remote Position
        </mat-checkbox>

        <mat-form-field class="application-dialog__field">
          <mat-label>Contact Person</mat-label>
          <input matInput formControlName="contactPerson">
        </mat-form-field>

        <mat-form-field class="application-dialog__field">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="4"></textarea>
        </mat-form-field>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" [disabled]="!form.valid" (click)="save()">
        Save
      </button>
    </mat-dialog-actions>
  `,
  styles: [`
    .application-dialog__form {
      display: flex;
      flex-direction: column;
      gap: 16px;
      min-width: 500px;
      padding: 16px 0;
    }

    .application-dialog__field {
      width: 100%;
    }
  `]
})
export class ApplicationDialog implements OnInit {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialog);
  companyService = inject(CompanyService);

  data?: Application;
  form: FormGroup;
  statusOptions: { value: ApplicationStatus; label: string }[] = [];

  constructor() {
    this.form = this.fb.group({
      companyId: ['', Validators.required],
      jobTitle: ['', Validators.required],
      jobUrl: [''],
      status: [ApplicationStatus.Draft, Validators.required],
      appliedDate: [new Date(), Validators.required],
      salaryRange: [''],
      location: [''],
      jobType: [''],
      isRemote: [false],
      contactPerson: [''],
      notes: ['']
    });

    this.statusOptions = Object.keys(ApplicationStatus)
      .filter(key => !isNaN(Number(ApplicationStatus[key as keyof typeof ApplicationStatus])))
      .map(key => ({
        value: ApplicationStatus[key as keyof typeof ApplicationStatus] as ApplicationStatus,
        label: ApplicationStatusLabels[ApplicationStatus[key as keyof typeof ApplicationStatus] as ApplicationStatus]
      }));

    if (this.data) {
      this.form.patchValue({
        ...this.data,
        appliedDate: new Date(this.data.appliedDate)
      });
    }
  }

  ngOnInit(): void {
    this.companyService.getCompanies().subscribe();
  }

  save(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        appliedDate: formValue.appliedDate.toISOString()
      };
      this.dialogRef.closeAll();
    }
  }
}

@Component({
  selector: 'app-applications',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatChipModule
  ],
  template: `
    <div class="applications">
      <div class="applications__header">
        <h1 class="applications__title">Applications</h1>
        <button mat-raised-button color="primary" (click)="openDialog()">
          <mat-icon>add</mat-icon>
          Add Application
        </button>
      </div>

      <mat-card class="applications__card">
        <table mat-table [dataSource]="(applicationService.applications$ | async) || []" class="applications__table">
          <ng-container matColumnDef="jobTitle">
            <th mat-header-cell *matHeaderCellDef>Job Title</th>
            <td mat-cell *matCellDef="let application">{{ application.jobTitle }}</td>
          </ng-container>

          <ng-container matColumnDef="company">
            <th mat-header-cell *matHeaderCellDef>Company</th>
            <td mat-cell *matCellDef="let application">{{ getCompanyName(application.companyId) }}</td>
          </ng-container>

          <ng-container matColumnDef="status">
            <th mat-header-cell *matHeaderCellDef>Status</th>
            <td mat-cell *matCellDef="let application">
              <mat-chip [class]="'applications__chip--' + getStatusClass(application.status)">
                {{ getStatusLabel(application.status) }}
              </mat-chip>
            </td>
          </ng-container>

          <ng-container matColumnDef="appliedDate">
            <th mat-header-cell *matHeaderCellDef>Applied Date</th>
            <td mat-cell *matCellDef="let application">{{ application.appliedDate | date:'mediumDate' }}</td>
          </ng-container>

          <ng-container matColumnDef="location">
            <th mat-header-cell *matHeaderCellDef>Location</th>
            <td mat-cell *matCellDef="let application">
              {{ application.location || '-' }}
              <mat-icon *ngIf="application.isRemote" class="applications__remote-icon">home</mat-icon>
            </td>
          </ng-container>

          <ng-container matColumnDef="salaryRange">
            <th mat-header-cell *matHeaderCellDef>Salary Range</th>
            <td mat-cell *matCellDef="let application">{{ application.salaryRange || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let application">
              <button mat-icon-button (click)="openDialog(application)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="deleteApplication(application.applicationId)">
                <mat-icon>delete</mat-icon>
              </button>
            </td>
          </ng-container>

          <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
          <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
        </table>

        <div *ngIf="(applicationService.applications$ | async)?.length === 0" class="applications__empty">
          <p>No applications found. Add your first application to get started!</p>
        </div>
      </mat-card>
    </div>
  `,
  styles: [`
    .applications {
      padding: 24px;
    }

    .applications__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 24px;
    }

    .applications__title {
      margin: 0;
      font-size: 32px;
      font-weight: 500;
    }

    .applications__card {
      overflow-x: auto;
    }

    .applications__table {
      width: 100%;
    }

    .applications__chip {
      font-size: 12px;
    }

    .applications__chip--draft {
      background-color: #9e9e9e;
      color: white;
    }

    .applications__chip--applied {
      background-color: #2196f3;
      color: white;
    }

    .applications__chip--under-review {
      background-color: #ff9800;
      color: white;
    }

    .applications__chip--phone-screen {
      background-color: #9c27b0;
      color: white;
    }

    .applications__chip--interviewing {
      background-color: #673ab7;
      color: white;
    }

    .applications__chip--offer-received {
      background-color: #4caf50;
      color: white;
    }

    .applications__chip--accepted {
      background-color: #8bc34a;
      color: white;
    }

    .applications__chip--rejected {
      background-color: #f44336;
      color: white;
    }

    .applications__chip--withdrawn {
      background-color: #607d8b;
      color: white;
    }

    .applications__remote-icon {
      font-size: 16px;
      width: 16px;
      height: 16px;
      vertical-align: middle;
      margin-left: 4px;
      color: #1976d2;
    }

    .applications__empty {
      padding: 48px;
      text-align: center;
      color: rgba(0, 0, 0, 0.6);
    }
  `]
})
export class Applications implements OnInit {
  applicationService = inject(ApplicationService);
  companyService = inject(CompanyService);
  private dialog = inject(MatDialog);

  displayedColumns = ['jobTitle', 'company', 'status', 'appliedDate', 'location', 'salaryRange', 'actions'];

  ngOnInit(): void {
    this.applicationService.getApplications().subscribe();
    this.companyService.getCompanies().subscribe();
  }

  getCompanyName(companyId: string): string {
    const companies = this.companyService['companiesSubject'].value;
    const company = companies.find(c => c.companyId === companyId);
    return company?.name || 'Unknown';
  }

  getStatusLabel(status: ApplicationStatus): string {
    return ApplicationStatusLabels[status];
  }

  getStatusClass(status: ApplicationStatus): string {
    const statusMap: Record<ApplicationStatus, string> = {
      [ApplicationStatus.Draft]: 'draft',
      [ApplicationStatus.Applied]: 'applied',
      [ApplicationStatus.UnderReview]: 'under-review',
      [ApplicationStatus.PhoneScreen]: 'phone-screen',
      [ApplicationStatus.Interviewing]: 'interviewing',
      [ApplicationStatus.OfferReceived]: 'offer-received',
      [ApplicationStatus.Accepted]: 'accepted',
      [ApplicationStatus.Rejected]: 'rejected',
      [ApplicationStatus.Withdrawn]: 'withdrawn'
    };
    return statusMap[status];
  }

  openDialog(application?: Application): void {
    const dialogRef = this.dialog.open(ApplicationDialog, {
      width: '600px',
      data: application
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (application) {
          const updateData: UpdateApplication = {
            applicationId: application.applicationId,
            ...result
          };
          this.applicationService.updateApplication(updateData).subscribe();
        } else {
          const createData: CreateApplication = {
            userId: '00000000-0000-0000-0000-000000000000',
            ...result
          };
          this.applicationService.createApplication(createData).subscribe();
        }
      }
    });
  }

  deleteApplication(id: string): void {
    if (confirm('Are you sure you want to delete this application?')) {
      this.applicationService.deleteApplication(id).subscribe();
    }
  }
}
