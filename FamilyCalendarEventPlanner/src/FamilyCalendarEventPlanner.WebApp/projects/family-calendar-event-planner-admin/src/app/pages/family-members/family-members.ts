import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatChipsModule } from '@angular/material/chips';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { BehaviorSubject, combineLatest, switchMap } from 'rxjs';
import { FamilyMembersService } from '../../services/family-members.service';
import { HouseholdsService } from '../../services/households.service';
import { FamilyMemberDto, MemberRole, getRoleLabel } from '../../models/family-member-dto';
import { HouseholdDto } from '../../models/household-dto';
import { CreateOrEditFamilyMemberDialog, CreateOrEditFamilyMemberDialogResult } from '../../components/create-or-edit-family-member-dialog';

type ImmediateFilter = 'all' | 'immediate' | 'extended';

@Component({
  selector: 'app-family-members',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule,
    MatButtonToggleModule,
    MatChipsModule,
    MatSelectModule,
    MatFormFieldModule
  ],
  templateUrl: './family-members.html',
  styleUrls: ['./family-members.scss']
})
export class FamilyMembers {
  private membersService = inject(FamilyMembersService);
  private householdsService = inject(HouseholdsService);
  private dialog = inject(MatDialog);

  private refresh$ = new BehaviorSubject<void>(undefined);
  immediateFilter$ = new BehaviorSubject<ImmediateFilter>('all');
  householdFilter$ = new BehaviorSubject<string | null>(null);

  households$ = this.householdsService.getHouseholds();

  displayedColumns: string[] = ['avatar', 'name', 'email', 'relationType', 'role', 'isImmediate', 'color', 'actions'];

  members$ = combineLatest([this.refresh$, this.immediateFilter$, this.householdFilter$]).pipe(
    switchMap(([_, immediateFilter, householdId]) => {
      const isImmediate = immediateFilter === 'all' ? undefined : immediateFilter === 'immediate';
      return this.membersService.getFamilyMembers({
        isImmediate,
        householdId: householdId || undefined
      });
    })
  );

  getInitials(name: string): string {
    const names = name.split(' ');
    return names.map(n => n[0]).join('').toUpperCase().slice(0, 2);
  }

  getRelationTypeLabel(relationType: string): string {
    const labels: Record<string, string> = {
      Self: 'Self',
      Spouse: 'Spouse',
      Child: 'Child',
      Parent: 'Parent',
      Sibling: 'Sibling',
      Grandparent: 'Grandparent',
      Grandchild: 'Grandchild',
      AuntUncle: 'Aunt/Uncle',
      NieceNephew: 'Niece/Nephew',
      Cousin: 'Cousin',
      InLaw: 'In-Law',
      Other: 'Other'
    };
    return labels[relationType] || relationType;
  }

  onFilterChange(filter: ImmediateFilter): void {
    this.immediateFilter$.next(filter);
  }

  onHouseholdFilterChange(householdId: string | null): void {
    this.householdFilter$.next(householdId);
  }

  getRoleLabel(role: number): string {
    return getRoleLabel(role);
  }

  isAdminRole(role: number): boolean {
    return role === MemberRole.Admin;
  }

  onCreateMember(): void {
    const dialogRef = this.dialog.open(CreateOrEditFamilyMemberDialog, {
      width: '500px',
      data: {
        familyId: crypto.randomUUID()
      }
    });

    dialogRef.afterClosed().subscribe((result: CreateOrEditFamilyMemberDialogResult) => {
      if (result?.action === 'create' && result.data) {
        console.log('CreateFamilyMemberCommand payload:', result.data);
        this.membersService.createFamilyMember(result.data).subscribe({
          next: () => {
            this.refresh$.next();
          },
          error: (error) => {
            console.error('Error creating member:', error);
          }
        });
      }
    });
  }

  onEditMember(member: FamilyMemberDto): void {
    const dialogRef = this.dialog.open(CreateOrEditFamilyMemberDialog, {
      width: '500px',
      data: {
        member,
        familyId: member.familyId
      }
    });

    dialogRef.afterClosed().subscribe((result: CreateOrEditFamilyMemberDialogResult) => {
      if (result?.action === 'update' && result.data) {
        this.membersService.updateFamilyMember(member.memberId, { ...result.data, memberId: member.memberId }).subscribe({
          next: () => {
            this.refresh$.next();
          },
          error: (error) => {
            console.error('Error updating member:', error);
          }
        });
      }
    });
  }

  onDeleteMember(member: FamilyMemberDto): void {
    if (confirm(`Are you sure you want to delete ${member.name}?`)) {
      this.membersService.deleteFamilyMember(member.memberId).subscribe({
        next: () => {
          this.refresh$.next();
        },
        error: (error) => {
          console.error('Error deleting member:', error);
        }
      });
    }
  }
}
