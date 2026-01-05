import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ContactsService, FollowUpsService, InteractionsService } from '../../services';
import { combineLatest, map } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class Dashboard {
  private _contactsService = inject(ContactsService);
  private _followUpsService = inject(FollowUpsService);
  private _interactionsService = inject(InteractionsService);

  viewModel$ = combineLatest([
    this._contactsService.contacts$,
    this._followUpsService.followUps$,
    this._interactionsService.interactions$
  ]).pipe(
    map(([contacts, followUps, interactions]) => ({
      contactsCount: contacts.length,
      priorityContacts: contacts.filter(c => c.isPriority).length,
      pendingFollowUps: followUps.filter(f => !f.isCompleted).length,
      interactionsCount: interactions.length
    }))
  );
}
