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
import { DocumentService } from '../services';
import { Document, DocumentCategoryEnum } from '../models';

@Component({
  selector: 'app-document-form-dialog',
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
    <h2 mat-dialog-title>{{ data.document ? 'Edit Document' : 'Create Document' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="document-form">
        <mat-form-field class="document-form__field">
          <mat-label>Name</mat-label>
          <input matInput formControlName="name" required>
        </mat-form-field>

        <mat-form-field class="document-form__field">
          <mat-label>Category</mat-label>
          <mat-select formControlName="category" required>
            <mat-option *ngFor="let category of categories" [value]="category.value">
              {{ category.label }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="document-form__field">
          <mat-label>File URL</mat-label>
          <input matInput formControlName="fileUrl">
        </mat-form-field>

        <mat-form-field class="document-form__field">
          <mat-label>Expiration Date</mat-label>
          <input matInput [matDatepicker]="picker" formControlName="expirationDate">
          <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
          <mat-datepicker #picker></mat-datepicker>
        </mat-form-field>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" (click)="onSubmit()" [disabled]="form.invalid">
        {{ data.document ? 'Update' : 'Create' }}
      </button>
    </mat-dialog-actions>
  `,
  styles: [`
    .document-form {
      display: flex;
      flex-direction: column;
      min-width: 400px;
      padding: 1rem 0;

      &__field {
        width: 100%;
      }
    }
  `]
})
export class DocumentFormDialog {
  form: FormGroup;
  categories = [
    { value: DocumentCategoryEnum.Personal, label: 'Personal' },
    { value: DocumentCategoryEnum.Financial, label: 'Financial' },
    { value: DocumentCategoryEnum.Legal, label: 'Legal' },
    { value: DocumentCategoryEnum.Medical, label: 'Medical' },
    { value: DocumentCategoryEnum.Insurance, label: 'Insurance' },
    { value: DocumentCategoryEnum.Tax, label: 'Tax' },
    { value: DocumentCategoryEnum.Property, label: 'Property' },
    { value: DocumentCategoryEnum.Other, label: 'Other' }
  ];

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<DocumentFormDialog>,
    @Inject(MAT_DIALOG_DATA) public data: { document?: Document }
  ) {
    this.form = this.fb.group({
      name: [data.document?.name || '', Validators.required],
      category: [data.document?.category ?? DocumentCategoryEnum.Personal, Validators.required],
      fileUrl: [data.document?.fileUrl || ''],
      expirationDate: [data.document?.expirationDate ? new Date(data.document.expirationDate) : null]
    });
  }

  onSubmit() {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        expirationDate: formValue.expirationDate ? formValue.expirationDate.toISOString() : null
      };
      this.dialogRef.close(result);
    }
  }
}

@Component({
  selector: 'app-documents',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatSnackBarModule
  ],
  template: `
    <div class="documents">
      <mat-card class="documents__card">
        <mat-card-header class="documents__header">
          <mat-card-title>Documents</mat-card-title>
          <button mat-raised-button color="primary" (click)="openCreateDialog()">
            <mat-icon>add</mat-icon>
            Add Document
          </button>
        </mat-card-header>
        <mat-card-content>
          <table mat-table [dataSource]="documents$ | async" class="documents__table">
            <ng-container matColumnDef="name">
              <th mat-header-cell *matHeaderCellDef>Name</th>
              <td mat-cell *matCellDef="let document">{{ document.name }}</td>
            </ng-container>

            <ng-container matColumnDef="category">
              <th mat-header-cell *matHeaderCellDef>Category</th>
              <td mat-cell *matCellDef="let document">{{ getCategoryLabel(document.category) }}</td>
            </ng-container>

            <ng-container matColumnDef="expirationDate">
              <th mat-header-cell *matHeaderCellDef>Expiration Date</th>
              <td mat-cell *matCellDef="let document">
                {{ document.expirationDate ? (document.expirationDate | date) : 'N/A' }}
              </td>
            </ng-container>

            <ng-container matColumnDef="createdAt">
              <th mat-header-cell *matHeaderCellDef>Created At</th>
              <td mat-cell *matCellDef="let document">{{ document.createdAt | date }}</td>
            </ng-container>

            <ng-container matColumnDef="actions">
              <th mat-header-cell *matHeaderCellDef>Actions</th>
              <td mat-cell *matCellDef="let document">
                <button mat-icon-button color="primary" (click)="openEditDialog(document)">
                  <mat-icon>edit</mat-icon>
                </button>
                <button mat-icon-button color="warn" (click)="deleteDocument(document.documentId)">
                  <mat-icon>delete</mat-icon>
                </button>
              </td>
            </ng-container>

            <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
            <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
          </table>
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .documents {
      padding: 2rem;

      &__card {
        margin-bottom: 2rem;
      }

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 1rem;
      }

      &__table {
        width: 100%;
      }
    }
  `]
})
export class Documents implements OnInit {
  documents$ = this.documentService.documents$;
  displayedColumns: string[] = ['name', 'category', 'expirationDate', 'createdAt', 'actions'];

  constructor(
    private documentService: DocumentService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit() {
    this.documentService.getDocuments().subscribe();
  }

  getCategoryLabel(category: DocumentCategoryEnum): string {
    const categoryMap: Record<DocumentCategoryEnum, string> = {
      [DocumentCategoryEnum.Personal]: 'Personal',
      [DocumentCategoryEnum.Financial]: 'Financial',
      [DocumentCategoryEnum.Legal]: 'Legal',
      [DocumentCategoryEnum.Medical]: 'Medical',
      [DocumentCategoryEnum.Insurance]: 'Insurance',
      [DocumentCategoryEnum.Tax]: 'Tax',
      [DocumentCategoryEnum.Property]: 'Property',
      [DocumentCategoryEnum.Other]: 'Other'
    };
    return categoryMap[category];
  }

  openCreateDialog() {
    const dialogRef = this.dialog.open(DocumentFormDialog, {
      width: '500px',
      data: {}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        // Generate a temporary userId - in a real app, this would come from auth
        const command = {
          ...result,
          userId: '00000000-0000-0000-0000-000000000001'
        };
        this.documentService.createDocument(command).subscribe({
          next: () => {
            this.snackBar.open('Document created successfully', 'Close', { duration: 3000 });
          },
          error: () => {
            this.snackBar.open('Error creating document', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  openEditDialog(document: Document) {
    const dialogRef = this.dialog.open(DocumentFormDialog, {
      width: '500px',
      data: { document }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const command = {
          ...result,
          documentId: document.documentId,
          userId: document.userId
        };
        this.documentService.updateDocument(document.documentId, command).subscribe({
          next: () => {
            this.snackBar.open('Document updated successfully', 'Close', { duration: 3000 });
          },
          error: () => {
            this.snackBar.open('Error updating document', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  deleteDocument(id: string) {
    if (confirm('Are you sure you want to delete this document?')) {
      this.documentService.deleteDocument(id).subscribe({
        next: () => {
          this.snackBar.open('Document deleted successfully', 'Close', { duration: 3000 });
        },
        error: () => {
          this.snackBar.open('Error deleting document', 'Close', { duration: 3000 });
        }
      });
    }
  }
}
