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
import { MatChipsModule } from '@angular/material/chips';
import { WikiPageService, WikiCategoryService } from '../services';
import { WikiPage, PageStatus, PageStatusLabels } from '../models';

@Component({
  selector: 'app-wiki-page-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatCheckboxModule,
    MatButtonModule,
    MatDialogModule
  ],
  template: `
    <h2 mat-dialog-title>{{ data.page ? 'Edit' : 'Create' }} Wiki Page</h2>
    <form [formGroup]="form" (ngSubmit)="onSubmit()">
      <mat-dialog-content>
        <mat-form-field class="form-field">
          <mat-label>User ID</mat-label>
          <input matInput formControlName="userId" />
        </mat-form-field>

        <mat-form-field class="form-field">
          <mat-label>Category</mat-label>
          <mat-select formControlName="categoryId">
            <mat-option [value]="null">None</mat-option>
            <mat-option *ngFor="let category of categories$ | async" [value]="category.wikiCategoryId">
              {{ category.name }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="form-field">
          <mat-label>Title</mat-label>
          <input matInput formControlName="title" required />
        </mat-form-field>

        <mat-form-field class="form-field">
          <mat-label>Slug</mat-label>
          <input matInput formControlName="slug" required />
        </mat-form-field>

        <mat-form-field class="form-field">
          <mat-label>Content</mat-label>
          <textarea matInput formControlName="content" rows="6" required></textarea>
        </mat-form-field>

        <mat-form-field class="form-field">
          <mat-label>Status</mat-label>
          <mat-select formControlName="status" required>
            <mat-option *ngFor="let status of statusOptions" [value]="status.value">
              {{ status.label }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-checkbox formControlName="isFeatured">Featured Page</mat-checkbox>
      </mat-dialog-content>
      <mat-dialog-actions align="end">
        <button mat-button type="button" mat-dialog-close>Cancel</button>
        <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid">
          {{ data.page ? 'Update' : 'Create' }}
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
export class WikiPageForm {
  private readonly fb = inject(FormBuilder);
  private readonly wikiPageService = inject(WikiPageService);
  private readonly wikiCategoryService = inject(WikiCategoryService);
  private readonly dialog = inject(MatDialog);

  data = inject<{ page?: WikiPage }>(MatDialog as any);

  categories$ = this.wikiCategoryService.wikiCategories$;

  statusOptions = [
    { value: PageStatus.Draft, label: PageStatusLabels[PageStatus.Draft] },
    { value: PageStatus.Published, label: PageStatusLabels[PageStatus.Published] },
    { value: PageStatus.Review, label: PageStatusLabels[PageStatus.Review] },
    { value: PageStatus.Archived, label: PageStatusLabels[PageStatus.Archived] }
  ];

  form: FormGroup;

  constructor() {
    const page = this.data?.page;
    this.form = this.fb.group({
      userId: [page?.userId || '', Validators.required],
      categoryId: [page?.categoryId || null],
      title: [page?.title || '', Validators.required],
      slug: [page?.slug || '', Validators.required],
      content: [page?.content || '', Validators.required],
      status: [page?.status ?? PageStatus.Draft, Validators.required],
      isFeatured: [page?.isFeatured || false]
    });

    this.wikiCategoryService.getAll().subscribe();
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      if (this.data?.page) {
        this.wikiPageService.update({
          wikiPageId: this.data.page.wikiPageId,
          ...formValue
        }).subscribe(() => {
          this.dialog.closeAll();
        });
      } else {
        this.wikiPageService.create(formValue).subscribe(() => {
          this.dialog.closeAll();
        });
      }
    }
  }
}

@Component({
  selector: 'app-wiki-pages',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule
  ],
  templateUrl: './wiki-pages.html',
  styleUrl: './wiki-pages.scss'
})
export class WikiPages implements OnInit {
  private readonly wikiPageService = inject(WikiPageService);
  private readonly dialog = inject(MatDialog);

  wikiPages$ = this.wikiPageService.wikiPages$;
  displayedColumns = ['title', 'slug', 'status', 'version', 'isFeatured', 'viewCount', 'createdAt', 'actions'];

  pageStatusLabels = PageStatusLabels;

  ngOnInit(): void {
    this.wikiPageService.getAll().subscribe();
  }

  openDialog(page?: WikiPage): void {
    this.dialog.open(WikiPageForm, {
      data: { page }
    });
  }

  delete(id: string): void {
    if (confirm('Are you sure you want to delete this wiki page?')) {
      this.wikiPageService.delete(id).subscribe();
    }
  }
}
