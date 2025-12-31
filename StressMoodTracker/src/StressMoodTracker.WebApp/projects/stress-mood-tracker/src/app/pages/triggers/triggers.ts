import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { TriggerService } from '../../services';
import { TriggerDialog } from '../../components';

@Component({
  selector: 'app-triggers',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatCardModule, MatDialogModule],
  templateUrl: './triggers.html',
  styleUrl: './triggers.scss'
})
export class Triggers implements OnInit {
  private _triggerService = inject(TriggerService);
  private _dialog = inject(MatDialog);

  triggers$ = this._triggerService.triggers$;
  displayedColumns = ['name', 'triggerType', 'impactLevel', 'actions'];

  ngOnInit(): void {
    this._triggerService.getTriggers().subscribe();
  }

  openDialog(trigger?: any): void {
    const dialogRef = this._dialog.open(TriggerDialog, {
      width: '500px',
      data: { trigger }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (trigger) {
          this._triggerService.updateTrigger(trigger.triggerId, result).subscribe();
        } else {
          this._triggerService.createTrigger(result).subscribe();
        }
      }
    });
  }

  deleteTrigger(id: string): void {
    if (confirm('Are you sure you want to delete this trigger?')) {
      this._triggerService.deleteTrigger(id).subscribe();
    }
  }
}
