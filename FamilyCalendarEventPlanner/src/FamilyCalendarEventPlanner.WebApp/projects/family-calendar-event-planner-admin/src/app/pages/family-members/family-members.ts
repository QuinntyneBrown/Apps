import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatChipsModule } from '@angular/material/chips';
import { BehaviorSubject, combineLatest, switchMap } from 'rxjs';
import { FamilyMembersService } from '../../services/family-members.service';
import { FamilyMemberDto } from '../../models/family-member-dto';
import { MemberRole, getRoleLabel } from '../../models/family-member-dto';
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
    MatChipsModule
  ],
  templateUrl: './family-members.html',
  styleUrls: ['./family-members.scss']
})
export class FamilyMembers {
  private membersService = inject(FamilyMembersService);
  private dialog = inject(MatDialog);

  private refresh$ = new BehaviorSubject<void>(undefined);
  immediateFilter$ = new BehaviorSubject<ImmediateFilter>('all');

  displayedColumns: string[] = ['avatar', 'name', 'email', 'relationType', 'role', 'isImmediate', 'color', 'actions'];

  members$ = combineLatest([this.refresh$, this.immediateFilter$]).pipe(
    switchMap(([_, filter]) => {
      const isImmediate = filter === 'all' ? undefined : filter === 'immediate';
      return this.membersService.getFamilyMembers({ isImmediate });
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
