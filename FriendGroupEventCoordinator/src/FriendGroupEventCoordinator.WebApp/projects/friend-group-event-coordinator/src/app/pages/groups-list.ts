import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { GroupsService } from '../services';
import { GroupCard } from '../components';
import { Group } from '../models';

@Component({
  selector: 'app-groups-list',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatDialogModule,
    GroupCard
  ],
  template: `
    <div class="groups-list">
      <div class="groups-list__header">
        <h1 class="groups-list__title">Groups</h1>
        <button mat-raised-button color="primary" class="groups-list__create-btn">
          <mat-icon>add</mat-icon>
          Create Group
        </button>
      </div>

      <div class="groups-list__content" *ngIf="groups$ | async as groups; else loading">
        <div class="groups-list__empty" *ngIf="groups.length === 0">
          <p>No groups found. Create your first group to get started!</p>
        </div>
        <div class="groups-list__grid" *ngIf="groups.length > 0">
          <app-group-card
            *ngFor="let group of groups"
            [group]="group"
            (onView)="viewGroup($event)"
            (onEdit)="editGroup($event)"
            (onViewMembers)="viewMembers($event)"
            (onViewEvents)="viewEvents($event)"
            (onDeactivate)="deactivateGroup($event)"
          ></app-group-card>
        </div>
      </div>

      <ng-template #loading>
        <div class="groups-list__loading">
          <mat-spinner></mat-spinner>
        </div>
      </ng-template>
    </div>
  `,
  styles: [`
    .groups-list {
      padding: 2rem;
    }

    .groups-list__header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }

    .groups-list__title {
      margin: 0;
      font-size: 2rem;
    }

    .groups-list__create-btn {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }

    .groups-list__grid {
      display: grid;
      grid-template-columns: repeat(auto-fill, minmax(350px, 1fr));
      gap: 1.5rem;
    }

    .groups-list__loading,
    .groups-list__empty {
      display: flex;
      justify-content: center;
      align-items: center;
      padding: 3rem;
    }
  `]
})
export class GroupsList implements OnInit {
  groups$ = this.groupsService.groups$;

  constructor(
    private groupsService: GroupsService,
    private router: Router,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.groupsService.getGroups().subscribe();
  }

  viewGroup(group: Group): void {
    this.router.navigate(['/groups', group.groupId]);
  }

  editGroup(group: Group): void {
    // TODO: Open edit dialog
    console.log('Edit group', group);
  }

  viewMembers(group: Group): void {
    this.router.navigate(['/groups', group.groupId, 'members']);
  }

  viewEvents(group: Group): void {
    this.router.navigate(['/groups', group.groupId, 'events']);
  }

  deactivateGroup(group: Group): void {
    if (confirm(`Are you sure you want to deactivate "${group.name}"?`)) {
      this.groupsService.deactivateGroup(group.groupId).subscribe();
    }
  }
}
