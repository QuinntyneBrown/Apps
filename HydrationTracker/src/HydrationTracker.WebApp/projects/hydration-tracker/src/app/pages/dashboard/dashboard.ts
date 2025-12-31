import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { IntakeService, GoalService } from '../../services';
import { Intake, Goal } from '../../models';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatProgressBarModule
  ],
  templateUrl: './dashboard.html',
  styleUrls: ['./dashboard.scss']
})
export class Dashboard implements OnInit {
  private intakeService = inject(IntakeService);
  private goalService = inject(GoalService);

  intakes$ = this.intakeService.intakes$;
  goals$ = this.goalService.goals$;
  loading$ = this.intakeService.loading$;

  todayIntakes: Intake[] = [];
  activeGoal: Goal | null = null;
  totalToday = 0;
  goalProgress = 0;

  ngOnInit(): void {
    this.intakeService.getIntakes().subscribe(intakes => {
      this.todayIntakes = this.getTodayIntakes(intakes);
      this.totalToday = this.calculateTotal(this.todayIntakes);
      this.updateProgress();
    });

    this.goalService.getGoals().subscribe(goals => {
      this.activeGoal = goals.find(g => g.isActive) || null;
      this.updateProgress();
    });
  }

  getTodayIntakes(intakes: Intake[]): Intake[] {
    const today = new Date();
    today.setHours(0, 0, 0, 0);
    return intakes.filter(intake => {
      const intakeDate = new Date(intake.intakeTime);
      intakeDate.setHours(0, 0, 0, 0);
      return intakeDate.getTime() === today.getTime();
    });
  }

  calculateTotal(intakes: Intake[]): number {
    return intakes.reduce((sum, intake) => sum + intake.amountMl, 0);
  }

  updateProgress(): void {
    if (this.activeGoal && this.activeGoal.dailyGoalMl > 0) {
      this.goalProgress = (this.totalToday / this.activeGoal.dailyGoalMl) * 100;
    }
  }
}
