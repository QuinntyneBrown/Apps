import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, ActivatedRoute, RouterLink } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { JournalEntryService } from '../services';
import { EntryType, EntryTypeLabels } from '../models';

@Component({
  selector: 'app-journal-entry-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterLink,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  template: `
    <div class="journal-entry-form">
      <h1 class="journal-entry-form__title">{{ isEditMode ? 'Edit' : 'Create' }} Journal Entry</h1>

      <form [formGroup]="form" (ngSubmit)="onSubmit()" class="journal-entry-form__form">
        <mat-form-field class="journal-entry-form__field">
          <mat-label>Title</mat-label>
          <input matInput formControlName="title" required>
          <mat-error *ngIf="form.get('title')?.hasError('required')">Title is required</mat-error>
        </mat-form-field>

        <mat-form-field class="journal-entry-form__field">
          <mat-label>Content</mat-label>
          <textarea matInput formControlName="content" rows="8" required></textarea>
          <mat-error *ngIf="form.get('content')?.hasError('required')">Content is required</mat-error>
        </mat-form-field>

        <mat-form-field class="journal-entry-form__field">
          <mat-label>Entry Type</mat-label>
          <mat-select formControlName="entryType" required>
            <mat-option *ngFor="let type of entryTypes" [value]="type.value">
              {{ type.label }}
            </mat-option>
          </mat-select>
          <mat-error *ngIf="form.get('entryType')?.hasError('required')">Entry type is required</mat-error>
        </mat-form-field>

        <mat-form-field class="journal-entry-form__field">
          <mat-label>Entry Date</mat-label>
          <input matInput [matDatepicker]="picker" formControlName="entryDate" required>
          <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
          <mat-datepicker #picker></mat-datepicker>
          <mat-error *ngIf="form.get('entryDate')?.hasError('required')">Entry date is required</mat-error>
        </mat-form-field>

        <mat-form-field class="journal-entry-form__field">
          <mat-label>Tags</mat-label>
          <input matInput formControlName="tags" placeholder="Comma-separated tags">
        </mat-form-field>

        <div class="journal-entry-form__checkboxes">
          <mat-checkbox formControlName="isSharedWithPartner">Share with partner</mat-checkbox>
          <mat-checkbox formControlName="isPrivate">Private entry</mat-checkbox>
        </div>

        <div class="journal-entry-form__actions">
          <button mat-raised-button type="button" routerLink="/journal-entries">Cancel</button>
          <button mat-raised-button color="primary" type="submit" [disabled]="form.invalid">
            {{ isEditMode ? 'Update' : 'Create' }}
          </button>
        </div>
      </form>
    </div>
  `,
  styles: [`
    .journal-entry-form {
      padding: 2rem;
      max-width: 800px;
      margin: 0 auto;
    }

    .journal-entry-form__title {
      margin-bottom: 2rem;
      color: #333;
    }

    .journal-entry-form__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
    }

    .journal-entry-form__field {
      width: 100%;
    }

    .journal-entry-form__checkboxes {
      display: flex;
      flex-direction: column;
      gap: 0.5rem;
      margin: 1rem 0;
    }

    .journal-entry-form__actions {
      display: flex;
      gap: 1rem;
      justify-content: flex-end;
      margin-top: 1rem;
    }
  `]
})
export class JournalEntryForm implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  private readonly journalEntryService = inject(JournalEntryService);

  form!: FormGroup;
  isEditMode = false;
  entryId?: string;

  entryTypes = Object.entries(EntryTypeLabels).map(([value, label]) => ({
    value: Number(value),
    label
  }));

  ngOnInit(): void {
    this.entryId = this.route.snapshot.paramMap.get('id') || undefined;
    this.isEditMode = !!this.entryId;

    this.form = this.fb.group({
      title: ['', Validators.required],
      content: ['', Validators.required],
      entryType: [EntryType.General, Validators.required],
      entryDate: [new Date(), Validators.required],
      tags: [''],
      isSharedWithPartner: [false],
      isPrivate: [false],
      userId: ['00000000-0000-0000-0000-000000000001']
    });

    if (this.isEditMode && this.entryId) {
      this.journalEntryService.getById(this.entryId).subscribe(entry => {
        this.form.patchValue({
          title: entry.title,
          content: entry.content,
          entryType: entry.entryType,
          entryDate: new Date(entry.entryDate),
          tags: entry.tags,
          isSharedWithPartner: entry.isSharedWithPartner,
          isPrivate: entry.isPrivate,
          userId: entry.userId
        });
      });
    }
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const journalEntry = {
        ...formValue,
        entryDate: formValue.entryDate.toISOString()
      };

      if (this.isEditMode && this.entryId) {
        this.journalEntryService.update({ ...journalEntry, journalEntryId: this.entryId }).subscribe(() => {
          this.router.navigate(['/journal-entries']);
        });
      } else {
        this.journalEntryService.create(journalEntry).subscribe(() => {
          this.router.navigate(['/journal-entries']);
        });
      }
    }
  }
}
