import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Organization } from '../../models';

@Component({
  selector: 'app-organization-card',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './organization-card.html',
  styleUrl: './organization-card.scss'
})
export class OrganizationCard {
  @Input() organization!: Organization;
  @Output() edit = new EventEmitter<Organization>();
  @Output() delete = new EventEmitter<string>();

  onEdit(): void {
    this.edit.emit(this.organization);
  }

  onDelete(): void {
    this.delete.emit(this.organization.organizationId);
  }
}
