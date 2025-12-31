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
import { InteractionsService, ContactsService } from '../services';

@Component({
  selector: 'app-interaction-form',
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
    <div class="interaction-form">
      <h1 class="interaction-form__title">{{ isEditMode ? 'Edit Interaction' : 'Log Interaction' }}</h1>

      <mat-card class="interaction-form__card">
        <form [formGroup]="interactionForm" (ngSubmit)="onSubmit()" class="interaction-form__form">
          <mat-form-field appearance="outline" class="interaction-form__field--full">
            <mat-label>Contact</mat-label>
            <mat-select formControlName="contactId" required [disabled]="isEditMode">
              <mat-option *ngFor="let contact of (contactsService.contacts$ | async)" [value]="contact.contactId">
                {{ contact.fullName }} - {{ contact.company || 'No Company' }}
              </mat-option>
            </mat-select>
            <mat-error *ngIf="interactionForm.get('contactId')?.hasError('required')">
              Contact is required
            </mat-error>
          </mat-form-field>

          <div class="interaction-form__row">
            <mat-form-field appearance="outline" class="interaction-form__field">
              <mat-label>Interaction Type</mat-label>
              <mat-select formControlName="interactionType" required>
                <mat-option value="Email">Email</mat-option>
                <mat-option value="Phone Call">Phone Call</mat-option>
                <mat-option value="Video Call">Video Call</mat-option>
                <mat-option value="In-Person Meeting">In-Person Meeting</mat-option>
                <mat-option value="LinkedIn Message">LinkedIn Message</mat-option>
                <mat-option value="Coffee Chat">Coffee Chat</mat-option>
                <mat-option value="Conference">Conference</mat-option>
                <mat-option value="Other">Other</mat-option>
              </mat-select>
              <mat-error *ngIf="interactionForm.get('interactionType')?.hasError('required')">
                Interaction type is required
              </mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="interaction-form__field">
              <mat-label>Interaction Date</mat-label>
              <input matInput [matDatepicker]="picker" formControlName="interactionDate" required>
              <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
              <mat-datepicker #picker></mat-datepicker>
              <mat-error *ngIf="interactionForm.get('interactionDate')?.hasError('required')">
                Interaction date is required
              </mat-error>
            </mat-form-field>
          </div>

          <mat-form-field appearance="outline" class="interaction-form__field--full">
            <mat-label>Subject</mat-label>
            <input matInput formControlName="subject">
          </mat-form-field>

          <mat-form-field appearance="outline" class="interaction-form__field--full">
            <mat-label>Duration (minutes)</mat-label>
            <input matInput type="number" formControlName="durationMinutes" min="0">
          </mat-form-field>

          <mat-form-field appearance="outline" class="interaction-form__field--full">
            <mat-label>Notes</mat-label>
            <textarea matInput formControlName="notes" rows="4"></textarea>
          </mat-form-field>

          <mat-form-field appearance="outline" class="interaction-form__field--full">
            <mat-label>Outcome</mat-label>
            <textarea matInput formControlName="outcome" rows="3"></textarea>
          </mat-form-field>

          <div class="interaction-form__actions">
            <button mat-button type="button" (click)="onCancel()">Cancel</button>
            <button mat-raised-button color="primary" type="submit" [disabled]="interactionForm.invalid">
              {{ isEditMode ? 'Update' : 'Log' }}
            </button>
          </div>
        </form>
      </mat-card>
    </div>
  `,
  styles: [`
    .interaction-form {
      padding: 2rem;
    }

    .interaction-form__title {
      margin-bottom: 2rem;
      color: #333;
    }

    .interaction-form__card {
      max-width: 800px;
    }

    .interaction-form__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
    }

    .interaction-form__row {
      display: grid;
      grid-template-columns: 1fr 1fr;
      gap: 1rem;
    }

    .interaction-form__field {
      width: 100%;
    }

    .interaction-form__field--full {
      width: 100%;
    }

    .interaction-form__actions {
      display: flex;
      justify-content: flex-end;
      gap: 1rem;
      margin-top: 1rem;
    }
  `]
})
export class InteractionForm implements OnInit {
  private fb = inject(FormBuilder);
  private interactionsService = inject(InteractionsService);
  contactsService = inject(ContactsService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  interactionForm!: FormGroup;
  isEditMode = false;
  interactionId?: string;

  ngOnInit(): void {
    this.contactsService.loadContacts().subscribe();

    this.interactionForm = this.fb.group({
      contactId: ['', Validators.required],
      interactionType: ['', Validators.required],
      interactionDate: [new Date(), Validators.required],
      subject: [''],
      durationMinutes: [null],
      notes: [''],
      outcome: ['']
    });

    const id = this.route.snapshot.paramMap.get('id');
    if (id && id !== 'new') {
      this.isEditMode = true;
      this.interactionId = id;
      this.loadInteraction(id);
    }
  }

  loadInteraction(id: string): void {
    this.interactionsService.getInteractionById(id).subscribe(interaction => {
      this.interactionForm.patchValue({
        contactId: interaction.contactId,
        interactionType: interaction.interactionType,
        interactionDate: interaction.interactionDate ? new Date(interaction.interactionDate) : new Date(),
        subject: interaction.subject,
        durationMinutes: interaction.durationMinutes,
        notes: interaction.notes,
        outcome: interaction.outcome
      });
    });
  }

  onSubmit(): void {
    if (this.interactionForm.valid) {
      const formValue = this.interactionForm.value;

      if (this.isEditMode && this.interactionId) {
        const updateRequest = {
          interactionId: this.interactionId,
          interactionType: formValue.interactionType,
          interactionDate: formValue.interactionDate.toISOString(),
          subject: formValue.subject || undefined,
          durationMinutes: formValue.durationMinutes || undefined,
          notes: formValue.notes || undefined,
          outcome: formValue.outcome || undefined
        };
        this.interactionsService.updateInteraction(updateRequest).subscribe(() => {
          this.router.navigate(['/interactions']);
        });
      } else {
        const createRequest = {
          userId: '00000000-0000-0000-0000-000000000000', // Default user ID
          contactId: formValue.contactId,
          interactionType: formValue.interactionType,
          interactionDate: formValue.interactionDate.toISOString(),
          subject: formValue.subject || undefined,
          durationMinutes: formValue.durationMinutes || undefined,
          notes: formValue.notes || undefined,
          outcome: formValue.outcome || undefined
        };
        this.interactionsService.createInteraction(createRequest).subscribe(() => {
          this.router.navigate(['/interactions']);
        });
      }
    }
  }

  onCancel(): void {
    this.router.navigate(['/interactions']);
  }
}
