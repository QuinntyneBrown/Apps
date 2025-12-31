import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { ReadingLogService } from '../../services';

@Component({
  selector: 'app-reading-logs',
  imports: [CommonModule, MatButtonModule, MatIconModule, MatTableModule, MatCardModule],
  templateUrl: './reading-logs.html',
  styleUrl: './reading-logs.scss'
})
export class ReadingLogs implements OnInit {
  private readonly readingLogService = inject(ReadingLogService);

  readingLogs$ = this.readingLogService.readingLogs$;
  displayedColumns = ['startTime', 'pages', 'duration', 'notes', 'actions'];

  ngOnInit(): void {
    this.readingLogService.getReadingLogs().subscribe();
  }

  onDeleteLog(log: any): void {
    if (confirm('Are you sure you want to delete this reading log?')) {
      this.readingLogService.deleteReadingLog(log.readingLogId).subscribe();
    }
  }
}
