import { Component, inject } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { ContactsService, FollowUpsService } from '../../services';
import { map } from 'rxjs';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive, MatListModule, MatIconModule],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.scss'
})
export class Sidebar {
  private _contactsService = inject(ContactsService);
  private _followUpsService = inject(FollowUpsService);

  contactsCount$ = this._contactsService.contacts$.pipe(
    map(contacts => contacts.length)
  );

  followUpsCount$ = this._followUpsService.followUps$.pipe(
    map(followUps => followUps.filter(f => !f.isCompleted).length)
  );

  menuItems = [
    { path: '/', label: 'Dashboard', icon: 'dashboard', exact: true },
    { path: '/contacts', label: 'Contacts', icon: 'people', count$: this.contactsCount$ },
    { path: '/follow-ups', label: 'Follow-Ups', icon: 'event', count$: this.followUpsCount$ },
    { path: '/events', label: 'Events', icon: 'event_note' },
    { path: '/opportunities', label: 'Opportunities', icon: 'business_center' },
    { path: '/goals', label: 'Goals', icon: 'flag' },
    { path: '/analytics', label: 'Analytics', icon: 'analytics' }
  ];
}
