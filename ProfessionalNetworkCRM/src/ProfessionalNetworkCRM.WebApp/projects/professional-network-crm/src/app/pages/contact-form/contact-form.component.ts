import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
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
import { ContactsService } from '../../services';
import { ContactType, ContactTypeLabels } from '../../models';
import { map, switchMap, of, take } from 'rxjs';

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
  templateUrl: './contact-form.component.html',
  styleUrl: './contact-form.component.scss'
})
export class ContactForm {
  private fb = inject(FormBuilder);
  private contactsService = inject(ContactsService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  contactTypes = Object.keys(ContactType)
    .filter(key => !isNaN(Number(key)))
    .map(key => ({
      value: Number(key),
      label: ContactTypeLabels[Number(key) as ContactType]
    }));

  private contactId$ = of(this.route.snapshot.paramMap.get('id')).pipe(
    map(id => id && id !== 'new' ? id : null)
  );

  contactForm = this.fb.group({
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

  viewModel$ = this.contactId$.pipe(
    switchMap(id => {
      if (id) {
        return this.contactsService.getContactById(id).pipe(
          map(contact => {
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
            return { isEditMode: true, contactId: id };
          })
        );
      }
      return of({ isEditMode: false, contactId: null });
    })
  );

  onSubmit(): void {
    if (this.contactForm.valid) {
      const formValue = this.contactForm.value;
      const tags = formValue.tagsInput
        ? formValue.tagsInput.split(',').map((tag: string) => tag.trim()).filter((tag: string) => tag)
        : [];

      this.contactId$.pipe(take(1)).subscribe(contactId => {
        if (contactId) {
          const updateRequest = {
            contactId: contactId,
            firstName: formValue.firstName!,
            lastName: formValue.lastName!,
            contactType: formValue.contactType!,
            company: formValue.company || undefined,
            jobTitle: formValue.jobTitle || undefined,
            email: formValue.email || undefined,
            phone: formValue.phone || undefined,
            linkedInUrl: formValue.linkedInUrl || undefined,
            location: formValue.location || undefined,
            notes: formValue.notes || undefined,
            tags: tags.length > 0 ? tags : undefined,
            dateMet: formValue.dateMet ? formValue.dateMet.toISOString() : undefined,
            isPriority: formValue.isPriority!
          };
          this.contactsService.updateContact(updateRequest).subscribe(() => {
            this.router.navigate(['/contacts']);
          });
        } else {
          const createRequest = {
            userId: '00000000-0000-0000-0000-000000000000',
            firstName: formValue.firstName!,
            lastName: formValue.lastName!,
            contactType: formValue.contactType!,
            company: formValue.company || undefined,
            jobTitle: formValue.jobTitle || undefined,
            email: formValue.email || undefined,
            phone: formValue.phone || undefined,
            linkedInUrl: formValue.linkedInUrl || undefined,
            location: formValue.location || undefined,
            notes: formValue.notes || undefined,
            tags: tags.length > 0 ? tags : undefined,
            dateMet: formValue.dateMet ? formValue.dateMet.toISOString() : undefined,
            isPriority: formValue.isPriority!
          };
          this.contactsService.createContact(createRequest).subscribe(() => {
            this.router.navigate(['/contacts']);
          });
        }
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/contacts']);
  }
}
