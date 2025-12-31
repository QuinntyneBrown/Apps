import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { FollowUpsService, ContactsService } from '../services';

@Component({
  selector: 'app-follow-up-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  template: `
    <div class="follow-up-form">
      <h1 class="follow-up-form__title">{{ isEditMode ? 'Edit Follow-up' : 'New Follow-up' }}</h1>

      <mat-card class="follow-up-form__card">
        <form [formGroup]="followUpForm" (ngSubmit)="onSubmit()" class="follow-up-form__form">
          <mat-form-field appearance="outline" class="follow-up-form__field--full">
            <mat-label>Contact</mat-label>
            <mat-select formControlName="contactId" required [disabled]="isEditMode">
              <mat-option *ngFor="let contact of (contactsService.contacts$ | async)" [value]="contact.contactId">
                {{ contact.fullName }} - {{ contact.company || 'No Company' }}
              </mat-option>
            </mat-select>
            <mat-error *ngIf="followUpForm.get('contactId')?.hasError('required')">
              Contact is required
            </mat-error>
          </mat-form-field>

          <mat-form-field appearance="outline" class="follow-up-form__field--full">
            <mat-label>Description</mat-label>
            <input matInput formControlName="description" required>
            <mat-error *ngIf="followUpForm.get('description')?.hasError('required')">
              Description is required
            </mat-error>
          </mat-form-field>

          <mat-form-field appearance="outline" class="follow-up-form__field--full">
            <mat-label>Due Date</mat-label>
            <input matInput [matDatepicker]="picker" formControlName="dueDate" required>
            <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
            <mat-datepicker #picker></mat-datepicker>
            <mat-error *ngIf="followUpForm.get('dueDate')?.hasError('required')">
              Due date is required
            </mat-error>
          </mat-form-field>

          <mat-form-field appearance="outline" class="follow-up-form__field--full">
            <mat-label>Priority</mat-label>
            <mat-select formControlName="priority" required>
              <mat-option value="Low">Low</mat-option>
              <mat-option value="Medium">Medium</mat-option>
              <mat-option value="High">High</mat-option>
            </mat-select>
            <mat-error *ngIf="followUpForm.get('priority')?.hasError('required')">
              Priority is required
            </mat-error>
          </mat-form-field>

          <mat-form-field appearance="outline" class="follow-up-form__field--full">
            <mat-label>Notes</mat-label>
            <textarea matInput formControlName="notes" rows="4"></textarea>
          </mat-form-field>

          <div class="follow-up-form__actions">
            <button mat-button type="button" (click)="onCancel()">Cancel</button>
            <button mat-raised-button color="primary" type="submit" [disabled]="followUpForm.invalid">
              {{ isEditMode ? 'Update' : 'Create' }}
            </button>
          </div>
        </form>
      </mat-card>
    </div>
  `,
  styles: [`
    .follow-up-form {
      padding: 2rem;
    }

    .follow-up-form__title {
      margin-bottom: 2rem;
      color: #333;
    }

    .follow-up-form__card {
      max-width: 800px;
    }

    .follow-up-form__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
    }

    .follow-up-form__field--full {
      width: 100%;
    }

    .follow-up-form__actions {
      display: flex;
      justify-content: flex-end;
      gap: 1rem;
      margin-top: 1rem;
    }
  `]
})
export class FollowUpForm implements OnInit {
  private fb = inject(FormBuilder);
  private followUpsService = inject(FollowUpsService);
  contactsService = inject(ContactsService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  followUpForm!: FormGroup;
  isEditMode = false;
  followUpId?: string;

  ngOnInit(): void {
    this.contactsService.loadContacts().subscribe();

    this.followUpForm = this.fb.group({
      contactId: ['', Validators.required],
      description: ['', Validators.required],
      dueDate: [new Date(), Validators.required],
      priority: ['Medium', Validators.required],
      notes: ['']
    });

    const id = this.route.snapshot.paramMap.get('id');
    if (id && id !== 'new') {
      this.isEditMode = true;
      this.followUpId = id;
      this.loadFollowUp(id);
    }
  }

  loadFollowUp(id: string): void {
    this.followUpsService.getFollowUpById(id).subscribe(followUp => {
      this.followUpForm.patchValue({
        contactId: followUp.contactId,
        description: followUp.description,
        dueDate: followUp.dueDate ? new Date(followUp.dueDate) : new Date(),
        priority: followUp.priority,
        notes: followUp.notes
      });
    });
  }

  onSubmit(): void {
    if (this.followUpForm.valid) {
      const formValue = this.followUpForm.value;

      if (this.isEditMode && this.followUpId) {
        const updateRequest = {
          followUpId: this.followUpId,
          description: formValue.description,
          dueDate: formValue.dueDate.toISOString(),
          priority: formValue.priority,
          notes: formValue.notes || undefined
        };
        this.followUpsService.updateFollowUp(updateRequest).subscribe(() => {
          this.router.navigate(['/follow-ups']);
        });
      } else {
        const createRequest = {
          userId: '00000000-0000-0000-0000-000000000000', // Default user ID
          contactId: formValue.contactId,
          description: formValue.description,
          dueDate: formValue.dueDate.toISOString(),
          priority: formValue.priority,
          notes: formValue.notes || undefined
        };
        this.followUpsService.createFollowUp(createRequest).subscribe(() => {
          this.router.navigate(['/follow-ups']);
        });
      }
    }
  }

  onCancel(): void {
    this.router.navigate(['/follow-ups']);
  }
}
