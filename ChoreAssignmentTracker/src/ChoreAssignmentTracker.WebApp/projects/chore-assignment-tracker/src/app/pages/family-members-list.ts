import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { FamilyMemberService } from '../services/family-member.service';

@Component({
  selector: 'app-family-members-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatChipsModule],
  templateUrl: './family-members-list.html',
  styleUrl: './family-members-list.scss'
})
export class FamilyMembersList {
  private familyMemberService = inject(FamilyMemberService);

  familyMembers$ = this.familyMemberService.familyMembers$;
  displayedColumns = ['name', 'age', 'totalPoints', 'availablePoints', 'completionRate', 'isActive', 'actions'];

  constructor() {
    this.familyMemberService.getAll().subscribe();
  }

  deleteFamilyMember(id: string): void {
    if (confirm('Are you sure you want to delete this family member?')) {
      this.familyMemberService.delete(id).subscribe();
    }
  }
}
