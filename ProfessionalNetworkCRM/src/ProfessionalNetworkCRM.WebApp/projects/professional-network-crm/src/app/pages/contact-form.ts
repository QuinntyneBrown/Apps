import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule } from '@angular/material/chips';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { ContactsService } from '../services';
import { ContactType, ContactTypeLabels } from '../models';

@Component({
  selector: 'app-contact-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatCheckboxModule,
    MatChipsModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  template: `
    <div class="contact-form">
      <h1 class="contact-form__title">{{ isEditMode ? 'Edit Contact' : 'New Contact' }}</h1>

      <mat-card class="contact-form__card">
        <form [formGroup]="contactForm" (ngSubmit)="onSubmit()" class="contact-form__form">
          <div class="contact-form__row">
            <mat-form-field appearance="outline" class="contact-form__field">
              <mat-label>First Name</mat-label>
              <input matInput formControlName="firstName" required>
              <mat-error *ngIf="contactForm.get('firstName')?.hasError('required')">
                First name is required
              </mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="contact-form__field">
              <mat-label>Last Name</mat-label>
              <input matInput formControlName="lastName" required>
              <mat-error *ngIf="contactForm.get('lastName')?.hasError('required')">
                Last name is required
              </mat-error>
            </mat-form-field>
          </div>

          <div class="contact-form__row">
            <mat-form-field appearance="outline" class="contact-form__field">
              <mat-label>Contact Type</mat-label>
              <mat-select formControlName="contactType" required>
                <mat-option *ngFor="let type of contactTypes" [value]="type.value">
                  {{ type.label }}
                </mat-option>
              </mat-select>
              <mat-error *ngIf="contactForm.get('contactType')?.hasError('required')">
                Contact type is required
              </mat-error>
            </mat-form-field>

            <mat-form-field appearance="outline" class="contact-form__field">
              <mat-label>Company</mat-label>
              <input matInput formControlName="company">
            </mat-form-field>
          </div>

          <div class="contact-form__row">
            <mat-form-field appearance="outline" class="contact-form__field">
              <mat-label>Job Title</mat-label>
              <input matInput formControlName="jobTitle">
            </mat-form-field>

            <mat-form-field appearance="outline" class="contact-form__field">
              <mat-label>Email</mat-label>
              <input matInput type="email" formControlName="email">
              <mat-error *ngIf="contactForm.get('email')?.hasError('email')">
                Please enter a valid email
              </mat-error>
            </mat-form-field>
          </div>

          <div class="contact-form__row">
            <mat-form-field appearance="outline" class="contact-form__field">
              <mat-label>Phone</mat-label>
              <input matInput formControlName="phone">
            </mat-form-field>

            <mat-form-field appearance="outline" class="contact-form__field">
              <mat-label>LinkedIn URL</mat-label>
              <input matInput formControlName="linkedInUrl">
            </mat-form-field>
          </div>

          <mat-form-field appearance="outline" class="contact-form__field--full">
            <mat-label>Location</mat-label>
            <input matInput formControlName="location">
          </mat-form-field>

          <mat-form-field appearance="outline" class="contact-form__field--full">
            <mat-label>Date Met</mat-label>
            <input matInput [matDatepicker]="picker" formControlName="dateMet">
            <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
            <mat-datepicker #picker></mat-datepicker>
          </mat-form-field>

          <mat-form-field appearance="outline" class="contact-form__field--full">
            <mat-label>Notes</mat-label>
            <textarea matInput formControlName="notes" rows="4"></textarea>
          </mat-form-field>

          <mat-form-field appearance="outline" class="contact-form__field--full">
            <mat-label>Tags (comma separated)</mat-label>
            <input matInput formControlName="tagsInput" placeholder="e.g., mentor, tech, conference">
          </mat-form-field>

          <div class="contact-form__checkbox">
            <mat-checkbox formControlName="isPriority">Mark as Priority</mat-checkbox>
          </div>

          <div class="contact-form__actions">
            <button mat-button type="button" (click)="onCancel()">Cancel</button>
            <button mat-raised-button color="primary" type="submit" [disabled]="contactForm.invalid">
              {{ isEditMode ? 'Update' : 'Create' }}
            </button>
          </div>
        </form>
      </mat-card>
    </div>
  `,
  styles: [`
    .contact-form {
      padding: 2rem;
    }

    .contact-form__title {
      margin-bottom: 2rem;
      color: #333;
    }

    .contact-form__card {
      max-width: 800px;
    }

    .contact-form__form {
      display: flex;
      flex-direction: column;
      gap: 1rem;
    }

    .contact-form__row {
      display: grid;
      grid-template-columns: 1fr 1fr;
      gap: 1rem;
    }

    .contact-form__field {
      width: 100%;
    }

    .contact-form__field--full {
      width: 100%;
    }

    .contact-form__checkbox {
      margin: 1rem 0;
    }

    .contact-form__actions {
      display: flex;
      justify-content: flex-end;
      gap: 1rem;
      margin-top: 1rem;
    }
  `]
})
export class ContactForm implements OnInit {
  private fb = inject(FormBuilder);
  private contactsService = inject(ContactsService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  contactForm!: FormGroup;
  isEditMode = false;
  contactId?: string;

  contactTypes = Object.keys(ContactType)
    .filter(key => !isNaN(Number(key)))
    .map(key => ({
      value: Number(key),
      label: ContactTypeLabels[Number(key)]
    }));

  ngOnInit(): void {
    this.contactForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      contactType: [ContactType.Colleague, Validators.required],
      company: [''],
      jobTitle: [''],
      email: ['', Validators.email],
      phone: [''],
      linkedInUrl: [''],
      location: [''],
      notes: [''],
      tagsInput: [''],
      dateMet: [new Date()],
      isPriority: [false]
    });

    const id = this.route.snapshot.paramMap.get('id');
    if (id && id !== 'new') {
      this.isEditMode = true;
      this.contactId = id;
      this.loadContact(id);
    }
  }

  loadContact(id: string): void {
    this.contactsService.getContactById(id).subscribe(contact => {
      this.contactForm.patchValue({
        firstName: contact.firstName,
        lastName: contact.lastName,
        contactType: contact.contactType,
        company: contact.company,
        jobTitle: contact.jobTitle,
        email: contact.email,
        phone: contact.phone,
        linkedInUrl: contact.linkedInUrl,
        location: contact.location,
        notes: contact.notes,
        tagsInput: contact.tags.join(', '),
        dateMet: contact.dateMet ? new Date(contact.dateMet) : new Date(),
        isPriority: contact.isPriority
      });
    });
  }

  onSubmit(): void {
    if (this.contactForm.valid) {
      const formValue = this.contactForm.value;
      const tags = formValue.tagsInput
        ? formValue.tagsInput.split(',').map((tag: string) => tag.trim()).filter((tag: string) => tag)
        : [];

      if (this.isEditMode && this.contactId) {
        const updateRequest = {
          contactId: this.contactId,
          firstName: formValue.firstName,
          lastName: formValue.lastName,
          contactType: formValue.contactType,
          company: formValue.company || undefined,
          jobTitle: formValue.jobTitle || undefined,
          email: formValue.email || undefined,
          phone: formValue.phone || undefined,
          linkedInUrl: formValue.linkedInUrl || undefined,
          location: formValue.location || undefined,
          notes: formValue.notes || undefined,
          tags: tags.length > 0 ? tags : undefined,
          dateMet: formValue.dateMet ? formValue.dateMet.toISOString() : undefined,
          isPriority: formValue.isPriority
        };
        this.contactsService.updateContact(updateRequest).subscribe(() => {
          this.router.navigate(['/contacts']);
        });
      } else {
        const createRequest = {
          userId: '00000000-0000-0000-0000-000000000000', // Default user ID
          firstName: formValue.firstName,
          lastName: formValue.lastName,
          contactType: formValue.contactType,
          company: formValue.company || undefined,
          jobTitle: formValue.jobTitle || undefined,
          email: formValue.email || undefined,
          phone: formValue.phone || undefined,
          linkedInUrl: formValue.linkedInUrl || undefined,
          location: formValue.location || undefined,
          notes: formValue.notes || undefined,
          tags: tags.length > 0 ? tags : undefined,
          dateMet: formValue.dateMet ? formValue.dateMet.toISOString() : undefined,
          isPriority: formValue.isPriority
        };
        this.contactsService.createContact(createRequest).subscribe(() => {
          this.router.navigate(['/contacts']);
        });
      }
    }
  }

  onCancel(): void {
    this.router.navigate(['/contacts']);
  }
}
