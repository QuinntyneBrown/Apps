import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { InteractionsService, ContactsService } from '../../services';
import { combineLatest, map } from 'rxjs';

@Component({
  selector: 'app-interactions-list',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule, MatCardModule],
  templateUrl: './interactions-list.component.html',
  styleUrl: './interactions-list.component.scss'
})
export class InteractionsList {
  private _interactionsService = inject(InteractionsService);
  private _contactsService = inject(ContactsService);
  private _router = inject(Router);

  viewModel$ = combineLatest([
    this._interactionsService.interactions$,
    this._contactsService.contacts$
  ]).pipe(
    map(([interactions, contacts]) => ({
      interactions,
      contactsMap: new Map(contacts.map(c => [c.contactId, c.fullName]))
    }))
  );

  displayedColumns: string[] = ['contactId', 'interactionType', 'interactionDate', 'subject', 'outcome', 'duration', 'actions'];

  editInteraction(id: string): void {
    this._router.navigate(['/interactions', id]);
  }

  deleteInteraction(id: string): void {
    if (confirm('Are you sure you want to delete this interaction?')) {
      this._interactionsService.deleteInteraction(id).subscribe();
    }
  }
}
