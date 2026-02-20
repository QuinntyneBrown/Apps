import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-budget-tracker',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './budget-tracker.html',
  styleUrl: './budget-tracker.scss'
})
export class BudgetTracker {
  @Input({ required: true }) budget!: number;
  @Input({ required: true }) spent!: number;
  @Input({ required: true }) remaining!: number;

  get progressPercent(): number {
    if (this.budget <= 0) return 0;
    return Math.min((this.spent / this.budget) * 100, 100);
  }
}
