import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { EventService, SessionService, ContactService, NoteService } from '../../services';
import { Event, Session, Contact, Note } from '../../models';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  events: Event[] = [];
  sessions: Session[] = [];
  contacts: Contact[] = [];
  notes: Note[] = [];
  loading = true;

  constructor(
    private eventService: EventService,
    private sessionService: SessionService,
    private contactService: ContactService,
    private noteService: NoteService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.loading = true;

    this.eventService.getAll().subscribe({
      next: (events) => {
        this.events = events.slice(0, 5);
      },
      error: (error) => console.error('Error loading events:', error)
    });

    this.sessionService.getAll().subscribe({
      next: (sessions) => {
        this.sessions = sessions.slice(0, 5);
      },
      error: (error) => console.error('Error loading sessions:', error)
    });

    this.contactService.getAll().subscribe({
      next: (contacts) => {
        this.contacts = contacts.slice(0, 5);
      },
      error: (error) => console.error('Error loading contacts:', error)
    });

    this.noteService.getAll().subscribe({
      next: (notes) => {
        this.notes = notes.slice(0, 5);
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading notes:', error);
        this.loading = false;
      }
    });
  }

  navigateToEvents(): void {
    this.router.navigate(['/events']);
  }

  navigateToSessions(): void {
    this.router.navigate(['/sessions']);
  }

  navigateToContacts(): void {
    this.router.navigate(['/contacts']);
  }

  navigateToNotes(): void {
    this.router.navigate(['/notes']);
  }
}
