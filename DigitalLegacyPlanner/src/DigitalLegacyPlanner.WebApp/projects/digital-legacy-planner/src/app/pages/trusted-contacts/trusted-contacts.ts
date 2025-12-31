import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatChipsModule } from '@angular/material/chips';
import { TrustedContactsService } from '../../services';
import { TrustedContact } from '../../models';
import { TrustedContactDialog } from '../../components';

@Component({
  selector: 'app-trusted-contacts',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatDialogModule, MatChipsModule],
  templateUrl: './trusted-contacts.html',
  styleUrl: './trusted-contacts.scss'
})
export class TrustedContacts implements OnInit {
  contacts$ = this.contactsService.contacts$;

  // Hardcoded userId for demo purposes
  private readonly userId = '00000000-0000-0000-0000-000000000001';

  constructor(
    private contactsService: TrustedContactsService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.loadContacts();
  }

  loadContacts(): void {
    this.contactsService.getAll().subscribe();
  }

  openCreateDialog(): void {
    const dialogRef = this.dialog.open(TrustedContactDialog, {
      width: '600px',
      data: { userId: this.userId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.contactsService.create({
          userId: this.userId,
          ...result
        }).subscribe();
      }
    });
  }

  openEditDialog(contact: TrustedContact): void {
    const dialogRef = this.dialog.open(TrustedContactDialog, {
      width: '600px',
      data: { contact, userId: this.userId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.contactsService.update(contact.trustedContactId, {
          trustedContactId: contact.trustedContactId,
          ...result
        }).subscribe();
      }
    });
  }

  deleteContact(contact: TrustedContact): void {
    if (confirm(`Are you sure you want to delete ${contact.fullName}?`)) {
      this.contactsService.delete(contact.trustedContactId).subscribe();
    }
  }
}
