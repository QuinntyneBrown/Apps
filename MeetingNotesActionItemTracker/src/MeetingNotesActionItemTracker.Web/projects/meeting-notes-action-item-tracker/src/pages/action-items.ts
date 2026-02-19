import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { ActionItemService } from '../services';
import { ActionItem, PRIORITY_LABELS, ACTION_ITEM_STATUS_LABELS } from '../models';

@Component({
  selector: 'app-action-items',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule, MatIconModule, MatCardModule, MatChipsModule],
  templateUrl: './action-items.html',
  styleUrl: './action-items.scss'
})
export class ActionItems implements OnInit {
  private actionItemService = inject(ActionItemService);
  private router = inject(Router);

  actionItems$ = this.actionItemService.actionItems$;
  displayedColumns: string[] = ['description', 'responsiblePerson', 'dueDate', 'priority', 'status', 'actions'];

  ngOnInit(): void {
    this.actionItemService.getActionItems().subscribe();
  }

  editActionItem(actionItem: ActionItem): void {
    this.router.navigate(['/action-items', actionItem.actionItemId]);
  }

  deleteActionItem(actionItem: ActionItem): void {
    if (confirm(`Are you sure you want to delete this action item?`)) {
      this.actionItemService.deleteActionItem(actionItem.actionItemId).subscribe();
    }
  }

  getPriorityLabel(priority: number): string {
    return PRIORITY_LABELS[priority];
  }

  getStatusLabel(status: number): string {
    return ACTION_ITEM_STATUS_LABELS[status];
  }

  formatDate(date?: string): string {
    return date ? new Date(date).toLocaleDateString() : 'N/A';
  }
}
