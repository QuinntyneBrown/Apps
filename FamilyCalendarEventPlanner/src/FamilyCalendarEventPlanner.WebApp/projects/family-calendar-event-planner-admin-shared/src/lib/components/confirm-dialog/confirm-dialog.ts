import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

export interface ConfirmDialogData {
  title: string;
  message: string;
  confirmText?: string;
  cancelText?: string;
}

export interface ConfirmDialogResult {
  confirmed: boolean;
}

@Component({
  selector: 'app-confirm-dialog',
  standalone: true,
  imports: [
    CommonModule,
    MatDialogModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './confirm-dialog.html',
  styleUrls: ['./confirm-dialog.scss']
})
export class ConfirmDialog {
  constructor(
    public dialogRef: MatDialogRef<ConfirmDialog>,
    @Inject(MAT_DIALOG_DATA) public data: ConfirmDialogData
  ) {}

  get confirmButtonText(): string {
    return this.data.confirmText || 'Delete';
  }

  get cancelButtonText(): string {
    return this.data.cancelText || 'Cancel';
  }

  onConfirm(): void {
    this.dialogRef.close({ confirmed: true } as ConfirmDialogResult);
  }

  onCancel(): void {
    this.dialogRef.close({ confirmed: false } as ConfirmDialogResult);
  }
}
