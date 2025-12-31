import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RecipeService, CookSessionService, TechniqueService } from '../services';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard implements OnInit {
  private recipeService = inject(RecipeService);
  private cookSessionService = inject(CookSessionService);
  private techniqueService = inject(TechniqueService);

  recipes$ = this.recipeService.recipes$;
  cookSessions$ = this.cookSessionService.cookSessions$;
  techniques$ = this.techniqueService.techniques$;

  ngOnInit(): void {
    this.recipeService.getRecipes().subscribe();
    this.cookSessionService.getCookSessions().subscribe();
    this.techniqueService.getTechniques().subscribe();
  }
}
