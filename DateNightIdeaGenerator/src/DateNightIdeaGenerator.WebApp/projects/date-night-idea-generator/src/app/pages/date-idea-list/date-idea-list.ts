import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { DateIdeaService } from '../../services';
import { Observable } from 'rxjs';
import { DateIdea, Category, BudgetRange } from '../../models';

@Component({
  selector: 'app-date-idea-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    FormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule,
    MatSelectModule,
    MatFormFieldModule
  ],
  templateUrl: './date-idea-list.html',
  styleUrl: './date-idea-list.scss'
})
export class DateIdeaList implements OnInit {
  dateIdeas$!: Observable<DateIdea[]>;
  categories = Object.keys(Category).filter(k => isNaN(Number(k)));
  budgetRanges = Object.keys(BudgetRange).filter(k => isNaN(Number(k)));

  selectedCategory?: Category;
  selectedBudgetRange?: BudgetRange;
  showFavoritesOnly = false;

  constructor(private dateIdeaService: DateIdeaService) {}

  ngOnInit(): void {
    this.dateIdeas$ = this.dateIdeaService.dateIdeas$;
    this.loadDateIdeas();
  }

  loadDateIdeas(): void {
    this.dateIdeaService.getAll(
      undefined,
      this.selectedCategory,
      this.selectedBudgetRange,
      this.showFavoritesOnly || undefined
    ).subscribe();
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
    }).subscribe(() => this.loadDateIdeas());
  }

  delete(id: string): void {
    if (confirm('Are you sure you want to delete this date idea?')) {
      this.dateIdeaService.delete(id).subscribe();
    }
  }
}
