import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatCardModule } from '@angular/material/card';
import { ContactsService } from '../../services';
import { ContactType, ContactTypeLabels } from '../../models';

@Component({
  selector: 'app-contacts-list',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule, MatChipsModule, MatCardModule],
  templateUrl: './contacts-list.component.html',
  styleUrl: './contacts-list.component.scss'
})
export class ContactsList {
  private _contactsService = inject(ContactsService);
  private _router = inject(Router);

  contacts$ = this._contactsService.contacts$;
  displayedColumns: string[] = ['name', 'type', 'company', 'jobTitle', 'email', 'priority', 'actions'];

  getContactTypeLabel(type: number): string {
    return ContactTypeLabels[type as ContactType] || 'Unknown';
  }

  editContact(id: string): void {
    this._router.navigate(['/contacts', id]);
  }

  deleteContact(id: string): void {
    if (confirm('Are you sure you want to delete this contact?')) {
      this._contactsService.deleteContact(id).subscribe();
    }
  }
}
