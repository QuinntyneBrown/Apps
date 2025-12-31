import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { Observable } from 'rxjs';
import { Contribution } from '../../models';
import { ContributionService } from '../../services';

@Component({
  selector: 'app-contribution-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatChipsModule
  ],
  templateUrl: './contribution-list.html',
  styleUrl: './contribution-list.scss'
})
export class ContributionList implements OnInit {
  contributions$: Observable<Contribution[]>;
  displayedColumns: string[] = ['amount', 'contributionDate', 'contributor', 'isRecurring', 'notes'];

  constructor(private contributionService: ContributionService) {
    this.contributions$ = this.contributionService.contributions$;
  }

  ngOnInit(): void {
    this.contributionService.getContributions().subscribe();
  }

  deleteContribution(id: string): void {
    if (confirm('Are you sure you want to delete this contribution?')) {
      this.contributionService.deleteContribution(id).subscribe();
    }
  }
}
