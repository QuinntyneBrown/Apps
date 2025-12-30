import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { map, combineLatest } from 'rxjs';

import { FamilyMembersService, EventsService } from '../../services';
import { MemberCard } from '../../components';
import { FamilyMember, MemberRole } from '../../services/models';

@Component({
  selector: 'app-family-members',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MemberCard
  ],
  templateUrl: './family-members.html',
  styleUrl: './family-members.scss'
})
export class FamilyMembers {
  private membersService = inject(FamilyMembersService);
  private eventsService = inject(EventsService);
  private dialog = inject(MatDialog);

  viewModel$ = combineLatest([
    this.membersService.getFamilyMembers(),
    this.eventsService.getEvents()
  ]).pipe(
    map(([members, events]) => {
      const memberEventCounts = new Map<string, number>();
      events.forEach(event => {
        const count = memberEventCounts.get(event.creatorId) || 0;
        memberEventCounts.set(event.creatorId, count + 1);
      });

      const adminCount = members.filter(m => m.role === MemberRole.Admin).length;

      return {
        members,
        memberEventCounts,
        stats: {
          activeMembers: members.length,
          admins: adminCount,
          pendingInvitations: 0
        }
      };
    })
  );

  getEventCount(memberEventCounts: Map<string, number>, memberId: string): number {
    return memberEventCounts.get(memberId) || 0;
  }

  onEditMember(member: FamilyMember): void {
    console.log('Edit member:', member);
  }

  onViewSchedule(member: FamilyMember): void {
    console.log('View schedule:', member);
  }

  onAddMember(): void {
    console.log('Add new member');
  }
}
