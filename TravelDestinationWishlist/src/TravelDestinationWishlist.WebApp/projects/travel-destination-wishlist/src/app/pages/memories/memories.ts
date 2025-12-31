import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MemoryService } from '../../services';
import { MemoryCard, MemoryDialog } from '../../components';
import { Memory } from '../../models';

@Component({
  selector: 'app-memories',
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatSnackBarModule,
    MemoryCard
  ],
  templateUrl: './memories.html',
  styleUrl: './memories.scss'
})
export class Memories implements OnInit {
  private memoryService = inject(MemoryService);
  private dialog = inject(MatDialog);
  private snackBar = inject(MatSnackBar);

  private readonly userId = '00000000-0000-0000-0000-000000000001';

  memories$ = this.memoryService.memories$;

  ngOnInit(): void {
    this.memoryService.getMemories(this.userId).subscribe();
  }

  onAdd(): void {
    const dialogRef = this.dialog.open(MemoryDialog, {
      width: '500px',
      data: { userId: this.userId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.memoryService.createMemory(result).subscribe({
          next: () => {
            this.snackBar.open('Memory added successfully', 'Close', { duration: 3000 });
          },
          error: () => {
            this.snackBar.open('Failed to add memory', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  onEdit(memory: Memory): void {
    const dialogRef = this.dialog.open(MemoryDialog, {
      width: '500px',
      data: { memory, userId: this.userId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.memoryService.updateMemory(memory.memoryId, result).subscribe({
          next: () => {
            this.snackBar.open('Memory updated successfully', 'Close', { duration: 3000 });
          },
          error: () => {
            this.snackBar.open('Failed to update memory', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  onDelete(memoryId: string): void {
    if (confirm('Are you sure you want to delete this memory?')) {
      this.memoryService.deleteMemory(memoryId, this.userId).subscribe({
        next: () => {
          this.snackBar.open('Memory deleted successfully', 'Close', { duration: 3000 });
        },
        error: () => {
          this.snackBar.open('Failed to delete memory', 'Close', { duration: 3000 });
        }
      });
    }
  }
}
