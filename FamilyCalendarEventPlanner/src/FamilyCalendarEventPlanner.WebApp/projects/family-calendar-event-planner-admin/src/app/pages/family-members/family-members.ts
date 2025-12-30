import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';
import { BehaviorSubject, switchMap } from 'rxjs';
import { FamilyMembersService } from '../../services/family-members.service';
import { FamilyMemberDto } from '../../models/family-member-dto';
import { CreateOrEditFamilyMemberDialog, CreateOrEditFamilyMemberDialogResult } from '../../components/create-or-edit-family-member-dialog';

@Component({
  selector: 'app-family-members',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule
  ],
  templateUrl: './family-members.html',
  styleUrls: ['./family-members.scss']
})
export class FamilyMembers {
  private membersService = inject(FamilyMembersService);
  private dialog = inject(MatDialog);

  private refresh$ = new BehaviorSubject<void>(undefined);

  displayedColumns: string[] = ['avatar', 'name', 'email', 'role', 'color', 'actions'];

  members$ = this.refresh$.pipe(
    switchMap(() => this.membersService.getFamilyMembers())
  );

  getInitials(name: string): string {
    const names = name.split(' ');
    return names.map(n => n[0]).join('').toUpperCase().slice(0, 2);
  }

  onCreateMember(): void {
    const dialogRef = this.dialog.open(CreateOrEditFamilyMemberDialog, {
      width: '500px',
      data: {
        familyId: 'default-family-id'
      }
    });

    dialogRef.afterClosed().subscribe((result: CreateOrEditFamilyMemberDialogResult) => {
      if (result?.action === 'create' && result.data) {
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
        this.membersService.updateFamilyMember(member.memberId, result.data).subscribe({
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
