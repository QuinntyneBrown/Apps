import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ProgressRecordService } from '../../services';
import { ProgressRecordCard } from '../../components';
import { ProgressRecord } from '../../models';

@Component({
  selector: 'app-progress',
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatIconModule, ProgressRecordCard],
  templateUrl: './progress.html',
  styleUrl: './progress.scss'
})
export class Progress {
  private progressRecordService = inject(ProgressRecordService);

  progressRecords$ = this.progressRecordService.getAll();

  onEdit(progressRecord: ProgressRecord): void {
    console.log('Edit progress record:', progressRecord);
  }

  onDelete(progressRecord: ProgressRecord): void {
    if (confirm('Are you sure you want to delete this progress record?')) {
      this.progressRecordService.delete(progressRecord.progressRecordId).subscribe({
        next: () => console.log('Progress record deleted successfully'),
        error: (error) => console.error('Error deleting progress record:', error)
      });
    }
  }

  onCreate(): void {
    console.log('Create new progress record');
  }
}
