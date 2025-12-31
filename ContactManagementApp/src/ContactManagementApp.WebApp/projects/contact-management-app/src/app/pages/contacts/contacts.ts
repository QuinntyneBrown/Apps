import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { ContactService } from '../../services';
import { ContactCard } from '../../components/contact-card/contact-card';
import { ContactDialog } from '../../components/contact-dialog/contact-dialog';
import { Contact } from '../../models';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-contacts',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
    ContactCard
  ],
  templateUrl: './contacts.html',
  styleUrl: './contacts.scss'
})
export class Contacts implements OnInit {
  private contactService = inject(ContactService);
  private dialog = inject(MatDialog);
  private snackBar = inject(MatSnackBar);

  contacts$!: Observable<Contact[]>;
  loading$!: Observable<boolean>;

  private userId = '00000000-0000-0000-0000-000000000001'; // Mock user ID

  ngOnInit(): void {
    this.contacts$ = this.contactService.contacts$;
    this.loading$ = this.contactService.loading$;
    this.loadContacts();
  }

  loadContacts(): void {
    this.contactService.getContacts(this.userId).subscribe();
  }

  openCreateDialog(): void {
    const dialogRef = this.dialog.open(ContactDialog, {
      width: '600px',
      data: { userId: this.userId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.contactService.createContact(result).subscribe({
          next: () => {
            this.snackBar.open('Contact created successfully', 'Close', { duration: 3000 });
          },
          error: () => {
            this.snackBar.open('Failed to create contact', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  onEditContact(contact: Contact): void {
    const dialogRef = this.dialog.open(ContactDialog, {
      width: '600px',
      data: { contact, userId: this.userId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.contactService.updateContact(contact.contactId, result).subscribe({
          next: () => {
            this.snackBar.open('Contact updated successfully', 'Close', { duration: 3000 });
          },
          error: () => {
            this.snackBar.open('Failed to update contact', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  onDeleteContact(contact: Contact): void {
    if (confirm(`Are you sure you want to delete ${contact.fullName}?`)) {
      this.contactService.deleteContact(contact.contactId).subscribe({
        next: () => {
          this.snackBar.open('Contact deleted successfully', 'Close', { duration: 3000 });
        },
        error: () => {
          this.snackBar.open('Failed to delete contact', 'Close', { duration: 3000 });
        }
      });
    }
  }

  onViewDetails(contact: Contact): void {
    this.contactService.setSelectedContact(contact);
    // In a real app, navigate to contact detail page
    this.snackBar.open(`Viewing ${contact.fullName}`, 'Close', { duration: 2000 });
  }
}
