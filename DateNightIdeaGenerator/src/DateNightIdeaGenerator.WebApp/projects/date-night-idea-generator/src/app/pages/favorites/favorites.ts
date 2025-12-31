import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { DateIdeaService } from '../../services';
import { Observable } from 'rxjs';
import { DateIdea, Category, BudgetRange } from '../../models';

@Component({
  selector: 'app-favorites',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule
  ],
  templateUrl: './favorites.html',
  styleUrl: './favorites.scss'
})
export class Favorites implements OnInit {
  favorites$!: Observable<DateIdea[]>;

  constructor(private dateIdeaService: DateIdeaService) {}

  ngOnInit(): void {
    this.favorites$ = this.dateIdeaService.dateIdeas$;
    this.loadFavorites();
  }

  loadFavorites(): void {
    this.dateIdeaService.getAll(undefined, undefined, undefined, true).subscribe();
  }

  getCategoryName(category: Category): string {
    return Category[category];
  }

  getBudgetRangeName(budgetRange: BudgetRange): string {
    return BudgetRange[budgetRange];
  }

  toggleFavorite(idea: DateIdea): void {
    this.dateIdeaService.update(idea.dateIdeaId, {
      ...idea,
      isFavorite: !idea.isFavorite
    }).subscribe(() => this.loadFavorites());
  }
}
