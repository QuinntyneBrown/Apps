import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatCardModule } from '@angular/material/card';
import { FollowUpsService, ContactsService } from '../../services';
import { combineLatest, map } from 'rxjs';

@Component({
  selector: 'app-follow-ups-list',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule, MatChipsModule, MatCardModule],
  templateUrl: './follow-ups-list.component.html',
  styleUrl: './follow-ups-list.component.scss'
})
export class FollowUpsList {
  private _followUpsService = inject(FollowUpsService);
  private _contactsService = inject(ContactsService);
  private _router = inject(Router);

  viewModel$ = combineLatest([
    this._followUpsService.followUps$,
    this._contactsService.contacts$
  ]).pipe(
    map(([followUps, contacts]) => ({
      followUps,
      contactsMap: new Map(contacts.map(c => [c.contactId, c.fullName]))
    }))
  );

  displayedColumns: string[] = ['description', 'contactId', 'dueDate', 'priority', 'status', 'actions'];

  completeFollowUp(id: string): void {
    this._followUpsService.completeFollowUp(id).subscribe();
  }

  editFollowUp(id: string): void {
    this._router.navigate(['/follow-ups', id]);
  }

  deleteFollowUp(id: string): void {
    if (confirm('Are you sure you want to delete this follow-up?')) {
      this._followUpsService.deleteFollowUp(id).subscribe();
    }
  }
}
