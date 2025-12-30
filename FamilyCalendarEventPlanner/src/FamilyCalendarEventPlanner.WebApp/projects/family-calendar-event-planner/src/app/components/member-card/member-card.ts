import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { FamilyMember, MemberRole } from '../../services/models';

@Component({
  selector: 'app-member-card',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule],
  templateUrl: './member-card.html',
  styleUrl: './member-card.scss'
})
export class MemberCard {
  @Input() member!: FamilyMember;
  @Input() eventCount = 0;
  @Output() editClick = new EventEmitter<FamilyMember>();
  @Output() viewScheduleClick = new EventEmitter<FamilyMember>();

  getInitials(): string {
    const names = this.member.name.split(' ');
    return names.map(n => n[0]).join('').toUpperCase().slice(0, 2);
  }

  getRoleBadgeClass(): string {
    return this.member.role === MemberRole.Admin ? 'member-card__role--admin' : '';
  }

  getRoleIcon(): string {
    switch (this.member.role) {
      case MemberRole.Admin:
        return 'admin_panel_settings';
      case MemberRole.Member:
        return 'person';
      case MemberRole.ViewOnly:
        return 'visibility';
      default:
        return 'person';
    }
  }

  onEditClick(): void {
    this.editClick.emit(this.member);
  }

  onViewScheduleClick(): void {
    this.viewScheduleClick.emit(this.member);
  }
}
