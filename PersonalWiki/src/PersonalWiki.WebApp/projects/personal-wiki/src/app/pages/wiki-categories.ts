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
import { WikiCategoryService } from '../services';
import { WikiCategory } from '../models';

@Component({
  selector: 'app-wiki-category-form',
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
    <h2 mat-dialog-title>{{ data.category ? 'Edit' : 'Create' }} Category</h2>
    <form [formGroup]="form" (ngSubmit)="onSubmit()">
      <mat-dialog-content>
        <mat-form-field class="form-field">
          <mat-label>User ID</mat-label>
          <input matInput formControlName="userId" />
        </mat-form-field>

        <mat-form-field class="form-field">
          <mat-label>Name</mat-label>
          <input matInput formControlName="name" required />
        </mat-form-field>

        <mat-form-field class="form-field">
          <mat-label>Description</mat-label>
          <textarea matInput formControlName="description" rows="3"></textarea>
        </mat-form-field>

        <mat-form-field class="form-field">
          <mat-label>Parent Category</mat-label>
          <mat-select formControlName="parentCategoryId">
            <mat-option [value]="null">None</mat-option>
            <mat-option *ngFor="let category of categories$ | async" [value]="category.wikiCategoryId">
              {{ category.name }}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field class="form-field">
          <mat-label>Icon</mat-label>
          <input matInput formControlName="icon" placeholder="e.g., folder, book, code" />
        </mat-form-field>
      </mat-dialog-content>
      <mat-dialog-actions align="end">
        <button mat-button type="button" mat-dialog-close>Cancel</button>
        <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid">
          {{ data.category ? 'Update' : 'Create' }}
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
export class WikiCategoryForm {
  private readonly fb = inject(FormBuilder);
  private readonly wikiCategoryService = inject(WikiCategoryService);
  private readonly dialog = inject(MatDialog);

  data = inject<{ category?: WikiCategory }>(MatDialog as any);

  categories$ = this.wikiCategoryService.wikiCategories$;

  form: FormGroup;

  constructor() {
    const category = this.data?.category;
    this.form = this.fb.group({
      userId: [category?.userId || '', Validators.required],
      name: [category?.name || '', Validators.required],
      description: [category?.description || ''],
      parentCategoryId: [category?.parentCategoryId || null],
      icon: [category?.icon || '']
    });

    this.wikiCategoryService.getAll().subscribe();
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      if (this.data?.category) {
        this.wikiCategoryService.update({
          wikiCategoryId: this.data.category.wikiCategoryId,
          ...formValue
        }).subscribe(() => {
          this.dialog.closeAll();
        });
      } else {
        this.wikiCategoryService.create(formValue).subscribe(() => {
          this.dialog.closeAll();
        });
      }
    }
  }
}

@Component({
  selector: 'app-wiki-categories',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './wiki-categories.html',
  styleUrl: './wiki-categories.scss'
})
export class WikiCategories implements OnInit {
  private readonly wikiCategoryService = inject(WikiCategoryService);
  private readonly dialog = inject(MatDialog);

  wikiCategories$ = this.wikiCategoryService.wikiCategories$;
  displayedColumns = ['name', 'description', 'icon', 'pageCount', 'createdAt', 'actions'];

  ngOnInit(): void {
    this.wikiCategoryService.getAll().subscribe();
  }

  openDialog(category?: WikiCategory): void {
    this.dialog.open(WikiCategoryForm, {
      data: { category }
    });
  }

  delete(id: string): void {
    if (confirm('Are you sure you want to delete this category?')) {
      this.wikiCategoryService.delete(id).subscribe();
    }
  }
}
