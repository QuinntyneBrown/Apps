import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { NutritionService } from '../../services';

@Component({
  selector: 'app-nutrition',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatProgressBarModule
  ],
  templateUrl: './nutrition.html',
  styleUrl: './nutrition.scss'
})
export class Nutrition implements OnInit {
  private nutritionService = inject(NutritionService);

  nutritions$ = this.nutritionService.nutritions$;

  ngOnInit(): void {
    this.nutritionService.getNutritions().subscribe();
  }
}
