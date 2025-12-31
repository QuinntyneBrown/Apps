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
import { PageRevisionService, WikiPageService } from '../services';
import { PageRevision } from '../models';

@Component({
  selector: 'app-page-revision-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatDialogModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data.revision ? 'Edit' : 'Create' }} Page Revision</h2>
    <form [formGroup]="form" (ngSubmit)="onSubmit()">
      <mat-dialog-content>
        <mat-form-field class="form-field">
          <mat-label>Wiki Page</mat-label>
          <mat-select formControlName="wikiPageId" required>
            <mat-option *ngFor="let page of wikiPages$ | async" [value]="page.wikiPageId">
              {{ page.title }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="form-field">
          <mat-label>Version</mat-label>
          <input matInput type="number" formControlName="version" required />
        </mat-form-field>

        <mat-form-field class="form-field">
          <mat-label>Content</mat-label>
          <textarea matInput formControlName="content" rows="6" required></textarea>
        </mat-form-field>

        <mat-form-field class="form-field">
          <mat-label>Change Summary</mat-label>
          <textarea matInput formControlName="changeSummary" rows="2"></textarea>
        </mat-form-field>

        <mat-form-field class="form-field">
          <mat-label>Revised By</mat-label>
          <input matInput formControlName="revisedBy" />
        </mat-form-field>
      </mat-dialog-content>
      <mat-dialog-actions align="end">
        <button mat-button type="button" mat-dialog-close>Cancel</button>
        <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid">
          {{ data.revision ? 'Update' : 'Create' }}
        </button>
      </mat-dialog-actions>
    </form>
  `,
  styles: [`
    .form-field {
      width: 100%;
      margin-bottom: 1rem;
    }

    mat-dialog-content {
      min-width: 500px;
      padding-top: 1rem;
    }
  `]
})
export class PageRevisionForm {
  private readonly fb = inject(FormBuilder);
  private readonly pageRevisionService = inject(PageRevisionService);
  private readonly wikiPageService = inject(WikiPageService);
  private readonly dialog = inject(MatDialog);

  data = inject<{ revision?: PageRevision }>(MatDialog as any);

  wikiPages$ = this.wikiPageService.wikiPages$;

  form: FormGroup;

  constructor() {
    const revision = this.data?.revision;
    this.form = this.fb.group({
      wikiPageId: [revision?.wikiPageId || '', Validators.required],
      version: [revision?.version || 1, [Validators.required, Validators.min(1)]],
      content: [revision?.content || '', Validators.required],
      changeSummary: [revision?.changeSummary || ''],
      revisedBy: [revision?.revisedBy || '']
    });

    this.wikiPageService.getAll().subscribe();
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      if (this.data?.revision) {
        this.pageRevisionService.update({
          pageRevisionId: this.data.revision.pageRevisionId,
          ...formValue
        }).subscribe(() => {
          this.dialog.closeAll();
        });
      } else {
        this.pageRevisionService.create(formValue).subscribe(() => {
          this.dialog.closeAll();
        });
      }
    }
  }
}

@Component({
  selector: 'app-page-revisions',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './page-revisions.html',
  styleUrl: './page-revisions.scss'
})
export class PageRevisions implements OnInit {
  private readonly pageRevisionService = inject(PageRevisionService);
  private readonly dialog = inject(MatDialog);

  pageRevisions$ = this.pageRevisionService.pageRevisions$;
  displayedColumns = ['version', 'changeSummary', 'revisedBy', 'createdAt', 'actions'];

  ngOnInit(): void {
    this.pageRevisionService.getAll().subscribe();
  }

  openDialog(revision?: PageRevision): void {
    this.dialog.open(PageRevisionForm, {
      data: { revision }
    });
  }

  delete(id: string): void {
    if (confirm('Are you sure you want to delete this revision?')) {
      this.pageRevisionService.delete(id).subscribe();
    }
  }
}
