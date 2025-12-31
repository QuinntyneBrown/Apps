import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { RouterLink } from '@angular/router';
import { map, combineLatest } from 'rxjs';
import { FamilyMemberService } from '../services/family-member.service';
import { AssignmentService } from '../services/assignment.service';
import { ChoreService } from '../services/chore.service';
import { RewardService } from '../services/reward.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, RouterLink],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard {
  private familyMemberService = inject(FamilyMemberService);
  private assignmentService = inject(AssignmentService);
  private choreService = inject(ChoreService);
  private rewardService = inject(RewardService);

  viewModel$ = combineLatest([
    this.familyMemberService.familyMembers$,
    this.assignmentService.assignments$,
    this.choreService.chores$,
    this.rewardService.rewards$
  ]).pipe(
    map(([familyMembers, assignments, chores, rewards]) => {
      const activeAssignments = assignments.filter(a => !a.isCompleted);
      const completedAssignments = assignments.filter(a => a.isCompleted);
      const overdueAssignments = assignments.filter(a => a.isOverdue && !a.isCompleted);

      return {
        totalFamilyMembers: familyMembers.length,
        totalChores: chores.filter(c => c.isActive).length,
        activeAssignments: activeAssignments.length,
        completedAssignments: completedAssignments.length,
        overdueAssignments: overdueAssignments.length,
        availableRewards: rewards.filter(r => r.isAvailable).length
      };
    })
  );

  constructor() {
    this.familyMemberService.getAll().subscribe();
    this.assignmentService.getAll().subscribe();
    this.choreService.getAll().subscribe();
    this.rewardService.getAll().subscribe();
  }
}
