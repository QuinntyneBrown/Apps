import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { PromptService, FavoriteService } from '@lib/api';
import { Prompt, Category, CategoryLabels, DepthLabels } from '@lib/api';
import {
  PageHeaderComponent,
  FilterChipComponent,
  PromptCardComponent
} from '@lib/components';

@Component({
  selector: 'app-prompts',
  standalone: true,
  imports: [
    CommonModule,
    PageHeaderComponent,
    FilterChipComponent,
    PromptCardComponent
  ],
  templateUrl: './prompts.html',
  styleUrl: './prompts.scss'
})
export class Prompts implements OnInit {
  prompts$: Observable<Prompt[]>;
  categoryLabels = CategoryLabels;
  depthLabels = DepthLabels;

  filters = [
    { label: 'All', category: undefined },
    { label: 'Deep', category: Category.Deep },
    { label: 'Relationship', category: Category.Relationship },
    { label: 'Fun', category: Category.Fun },
    { label: 'Reflective', category: Category.Reflective }
  ];

  activeFilter = 'All';

  private selectedCategory?: Category;
  private readonly userId = '00000000-0000-0000-0000-000000000001';

  constructor(
    private promptService: PromptService,
    private favoriteService: FavoriteService
  ) {
    this.prompts$ = this.promptService.prompts$;
  }

  ngOnInit(): void {
    this.loadPrompts();
    this.loadFavorites();
  }

  loadPrompts(): void {
    this.promptService.getAll(undefined, this.selectedCategory).subscribe();
  }

  loadFavorites(): void {
    this.favoriteService.getByUserId(this.userId).subscribe();
  }

  onFilterToggle(filter: { label: string; category: Category | undefined }): void {
    this.activeFilter = filter.label;
    this.selectedCategory = filter.category;
    this.loadPrompts();
  }

  toggleFavorite(prompt: Prompt): void {
    const favorite = this.favoriteService.getFavoriteByPromptId(prompt.promptId);
    if (favorite) {
      this.favoriteService.delete(favorite.favoriteId).subscribe();
    } else {
      this.favoriteService.create({
        userId: this.userId,
        promptId: prompt.promptId
      }).subscribe();
    }
  }

  isFavorite(promptId: string): boolean {
    return this.favoriteService.isFavorite(promptId);
  }

  getPromptBadges(prompt: Prompt): { label: string; color: string }[] {
    return [
      { label: this.categoryLabels[prompt.category], color: 'primary' },
      { label: this.depthLabels[prompt.depth], color: 'accent' }
    ];
  }

  getUsageText(prompt: Prompt): string {
    return prompt.usageCount > 0 ? `Used ${prompt.usageCount} times` : '';
  }

  getRandom(): void {
    this.promptService.getRandom(this.selectedCategory).subscribe();
  }
}
