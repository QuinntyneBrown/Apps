import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { TimeBlockService } from '../../services';
import { TimeBlockDialog } from '../../components/time-block-dialog/time-block-dialog';

@Component({
  selector: 'app-time-blocks',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatDialogModule
  ],
  templateUrl: './time-blocks.html',
  styleUrl: './time-blocks.scss'
})
export class TimeBlocks implements OnInit {
  private _timeBlockService = inject(TimeBlockService);
  private _dialog = inject(MatDialog);

  timeBlocks$ = this._timeBlockService.timeBlocks$;
  displayedColumns = ['description', 'category', 'startTime', 'endTime', 'durationInMinutes', 'isProductive', 'actions'];

  ngOnInit(): void {
    this._timeBlockService.getAll().subscribe();
  }

  openDialog(timeBlock?: any): void {
    const dialogRef = this._dialog.open(TimeBlockDialog, {
      width: '500px',
      data: {
        timeBlock,
        userId: '00000000-0000-0000-0000-000000000000' // TODO: Get from auth service
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (timeBlock) {
          this._timeBlockService.update(timeBlock.timeBlockId, result).subscribe();
        } else {
          this._timeBlockService.create(result).subscribe();
        }
      }
    });
  }

  deleteTimeBlock(id: string): void {
    if (confirm('Are you sure you want to delete this time block?')) {
      this._timeBlockService.delete(id).subscribe();
    }
  }
}
