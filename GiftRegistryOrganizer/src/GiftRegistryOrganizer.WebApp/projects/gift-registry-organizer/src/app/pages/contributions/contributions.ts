import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { ContributionService } from '../../services';
import { Contribution } from '../../models';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-contributions',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatSnackBarModule
  ],
  templateUrl: './contributions.html',
  styleUrl: './contributions.scss'
})
export class Contributions implements OnInit {
  private contributionService = inject(ContributionService);
  private snackBar = inject(MatSnackBar);

  contributions$!: Observable<Contribution[]>;
  loading$!: Observable<boolean>;

  displayedColumns = ['contributorName', 'contributorEmail', 'quantity', 'contributedAt', 'actions'];

  ngOnInit(): void {
    this.contributions$ = this.contributionService.contributions$;
    this.loading$ = this.contributionService.loading$;
    this.loadContributions();
  }

  loadContributions(): void {
    this.contributionService.getContributions().subscribe();
  }

  onDeleteContribution(contribution: Contribution): void {
    if (confirm(`Are you sure you want to delete this contribution from ${contribution.contributorName}?`)) {
      this.contributionService.deleteContribution(contribution.contributionId).subscribe({
        next: () => {
          this.snackBar.open('Contribution deleted successfully', 'Close', { duration: 3000 });
        },
        error: () => {
          this.snackBar.open('Failed to delete contribution', 'Close', { duration: 3000 });
        }
      });
    }
  }
}
