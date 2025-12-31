import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { FollowUpService } from '../../services';
import { FollowUp } from '../../models';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-follow-ups',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatChipsModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './follow-ups.html',
  styleUrl: './follow-ups.scss'
})
export class FollowUps implements OnInit {
  private followUpService = inject(FollowUpService);

  followUps$!: Observable<FollowUp[]>;
  loading$!: Observable<boolean>;

  private userId = '00000000-0000-0000-0000-000000000001';

  ngOnInit(): void {
    this.followUps$ = this.followUpService.followUps$;
    this.loading$ = this.followUpService.loading$;
    this.loadFollowUps();
  }

  loadFollowUps(): void {
    this.followUpService.getFollowUps(this.userId).subscribe();
  }

  completeFollowUp(followUp: FollowUp): void {
    this.followUpService.completeFollowUp(followUp.followUpId).subscribe();
  }
}
