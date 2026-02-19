import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTooltipModule } from '@angular/material/tooltip';
import { LetterService } from '../services';
import { Letter, DeliveryStatus, DeliveryStatusLabels } from '../models';

@Component({
  selector: 'app-letters',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatChipsModule,
    MatProgressSpinnerModule,
    MatTooltipModule
  ],
  templateUrl: './letters.html',
  styleUrl: './letters.scss'
})
export class Letters implements OnInit {
  private letterService = inject(LetterService);
  private router = inject(Router);

  letters$ = this.letterService.letters$;
  loading$ = this.letterService.loading$;

  displayedColumns: string[] = ['subject', 'writtenDate', 'scheduledDeliveryDate', 'deliveryStatus', 'hasBeenRead', 'actions'];
  DeliveryStatusLabels = DeliveryStatusLabels;

  ngOnInit(): void {
    this.letterService.getAll().subscribe();
  }

  onEdit(letter: Letter): void {
    this.router.navigate(['/letters', letter.letterId]);
  }

  onDelete(letter: Letter): void {
    if (confirm(`Are you sure you want to delete the letter "${letter.subject}"?`)) {
      this.letterService.delete(letter.letterId).subscribe();
    }
  }

  onMarkAsRead(letter: Letter): void {
    this.letterService.markAsRead(letter.letterId).subscribe();
  }

  getStatusColor(status: DeliveryStatus): string {
    switch (status) {
      case DeliveryStatus.Pending:
        return 'warn';
      case DeliveryStatus.Delivered:
        return 'primary';
      case DeliveryStatus.Cancelled:
        return '';
      case DeliveryStatus.Failed:
        return 'accent';
      default:
        return '';
    }
  }

  formatDate(dateString: string): string {
    return new Date(dateString).toLocaleDateString();
  }
}
