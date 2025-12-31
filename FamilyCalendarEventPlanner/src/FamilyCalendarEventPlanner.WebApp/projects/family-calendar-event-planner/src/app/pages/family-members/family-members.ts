import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { map, combineLatest, switchMap, BehaviorSubject } from 'rxjs';

import { FamilyMembersService, EventsService } from '../../services';
import { MemberCard } from '../../components';
import { FamilyMember, MemberRole } from '../../models';

type ImmediateFilter = 'all' | 'immediate' | 'extended';

@Component({
  selector: 'app-family-members',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatButtonToggleModule,
    MemberCard
  ],
  templateUrl: './family-members.html',
  styleUrl: './family-members.scss'
})
export class FamilyMembers {
  private membersService = inject(FamilyMembersService);
  private eventsService = inject(EventsService);
  private dialog = inject(MatDialog);

  immediateFilter$ = new BehaviorSubject<ImmediateFilter>('all');

  viewModel$ = combineLatest([
    this.immediateFilter$.pipe(
      switchMap(filter => {
        const isImmediate = filter === 'all' ? undefined : filter === 'immediate';
        return this.membersService.getFamilyMembers({ isImmediate });
      })
    ),
    this.eventsService.getEvents()
  ]).pipe(
    map(([members, events]) => {
      const memberEventCounts = new Map<string, number>();
      events.forEach(event => {
        const count = memberEventCounts.get(event.creatorId) || 0;
        memberEventCounts.set(event.creatorId, count + 1);
      });

      const adminCount = members.filter(m => m.role === MemberRole.Admin).length;
      const immediateCount = members.filter(m => m.isImmediate).length;

      return {
        members,
        memberEventCounts,
        stats: {
          activeMembers: members.length,
          admins: adminCount,
          immediateFamily: immediateCount
        }
      };
    })
  );

  onFilterChange(filter: ImmediateFilter): void {
    this.immediateFilter$.next(filter);
  }

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
