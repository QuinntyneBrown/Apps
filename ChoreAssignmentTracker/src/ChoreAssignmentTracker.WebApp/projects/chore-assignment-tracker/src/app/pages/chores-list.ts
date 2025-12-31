import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { ChoreService } from '../services/chore.service';

@Component({
  selector: 'app-chores-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatChipsModule],
  templateUrl: './chores-list.html',
  styleUrl: './chores-list.scss'
})
export class ChoresList {
  private choreService = inject(ChoreService);

  chores$ = this.choreService.chores$;
  displayedColumns = ['name', 'category', 'frequency', 'points', 'estimatedMinutes', 'isActive', 'actions'];

  constructor() {
    this.choreService.getAll().subscribe();
  }

  deleteChore(id: string): void {
    if (confirm('Are you sure you want to delete this chore?')) {
      this.choreService.delete(id).subscribe();
    }
  }
}
