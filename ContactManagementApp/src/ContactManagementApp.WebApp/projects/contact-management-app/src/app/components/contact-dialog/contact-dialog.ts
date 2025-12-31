import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { Contact, ContactType, ContactTypeLabels } from '../../models';

export interface ContactDialogData {
  contact?: Contact;
  userId: string;
}

@Component({
  selector: 'app-contact-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatCheckboxModule,
    MatChipsModule,
    MatIconModule
  ],
  templateUrl: './contact-dialog.html',
  styleUrl: './contact-dialog.scss'
})
export class ContactDialog implements OnInit {
  form!: FormGroup;
  contactTypes = Object.values(ContactType).filter(v => typeof v === 'number') as ContactType[];
  contactTypeLabels = ContactTypeLabels;
  isEditMode = false;
  tagInput = '';

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<ContactDialog>,
    @Inject(MAT_DIALOG_DATA) public data: ContactDialogData
  ) {}

  ngOnInit(): void {
    this.isEditMode = !!this.data.contact;
    this.initForm();
  }

  initForm(): void {
    this.form = this.fb.group({
      firstName: [this.data.contact?.firstName || '', Validators.required],
      lastName: [this.data.contact?.lastName || '', Validators.required],
      contactType: [this.data.contact?.contactType ?? ContactType.Colleague, Validators.required],
      company: [this.data.contact?.company || ''],
      jobTitle: [this.data.contact?.jobTitle || ''],
      email: [this.data.contact?.email || '', Validators.email],
      phone: [this.data.contact?.phone || ''],
      linkedInUrl: [this.data.contact?.linkedInUrl || ''],
      location: [this.data.contact?.location || ''],
      notes: [this.data.contact?.notes || ''],
      tags: [this.data.contact?.tags || []],
      dateMet: [this.data.contact?.dateMet ? new Date(this.data.contact.dateMet) : new Date(), Validators.required],
      isPriority: [this.data.contact?.isPriority || false]
    });
  }

  addTag(): void {
    if (this.tagInput.trim()) {
      const tags = this.form.get('tags')?.value || [];
      if (!tags.includes(this.tagInput.trim())) {
        this.form.patchValue({ tags: [...tags, this.tagInput.trim()] });
      }
      this.tagInput = '';
    }
  }

  removeTag(tag: string): void {
    const tags = this.form.get('tags')?.value || [];
    this.form.patchValue({ tags: tags.filter((t: string) => t !== tag) });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const formValue = this.form.value;
      const result = {
        ...formValue,
        dateMet: formValue.dateMet.toISOString(),
        userId: this.data.userId,
        ...(this.isEditMode && { contactId: this.data.contact!.contactId })
      };
      this.dialogRef.close(result);
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
