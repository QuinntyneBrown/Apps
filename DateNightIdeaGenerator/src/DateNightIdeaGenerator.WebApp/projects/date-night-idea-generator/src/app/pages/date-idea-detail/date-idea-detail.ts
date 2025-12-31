import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatTabsModule } from '@angular/material/tabs';
import { DateIdeaService, ExperienceService } from '../../services';
import { Observable } from 'rxjs';
import { DateIdea, Experience, Category, BudgetRange } from '../../models';

@Component({
  selector: 'app-date-idea-detail',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule,
    MatTabsModule
  ],
  templateUrl: './date-idea-detail.html',
  styleUrl: './date-idea-detail.scss'
})
export class DateIdeaDetail implements OnInit {
  dateIdea$!: Observable<DateIdea | null>;
  experiences$!: Observable<Experience[]>;
  dateIdeaId!: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private dateIdeaService: DateIdeaService,
    private experienceService: ExperienceService
  ) {}

  ngOnInit(): void {
    this.dateIdeaId = this.route.snapshot.paramMap.get('id')!;
    this.dateIdea$ = this.dateIdeaService.selectedDateIdea$;
    this.experiences$ = this.experienceService.experiences$;

    this.dateIdeaService.getById(this.dateIdeaId).subscribe();
    this.experienceService.getAll(this.dateIdeaId).subscribe();
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
    }).subscribe();
  }

  delete(id: string): void {
    if (confirm('Are you sure you want to delete this date idea?')) {
      this.dateIdeaService.delete(id).subscribe(() => {
        this.router.navigate(['/date-ideas']);
      });
    }
  }
}
