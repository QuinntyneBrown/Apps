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
import { SearchQueryService } from '../services';
import { SearchQuery } from '../models';

@Component({
  selector: 'app-search-query-dialog',
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
    <h2 mat-dialog-title>{{ data ? 'Edit Search Query' : 'Create Search Query' }}</h2>
    <mat-dialog-content>
      <form [formGroup]="form" class="search-query-dialog__form">
        <mat-form-field class="search-query-dialog__field">
          <mat-label>Query Text</mat-label>
          <textarea matInput formControlName="queryText" rows="3" required></textarea>
        </mat-form-field>

        <mat-form-field class="search-query-dialog__field">
          <mat-label>Name (Optional)</mat-label>
          <input matInput formControlName="name">
        </mat-form-field>

        <mat-checkbox formControlName="isSaved" class="search-query-dialog__checkbox">
          Save this query
        </mat-checkbox>
      </form>
    </mat-dialog-content>
    <mat-dialog-actions align="end">
      <button mat-button mat-dialog-close>Cancel</button>
      <button mat-raised-button color="primary" (click)="save()" [disabled]="form.invalid">
        {{ data ? 'Update' : 'Create' }}
      </button>
    </mat-dialog-actions>
  `,
  styles: [`
    .search-query-dialog {
      &__form {
        display: flex;
        flex-direction: column;
        min-width: 500px;
        padding: 1rem 0;
      }

      &__field {
        width: 100%;
        margin-bottom: 1rem;
      }

      &__checkbox {
        margin-top: 1rem;
      }
    }
  `]
})
export class SearchQueryDialog {
  private readonly fb = inject(FormBuilder);
  private readonly dialogRef = inject(MatDialogModule);

  data: SearchQuery | null = null;
  form: FormGroup;

  constructor() {
    this.form = this.fb.group({
      queryText: ['', Validators.required],
      name: [null],
      isSaved: [false]
    });

    if (this.data) {
      this.form.patchValue(this.data);
    }
  }

  save() {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        name: formValue.name || null
      };

      if (this.data) {
        (this.dialogRef as any).close({ ...result, searchQueryId: this.data.searchQueryId });
      } else {
        (this.dialogRef as any).close({ ...result, userId: '00000000-0000-0000-0000-000000000000' });
      }
    }
  }
}

@Component({
  selector: 'app-search-queries',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule
  ],
  template: `
    <div class="search-queries">
      <div class="search-queries__header">
        <h1 class="search-queries__title">Search Queries</h1>
        <button mat-raised-button color="primary" (click)="openDialog()">
          <mat-icon>add</mat-icon>
          Create Search Query
        </button>
      </div>

      <div class="search-queries__table-container">
        <table mat-table [dataSource]="searchQueries$ | async" class="search-queries__table">
          <ng-container matColumnDef="name">
            <th mat-header-cell *matHeaderCellDef>Name</th>
            <td mat-cell *matCellDef="let query">{{ query.name || 'Unnamed' }}</td>
          </ng-container>

          <ng-container matColumnDef="queryText">
            <th mat-header-cell *matHeaderCellDef>Query Text</th>
            <td mat-cell *matCellDef="let query">
              <div class="search-queries__query-text">{{ query.queryText }}</div>
            </td>
          </ng-container>

          <ng-container matColumnDef="isSaved">
            <th mat-header-cell *matHeaderCellDef>Saved</th>
            <td mat-cell *matCellDef="let query">
              <mat-icon>{{ query.isSaved ? 'check' : 'close' }}</mat-icon>
            </td>
          </ng-container>

          <ng-container matColumnDef="executionCount">
            <th mat-header-cell *matHeaderCellDef>Executions</th>
            <td mat-cell *matCellDef="let query">{{ query.executionCount }}</td>
          </ng-container>

          <ng-container matColumnDef="lastExecutedAt">
            <th mat-header-cell *matHeaderCellDef>Last Executed</th>
            <td mat-cell *matCellDef="let query">
              {{ query.lastExecutedAt ? (query.lastExecutedAt | date:'short') : 'Never' }}
            </td>
          </ng-container>

          <ng-container matColumnDef="createdAt">
            <th mat-header-cell *matHeaderCellDef>Created</th>
            <td mat-cell *matCellDef="let query">{{ query.createdAt | date:'short' }}</td>
          </ng-container>

          <ng-container matColumnDef="actions">
            <th mat-header-cell *matHeaderCellDef>Actions</th>
            <td mat-cell *matCellDef="let query">
              <button mat-icon-button color="primary" (click)="openDialog(query)">
                <mat-icon>edit</mat-icon>
              </button>
              <button mat-icon-button color="warn" (click)="deleteSearchQuery(query.searchQueryId)">
                <mat-icon>delete</mat-icon>
              </button>
            </td>
          </ng-container>

          <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
          <tr mat-row *matRowDef="let row; columns: displayedColumns"></tr>
        </table>
      </div>
    </div>
  `,
  styles: [`
    .search-queries {
      padding: 2rem;

      &__header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 2rem;
      }

      &__title {
        margin: 0;
        color: #333;
      }

      &__table-container {
        overflow-x: auto;
      }

      &__table {
        width: 100%;
        background: white;
      }

      &__query-text {
        max-width: 300px;
        overflow: hidden;
        text-overflow: ellipsis;
        white-space: nowrap;
      }
    }
  `]
})
export class SearchQueries implements OnInit {
  private readonly searchQueryService = inject(SearchQueryService);
  private readonly dialog = inject(MatDialog);

  searchQueries$ = this.searchQueryService.searchQueries$;
  displayedColumns = ['name', 'queryText', 'isSaved', 'executionCount', 'lastExecutedAt', 'createdAt', 'actions'];

  ngOnInit() {
    this.searchQueryService.getSearchQueries().subscribe();
  }

  openDialog(searchQuery?: SearchQuery) {
    const dialogRef = this.dialog.open(SearchQueryDialog, {
      width: '600px',
      data: searchQuery || null
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (searchQuery) {
          this.searchQueryService.updateSearchQuery(result).subscribe();
        } else {
          this.searchQueryService.createSearchQuery(result).subscribe();
        }
      }
    });
  }

  deleteSearchQuery(id: string) {
    if (confirm('Are you sure you want to delete this search query?')) {
      this.searchQueryService.deleteSearchQuery(id).subscribe();
    }
  }
}
