import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatChipsModule } from '@angular/material/chips';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { ResourceService } from '../services';
import { Resource, ResourceType, ResourceTypeLabels } from '../models';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-resource-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatChipsModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data ? 'Edit Resource' : 'Add Resource' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="resource-dialog__form">
        <mat-form-field appearance="outline" class="resource-dialog__field">
          <mat-label>Title</mat-label>
          <input matInput formControlName="title" required>
        </mat-form-field>

        <mat-form-field appearance="outline" class="resource-dialog__field">
          <mat-label>Resource Type</mat-label>
          <mat-select formControlName="resourceType" required>
            <mat-option *ngFor="let type of resourceTypes" [value]="type.value">
              {{ type.label }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field appearance="outline" class="resource-dialog__field">
          <mat-label>Author</mat-label>
          <input matInput formControlName="author">
        </mat-form-field>

        <mat-form-field appearance="outline" class="resource-dialog__field">
          <mat-label>Publisher</mat-label>
          <input matInput formControlName="publisher">
        </mat-form-field>

        <mat-form-field appearance="outline" class="resource-dialog__field">
          <mat-label>Publication Date</mat-label>
          <input matInput [matDatepicker]="picker" formControlName="publicationDate">
          <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
          <mat-datepicker #picker></mat-datepicker>
        </mat-form-field>

        <mat-form-field appearance="outline" class="resource-dialog__field">
          <mat-label>URL</mat-label>
          <input matInput formControlName="url" type="url">
        </mat-form-field>

        <mat-form-field appearance="outline" class="resource-dialog__field">
          <mat-label>ISBN</mat-label>
          <input matInput formControlName="isbn">
        </mat-form-field>

        <mat-form-field appearance="outline" class="resource-dialog__field">
          <mat-label>Total Pages</mat-label>
          <input matInput formControlName="totalPages" type="number">
        </mat-form-field>

        <mat-form-field appearance="outline" class="resource-dialog__field">
          <mat-label>Topics (comma-separated)</mat-label>
          <input matInput formControlName="topicsInput">
        </mat-form-field>

        <mat-form-field appearance="outline" class="resource-dialog__field resource-dialog__field--full">
          <mat-label>Notes</mat-label>
          <textarea matInput formControlName="notes" rows="3"></textarea>
        </mat-form-field>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" (click)="save()" [disabled]="!form.valid">
        Save
      </button>
    </mat-dialog-actions>
  `,
  styles: [`
    .resource-dialog__form {
      display: grid;
      grid-template-columns: 1fr 1fr;
      gap: 16px;
      min-width: 600px;
      padding: 16px 0;
    }

    .resource-dialog__field {
      width: 100%;
    }

    .resource-dialog__field--full {
      grid-column: 1 / -1;
    }
  `]
})
export class ResourceDialog implements OnInit {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialog);

  data: Resource | null = null;

  form = this.fb.group({
    title: ['', Validators.required],
    resourceType: [ResourceType.Book, Validators.required],
    author: [''],
    publisher: [''],
    publicationDate: [''],
    url: [''],
    isbn: [''],
    totalPages: [null as number | null],
    topicsInput: [''],
    notes: ['']
  });

  resourceTypes = Object.entries(ResourceTypeLabels).map(([value, label]) => ({
    value: Number(value),
    label
  }));

  ngOnInit() {
    if (this.data) {
      this.form.patchValue({
        title: this.data.title,
        resourceType: this.data.resourceType,
        author: this.data.author || '',
        publisher: this.data.publisher || '',
        publicationDate: this.data.publicationDate || '',
        url: this.data.url || '',
        isbn: this.data.isbn || '',
        totalPages: this.data.totalPages || null,
        topicsInput: this.data.topics.join(', '),
        notes: this.data.notes || ''
      });
    }
  }

  save() {
    if (this.form.valid) {
      const topicsInput = this.form.value.topicsInput || '';
      const topics = topicsInput.split(',').map(t => t.trim()).filter(t => t);

      const result = {
        ...this.form.value,
        topics
      };
      this.dialogRef.closeAll();
    }
  }
}

@Component({
  selector: 'app-resources',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule
  ],
  template: `
    <div class="resources">
      <div class="resources__header">
        <h1 class="resources__title">Resources</h1>
        <button mat-raised-button color="primary" (click)="openDialog()">
          <mat-icon>add</mat-icon>
          Add Resource
        </button>
      </div>

      <div class="resources__table-container">
        <table mat-table [dataSource]="resources$ | async" class="resources__table">
          <ng-container matColumnDef="title">
            <th mat-header-cell *matHeaderCellDef>Title</th>
            <td mat-cell *matCellDef="let resource">{{ resource.title }}</td>
          </ng-container>

          <ng-container matColumnDef="resourceType">
            <th mat-header-cell *matHeaderCellDef>Type</th>
            <td mat-cell *matCellDef="let resource">{{ getResourceTypeLabel(resource.resourceType) }}</td>
          </ng-container>

          <ng-container matColumnDef="author">
            <th mat-header-cell *matHeaderCellDef>Author</th>
            <td mat-cell *matCellDef="let resource">{{ resource.author || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="topics">
            <th mat-header-cell *matHeaderCellDef>Topics</th>
            <td mat-cell *matCellDef="let resource">{{ resource.topics.join(', ') || '-' }}</td>
          </ng-container>

          <ng-container matColumnDef="dateAdded">
            <th mat-header-cell *matHeaderCellDef>Date Added</th>
            <td mat-cell *matCellDef="let resource">{{ resource.dateAdded | date }}</td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let resource">
              <button mat-icon-button (click)="openDialog(resource)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="delete(resource.resourceId)">
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
    .resources {
      padding: 24px;
    }

    .resources__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 24px;
    }

    .resources__title {
      margin: 0;
      font-size: 32px;
      font-weight: 400;
    }

    .resources__table-container {
      overflow-x: auto;
    }

    .resources__table {
      width: 100%;
    }
  `]
})
export class Resources implements OnInit {
  private resourceService = inject(ResourceService);
  private dialog = inject(MatDialog);

  resources$: Observable<Resource[]> = this.resourceService.resources$;
  displayedColumns = ['title', 'resourceType', 'author', 'topics', 'dateAdded', 'actions'];

  ngOnInit() {
    this.resourceService.getAll().subscribe();
  }

  getResourceTypeLabel(type: ResourceType): string {
    return ResourceTypeLabels[type];
  }

  openDialog(resource?: Resource) {
    const dialogRef = this.dialog.open(ResourceDialog, {
      width: '700px',
      data: resource
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (resource) {
          this.resourceService.update(resource.resourceId, {
            resourceId: resource.resourceId,
            ...result
          }).subscribe();
        } else {
          this.resourceService.create({
            userId: '00000000-0000-0000-0000-000000000000',
            ...result
          }).subscribe();
        }
      }
    });
  }

  delete(id: string) {
    if (confirm('Are you sure you want to delete this resource?')) {
      this.resourceService.delete(id).subscribe();
    }
  }
}
