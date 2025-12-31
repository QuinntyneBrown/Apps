import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatChipsModule } from '@angular/material/chips';
import { Observable, map } from 'rxjs';
import { PetService } from '../../services';
import { Pet, PetType } from '../../models';
import { PetCard } from '../../components';
import { FormControl, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-pet-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatChipsModule,
    PetCard
  ],
  templateUrl: './pet-list.html',
  styleUrl: './pet-list.scss'
})
export class PetList implements OnInit {
  private petService = inject(PetService);

  searchControl = new FormControl('');
  selectedType: PetType | null = null;

  pets$!: Observable<Pet[]>;

  ngOnInit(): void {
    this.petService.getPets().subscribe();
    this.updatePets();
  }

  filterByType(type: PetType | null): void {
    this.selectedType = type;
    this.updatePets();
  }

  private updatePets(): void {
    this.pets$ = this.petService.pets$.pipe(
      map(pets => {
        let filtered = pets;

        if (this.selectedType) {
          filtered = filtered.filter(p => p.petType === this.selectedType);
        }

        const searchTerm = this.searchControl.value?.toLowerCase();
        if (searchTerm) {
          filtered = filtered.filter(p =>
            p.name.toLowerCase().includes(searchTerm) ||
            p.breed?.toLowerCase().includes(searchTerm)
          );
        }

        return filtered;
      })
    );

    this.searchControl.valueChanges.subscribe(() => this.updatePets());
  }

  onEdit(pet: Pet): void {
    console.log('Edit pet:', pet);
  }

  onDelete(pet: Pet): void {
    if (confirm(`Are you sure you want to delete ${pet.name}?`)) {
      this.petService.deletePet(pet.petId).subscribe();
    }
  }
}
