import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatDividerModule } from '@angular/material/divider';
import { MatChipsModule } from '@angular/material/chips';
import { map, combineLatest } from 'rxjs';

import { HouseholdsService, FamilyMembersService } from '../../services';
import { Household, FamilyMember } from '../../models';

@Component({
  selector: 'app-households',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatListModule,
    MatDividerModule,
    MatChipsModule
  ],
  templateUrl: './households.html',
  styleUrl: './households.scss'
})
export class Households {
  private householdsService = inject(HouseholdsService);
  private membersService = inject(FamilyMembersService);

  households$ = this.householdsService.getHouseholds();
  members$ = this.membersService.getFamilyMembers();

  viewModel$ = combineLatest([
    this.households$,
    this.members$
  ]).pipe(
    map(([households, members]) => ({
      households: households.map(household => ({
        ...household,
        members: members.filter(m => m.householdId === household.householdId),
        memberCount: members.filter(m => m.householdId === household.householdId).length
      })),
      totalHouseholds: households.length,
      totalMembers: members.filter(m => m.householdId !== null).length
    }))
  );

  getMemberInitials(member: FamilyMember): string {
    const names = member.name.split(' ');
    return names.map(n => n[0]).join('').toUpperCase().slice(0, 2);
  }

  formatProvince(province: string): string {
    return province.replace(/([A-Z])/g, ' $1').trim();
  }
}
