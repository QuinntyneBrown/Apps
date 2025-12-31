import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatCardModule } from '@angular/material/card';
import { ScreeningService } from '../services';

@Component({
  selector: 'app-screenings',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatChipsModule, MatCardModule],
  templateUrl: './screenings.html',
  styleUrl: './screenings.scss'
})
export class Screenings implements OnInit {
  private readonly screeningService = inject(ScreeningService);

  screenings$ = this.screeningService.screenings$;
  displayedColumns = ['name', 'screeningType', 'lastScreeningDate', 'nextDueDate', 'provider', 'status', 'actions'];

  ngOnInit(): void {
    this.screeningService.getAll().subscribe();
  }

  deleteScreening(id: string): void {
    if (confirm('Are you sure you want to delete this screening?')) {
      this.screeningService.delete(id).subscribe();
    }
  }
}
