import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { AssignmentService } from '../services/assignment.service';

@Component({
  selector: 'app-assignments-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatChipsModule],
  templateUrl: './assignments-list.html',
  styleUrl: './assignments-list.scss'
})
export class AssignmentsList {
  private assignmentService = inject(AssignmentService);

  assignments$ = this.assignmentService.assignments$;
  displayedColumns = ['choreName', 'familyMemberName', 'assignedDate', 'dueDate', 'status', 'pointsEarned', 'actions'];

  constructor() {
    this.assignmentService.getAll().subscribe();
  }

  deleteAssignment(id: string): void {
    if (confirm('Are you sure you want to delete this assignment?')) {
      this.assignmentService.delete(id).subscribe();
    }
  }

  completeAssignment(id: string): void {
    this.assignmentService.complete(id, {}).subscribe();
  }

  verifyAssignment(id: string, points: number): void {
    this.assignmentService.verify(id, { points }).subscribe();
  }
}
