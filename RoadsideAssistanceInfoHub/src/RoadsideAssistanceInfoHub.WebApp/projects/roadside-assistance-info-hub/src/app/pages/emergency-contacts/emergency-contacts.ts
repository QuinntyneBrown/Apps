import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { EmergencyContactService } from '../../services';
import { EmergencyContactFormDialog } from './emergency-contact-form-dialog';

@Component({
  selector: 'app-emergency-contacts',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatCardModule, MatDialogModule],
  templateUrl: './emergency-contacts.html',
  styleUrl: './emergency-contacts.scss'
})
export class EmergencyContacts implements OnInit {
  private _emergencyContactService = inject(EmergencyContactService);
  private _dialog = inject(MatDialog);

  emergencyContacts$ = this._emergencyContactService.emergencyContacts$;
  displayedColumns = ['name', 'phoneNumber', 'relationship', 'isPrimaryContact', 'isActive', 'actions'];

  ngOnInit(): void {
    this._emergencyContactService.getEmergencyContacts().subscribe();
  }

  openCreateDialog(): void {
    const dialogRef = this._dialog.open(EmergencyContactFormDialog, {
      width: '600px',
      data: null
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._emergencyContactService.createEmergencyContact(result).subscribe();
      }
    });
  }

  openEditDialog(contact: any): void {
    const dialogRef = this._dialog.open(EmergencyContactFormDialog, {
      width: '600px',
      data: contact
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._emergencyContactService.updateEmergencyContact(contact.emergencyContactId, result).subscribe();
      }
    });
  }

  deleteContact(id: string): void {
    if (confirm('Are you sure you want to delete this emergency contact?')) {
      this._emergencyContactService.deleteEmergencyContact(id).subscribe();
    }
  }
}
