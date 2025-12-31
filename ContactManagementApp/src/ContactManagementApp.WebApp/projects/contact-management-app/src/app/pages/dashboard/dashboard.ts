import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ContactService, InteractionService, FollowUpService } from '../../services';
import { Observable, combineLatest, map } from 'rxjs';

interface DashboardStats {
  totalContacts: number;
  priorityContacts: number;
  recentInteractions: number;
  pendingFollowUps: number;
}

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
  private contactService = inject(ContactService);
  private interactionService = inject(InteractionService);
  private followUpService = inject(FollowUpService);

  stats$!: Observable<DashboardStats>;
  loading$!: Observable<boolean>;

  ngOnInit(): void {
    // Mock user ID - in real app this would come from auth service
    const userId = '00000000-0000-0000-0000-000000000001';

    this.contactService.getContacts(userId).subscribe();
    this.interactionService.getInteractions(userId).subscribe();
    this.followUpService.getFollowUps(userId, undefined, false).subscribe();

    this.stats$ = combineLatest([
      this.contactService.contacts$,
      this.interactionService.interactions$,
      this.followUpService.followUps$
    ]).pipe(
      map(([contacts, interactions, followUps]) => ({
        totalContacts: contacts.length,
        priorityContacts: contacts.filter(c => c.isPriority).length,
        recentInteractions: interactions.length,
        pendingFollowUps: followUps.filter(f => !f.isCompleted).length
      }))
    );

    this.loading$ = combineLatest([
      this.contactService.loading$,
      this.interactionService.loading$,
      this.followUpService.loading$
    ]).pipe(
      map(([l1, l2, l3]) => l1 || l2 || l3)
    );
  }
}
