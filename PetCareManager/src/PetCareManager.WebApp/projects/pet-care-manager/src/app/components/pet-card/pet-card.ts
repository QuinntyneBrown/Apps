import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { Pet } from '../../models';

@Component({
  selector: 'app-pet-card',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatMenuModule
  ],
  templateUrl: './pet-card.html',
  styleUrl: './pet-card.scss'
})
export class PetCard {
  @Input() pet!: Pet;
  @Output() edit = new EventEmitter<Pet>();
  @Output() delete = new EventEmitter<Pet>();

  onEdit(): void {
    this.edit.emit(this.pet);
  }

  onDelete(): void {
    this.delete.emit(this.pet);
  }

  getAge(dateOfBirth: string | undefined): string {
    if (!dateOfBirth) return 'Age unknown';

    const birth = new Date(dateOfBirth);
    const today = new Date();
    const years = today.getFullYear() - birth.getFullYear();
    const months = today.getMonth() - birth.getMonth();

    if (years === 0) {
      return `${months} months`;
    } else if (months < 0) {
      return `${years - 1} years ${12 + months} months`;
    } else {
      return `${years} years ${months} months`;
    }
  }
}
