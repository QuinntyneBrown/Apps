import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { RewardService } from '../services/reward.service';

@Component({
  selector: 'app-rewards-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatChipsModule],
  templateUrl: './rewards-list.html',
  styleUrl: './rewards-list.scss'
})
export class RewardsList {
  private rewardService = inject(RewardService);

  rewards$ = this.rewardService.rewards$;
  displayedColumns = ['name', 'category', 'pointCost', 'isAvailable', 'redeemedDate', 'actions'];

  constructor() {
    this.rewardService.getAll().subscribe();
  }

  deleteReward(id: string): void {
    if (confirm('Are you sure you want to delete this reward?')) {
      this.rewardService.delete(id).subscribe();
    }
  }
}
